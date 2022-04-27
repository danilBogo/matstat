using ZedGraph;

namespace MathStatApp;

public static class ZedGraph
{
    public static void DrawGraph(ZedGraphControl zedGraph, List<Column> columns)
    {
        var pane = zedGraph.GraphPane;
        pane.CurveList.Clear();

        // var itemscount = 5;
        //
        // var rnd = new Random ();
        //
        // // Подписи под столбиками
        // var names = new string[itemscount];
        //
        // // Высота столбиков
        // var values = new double[itemscount];
        //
        // // Заполним данные
        // for (int i = 0; i < columns.Count; i++)
        // {
        //     names[i] = string.Format ("Текст {0}", i);
        //     values[i] = rnd.NextDouble ();
        // }

        // Создадим кривую-гистограмму
        // Первый параметр - название кривой для легенды
        // Второй параметр - значения для оси X, т.к. у нас по этой оси будет идти текст, а функция ожидает тип параметра double[], то пока передаем null
        // Третий параметр - значения для оси Y
        // Четвертый параметр - цвет
        pane.AddBar("Гистограмма", null, columns.Select(c => c.Height).ToArray(), Color.Blue);

        // Настроим ось X так, чтобы она отображала текстовые данные
        pane.XAxis.Type = AxisType.Text;

        // Уставим для оси наши подписи
        pane.XAxis.Scale.TextLabels = columns.Select(c => c.Name).ToArray();

        // Вызываем метод AxisChange (), чтобы обновить данные об осях.
        zedGraph.AxisChange();

        // Обновляем график
        zedGraph.Invalidate();
    }
}