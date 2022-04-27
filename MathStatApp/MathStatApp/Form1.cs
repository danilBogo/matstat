using MathStatApp.MathProcessor;
using ZedGraph;

namespace MathStatApp;

public partial class Form1 : Form
{
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

        var filePath = openFileDialog.FileName;
        var parser = new Parser(filePath);
        var initialDataset = parser.GetData<ColumnsDanil>().Select(c => c.CitricAcid);
        var result = ResultBuilder.GetResult(initialDataset);
        MessageBox.Show(result);

        if (Controls["graph"] is not ZedGraphControl zedGraph)
            return;
        ZedGraph.DrawGraph(zedGraph, new List<Column>());
    }

    private string GetInitialDirectory()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        return dir.Parent!.Parent!.Parent!.Parent!.Parent!.ToString();
    }
}