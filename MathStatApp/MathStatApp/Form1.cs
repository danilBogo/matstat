using MathStatApp.Graph;
using MathStatApp.MathProcessor;
using ZedGraph;
using Label = System.Windows.Forms.Label;

namespace MathStatApp;

public partial class Form1 : Form
{
    private string _pathFile;
    
    public Form1()
    {
        InitializeComponent();
    }

    private void ButtonOpenFile(object sender, EventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Выберите csv файл";
        openFileDialog.Filter = "Файлы CSV|*.csv";

        openFileDialog.InitialDirectory = GetInitialDirectory();
        if (openFileDialog.ShowDialog() != DialogResult.OK)
        {
            openFileDialog.Reset();
            return;
        }
        
        _pathFile = openFileDialog.FileName;
    }

    private void ButtonClick<T>(object sender, EventArgs e, Func<T, string> selector)
    {
        if (string.IsNullOrEmpty(_pathFile))
            return;
        var parser = new Parser.Parser(_pathFile);
        var initialDataset = parser.GetData<T>().Select(selector);
        var result = ResultBuilder.GetResult(initialDataset);
        if (Controls["textBoxResult"] is not TextBox textBox)
            return;
        textBox.Text = result.Message;

        if (Controls["graph"] is not ZedGraphControl zedGraph)
            return;
        var list = new List<GraphColumn>();
        for (var i = 0; i < result.Intervals.Length; i++)
        {
            list.Add(new GraphColumn(i, result.Intervals[i], result.RelativeFrequencies[i]));
        }
        Graph.ZedGraph.DrawGraph(zedGraph, list);
    }

    private string GetInitialDirectory()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        return dir.Parent!.Parent!.Parent!.Parent!.Parent!.ToString();
    }
}