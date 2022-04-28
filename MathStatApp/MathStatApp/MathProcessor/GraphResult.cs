namespace MathStatApp.MathProcessor;

public class GraphResult
{
    public string Message { get; }

    public double[] RelativeFrequencies { get; }

    public double[] Intervals { get; }

    public GraphResult(string message, double[] relativeFrequencies, double[] intervals)
    {
        Message = message;
        RelativeFrequencies = relativeFrequencies;
        Intervals = intervals;
    }
}