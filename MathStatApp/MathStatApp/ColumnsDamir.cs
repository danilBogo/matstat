using CsvHelper.Configuration.Attributes;

namespace MathStatApp;

public class ColumnsDamir
{
    [Constant("fixed acidity")]
    public double FixedAcidity { get; set; }
    
    [Constant("volatile acidity")]
    public double VolatileAcidity { get; set; }

    [Constant("citric acid")]
    public double CitricAcid { get; set; }
    
    [Constant("residual sugar")]
    public double ResidualSugar { get; set; }
    
    [Constant("chlorides")]
    public double Chlorides { get; set; }
    
    [Constant("free sulfur dioxide")]
    public double FreeSulfurDioxide { get; set; }
}