//Problema 1:Triangulare multime de puncte

/*
namespace seminar6
{
    public class Segment { 
    public PointF start { get; set; }
    public PointF end { get; set; }

        public Segment(PointF start, PointF end) {
            this.start = start;
            this.end = end;
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        
        }
        public double Lungime(Segment segment) {
            PointF start = segment.start;
            PointF end = segment.end;
            return Math.Sqrt(Math.Pow(start.X-end.X,2)+Math.Pow(start.Y-end.Y,2));
        }
        public List<Segment> SorteazaSegmente(List<Segment> segmente) {
            List<Segment> toR = new List<Segment>();
            toR = segmente.OrderBy(s=>Lungime(s)).ToList();
            return toR;

        }
        public float Determinant(PointF p1,PointF p2,PointF p3) {
            return (p3.Y-p1.Y) * (p2.X-p1.X) - (p2.Y-p1.Y) * (p3.X-p1.X);
        }
        public bool Intersecteaza(Segment seg1, Segment seg2) {
            PointF start1 = seg1.start, end1 = seg1.end, start2 = seg2.start, end2 = seg2.end;
            float det11 = Determinant(end1,start1,start2);
            float det12 = Determinant(end1,start1,end2);
            float det21 = Determinant(end2,start2,start1);
            float det22 = Determinant(end2,start2,end1);

            if (det11 * det12 < 0 && det21 * det22 < 0) { return true; }
            return false;

            
        }


       
        public Random rnd = new Random();
        public int epsilon = 10;
        public int padding = 20;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int n = rnd.Next(4,30);
            List<PointF> puncte = new List<PointF>();
            for (int i = 0; i < n; i++) {
                float x = rnd.Next(padding,ClientSize.Width-padding);
                float y = rnd.Next(padding,ClientSize.Height-padding);
                PointF punct = new PointF(x,y);
                puncte.Add(punct);

            }
            List<Segment> segmente = new List<Segment>();
            foreach (PointF p1 in puncte) {
                foreach (PointF p2 in puncte) {
                    if (p1 == p2) { continue; }
                    Segment newSegment = new Segment(p1,p2);
                    segmente.Add(newSegment);
                }
            }
            List<Segment> segmenteSortate = SorteazaSegmente(segmente);
            List<Segment> laturiTriunghiuri = new List<Segment>();
            foreach (Segment new_segment in segmenteSortate) {
                bool intersecteaza = false;
                foreach (Segment old_segment in laturiTriunghiuri) {
                    if (new_segment.start == old_segment.start && new_segment.end==old_segment.end) { intersecteaza = false;break; }
                    
                    if (Intersecteaza(new_segment,old_segment)) { intersecteaza = true;break; }
                }
                if (!intersecteaza) {
                    laturiTriunghiuri.Add(new_segment);
                }
            }
            foreach (Segment latura in laturiTriunghiuri) {
                PointF start = latura.start, end = latura.end;
                g.DrawLine(Pens.Blue, start, end);
            }
            foreach (PointF punct in puncte) {
                g.FillEllipse(Brushes.Green,punct.X-epsilon/2,punct.Y-epsilon/2,epsilon,epsilon);
            }
            


            
        }
    }
}
*/













/*problema2
using System.Windows.Forms.Design;

namespace WinFormsApp1
{


    public partial class Form1 : Form
    {
        public static int Determinant(Point p1, Point p2, Point p3)
        {
            return (p3.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }
        public static bool Intersect(Point start1, Point end1, Point start2, Point end2)
        {
            int det11 = Determinant(end1, start1, start2);
            int det12 = Determinant(end1, start1, end2);
            int det21 = Determinant(end2, start2, start1);
            int det22 = Determinant(end2, start2, end1);
            if (det11 * det12 < 0 && det21 * det22 < 0) { return true; }
            return false;
        }
        public static bool Intersect(Segment seg1, Segment seg2)
        {
            return Intersect(seg1.start, seg1.end, seg2.start, seg2.end);
        }
        public Form1()
        {
            InitializeComponent();
        }
        int epsilon = 6;
        List<Segment> sides = new List<Segment>();
        List<Point> points = new List<Point>();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        private void Form1_Click(object sender, EventArgs e)
        {
            Point Position = PointToClient(new Point(Form1.MousePosition.X, Form1.MousePosition.Y));
            points.Add(Position);
            points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            sides.Clear();
            for (int i = 0; i < points.Count; i++) {
                int i1 = i;
                int i2 = (i + 1)%points.Count;
                Point p1 = points[i1];
                Point p2 = points[i2];
                Segment newSide = new Segment(p1,p2);
                bool ok = true;
                foreach (Segment prevSide in sides) {
                    if (Intersect(prevSide, newSide)) { ok = false;break; }
                }
                if (!ok) { continue; }
                sides.Add(newSide);
            }




            Invalidate();


        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (Segment side in sides)
            {
                g.DrawLine(Pens.Green, side.start, side.end);
            }


            foreach (Point p in points)
            {
                g.FillEllipse(Brushes.Red, p.X - epsilon / 2, p.Y - epsilon / 2, epsilon, epsilon);
            }

        }

    }
    public class Segment
    {
        public Point start, end;
        public Segment(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }
    }
}

*/









//problema 3:Triangularea unui poligon convex

/*
namespace seminar6
{
    public class Segment { 
    public PointF start { get; set; }
    public PointF end { get; set; }

    public double Lungime { get; set; }

        public Segment(PointF start,PointF end) {
            this.start = start;
            this.end = end;
            Lungime = Form1.Distance(this.start,this.end);

        }
    }
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {


        }
        public static double Distance(PointF p1,PointF p2) {
            float x1 = p1.X, x2 = p2.X, y1 = p1.Y, y2 = p2.Y;
            return Math.Sqrt(Math.Pow(y2-y1,2)+Math.Pow(x2-x1,2));
        }
        public static float CrossProduct(PointF p1, PointF p2, PointF p3) {
            float x1 = p1.X, x2 = p2.X, x3 = p3.X, y1 = p1.Y, y2 = p2.Y, y3 = p3.Y;
            return (y3-y1) * (x2-x1) - (y2-y1) * (x3-x1);
        }
        public static List<PointF> JarvisAlgorithm(List<PointF> points) {
            List<PointF> toR = new List<PointF>();
            PointF start = points.OrderBy(p=>p.Y).ThenBy(p=>p.X).First();
            PointF current = start;
            do {
                toR.Add(current);
                PointF next = points[0];
                foreach (PointF p in points) {
                    if (current == p) { continue; }
                    float cross = CrossProduct(current,next,p);
                    if (cross > 0 || (cross == 0 && Distance(current, next) < Distance(current, p))) {
                        next = p;
                    }
                }
                current = next;
            } while (start!=current);
            return toR;
        }

        public static bool Intersect(Segment s1, Segment s2)
        {
            PointF start1 = s1.start, start2 = s2.start, end1 = s1.end, end2 = s2.end;
            float cross11 = CrossProduct(end1, start1, start2);
            float cross12 = CrossProduct(end1, start1, end2);
            float cross21 = CrossProduct(end2, start2, start1);
            float cross22 = CrossProduct(end2, start2, end1);
            if (cross11 * cross12 < 0 && cross21 * cross22 < 0) { return true; }
            return false;

        }
        public static List<Segment> Trianguleaza(List<PointF> points) {
            List<Segment> segmente = new List<Segment>();
            foreach (PointF start in points) {
                foreach (PointF end in points) {
                    if (start == end) { continue; }
                    Segment segment = new Segment(start,end);
                    segmente.Add(segment);
                }
            }
            return Trianguleaza(segmente);
        }
        public static List<Segment> Trianguleaza(List<Segment> segments) {
            List<Segment> toR = new List<Segment>();
            segments = segments.OrderBy(s=>s.Lungime).ToList();
            foreach (Segment segment in segments) {
                bool add = true;
                foreach (Segment inSegment in toR) {
                    if (Intersect(segment, inSegment)) { add = false;break; }
                }
                if (add) {
                    toR.Add(segment);
                }
            }
            return toR;
        }


        public Random rnd = new Random();
        public int padding = 20;
        public float epsilon = 8;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            List<PointF> varfuri = new List<PointF>();
            int n = rnd.Next(10,40);
            for (int i = 0; i < n; i++) {
                varfuri.Add(new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding)));
            }
            varfuri = JarvisAlgorithm(varfuri);
            List<Segment> laturiTriunghiuri = Trianguleaza(varfuri);

            foreach (Segment latura in laturiTriunghiuri) {
                PointF start = latura.start;
                PointF end = latura.end;
                g.DrawLine(Pens.Blue,start,end);
            }
            foreach (PointF varf in varfuri) {
                g.FillEllipse(Brushes.Green,varf.X-epsilon/2,varf.Y-epsilon/2,epsilon,epsilon);
            }
            
            }

        private void Form1_Click(object sender, EventArgs e) {
        
        }
           



        }
    }


*/
