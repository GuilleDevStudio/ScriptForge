using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ScriptForge;

/// <summary>Filas elegidas de una tabla para generar sus INSERT.</summary>
public record DataSelection(DbObj Table, DataTable Data, bool IncludeIdentity, bool SkipExisting);

public class ScriptGenerator
{
    readonly DbService _db;
    public ScriptGenerator(DbService db) => _db = db;

    static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    static readonly Regex CreateRx = new(
        @"\bCREATE(\s+OR\s+ALTER)?(?=\s+(PROC|PROCEDURE|VIEW|FUNCTION)\b)",
        RegexOptions.IgnoreCase);

    // Orden lógico de los ficheros. La numeración se asigna al final, solo a los que tengan contenido.
    static readonly string[] Order =
    {
        "Tablas_CREATE", "Tablas_ALTER",
        "Vistas_CREATE", "Vistas_ALTER",
        "Procedimientos_CREATE", "Procedimientos_ALTER",
        "Funciones_CREATE", "Funciones_ALTER",
        "Permisos", "Datos"
    };

    static string Q(string name) => "[" + name.Replace("]", "]]") + "]";

    static string ModuleKey(ObjKind k, string action)
    {
        var kind = k switch { ObjKind.View => "Vistas", ObjKind.Proc => "Procedimientos", _ => "Funciones" };
        return $"{kind}_{(action == "CREATE" ? "CREATE" : "ALTER")}";
    }

    /// <summary>Genera los ficheros en 'folder' y devuelve las rutas creadas.</summary>
    public async Task<List<string>> GenerateAsync(List<DbObj> selected, List<DataSelection> data,
                                                  List<string> grantees, string folder)
    {
        Directory.CreateDirectory(folder);
        var parts = new Dictionary<string, StringBuilder>();

        StringBuilder P(string key)
        {
            if (!parts.TryGetValue(key, out var sb)) parts[key] = sb = new StringBuilder();
            return sb;
        }

        foreach (var o in selected.OrderBy(x => x.Schema).ThenBy(x => x.Name))
        {
            if (o.Kind == ObjKind.Table)
            {
                if (o.Action == "CREATE") P("Tablas_CREATE").Append(await TableCreateAsync(o));
                else P("Tablas_ALTER").Append(await TableAlterAsync(o));
            }
            else
            {
                P(ModuleKey(o.Kind, o.Action)).Append(await ModuleAsync(o));
            }
        }

        // Permisos
        var grantObjs = selected.Where(o => o.Grant && (o.Kind == ObjKind.Proc || o.Kind == ObjKind.Function)).ToList();
        if (grantObjs.Count > 0 && grantees.Count > 0)
        {
            var sb = P("Permisos");
            foreach (var u in grantees)
            {
                foreach (var o in grantObjs)
                {
                    // Funciones de tabla (IF/TF) necesitan SELECT; escalares y procs, EXECUTE
                    var perm = (o.Kind == ObjKind.Proc || o.SqlType == "FN") ? "EXECUTE" : "SELECT";
                    sb.AppendLine($"GRANT {perm} ON {o.Full} TO {Q(u)};");
                    sb.AppendLine("GO");
                }
                sb.AppendLine();
            }
        }

        // Datos
        foreach (var d in data)
            P("Datos").Append(await DataScriptAsync(d));

        // Numeración consecutiva solo de los ficheros con contenido
        var written = new List<string>();
        int n = 1;
        foreach (var key in Order)
        {
            if (!parts.TryGetValue(key, out var sb) || sb.Length == 0) continue;

            var name = $"{n:00}_{key}.sql";
            n++;
            var header = $"-- {name}{Environment.NewLine}-- Generado: {DateTime.Now:yyyy-MM-dd HH:mm}{Environment.NewLine}{Environment.NewLine}";
            var path = Path.Combine(folder, name);
            await File.WriteAllTextAsync(path, header + sb, new UTF8Encoding(true));
            written.Add(path);
        }
        return written;
    }

    // ---------- Vistas / procs / funciones ----------
    async Task<string> ModuleAsync(DbObj o)
    {
        var def = await _db.GetDefinitionAsync(o);
        var sb = new StringBuilder();
        sb.AppendLine($"-- {o.Full}");
        if (def == null)
        {
            sb.AppendLine("-- No se pudo obtener la definición (objeto cifrado o sin permisos).");
        }
        else
        {
            if (o.Action == "ALTER") def = CreateRx.Replace(def, "ALTER", 1);
            sb.AppendLine(def.Trim());
        }
        sb.AppendLine("GO");
        sb.AppendLine();
        return sb.ToString();
    }

    // ---------- Tablas ----------
    static string ColDef(ColInfo c)
    {
        if (c.Computed) return $"{Q(c.Name)} AS {c.ComputedDef}";
        var s = $"{Q(c.Name)} {c.TypeSql}";
        if (c.Identity) s += $" IDENTITY({c.Seed},{c.Incr})";
        s += c.Nullable ? " NULL" : " NOT NULL";
        if (c.DefName != null) s += $" CONSTRAINT {Q(c.DefName)} DEFAULT {c.DefDef}";
        return s;
    }

    async Task<string> TableCreateAsync(DbObj o)
    {
        var cols = await _db.GetColumnsAsync(o);
        var pk = await _db.GetPrimaryKeyAsync(o);

        var lines = cols.Select(c => "        " + ColDef(c)).ToList();
        if (pk != null)
        {
            var pkCols = string.Join(", ", pk.Cols.Select(x => Q(x.Col) + (x.Desc ? " DESC" : " ASC")));
            lines.Add($"        CONSTRAINT {Q(pk.Name)} PRIMARY KEY {pk.Type} ({pkCols})");
        }

        var sb = new StringBuilder();
        sb.AppendLine($"-- {o.Full}");
        sb.AppendLine($"IF OBJECT_ID(N'{o.Full}', N'U') IS NULL");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    CREATE TABLE {o.Full} (");
        sb.AppendLine(string.Join("," + Environment.NewLine, lines));
        sb.AppendLine("    );");
        sb.AppendLine("END");
        sb.AppendLine("GO");
        sb.AppendLine();
        return sb.ToString();
    }

    async Task<string> TableAlterAsync(DbObj o)
    {
        var cols = (await _db.GetColumnsAsync(o)).Where(c => o.Columns.Contains(c.Name)).ToList();
        var sb = new StringBuilder();
        foreach (var c in cols)
        {
            // Idempotente: si la columna no existe la añade; si existe la modifica.
            sb.AppendLine($"-- {o.Full} : columna {Q(c.Name)}");
            sb.AppendLine($"IF COL_LENGTH(N'{o.Full}', N'{c.Name.Replace("'", "''")}') IS NULL");
            sb.AppendLine($"    ALTER TABLE {o.Full} ADD {ColDef(c)};");
            sb.AppendLine("ELSE");
            if (c.Computed)
                sb.AppendLine("    PRINT N'La columna computada ya existe; revisar manualmente.';");
            else
                sb.AppendLine($"    ALTER TABLE {o.Full} ALTER COLUMN {Q(c.Name)} {c.TypeSql} {(c.Nullable ? "NULL" : "NOT NULL")};");
            sb.AppendLine("GO");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    // ---------- Datos ----------
    async Task<string> DataScriptAsync(DataSelection d)
    {
        var rows = d.Data.Rows.Cast<DataRow>().Where(r => r["__sel"] is bool b && b).ToList();
        if (rows.Count == 0) return "";

        var infos = await _db.GetColumnsAsync(d.Table);

        // Columnas que se pueden insertar: sin calculadas, sin rowversion y (opcional) sin identity
        var cols = infos.Where(c => !c.Computed
                                 && !c.TypeSql.Equals("timestamp", StringComparison.OrdinalIgnoreCase)
                                 && (!c.Identity || d.IncludeIdentity)
                                 && d.Data.Columns.Contains(c.Name)).ToList();

        bool useIdentity = d.IncludeIdentity && cols.Any(c => c.Identity);
        var pk = await _db.GetPrimaryKeyAsync(d.Table);
        bool canCheck = d.SkipExisting && pk != null && pk.Cols.All(p => cols.Any(c => c.Name == p.Col));
        var colList = string.Join(", ", cols.Select(c => Q(c.Name)));

        var sb = new StringBuilder();
        sb.AppendLine($"-- {d.Table.Full} ({rows.Count} fila(s))");
        if (d.SkipExisting && !canCheck)
            sb.AppendLine("-- Sin clave primaria utilizable: no se comprueba si la fila ya existe.");
        if (useIdentity) sb.AppendLine($"SET IDENTITY_INSERT {d.Table.Full} ON;");

        foreach (var r in rows)
        {
            var values = string.Join(", ", cols.Select(c => Lit(r[c.Name], c.TypeSql)));
            var insert = $"INSERT INTO {d.Table.Full} ({colList}) VALUES ({values});";

            if (canCheck)
            {
                var cond = string.Join(" AND ", pk.Cols.Select(p =>
                {
                    var c = cols.First(x => x.Name == p.Col);
                    return $"{Q(p.Col)} = {Lit(r[p.Col], c.TypeSql)}";
                }));
                sb.AppendLine($"IF NOT EXISTS (SELECT 1 FROM {d.Table.Full} WHERE {cond})");
                sb.AppendLine("    " + insert);
            }
            else sb.AppendLine(insert);
        }

        if (useIdentity) sb.AppendLine($"SET IDENTITY_INSERT {d.Table.Full} OFF;");
        sb.AppendLine("GO");
        sb.AppendLine();
        return sb.ToString();
    }

    /// <summary>Convierte un valor .NET en un literal T-SQL.</summary>
    static string Lit(object v, string sqlType)
    {
        if (v == null || v is DBNull) return "NULL";
        var t = sqlType.ToLowerInvariant();

        switch (v)
        {
            case string s: return "N'" + s.Replace("'", "''") + "'";
            case bool b: return b ? "1" : "0";
            case byte[] bytes: return "0x" + Convert.ToHexString(bytes);
            case DateTime dt:
                if (t == "date") return "'" + dt.ToString("yyyy-MM-dd", Inv) + "'";
                if (t.StartsWith("datetime2")) return "'" + dt.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff", Inv) + "'";
                if (t == "smalldatetime") return "'" + dt.ToString("yyyy-MM-dd'T'HH:mm:ss", Inv) + "'";
                return "'" + dt.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", Inv) + "'";
            case DateTimeOffset dto: return "'" + dto.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffzzz", Inv) + "'";
            case TimeSpan ts: return "'" + ts.ToString(@"hh\:mm\:ss\.fffffff", Inv) + "'";
            case Guid g: return "'" + g.ToString() + "'";
            case float f: return f.ToString("R", Inv);
            case double dbl: return dbl.ToString("R", Inv);
            case IFormattable fm: return fm.ToString(null, Inv);
            default: return "N'" + v.ToString().Replace("'", "''") + "'";
        }
    }
}
