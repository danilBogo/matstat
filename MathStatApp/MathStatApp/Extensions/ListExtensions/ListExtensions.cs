namespace MathStatApp.Extensions.ListExtensions;

public static class ListExtensions
{
    public static double Min(this List<double> list)
    {
        var min = double.MaxValue;
        foreach (var el in list)
            if (el < min) min = el;
        return min;
    }
    public static double Max(this List<double> list)
    {
        var max = double.MinValue;
        foreach (var el in list)
            if (el > max) max = el;
        return max;
    }
}