using MathStatApp.Extensions.ListExtensions;

namespace MathStatApp.MathProcessor;

public class Processor
{
    private List<double> DataSet { get; }
    public int DataSetCount => DataSet.Count;
    public double[] Intervals { get; set; }
    public double[] Averages { get; set; }
    public int[] Frequencies { get; set; }
    public int[] AccumulatedFrequencies { get; set; }
    public double[] RelativeFrequencies { get; set; }
    public double[] ZValues { get; set; }
    
    public double Dispersion;
    public readonly double XMin;
    public readonly double XMax;
    public readonly int IntervalsCount;
    public readonly double IntervalLength;
    public readonly double MathExpectation;
    public readonly double ExcessCoefficient;
    public readonly string ExcessCoefficientType;
    public readonly double Fashion;
    public readonly double MedianValue;
    public readonly double StandardDeviation;
    public readonly double SkewnessCoefficient;
    public readonly string SkewnessCoefficientType;
    public readonly double VariationCoefficient;
    public readonly string VariationCoefficientType;

    public Processor(List<double> dataSet)
    {
        DataSet = dataSet;
        XMin = dataSet.Min();
        XMax = dataSet.Max();
        IntervalsCount = GetIntervalsCount();
        IntervalLength = GetIntervalsLength();
        SetIntervalsAndAverages();
        SetFrequencies();
        MathExpectation = GetMathExpectation();
        SetDispersionAndZValues();
        ExcessCoefficient = GetExcessCoefficient();
        ExcessCoefficientType = GetExcessCoefficientType();
        Fashion = GetModaValue();
        MedianValue = GetMedianValue();
        StandardDeviation = Math.Round(Math.Sqrt(Dispersion), 2);
        SkewnessCoefficient = GetSkewnessCoefficient();
        SkewnessCoefficientType = GetSkewnessCoefficientType();
        VariationCoefficient = StandardDeviation / MathExpectation;
        VariationCoefficientType = GetVariationCoefficientType();
    }

    private string GetVariationCoefficientType() =>
        VariationCoefficient < 0.3 ? "Однородная выборка" : "Неоднородная выборка";

    private string GetExcessCoefficientType() =>
        ExcessCoefficient switch
        {
            > 0 => "выше относительно нормального",
            < 0 => "ниже относительно нормального",
            _ => "совпадает с относительно нормальным"
        };

    private string GetSkewnessCoefficientType() =>
        SkewnessCoefficient switch
        {
            < 0.25 => "слабая",
            < 0.5 => "умеренная",
            _ => "существеная"
        };

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
        Intervals = new double[IntervalsCount + 1];
        Averages = new double[IntervalsCount + 1];
        Intervals[0] = XMin;
        for (var i = 1; i < IntervalsCount + 1; i++)
        {
            Intervals[i] = Intervals[i - 1] + IntervalLength;
            Averages[i - 1] = (Intervals[i - 1] + Intervals[i]) / 2;
        }
    }

    private void SetFrequencies()
    {
        Frequencies = new int[IntervalsCount];
        AccumulatedFrequencies = new int[IntervalsCount];
        
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
        RelativeFrequencies = new double[IntervalsCount + 1];
        for (int i = 0; i < IntervalsCount; i++)
        {
            RelativeFrequencies[i] = Math.Round((double) (Frequencies[i] / DataSet.Count), 4);
            if (i != 0)
                AccumulatedFrequencies[i] = AccumulatedFrequencies[i - 1] + Frequencies[i];
        }
    }

    private double GetMathExpectation()
    {
        double sum = 0;
        for (int i = 0; i < Averages.Length; i++)
            sum += Averages[i] * RelativeFrequencies[i];
        return Math.Round(sum, 2);
    }

    private void SetDispersionAndZValues()
    {
        ZValues = new double[Averages.Length];
        for (int i = 0; i < Averages.Length; i++)
        {
            ZValues[i] = Averages[i] - MathExpectation;
            Dispersion += ZValues[i] * ZValues[i] * RelativeFrequencies[i];
        }
    }

    private double GetModaValue()
    {
        var idx = Frequencies.ToList().IndexOf(Frequencies.Max());
        var num = Frequencies[idx];
        if (idx != 0)
            num = Frequencies[idx] - Frequencies[idx - 1];
        var den = num + Frequencies[idx];
        if (idx != Frequencies.Length)
            den = num + Frequencies[idx] - Frequencies[idx + 1];
        return Math.Round(Intervals[idx] + num / den * IntervalLength, 4);
    }

    private double GetMedianValue()
    {
        var idx = AccumulatedFrequencies.ToList().FindIndex(x => x >= DataSet.Count / 2);
        double num = 0.5d * DataSet.Count - AccumulatedFrequencies[idx - 1];
        double den = Frequencies[idx];
        return Math.Round(Intervals[idx] + num / den * IntervalLength, 4);
    }

    private double GetSkewnessCoefficient()
    {
        double m3 = 0;
        for (int i = 0; i < RelativeFrequencies.Length; i++)
            m3 += ZValues[i] * ZValues[i] * ZValues[i] * RelativeFrequencies[i];
        return Math.Round(m3 / Dispersion / StandardDeviation, 4);
    }

    private double GetExcessCoefficient()
    {
        double m4 = 0;
        for (int i = 0; i < RelativeFrequencies.Length; i++)
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