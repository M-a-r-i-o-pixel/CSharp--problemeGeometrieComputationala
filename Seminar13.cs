namespace Seminar13
{
    public partial class Form1 : Form
    {
        List<PointF> points;
        const int radius = 3;
        Dictionary<PointF, Brush> pointToColor;
        bool voronoi;

        Brush[] brushes = new Brush[] { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Purple, Brushes.Yellow, Brushes.DarkBlue };
        //the number of colors is 6 so the max number of points must be 6 
        public Form1()
        {
            InitializeComponent();
            points = new List<PointF>();
            pointToColor = new Dictionary<PointF, Brush>();
            voronoi = false;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            PointF newPoint = this.PointToClient(MousePosition);
            points.Add(newPoint);
            if (points.Count > brushes.Length) {
                points.RemoveAt(0);
            }
            pointToColor = new Dictionary<PointF, Brush>();
            voronoi = false;
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

            if (voronoi) {
                foreach (var kv in pointToColor) {
                    PointF p = kv.Key;
                    Brush brush = kv.Value;
                    if (brush == Brushes.White)
                        continue;
                    g.FillEllipse(brush,p.X-radius,p.Y-radius,2*radius,2*radius);
                    
                    
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < ClientSize.Height; i++) {
                for (int j = 0; j < ClientSize.Width; j++) {
                    int x = j;
                    int y = i;
                    if (x % 7 == 0) continue;
                    if(y%7==0) continue;
                    PointF current = new PointF(x,y);
                    int minIndex = -1;
                    float minDistance = float.MaxValue;
                    for (int k = 0; k < points.Count; k++) {
                        float distance = Distance(current, points[k]);
                        if (distance < minDistance) { minDistance = distance;minIndex = k; }
                    }
                    PointF minPoint = points[minIndex];
                    if (minDistance < 20) {
                        pointToColor[current] = Brushes.White;
                        continue;
                    }
                    Brush currentBrush = brushes[minIndex];
                    pointToColor[current] = currentBrush;

                }
            }
            voronoi = true;
            Invalidate();
        }
        public static float Distance(PointF p1, PointF p2) {
            return (float)Math.Sqrt(Math.Pow(p1.X-p2.X,2)+Math.Pow(p1.Y-p2.Y,2));
        }
    }
}
