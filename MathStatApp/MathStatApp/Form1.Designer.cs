using System.ComponentModel;
using MathStatApp.Graph;
using ZedGraph;
using Label = System.Windows.Forms.Label;

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
        this.ClientSize = new System.Drawing.Size(1000, 900);
        this.Text = "Form1";

        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(1000, 50), "Выберите файл", new Point(0, 0), ButtonOpenFile);
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 150), "1", new Point(0, 50), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.Chlorides));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 150), "2", new Point(0, 200), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.CitricAcid));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 150), "3", new Point(0, 350), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.ResidualSugar));
        AddButton(new Font("Microsoft Sans Serif", 14F), new Size(100, 150), "4", new Point(0, 500), (sender, e) => 
            ButtonClick<ColumnsCsv>(sender, e, c => c.FreeSulfurDioxide));

        var zedgraphControl = new ZedGraphControl();
        zedgraphControl.Name = "graph";
        zedgraphControl.Size = new Size(897, 597);
        zedgraphControl.Location = new Point(100, 50);
        AddToControls(zedgraphControl);
        
        var textBox = new TextBox();
        textBox.Name = "textBoxResult";
        textBox.Size = new Size(1000, 400);
        textBox.Location = new Point(0, 650);
        textBox.Font = new Font("Microsoft Sans Serif", 12F);
        textBox.ScrollBars = ScrollBars.Vertical;
        textBox.Multiline = true;
        AddToControls(textBox);
        
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