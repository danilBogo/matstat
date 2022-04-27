using CsvHelper.Configuration.Attributes;

namespace MathStatApp;

public class ColumnsCsv
{
    [Name("fixed acidity")]
    public string FixedAcidity { get; set; }
    
    [Name("volatile acidity")]
    public string VolatileAcidity { get; set; }

    [Name("citric acid")]
    public string CitricAcid { get; set; }
    
    [Name("residual sugar")]
    public string ResidualSugar { get; set; }
    
    [Name("chlorides")]
    public string Chlorides { get; set; }
    
    [Name("free sulfur dioxide")]
    public string FreeSulfurDioxide { get; set; }
}