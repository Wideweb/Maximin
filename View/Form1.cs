using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logic.Domain;
using Logic.Services;
using Logic.Services.Options;
using Point = Logic.Domain.Point;

namespace View
{
    public partial class Form1 : Form
    {
        private readonly Graphics graphics;

        public Form1()
        {
            InitializeComponent();
            graphics = CreateGraphics();
        }

        private void Render()
        {
            var generatorOptions = new PointsGeneratorOptions
            {
                PointsCount = 20000,
                Min = 20,
                Max = 600
            };
            var generator = new PointsGenerator(generatorOptions);

            DrawClusters(new MaximinAlgorithm().Classify(generator.GeneratePoints()));
        }

        private void DrawClusters(List<Cluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                var color = GenerateColor();
                cluster.Points.ForEach(p => DrawPoint(p, color));
                DrawCore(cluster.Core, color);
            }
        }

        private void DrawPoint(Point point, Color color)
        {
            var p = new Pen(color, 1);
            graphics.DrawRectangle(p, point.X, point.Y, 1, 1);
        }

        private void DrawCore(Point core, Color color)
        {
            var p = new Pen(color, 5);
            graphics.DrawRectangle(p, core.X, core.Y, 5, 5);
        }

        private Color GenerateColor()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            return Color.FromArgb(255, 
                rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        private void ClassifyBtn_Click(object sender, EventArgs e)
        {
            graphics.Clear(BackColor);
            Render();
        }
    }
}
