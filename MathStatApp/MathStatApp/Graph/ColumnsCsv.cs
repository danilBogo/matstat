using CsvHelper.Configuration.Attributes;

namespace MathStatApp.Graph;

public class ColumnsCsv
{
    [Name("citric acid")]
    public string CitricAcid { get; set; }
    
    [Name("residual sugar")]
    public string ResidualSugar { get; set; }
    
    [Name("chlorides")]
    public string Chlorides { get; set; }
    
    [Name("free sulfur dioxide")]
    public string FreeSulfurDioxide { get; set; }
}