using System.Globalization;

namespace MathStatApp.MathProcessor;

public static class ResultBuilder
{
    public static GraphResult GetResult(IEnumerable<string> initialDataSet)
    {
        var dataSet = GetDataSet(initialDataSet);
        var processor = new Processor(dataSet);
        var message = $"Объем выборки: {processor.DataSetCount}{Environment.NewLine}" +
                     $"Максимальный элемент: {processor.XMax}{Environment.NewLine}" +
                     $"Минимальный элемент: {processor.XMin}{Environment.NewLine}" +
                     $"Количество интервалов: {processor.IntervalsCount}{Environment.NewLine}" +
                     $"Длина интервала: {processor.IntervalLength}{Environment.NewLine}" +
                     $"Математическое ожидание: {processor.MathExpectation}{Environment.NewLine}" +
                     $"Дисперсия: {processor.Dispersion}{Environment.NewLine}" +
                     $"Стандартное отклонение: {processor.StandardDeviation}{Environment.NewLine}" +
                     $"Мода: {processor.Fashion}{Environment.NewLine}" +
                     $"Медиана: {processor.MedianValue}{Environment.NewLine}" +
                     $"Коэффициент ассиметрии: {processor.SkewnessCoefficient}{Environment.NewLine}" +
                     $"Скошенность: {processor.SkewnessCoefficientType}{Environment.NewLine}" +
                     $"Коэффициент эксцесса: {processor.ExcessCoefficient} ({processor.ExcessCoefficientType}){Environment.NewLine}" +
                     $"Коэффициент вариации: {processor.VariationCoefficient}{Environment.NewLine}" +
                     $"{processor.VariationCoefficientType}{Environment.NewLine}";
        return new GraphResult(message, processor.RelativeFrequencies, processor.Intervals);
    }

    private static List<double> GetDataSet(IEnumerable<string> initialDataSet) =>
        initialDataSet.Select(element => double.Parse(element.Replace("\"", ""), CultureInfo.InvariantCulture)).ToList();
}