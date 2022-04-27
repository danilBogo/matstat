using System.Globalization;

namespace MathStatApp.MathProcessor;

public static class ResultBuilder
{
    public static string GetResult(IEnumerable<string> initialDataSet)
    {
        var dataSet = GetDataSet(initialDataSet);
        var processor = new Processor(dataSet);
        var result = $"Объем выборки: {processor.DataSetCount}" +
                     $"Максимальный элемент: {processor.XMax}" +
                     $"Минимальный элемент: {processor.XMin}" +
                     $"Количество интервалов: {processor.IntervalsCount}" +
                     $"Длина интервала: {processor.IntervalLength}" +
                     $"Математическое ожидание: {processor.MathExpectation}" +
                     $"Дисперсия: {processor.Dispersion}" +
                     $"Стандартное отклонение: {processor.StandardDeviation}" +
                     $"Мода: {processor.Fashion}" +
                     $"Медиана: {processor.MedianValue}" +
                     $"Коэффициент ассиметрии: {processor.SkewnessCoefficient}" +
                     $"Скошенность: {processor.SkewnessCoefficientType}" +
                     $"Коэффициент эксцесса: {processor.ExcessCoefficient} ({processor.ExcessCoefficientType})" +
                     $"Коэффициент вариации: {processor.VariationCoefficient}" +
                     $"{processor.VariationCoefficientType}";
        return result;
    }

    private static List<double> GetDataSet(IEnumerable<string> initialDataSet) =>
        initialDataSet.Select(element => double.Parse(element.Replace("\"", ""), CultureInfo.InvariantCulture)).ToList();
}