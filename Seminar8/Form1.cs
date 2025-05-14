namespace Seminar8
{

    public partial class Form1 : Form
    {
        #region fields
        const int radius = 3;
        List<Point> points;
        bool drawSides;
        List<(Point, Point)> diagonals;
        bool drawDiagonals;
        List<(Point, Point, Point)> triangles;
        Dictionary<Point, Pen> pens;
        Dictionary<Point, Brush> brushes;
        #endregion
        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
            drawSides = false;
            diagonals = new List<(Point, Point)>();
            drawDiagonals = false;
            triangles = new List<(Point, Point, Point)>();
            pens = new Dictionary<Point, Pen>();
            brushes = new Dictionary<Point, Brush>();

        }
        #region aux methods
        public static int increment(int i, int n)
        {
            return (i + 1 < n) ? i + 1 : 0;
        }
        public static int increment(int i, List<Point> points)
        {
            int n = points.Count;
            return increment(i, n);
        }
        public static int decrement(int i, int n)
        {
            return (i - 1 >= 0) ? i - 1 : n - 1;
        }
        public static int decrement(int i, List<Point> points)
        {
            int n = points.Count;
            return decrement(i, n);
        }
        public static int CrossProduct(Point p1, Point p2, Point p3)
        {
            int x1 = p1.X, x2 = p2.X, x3 = p3.X;
            int y1 = p1.Y, y2 = p2.Y, y3 = p3.Y;
            return (y3 - y1) * (x2 - x1) - (y2 - y1) * (x3 - x1);
        }
        public static int CrossProduct(int i1, int i2, int i3, List<Point> points)
        {
            Point p1 = points[i1];
            Point p2 = points[i2];
            Point p3 = points[i3];
            return CrossProduct(p1, p2, p3);
        }
        public static bool LeftTurn(Point p1, Point p2, Point p3)
        {
            return CrossProduct(p1, p2, p3) < 0;
        }
        public static bool LeftTurn(int i1, int i2, int i3, List<Point> points)
        {
            return CrossProduct(i1, i2, i3, points) < 0;
        }
        public static bool LeftTurn(int i, List<Point> points)
        {
            int i_prev = decrement(i, points);
            int i_next = increment(i, points);
            return LeftTurn(i_prev, i, i_next, points);
        }
        public static bool LeftTurn(Point p, List<Point> points)
        {
            int index = -1;
            int n = points.Count;
            for (int i = 0; i < n; i++)
            {
                if (points[i] == p) { index = i; break; }
            }
            if (index != -1)
            {
                return LeftTurn(index, points);
            }
            else
            {
                return false;
            }
        }
        public static bool RightTurn(Point p1, Point p2, Point p3)
        {
            return CrossProduct(p1, p2, p3) > 0;
        }
        public static bool RightTurn(int i1, int i2, int i3, List<Point> points)
        {
            Point p1 = points[i1];
            Point p2 = points[i2];
            Point p3 = points[i3];
            return RightTurn(p1, p2, p3);
        }
        public static bool RightTurn(int i, List<Point> points)
        {
            int prev_i = decrement(i, points);
            int next_i = increment(i, points);
            return RightTurn(prev_i, i, next_i, points);
        }
        public static bool RightTurn(Point p, List<Point> points)
        {
            int index = -1;
            int n = points.Count;
            for (int i = 0; i < n; i++)
            {
                if (points[i] == p)
                {
                    index = i; break;
                }
            }
            if (index == -1) { return false; }
            return RightTurn(index, points);
        }
        public static bool Convex(int i, List<Point> points)
        {
            return RightTurn(i, points);
        }
        public static bool Convex(Point p, List<Point> points)
        {
            return RightTurn(p, points);
        }
        public static bool Convex(Point p1, Point p2, Point p3)
        {
            return RightTurn(p1, p2, p3);
        }
        public static bool Convex(int i1, int i2, int i3, List<Point> points)
        {
            return RightTurn(i1, i2, i3, points);
        }
        public static bool Reflex(int i, List<Point> points)
        {
            return LeftTurn(i, points);
        }
        public static bool Reflex(Point p, List<Point> points)
        {
            return LeftTurn(p, points);
        }
        public static bool Reflex(Point p1, Point p2, Point p3)
        {
            return LeftTurn(p1, p2, p3);
        }
        public static bool Reflex(int i1, int i2, int i3, List<Point> points)
        {
            return LeftTurn(i1, i2, i3, points);
        }
        public static bool Intersect(Point start1, Point end1, Point start2, Point end2)
        {
            int cross11 = CrossProduct(end1, start1, start2);
            int cross12 = CrossProduct(end1, start1, end2);
            int cross21 = CrossProduct(end2, start2, start1);
            int cross22 = CrossProduct(end2, start2, end1);

            if (cross11 * cross12 <= 0 && cross21 * cross22 <= 0) { return true; }
            return false;
        }
        public static bool Intersect(int i1, int i2, int i3, int i4, List<Point> points)
        {
            Point start1 = points[i1];
            Point end1 = points[i2];
            Point start2 = points[i3];
            Point end2 = points[i4];
            return Intersect(start1, end1, start2, end2);
        }
        public static int GetIndex(Point p, List<Point> points)
        {
            int n = points.Count;
            for (int i = 0; i < n; i++)
            {
                if (points[i] == p) { return i; }
            }
            return -1;
        }
        public static bool Inside(int i, int j, List<Point> points)
        {
            int n = points.Count;
            int prev_i = decrement(i, n);
            int next_i = increment(i, n);
            if (Convex(i, points) && LeftTurn(i, j, next_i, points) && LeftTurn(i, prev_i, j, points)) { return true; }
            if (Reflex(i, points) && RightTurn(i, j, next_i, points) && RightTurn(i, prev_i, j, points)) { return true; }
            return false;
        }
        public static bool Inside(Point start, Point end, List<Point> points)
        {
            int i = GetIndex(start, points);
            int j = GetIndex(end, points);
            return Inside(i, j, points);
        }
        public static bool Diagonal(int i, int j, List<Point> points)
        {
            int n = points.Count;
            if (i == increment(j, n) || i == decrement(j, n))
            { return false; }
            for (int k = 0; k < n - 1; k++)
            {
                if (i == k || i == k + 1 || j == k || j == k + 1) { continue; }
                if (Intersect(i, j, k, k + 1, points)) { return false; }
            }
            if (!Inside(i, j, points)) { return false; }
            return true;
        }


        #endregion
        public List<(Point, Point)> GetDiagonals(List<Point> points)
        {
            List<Point> cpoints = new List<Point>(points);
            List<(Point, Point)> diagonals = new List<(Point, Point)>();
            int n = cpoints.Count;
            while (n > 3)
            {
                for (int i = 0; i < n; i++)
                {
                    int i1 = i;
                    int i2 = increment(i1, n);
                    int i3 = increment(i2, n);
                    if (Diagonal(i1, i3, cpoints))
                    {
                        diagonals.Add((cpoints[i1], cpoints[i3]));
                        triangles.Add((cpoints[i1], cpoints[i2], cpoints[i3]));
                        cpoints.RemoveAt(i2);
                        n -= 1;
                        break;
                    }
                }
            }
            return diagonals;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            #region draw points
            {
                int n = points.Count;
                for (int i = 0; i < n; i++)
                {
                    Point p = points[i];
                    int x = p.X;
                    int y = p.Y;
                    g.DrawEllipse(pens[p], x - radius, y - radius, 2 * radius, 2 * radius);
                    string text = (i + 1).ToString();
                    g.DrawString(text, new Font(FontFamily.GenericMonospace, 13), brushes[p], x - radius, y + radius);
                    if (brushes[p] != Brushes.Navy) 
                    {
                        g.FillEllipse(brushes[p],x-radius,y-radius,2*radius,2*radius);
                    }
                }
            }
            #endregion
            #region draw sides
            if (drawSides)
            {
                int n = points.Count;
                for (int i = 0; i < n; i++)
                {
                    int i1 = i;
                    int i2 = increment(i, n);
                    Point p1 = points[i1];
                    Point p2 = points[i2];
                    g.DrawLine(Pens.Navy, p1, p2);
                }
            }
            #endregion
            #region draw diagonals
            if (drawDiagonals)
            {
                foreach (var diag in diagonals)
                {
                    Point start = diag.Item1;
                    Point end = diag.Item2;
                    g.DrawLine(Pens.Navy, start, end);
                }
            }
            #endregion
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Point newPoint = this.PointToClient(Form1.MousePosition);
            points.Add(newPoint);
            drawSides = false;
            drawDiagonals = false;
            diagonals = new List<(Point, Point)>();
            triangles = new List<(Point, Point, Point)>();
            pens = new Dictionary<Point, Pen>();
            brushes = new Dictionary<Point, Brush>();
            for (int i = 0; i < points.Count; i++)
            {
                Point point = points[i];
                pens[point] = Pens.Navy;
                brushes[point] = Brushes.Navy;
            }
            Invalidate();
        }

        private void DRAW_SIDES_Click(object sender, EventArgs e)
        {
            drawSides = true;
            Invalidate();
        }

        private void TRIANGULARE_PRIN_OTECTOMIE_Click(object sender, EventArgs e)
        {
            drawDiagonals = true;
            diagonals = GetDiagonals(points);
            Invalidate();
        }

        private void THREE_COLORING_Click(object sender, EventArgs e)
        {
            int n = triangles.Count;
            (Point, Point, Point) lastTriangle = triangles[n - 1];
            Point z1 = lastTriangle.Item1, z2 = lastTriangle.Item2, z3 = lastTriangle.Item3;
            pens[z1] = Pens.Red;
            pens[z2] = Pens.Green;
            pens[z3] = Pens.Blue;
            brushes[z1] = Brushes.Red;
            brushes[z2] = Brushes.Green;
            brushes[z3] = Brushes.Blue;
            for (int i = n - 2; i >= 0; i--)
            {
                (Point, Point, Point) currentTriangle = triangles[i];
                Point p1 = currentTriangle.Item1, p2 = currentTriangle.Item2, p3 = currentTriangle.Item3;
                if (pens[p1] == Pens.Navy)
                {
                    if (pens[p2] != Pens.Red && pens[p3] != Pens.Red) { pens[p1] = Pens.Red; brushes[p1] = Brushes.Red; }
                    else if (pens[p2] != Pens.Green && pens[p3] != Pens.Green) { pens[p1] = Pens.Green; brushes[p1] = Brushes.Green; }
                    else
                    {
                        pens[p1] = Pens.Blue; brushes[p1] = Brushes.Blue;
                    }
                }
                else if (pens[p2] == Pens.Navy)
                {
                    if (pens[p1] != Pens.Red && pens[p3] != Pens.Red) { pens[p2] = Pens.Red; brushes[p2] = Brushes.Red; }
                    else if (pens[p1] != Pens.Green && pens[p3] != Pens.Green) { pens[p2] = Pens.Green; brushes[p2] = Brushes.Green; }
                    else
                    {
                        pens[p2] = Pens.Blue; brushes[p2] = Brushes.Blue;
                    }
                }
                else
                {
                    if (pens[p2] != Pens.Red && pens[p1] != Pens.Red) { pens[p3] = Pens.Red; brushes[p3] = Brushes.Red; }
                    else if (pens[p2] != Pens.Green && pens[p1] != Pens.Green) { pens[p3] = Pens.Green; brushes[p3] = Brushes.Green; }
                    else
                    {
                        pens[p3] = Pens.Blue; brushes[p3] = Brushes.Blue;
                    }
                }

            }
            
        


            Invalidate();
        }
    }
}
