namespace Seminar13
{
    public partial class Form1 : Form
    {
        List<PointF> points;
        const int radius = 3;
        Dictionary<PointF, Brush> pointToColor;
        bool voronoi1;
        bool voronoi2;
        List<PointF> vorPoints;

        Brush[] brushes = new Brush[] { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Purple, Brushes.Yellow, Brushes.DarkBlue };
        //the number of colors is 6 so the max number of points must be 6 
        public Form1()
        {
            InitializeComponent();
            points = new List<PointF>();
            pointToColor = new Dictionary<PointF, Brush>();
            voronoi1 = false;
            voronoi2 = false;
            vorPoints = new List<PointF>();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            PointF newPoint = this.PointToClient(MousePosition);
            points.Add(newPoint);
            if (points.Count > brushes.Length)
            {
                points.RemoveAt(0);
            }
            pointToColor = new Dictionary<PointF, Brush>();
            voronoi1 = false;
            voronoi2 = false;
            vorPoints = new List<PointF>();
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            #region plot points
            {
                int n = points.Count;
                for (int i = 0; i < n; i++)
                {
                    string text = (i + 1).ToString();
                    PointF p = points[i];
                    g.DrawString(text, new Font(FontFamily.GenericMonospace, 10), Brushes.Navy, p.X - radius, p.Y + radius);
                    g.DrawEllipse(Pens.Navy, p.X - radius, p.Y - radius, 2 * radius, 2 * radius);
                }
            }
            #endregion

            #region voronoi1
            if (voronoi1)
            {
                foreach (var kv in pointToColor)
                {
                    PointF p = kv.Key;
                    Brush brush = kv.Value;
                    if (brush == Brushes.White)
                        continue;
                    g.FillEllipse(brush, p.X - radius, p.Y - radius, 2 * radius, 2 * radius);


                }
            }
            #endregion

            #region voronoi2
            if (voronoi2)
            {
                foreach (PointF vp in vorPoints)
                    g.FillEllipse(Brushes.Black,vp.X-1,vp.Y-1,2,2);
            }
            #endregion

        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < ClientSize.Height; i++)
            {
                for (int j = 0; j < ClientSize.Width; j++)
                {
                    int x = j;
                    int y = i;
                    if (x % 2 == 0) continue;
                    if (y % 2 == 0) continue;
                    PointF current = new PointF(x, y);
                    int minIndex = -1;
                    float minDistance = float.MaxValue;
                    for (int k = 0; k < points.Count; k++)
                    {
                        float distance = Distance(current, points[k]);
                        if (distance < minDistance) { minDistance = distance; minIndex = k; }
                    }
                    PointF minPoint = points[minIndex];
                    if (minDistance < 20)
                    {
                        pointToColor[current] = Brushes.White;
                        continue;
                    }
                    Brush currentBrush = brushes[minIndex];
                    pointToColor[current] = currentBrush;

                }
            }
            voronoi1 = true;
            Invalidate();
        }
        public static float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (points.Count < 2)
                return;
            voronoi2 = true;
            for (int i = 0; i < ClientSize.Height; i++) {
                for (int j = 0; j < ClientSize.Width; j++) {
                    PointF p = new PointF(j,i);
                    for (int k1 = 0; k1 < points.Count - 1; k1++) 
                        for (int k2 = k1 + 1; k2 < points.Count; k2++) {
                            int d1 = (int)Distance(p, points[k1]);
                            int d2 = (int)Distance(p, points[k2]);
                            if (d1 == d2)
                                vorPoints.Add(p);
                        }
                    
                }
            }
            Invalidate();
        }
    }
}
