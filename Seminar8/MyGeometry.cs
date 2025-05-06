using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Seminar8
{
    public static class MyGeometry
    {
        public static int CrossProduct(Point p1,Point p2,Point p3) {
            int x1 = p1.X, x2 = p2.X, x3 = p3.X, y1 = p1.Y, y2 = p2.Y, y3 = p3.Y;
            return (y3 - y1) * (x2 - x1) - (y2 - y1) * (x3 - x1);
        }

       public static int CrossProduct(int i1, int i2, int i3, List<Point> points) {
            Point p1 = points[i1];
            Point p2 = points[i2];
            Point p3 = points[i3];
            return CrossProduct(p1, p2, p3);
        }




       public static bool Intersect(Point start1, Point end1, Point start2, Point end2) {
            int det11 = CrossProduct(end1, start1, start2);
            int det12 = CrossProduct(end1, start1, end2);
            int det21 = CrossProduct(end2, start2, start1);
            int det22 = CrossProduct(end2, start2, end1);

            bool result = det11 * det12 <= 0 && det21 * det22 <= 0;
            return result;
        }
       public static bool Intersect(int s1, int e1, int s2, int e2, List<Point> points) {
            Point start1 = points[s1];
            Point start2 = points[s2];
            Point end1 = points[e1];
            Point end2 = points[e2];

            return Intersect(start1, end1, start2, end2);
        }



       public static bool RightTurn(Point p1, Point p2, Point p3) {
            bool result = CrossProduct(p1, p2, p3) > 0;
            return result;
        }
       public static bool LeftTurn(Point p1, Point p2, Point p3) {
            bool result = CrossProduct(p1, p2, p3) < 0;
            return result;
        }

      public  static bool RightTurn(int i1, int i2, int i3, List<Point> points) {
            Point p1 = points[i1];
            Point p2 = points[i2];
            Point p3 = points[i3];

            return RightTurn(p1, p2, p3);
        }
      public  static bool LeftTurn(int i1, int i2, int i3, List<Point> points) {
            Point p1 = points[i1];
            Point p2 = points[i2];
            Point p3 = points[i3];

            return LeftTurn(p1, p2, p3);
        }

       public static bool ConvexPeak(int index, List<Point> points) {
            int n = points.Count;
            int prev_index = (index > 0) ? index - 1 : n - 1;
            int next_index = (index < n - 1) ? index + 1 : 0;
            return RightTurn(prev_index, index, next_index,points);
        }
       public static bool ReflexPeak(int index, List<Point> points) {
            int n = points.Count;
            int prev_index = (index > 0) ? index - 1 : n - 1;
            int next_index = (index < n - 1) ? index + 1 : 0;
            return LeftTurn(prev_index, index, next_index, points);
        }




      public  static bool InsidePolygon(int i, int j,List<Point> points) {
            int n = points.Count;
            int prev_i = (i - 1 >= 0) ? i - 1 : n - 1;
            int next_i = (i + 1 <= n - 1) ? i + 1 : 0;
            bool nec1 = ConvexPeak(i, points) && LeftTurn(i, j, next_i, points) && LeftTurn(i,prev_i,j,points);
            bool nec2 = ReflexPeak(i, points) && RightTurn(i, prev_i, j, points) && RightTurn(i, j, next_i, points);
            return nec1 || nec2;
        }



       public  static bool Diagonal(int i, int j,List<Point> points) {
            int n = points.Count;
            if (!InsidePolygon(i, j, points)) return false;

            for (int t = 0; t < n; t++) {
                int t1 = t;
                int t2 = (t + 1) % n;
                if (i == t1 || i == t2 || j == t1 || j == t2)
                    continue;
                if (Intersect(i, j, t1, t2, points))
                    return false;
            }


            return true;
        }
        public static bool AreSegmentsIdentical(Point a1, Point a2, Point b1, Point b2)
        {
            // Use cross product to check collinearity
            int dx1 = a2.X - a1.X;
            int dy1 = a2.Y - a1.Y;
            int dx2 = b2.X - b1.X;
            int dy2 = b2.Y - b1.Y;

            int cross = dx1 * dy2 - dy1 * dx2;
            if (cross != 0)
                return false; // not collinear

            // Check if the segments share the same endpoints (ignoring order)
            bool same1 = (a1 == b1 && a2 == b2);
            bool same2 = (a1 == b2 && a2 == b1);
            return same1 || same2;
        }

    }
}
