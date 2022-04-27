using CsvHelper.Configuration.Attributes;

namespace MathStatApp;

public class ColumnsDanil
{
    [Name("fixed acidity")]
    public string FixedAcidity { get; set; }
    
    [Name("volatile acidity")]
    public string VolatileAcidity { get; set; }

    [Name("citric acid")]
    public string CitricAcid { get; set; }
    
    [Name("residual sugar")]
    public string ResidualSugar { get; set; }
}