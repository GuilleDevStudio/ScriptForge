using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace ScriptForge;

public class DbService
{
    readonly string _cs;
    public DbService(string connectionString) => _cs = connectionString;

    public static string BuildConnectionString(string server, string db, bool windowsAuth, string user, string pwd)
    {
        var b = new SqlConnectionStringBuilder
        {
            DataSource = server,
            InitialCatalog = db,
            TrustServerCertificate = true,
            ConnectTimeout = 10
        };
        if (windowsAuth) b.IntegratedSecurity = true;
        else { b.UserID = user; b.Password = pwd; }
        return b.ConnectionString;
    }

    public static async Task TestAsync(string cs)
    {
        await using var cn = new SqlConnection(cs);
        await cn.OpenAsync();
    }

    async Task<List<T>> QueryAsync<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] ps)
    {
        var list = new List<T>();
        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync();
        await using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddRange(ps);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync()) list.Add(map(r));
        return list;
    }

    static string S(SqlDataReader r, int i) => r.IsDBNull(i) ? null : r.GetString(i);

    // ---------- Listado de objetos ----------
    public Task<List<DbObj>> GetObjectsAsync() => QueryAsync(@"
        SELECT s.name, o.name, RTRIM(o.type)
        FROM sys.objects o
        JOIN sys.schemas s ON s.schema_id = o.schema_id
        WHERE o.is_ms_shipped = 0
          AND o.type IN ('U','V','P','FN','IF','TF')
          AND o.name <> 'sysdiagrams'
        ORDER BY s.name, o.name",
        r =>
        {
            var t = r.GetString(2);
            return new DbObj
            {
                Schema = r.GetString(0),
                Name = r.GetString(1),
                SqlType = t,
                Kind = t switch { "U" => ObjKind.Table, "V" => ObjKind.View, "P" => ObjKind.Proc, _ => ObjKind.Function }
            };
        });

    // ---------- Vistas / procs / funciones ----------
    public async Task<string> GetDefinitionAsync(DbObj o)
    {
        var res = await QueryAsync("SELECT OBJECT_DEFINITION(OBJECT_ID(@n))", r => S(r, 0),
            new SqlParameter("@n", o.Full));
        return res.FirstOrDefault();
    }

    // ---------- Tablas ----------
    public Task<List<ColInfo>> GetColumnsAsync(DbObj o) => QueryAsync(@"
        SELECT c.name, t.name, c.max_length, c.precision, c.scale,
               c.is_nullable, c.is_identity, c.is_computed,
               CAST(ic.seed_value AS bigint), CAST(ic.increment_value AS bigint),
               dc.name, dc.definition, cc.definition
        FROM sys.columns c
        JOIN sys.types t ON t.user_type_id = c.user_type_id
        LEFT JOIN sys.identity_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        LEFT JOIN sys.default_constraints dc ON dc.object_id = c.default_object_id
        LEFT JOIN sys.computed_columns cc ON cc.object_id = c.object_id AND cc.column_id = c.column_id
        WHERE c.object_id = OBJECT_ID(@t)
        ORDER BY c.column_id",
        r => new ColInfo
        {
            Name = r.GetString(0),
            TypeSql = FormatType(r.GetString(1), Convert.ToInt32(r.GetValue(2)),
                                 Convert.ToInt32(r.GetValue(3)), Convert.ToInt32(r.GetValue(4))),
            Nullable = r.GetBoolean(5),
            Identity = r.GetBoolean(6),
            Computed = r.GetBoolean(7),
            Seed = r.IsDBNull(8) ? 1 : r.GetInt64(8),
            Incr = r.IsDBNull(9) ? 1 : r.GetInt64(9),
            DefName = S(r, 10),
            DefDef = S(r, 11),
            ComputedDef = S(r, 12)
        },
        new SqlParameter("@t", o.Full));

    public async Task<PkInfo> GetPrimaryKeyAsync(DbObj o)
    {
        var rows = await QueryAsync(@"
            SELECT i.name, i.type_desc, c.name, ic.is_descending_key
            FROM sys.indexes i
            JOIN sys.index_columns ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id
            JOIN sys.columns c ON c.object_id = ic.object_id AND c.column_id = ic.column_id
            WHERE i.object_id = OBJECT_ID(@t) AND i.is_primary_key = 1
            ORDER BY ic.key_ordinal",
            r => (Name: r.GetString(0), Type: r.GetString(1), Col: r.GetString(2), Desc: r.GetBoolean(3)),
            new SqlParameter("@t", o.Full));

        if (rows.Count == 0) return null;
        var pk = new PkInfo { Name = rows[0].Name, Type = rows[0].Type };
        foreach (var x in rows) pk.Cols.Add((x.Col, x.Desc));
        return pk;
    }

    static string FormatType(string t, int len, int prec, int scale) => t.ToLower() switch
    {
        "char" or "varchar" or "binary" or "varbinary" => $"{t}({(len == -1 ? "max" : len.ToString())})",
        "nchar" or "nvarchar" => $"{t}({(len == -1 ? "max" : (len / 2).ToString())})",
        "decimal" or "numeric" => $"{t}({prec},{scale})",
        "datetime2" or "time" or "datetimeoffset" => $"{t}({scale})",
        _ => t
    };

    // ---------- Datos de una tabla ----------
    /// <summary>Lee las primeras 'top' filas de la tabla, con un WHERE opcional escrito por el usuario.</summary>
    public async Task<DataTable> GetDataAsync(DbObj o, int top, string where)
    {
        var sql = $"SELECT TOP ({top}) * FROM {o.Full}";
        if (!string.IsNullOrWhiteSpace(where))
            sql += " WHERE " + Regex.Replace(where.Trim(), @"^where\s+", "", RegexOptions.IgnoreCase);

        await using var cn = new SqlConnection(_cs);
        await cn.OpenAsync();
        using var da = new SqlDataAdapter(sql, cn);
        var dt = new DataTable();
        await Task.Run(() => da.Fill(dt));
        return dt;
    }

    // ---------- Usuarios para permisos ----------
    public Task<List<string>> GetPrincipalsAsync() => QueryAsync(@"
        SELECT name FROM sys.database_principals
        WHERE type IN ('S','U','G','R') AND is_fixed_role = 0
          AND name NOT IN ('dbo','guest','INFORMATION_SCHEMA','sys')
        ORDER BY name",
        r => r.GetString(0));
}
