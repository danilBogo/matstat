using MathStatApp.Extensions.ListExtensions;

namespace MathStatApp.MathProcessor;

public class Processor
{
    public List<double> DataSet { get; set;}
    public List<double> Intervals { get; set;}
    public List<double> Averages { get; set;}
    public List<int> Frequencies { get; set;}
    public List<int> AccumulatedFrequencies { get; set;}
    public List<double> RelativeFrequencies { get; set;}
    public List<double> ZValues;
    public double Dispersion { get; set; }
    
    public readonly double XMin;
    public readonly double XMax;
    public readonly double IntervalsCount;
    public readonly double IntervalsLength;
    public readonly double MathExpectation;
    public readonly double ExcessCoefficient;
    public readonly double Moda;
    public readonly double MedianValue;
    public readonly double StandardDeviation;
    public readonly double SkewnessCoefficient;
    
    public Processor(List<double> dataSet)
    {
        DataSet = dataSet;
        XMin = dataSet.Min();
        XMax = dataSet.Max();
        IntervalsCount = GetIntervalsCount();
        IntervalsLength = GetIntervalsLength();
        SetIntervalsAndAverages();
        SetFrequencies();
        MathExpectation = GetMathExpectation();
        SetDispersionAndZValues();
        ExcessCoefficient = GetExcessCoefficient();
        Moda = GetModaValue();
        MedianValue = GetMedianValue();
        StandardDeviation = Math.Round(Math.Sqrt(Dispersion), 2);
        SkewnessCoefficient = GetSkewnessCoefficient();
    }
    private int GetIntervalsCount()
    {
        var k = 1 + 3.322d * Math.Log10(DataSet.Count);
        var m = (int) Math.Ceiling(k);
        return m;
    }
    private double GetIntervalsLength()
    {
        var l = Math.Round((XMax - XMin) / IntervalsCount, 3);
        return l;
    }
    private void SetIntervalsAndAverages()
    {
        for (int i = 1; i < IntervalsCount + 1; i++)
        {
            Intervals[i] = Intervals[i - 1] + IntervalsLength;
            Averages[i - 1] = (Intervals[i - 1] + Intervals[i]) / 2;
        }
    }
    private void SetFrequencies()
    {
        foreach (var e in DataSet)
        {
            for (int i = 1; i < IntervalsCount + 1; i++)
            {
                if (Intervals[i - 1] <= e && e < Intervals[i])
                {
                    Frequencies[i - 1]++;
                    break;
                }
            }
        }
        
        AccumulatedFrequencies[0] = Frequencies[0];
        for (int i = 0; i < IntervalsCount; i++)
        {
            RelativeFrequencies[i] = Math.Round((double)Frequencies[i] / DataSet.Count, 4);
            if (i != 0)
                AccumulatedFrequencies[i] = AccumulatedFrequencies[i - 1] + Frequencies[i];
        }
    }
     private double GetMathExpectation()
    {
        double sum = 0;
        for (int i = 0; i < Averages.Count; i++)
            sum += Averages[i] * RelativeFrequencies[i];
        return Math.Round(sum, 2);
    }

    private void SetDispersionAndZValues()
    {
        Dispersion = 0;
        for (int i = 0; i < Averages.Count; i++)
        {
            ZValues[i] = Averages[i] - MathExpectation;
            Dispersion += ZValues[i] * ZValues[i] * RelativeFrequencies[i];
        }
    }

    private double GetModaValue()
    {
        var idx = Frequencies.ToList().IndexOf(Frequencies.Max());
        double num = Frequencies[idx] - Frequencies[idx - 1];
        double den = num + Frequencies[idx] - Frequencies[idx + 1];
        return Math.Round(Intervals[idx] + num / den * IntervalsLength, 4);
    }
    
    private double GetMedianValue()
    {
        var idx = AccumulatedFrequencies.ToList().FindIndex(x => x >= DataSet.Count / 2);
        double num = 0.5d * DataSet.Count - AccumulatedFrequencies[idx - 1];
        double den = Frequencies[idx];
        return Math.Round(Intervals[idx] + num / den * IntervalsLength, 4);
    }
    
    private double GetSkewnessCoefficient()
    {
        double m3 = 0;
        for (int i = 0; i < RelativeFrequencies.Count; i++)
            m3 += ZValues[i] * ZValues[i] * ZValues[i] * RelativeFrequencies[i];
        return Math.Round(m3 / Dispersion / StandardDeviation, 4);
    }
    
    private double GetExcessCoefficient()
    {
        double m4 = 0;
        for (int i = 0; i < RelativeFrequencies.Count; i++)
            m4 += ZValues[i] * ZValues[i] * ZValues[i] * ZValues[i] * RelativeFrequencies[i];
        return Math.Round(m4 / Dispersion / Dispersion - 3, 4);
    }
    
    public double MathExpectationDovInterval(double standardDeviation, int n)
    {
        var t = 2.045d;
        double delta = t * standardDeviation / (double) Math.Sqrt(n);
        return delta;
    }
}