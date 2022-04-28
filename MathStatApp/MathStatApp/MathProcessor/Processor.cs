using MathStatApp.Extensions.ListExtensions;

namespace MathStatApp.MathProcessor;

public class Processor
{
    private List<double> DataSet { get; }
    public int DataSetCount => DataSet.Count;
    public double[] Intervals { get; private set; }
    private double[] Averages { get; set; }
    private int[] Frequencies { get; set; }
    private int[] AccumulatedFrequencies { get; set; }
    public double[] RelativeFrequencies { get; private set; }
    private double[] ZValues { get; set; }

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
        Fashion = GetFashionValue();
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
            for (var i = 1; i < IntervalsCount + 1; i++)
            {
                if (!(Intervals[i - 1] <= e) || !(e < Intervals[i])) continue;
                Frequencies[i - 1]++;
                break;
            }
        }

        AccumulatedFrequencies[0] = Frequencies[0];
        RelativeFrequencies = new double[IntervalsCount + 1];
        for (var i = 0; i < IntervalsCount; i++)
        {
            RelativeFrequencies[i] = (double) Frequencies[i] / DataSet.Count;
            if (i != 0)
                AccumulatedFrequencies[i] = AccumulatedFrequencies[i - 1] + Frequencies[i];
        }
    }

    private double GetMathExpectation() =>
        Math.Round(Averages.Select((t, i) => t * RelativeFrequencies[i]).Sum(), 2);

    private void SetDispersionAndZValues()
    {
        ZValues = new double[Averages.Length];
        for (var i = 0; i < Averages.Length; i++)
        {
            ZValues[i] = Averages[i] - MathExpectation;
            Dispersion += ZValues[i] * ZValues[i] * RelativeFrequencies[i];
        }
    }

    private double GetFashionValue()
    {
        var idx = Frequencies.ToList().IndexOf(Frequencies.Max());
        var num = Frequencies[idx];
        if (idx != 0)
            num = Frequencies[idx] - Frequencies[idx - 1];
        var den = num + Frequencies[idx];
        if (idx != Frequencies.Length)
            den = num + Frequencies[idx] - Frequencies[idx + 1];
        return Math.Round(Intervals[idx] + (double) num / den * IntervalLength, 4);
    }

    private double GetMedianValue()
    {
        var idx = AccumulatedFrequencies.ToList().FindIndex(x => x >= DataSet.Count / 2);
        var num = 0.5d * DataSet.Count;
        if (idx - 1 > 0)
            num = 0.5d * DataSet.Count - AccumulatedFrequencies[idx - 1];
        double den = Frequencies[idx];
        return Math.Round(Intervals[idx] + num / den * IntervalLength, 4);
    }

    private double GetSkewnessCoefficient()
    {
        var m3 = RelativeFrequencies.Select((t, i) => ZValues[i] * ZValues[i] * ZValues[i] * t).Sum();
        return Math.Round(m3 / Dispersion / StandardDeviation, 4);
    }

    private double GetExcessCoefficient()
    {
        var m4 = RelativeFrequencies.Select((t, i) => ZValues[i] * ZValues[i] * ZValues[i] * ZValues[i] * t).Sum();
        return Math.Round(m4 / Dispersion / Dispersion - 3, 4);
    }

    public double MathExpectationDovInterval(double standardDeviation, int n)
    {
        const double t = 2.045d;
        var delta = t * standardDeviation / Math.Sqrt(n);
        return delta;
    }
}