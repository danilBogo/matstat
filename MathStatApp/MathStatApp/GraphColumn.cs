using System.Globalization;

namespace MathStatApp;

public class GraphColumn
{
    public string X { get; }
    
    public double Y { get; }

    public GraphColumn(int i, double x, double y)
    {
        X = ($"({i})" + Math.Round(x, 4)).ToString(CultureInfo.InvariantCulture);
        Y = y;
    }
}