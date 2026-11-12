namespace ScriptForge;

public enum ObjKind { Table, View, Proc, Function }

public class DbObj
{
    public string Schema { get; set; }
    public string Name { get; set; }
    public ObjKind Kind { get; set; }
    public string SqlType { get; set; }          // U, V, P, FN, IF, TF
    public bool Selected { get; set; }
    public string Action { get; set; } = "CREATE";
    public bool Grant { get; set; }
    public List<string> Columns { get; set; } = new();   // solo para ALTER de tablas
    public string ColumnsInfo => Columns.Count == 0 ? "" : $"{Columns.Count} columna(s)";
    public string Full => $"[{Schema}].[{Name}]";
}

public class ColInfo
{
    public string Name, TypeSql, DefName, DefDef, ComputedDef;
    public bool Nullable, Identity, Computed;
    public long Seed = 1, Incr = 1;
}

public class PkInfo
{
    public string Name, Type;
    public List<(string Col, bool Desc)> Cols = new();
}
