using System.ComponentModel;
using ZedGraph;

namespace MathStatApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 650);
        this.Text = "Form1";

        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(800, 50), "Выберите файл", new Point(0, 0), ButtonOpenFile);
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 100), "1", new Point(0, 50), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.Chlorides));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 100), "2", new Point(0, 150), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.CitricAcid));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 100), "3", new Point(0, 250), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.FixedAcidity));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 100), "4", new Point(0, 350), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.ResidualSugar));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 100), "5", new Point(0, 450), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.VolatileAcidity));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 100), "6", new Point(0, 550), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.FreeSulfurDioxide));

        var zedgraphControl = new ZedGraphControl();
        zedgraphControl.Name = "graph";
        zedgraphControl.Size = new Size(697, 597);
        zedgraphControl.Location = new Point(100, 50);
        AddToControls(zedgraphControl);
    }
    
    private void AddToControls(Control contol) => Controls.Add(contol);

    private void AddButton(Font font, Size size, string text, Point location, EventHandler eventHandler)
    {
        var button = new Button();
        button.Font = font;
        button.Size = size;
        button.TextAlign = ContentAlignment.BottomCenter;
        button.Text = text;
        button.Location = location;
        button.Click += eventHandler;
        AddToControls(button);
    }

    #endregion
}