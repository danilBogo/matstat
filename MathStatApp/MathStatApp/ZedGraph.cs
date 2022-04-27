using ZedGraph;

namespace MathStatApp;

public static class ZedGraph
{
    public static void DrawGraph(ZedGraphControl zedGraph, List<GraphColumn> columns)
    {
        var pane = zedGraph.GraphPane;
        pane.CurveList.Clear();

        pane.AddBar(null, null, columns.Select(c => c.Y).ToArray(), Color.Blue);

        pane.XAxis.Type = AxisType.Text;

        pane.XAxis.Scale.TextLabels = columns.Select(c => c.X).ToArray();

        zedGraph.AxisChange();

        zedGraph.Invalidate();
    }
}