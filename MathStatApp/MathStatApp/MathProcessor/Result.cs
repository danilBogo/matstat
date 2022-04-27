namespace MathStatApp.MathProcessor;

public class Result
{
    public string Message { get; }

    public double[] RelativeFreaquences { get; }

    public double[] Intervals { get; }

    public Result(string message, double[] relativeFrequencies, double[] intervals)
    {
        Message = message;
        RelativeFreaquences = relativeFrequencies;
        Intervals = intervals;
    }
}