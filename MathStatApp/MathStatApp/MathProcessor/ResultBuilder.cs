using System.Globalization;

namespace MathStatApp.MathProcessor;

public static class ResultBuilder
{
    public static Result GetResult(IEnumerable<string> initialDataSet)
    {
        var dataSet = GetDataSet(initialDataSet);
        var processor = new Processor(dataSet);
        var message = $"Объем выборки: {processor.DataSetCount}\n" +
                     $"Максимальный элемент: {processor.XMax}\n" +
                     $"Минимальный элемент: {processor.XMin}\n" +
                     $"Количество интервалов: {processor.IntervalsCount}\n" +
                     $"Длина интервала: {processor.IntervalLength}\n" +
                     $"Математическое ожидание: {processor.MathExpectation}\n" +
                     $"Дисперсия: {processor.Dispersion}\n" +
                     $"Стандартное отклонение: {processor.StandardDeviation}\n" +
                     $"Мода: {processor.Fashion}\n" +
                     $"Медиана: {processor.MedianValue}\n" +
                     $"Коэффициент ассиметрии: {processor.SkewnessCoefficient}\n" +
                     $"Скошенность: {processor.SkewnessCoefficientType}\n" +
                     $"Коэффициент эксцесса: {processor.ExcessCoefficient} ({processor.ExcessCoefficientType})\n" +
                     $"Коэффициент вариации: {processor.VariationCoefficient}\n" +
                     $"{processor.VariationCoefficientType}\n";
        return new Result(message, processor.RelativeFrequencies, processor.Intervals);
    }

    private static List<double> GetDataSet(IEnumerable<string> initialDataSet) =>
        initialDataSet.Select(element => double.Parse(element.Replace("\"", ""), CultureInfo.InvariantCulture)).ToList();
}