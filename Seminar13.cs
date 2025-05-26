using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoronoiWinForms
{
    public class VoronoiDiagram
    {
        private List<PointF> sites;
        private RectangleF bounds;

        public VoronoiDiagram(List<PointF> sites, RectangleF bounds)
        {
            this.sites = sites;
            this.bounds = bounds;
        }

        public List<PointF[]> Compute()
        {
            var cells = new List<PointF[]>();
            foreach (var site in sites)
            {
                List<PointF> cell = new List<PointF>
                {
                    new PointF(bounds.Left, bounds.Top),
                    new PointF(bounds.Right, bounds.Top),
                    new PointF(bounds.Right, bounds.Bottom),
                    new PointF(bounds.Left, bounds.Bottom)
                };

                foreach (var other in sites)
                {
                    if (other == site) continue;

                    float midX = (site.X + other.X) / 2;
                    float midY = (site.Y + other.Y) / 2;
                    float dx = other.Y - site.Y;
                    float dy = site.X - other.X;

                    float length = (float)System.Math.Sqrt(dx * dx + dy * dy);
                    dx /= length;
                    dy /= length;

                    float a = dx;
                    float b = dy;
                    float c = -(a * midX + b * midY);

                    List<PointF> newCell = new List<PointF>();
                    for (int i = 0; i < cell.Count; i++)
                    {
                        PointF current = cell[i];
                        PointF next = cell[(i + 1) % cell.Count];

                        bool insideCurrent = a * current.X + b * current.Y + c <= 0;
                        bool insideNext = a * next.X + b * next.Y + c <= 0;

                        if (insideCurrent)
                            newCell.Add(current);

                        if (insideCurrent != insideNext)
                        {
                            float dxLine = next.X - current.X;
                            float dyLine = next.Y - current.Y;

                            float denominator = a * dxLine + b * dyLine;
                            if (denominator == 0) continue;

                            float t = -(a * current.X + b * current.Y + c) / denominator;
                            if (t >= 0 && t <= 1)
                            {
                                PointF intersection = new PointF(
                                    current.X + t * dxLine,
                                    current.Y + t * dyLine
                                );
                                newCell.Add(intersection);
                            }
                        }
                    }

                    cell = newCell;
                }

                cells.Add(cell.ToArray());
            }

            return cells;
        }
    }
    public partial class Form1 : Form
    {
        List<PointF> points;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            points = new List<PointF>();
            this.Paint += Form1_Paint;
            this.MouseClick += Form1_MouseClick;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            points.Add(e.Location);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count < 2) return;

            RectangleF bounds = new RectangleF(0, 0, ClientSize.Width, ClientSize.Height);
            VoronoiDiagram voronoi = new VoronoiDiagram(points, bounds);
            var cells = voronoi.Compute();

            int i = 0;
            foreach (var cell in cells)
            {
                var color = Color.FromArgb(80, 100 + i * 20 % 155, 100 + i * 40 % 155, 255);
                e.Graphics.FillPolygon(new SolidBrush(color), cell);
                e.Graphics.DrawPolygon(Pens.Black, cell);
                i++;
            }

            foreach (var p in points)
                e.Graphics.FillEllipse(Brushes.Red, p.X - 3, p.Y - 3, 6, 6);
        }
    }
}
