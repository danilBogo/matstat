﻿using System.ComponentModel;
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
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Form1";

        var button = new Button();
        button.Font = new Font("Microsoft Sans Serif", 10F);
        button.Size = new Size(100, 100);
        button.Text = "Выберите файл";
        button.Click += ButtonOpenFile;
        AddToControls(button);

        var zedgraphControl = new ZedGraphControl();
        zedgraphControl.Name = "graph";
        zedgraphControl.Size = new Size(500, 500);
        zedgraphControl.Location = new Point(100, 100);
        AddToControls(zedgraphControl);
    }
    
    private void AddToControls(Control contol) => Controls.Add(contol);

    #endregion
}