namespace Seminar8
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<Point> points = new List<Point>();
        int radius = 3;
        List<(int, int)> diagonals = new List<(int, int)>();
        List<(Point, Point, Point)> triangles = new List<(Point, Point, Point)>();
        Brush[] brushes = new Brush[] { Brushes.Red, Brushes.Green, Brushes.Blue };
        Dictionary<Point, int> pointToColor = new Dictionary<Point, int>();
        bool drawPolygon = false;
        bool drawDiagonals = false;
        bool drawTriangulation = false;
        bool treiColorare = false;


        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //draw points
            for (int i = 0; i < points.Count; i++)
            {
                g.DrawEllipse(Pens.Navy, points[i].X - radius, points[i].Y - radius, 2 * radius, 2 * radius);
                g.DrawString((i + 1).ToString(), new Font(FontFamily.GenericSansSerif, 10), Brushes.Navy, new Point(points[i].X - radius, points[i].Y + radius));

            }
            //draw polygon
            if (drawPolygon)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    int i1 = i;
                    int i2 = (i + 1) % points.Count;
                    g.DrawLine(Pens.Navy, points[i1], points[i2]);
                }
            }
                //draw diagonals
                if (drawDiagonals) {
                    foreach ((int, int) diagonal in diagonals) {
                        g.DrawLine(Pens.Navy, points[diagonal.Item1], points[diagonal.Item2]);
                    }
                }
                //draw triangulation
                if (drawTriangulation) {
                    foreach ((Point, Point, Point) triangle in triangles) {
                        Point p1 = triangle.Item1;
                        Point p2 = triangle.Item2;
                        Point p3 = triangle.Item3;

                        g.DrawLine(Pens.Purple, p1, p2);
                        g.DrawLine(Pens.Purple, p2, p3);
                        g.DrawLine(Pens.Purple, p1, p3);
                        //Three coloration
                        if (treiColorare)
                        {

                            for (int i = 0;i<points.Count;i++)
                            {
                                Point point = points[i];
                                int brushIndex = pointToColor[point];

                                Brush brush = brushes[brushIndex];
                                g.FillEllipse(brush, point.X - radius, point.Y - radius, 2 * radius, 2 * radius);
                                g.DrawString((i+1).ToString(),new Font(FontFamily.GenericSansSerif,10),brush,point.X-radius,point.Y+radius);

                            }
                        }
                    }
                }
            

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Point newPoint = this.PointToClient(Form1.MousePosition);
            points.Add(newPoint);
            triangles.Clear();
            diagonals.Clear();
            pointToColor.Clear();
            drawDiagonals = false;
            drawTriangulation = false;
            drawPolygon = false;
            treiColorare = false;
            
            Invalidate();
        }

        private void ConstruiestePoligon_Click(object sender, EventArgs e)
        {
            drawPolygon = true;
            Invalidate();
        }

        private void DeseneazaDiagonale_Click(object sender, EventArgs e)
        {
            drawDiagonals = true;
            int n = points.Count;
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = i + 2; j < n; j++)
                {
                    if (MyGeometry.Diagonal(i, j, points))
                    {
                        diagonals.Add((i, j));
                    }
                }
            }
            Invalidate();
        }

        private void TriangulareOtectomie_Click(object sender, EventArgs e)
        {
            drawTriangulation = true;
            int n = points.Count;
            List<Point> points2 = new List<Point>(points);
            while (n > 3)
            {
                for (int i = 0; i < n; i++)
                {
                    int i1 = i;
                    int i2 = (i + 1) % n;
                    int i3 = (i + 2) % n;
                    if (MyGeometry.Diagonal(i1, i3, points2))
                    {
                        Point p1 = points2[i1];
                        Point p2 = points2[i2];
                        Point p3 = points2[i3];
                        triangles.Add((p1, p2, p3));
                        points2.RemoveAt(i2);
                        n--;
                        break;
                    }
                }
                triangles.Add((points2[0], points2[1], points2[2]));
            }
            Invalidate();
        }

        private void TreiColorare_Click(object sender, EventArgs e)
        {
            if (drawTriangulation == false) {
                MessageBox.Show("Aceasta operatie deinde de triangulare.\n" +
                    "Deci prima data apasa butonul de triangulare");
            }
            treiColorare = true;
            for (int i = 0; i < points.Count; i++)
            {
                pointToColor[points[i]] = -1;
            }
            (Point, Point, Point) lastTriangle = triangles.Last();
            Point LastPoint1 = lastTriangle.Item1;
            Point lastPoint2 = lastTriangle.Item2;
            Point lastPoint3 = lastTriangle.Item3;
            int colorSum = 3;//0+1+2
            pointToColor[LastPoint1] = 0;
            pointToColor[lastPoint2] = 1;
            pointToColor[lastPoint3] = 2;

            for (int i = triangles.Count - 2; i >= 0; i--) {
                Point p1 = triangles[i].Item1;
                Point p2 = triangles[i].Item2;
                Point p3 = triangles[i].Item3;

                if (pointToColor[p1] == -1) pointToColor[p1] = colorSum - pointToColor[p2] - pointToColor[p3];
                else if (pointToColor[p2] == -1) pointToColor[p2] = colorSum - pointToColor[p1] - pointToColor[p3];
                else pointToColor[p3] = colorSum - pointToColor[p1] - pointToColor[p2];



            }


            Invalidate();
            
        }



    }
}
