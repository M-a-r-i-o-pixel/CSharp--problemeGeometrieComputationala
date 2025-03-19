
//winform project
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace setProbleme2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int padding = 20;
        public int epsilon = 10;
        public Random rnd = new Random();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private float distanta(PointF a, PointF b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        private float ariaTriunghi(PointF a, PointF b, PointF c)
        {
            float x1 = a.X, x2 = b.X, x3 = c.X;
            float y1 = a.Y, y2 = b.Y, y3 = c.Y;
            float d = x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2);
            return 0.5f * Math.Abs(d);
        }

        private bool coliniare(PointF a, PointF b, PointF c)
        {
            float x1 = a.X, x2 = b.X, x3 = c.X;
            float y1 = a.Y, y2 = b.Y, y3 = c.Y;
            float determinant = x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2);
            if (determinant == 0) { return true; }
            return false;
        }
        private float produsVectorial(PointF a, PointF b, PointF c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
        }
        private List<PointF> invelitoareConvexa(PointF[] puncte)
        {
            Stack<PointF> hull = new Stack<PointF>();
            PointF pivot = puncte.OrderBy(p => p.Y).ThenBy(p => p.X).First();
            puncte = puncte.OrderBy(p => Math.Atan2(p.Y - pivot.Y, p.X - pivot.X)).ThenBy(p => distanta(p, pivot)).ToArray();
            hull.Push(puncte[0]);
            hull.Push(puncte[1]);
            for (int i = 2; i < puncte.Length; i++)
            {
                while (hull.Count >= 2 && produsVectorial(pivot, hull.ElementAt(hull.Count - 2), puncte[i]) <= 0)
                {
                    hull.Pop();
                }
                hull.Push(puncte[i]);
            }
            return hull.ToList();

        }



        //start cod necesar pentru algoritmul lui welzl

        #region algoritmul lui Welzl

        private class Cerc
        {
            public float Raza { set; get; }
            public PointF Centru { set; get; }

            public Cerc(PointF centru, float raza)
            {
                Raza = raza;
                Centru = new PointF(centru.X, centru.Y);

            }

        }

        private Cerc CercMinim(List<PointF> puncte)
        {
            int n = puncte.Count;
            if (n == 1) { return new Cerc(new PointF(puncte[0].X, puncte[0].Y), 0); }
            if (n == 2)
            {
                PointF Centru = new PointF((puncte[0].X + puncte[1].X) / 2, (puncte[0].Y + puncte[1].Y) / 2);
                float Raza = distanta(puncte[0], puncte[1]) / 2;
                return new Cerc(Centru, Raza);
            }
            if (n == 3)
            {


                //start calcul determinare centru curent
                if (coliniare(puncte[0], puncte[1], puncte[2])) { return new Cerc(new PointF(0, 0), 0); }
                float x1 = puncte[0].X, x2 = puncte[1].X, x3 = puncte[2].X;
                float y1 = puncte[0].Y, y2 = puncte[1].Y, y3 = puncte[2].Y;
                float t1 = x1 * x1 - x2 * x2 + y1 * y1 - y2 * y2;
                float t2 = x1 * x1 - x3 * x3 + y1 * y1 - y3 * y3;
                float p1 = 2 * (x2 - x1);
                float p2 = 2 * (x3 - x1);
                float s1 = 2 * (y2 - y1);
                float s2 = 2 * (y3 - y1);

                float yCentru = (t1 * p2 - t2 * p1) / (s2 * p1 - s1 * p2);
                float xCentru = (-t2 - yCentru * s2) / p2;
                PointF centru = new PointF(xCentru, yCentru);
                //sfarsit calcul determinare centru curent
                float raza = distanta(centru, puncte[0]);
                return new Cerc(centru, raza);

            }
            return new Cerc(new PointF(0, 0), 0);
        }

        private Cerc Welzl(List<PointF> puncte, List<PointF> puncteExcluse)
        {
            if (puncte.Count == 0 || puncteExcluse.Count == 3)
            {
                return CercMinim(puncteExcluse);
            }

            int i = rnd.Next(0, puncte.Count);
            PointF punctExclus = puncte[i];
            puncte.RemoveAt(i);

            Cerc cerc = Welzl(puncte, puncteExcluse);

            if (distanta(cerc.Centru, punctExclus) > cerc.Raza)
            {
                puncteExcluse.Add(punctExclus);
                cerc = Welzl(puncte, puncteExcluse);
                puncteExcluse.RemoveAt(puncteExcluse.Count - 1);
            }

            puncte.Add(punctExclus);
            return cerc;
        }




        #endregion

        //sfarsit cod necesar pentru algoritmul lui welzl



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //problema 1:
            //cerinta: n puncte in plan ,constanta d>0
            //toate punctele care sunt la o distanta mai mica sau egala de un punct fixat q

            //q - albastru 
            //puncte apropiate -verde
            //puncte - rosii

            //rezolvare:
            /*
            int n = rnd.Next(10,50);
            float d = rnd.Next(1,Math.Min(ClientSize.Width,ClientSize.Height)-padding);
            PointF[] puncte = new PointF[n];
            for (int i=0;i<n;i++) {
                puncte[i] = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
            }
            PointF q = puncte[rnd.Next(0,n)];
            List<PointF> puncteApropiate = new List<PointF>();
            for (int i=0;i<n;i++) {
                Brush brush = Brushes.Red;
                if (puncte[i] == q) { brush = Brushes.Blue; }
                g.FillEllipse(brush, puncte[i].X - epsilon / 2, puncte[i].Y-epsilon/2,epsilon,epsilon);
            }
            for (int i=0;i<n;i++) {
                if (i == Array.IndexOf(puncte,q)) { continue; }
                if (distanta(puncte[i], q) <= d) { puncteApropiate.Add(puncte[i]); }
            }
            foreach (PointF punct in puncteApropiate) {
                g.FillEllipse(Brushes.Green,punct.X-epsilon/2,punct.Y-epsilon/2,epsilon,epsilon);
            }*/



            //problema2:
            /*Cerinta:
             * O multime de puncte in plan.
             * Triunghiul de arie minima desen
             * varfurile fac parte din multime
             */

            //rezolvare banala:



            /* int n = rnd.Next(6,6);
             PointF[] puncte = new PointF[n];
             PointF[] varfuri = new PointF[3];
             for (int i = 0; i < n; i++) {
                 puncte[i] = new PointF(rnd.Next(padding, ClientSize.Width - padding), rnd.Next(padding, ClientSize.Height - padding));
                 g.FillEllipse(Brushes.Red, puncte[i].X - epsilon / 2, puncte[i].Y-epsilon/2,epsilon,epsilon);
             }
             float minArea = (ClientSize.Width - padding) * (ClientSize.Height-padding);
             for (int i=0;i<n-2;i++) {
                 for (int j=i+1;j<n-1;j++) {
                     for (int k=j+1;k<n;k++) {
                         float currentArea = ariaTriunghi(puncte[i], puncte[j], puncte[k]);
                         if (currentArea<minArea && currentArea>0) { minArea = currentArea;
                             varfuri[0] = puncte[i];
                             varfuri[1] = puncte[j];
                             varfuri[2] = puncte[k];
                         }
                     }
                 }
             }
             for (int i=0;i<3;i++) {
                 g.FillEllipse(Brushes.Blue, varfuri[i].X - epsilon / 2, varfuri[i].Y-epsilon/2,epsilon,epsilon);
             }
             for (int i = 0; i < 3; i++) {
                 g.DrawLine(new Pen(Color.Green, 2), varfuri[i].X, varfuri[i].Y, varfuri[(i + 1)%3].X, varfuri[(i+1)%3].Y);
             }

             */

            //problema 2 rezolvare eficienta:  (3-suma de minimizare)



            /*   int n = rnd.Next(10, 100);
               PointF[] puncte = new PointF[n];
               for (int  i=0;i<n;i++) {
                   puncte[i] = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
                   g.FillEllipse(Brushes.Red, puncte[i].X-epsilon/2, puncte[i].Y-epsilon/2,epsilon,epsilon);
               }
               float minArea = (ClientSize.Width-padding) * (ClientSize.Height-padding);
               PointF[] varfuri = new PointF[3];
               for (int i=0;i<n;i++) {
                   PointF[] puncteTranslatate = new PointF[n - 1];
                   int count = 0;
                   for (int j=0;j<i;j++) {
                       puncteTranslatate[count] = new PointF(puncte[j].X - puncte[i].X, puncte[j].Y - puncte[i].Y);
                       count++;
                   }
                   for (int j=i+1;j<n;j++) {
                       puncteTranslatate[count] = new PointF(puncte[j].X - puncte[i].X, puncte[j].Y - puncte[i].Y);
                       count++;
                   }
                   Array.Sort(puncteTranslatate,(PointF a,PointF b) => {
                   float d=a.X*b.Y - b.X * a.Y;
                       if (d == 0) { return 0; }
                       return (int)(d / Math.Abs(d));

                   });
                   for (int j=0;j<n-2;j++) {
                       float currentArea = ariaTriunghi(puncte[i], puncteTranslatate[j], puncteTranslatate[j+1]);
                       if (currentArea>0 && currentArea<minArea) {
                           minArea = currentArea;
                           varfuri[0] = puncte[i];
                           varfuri[1] = new PointF(puncteTranslatate[j].X + puncte[i].X, puncteTranslatate[j].Y + puncte[i].Y);
                           varfuri[2] = new PointF(puncteTranslatate[j + 1].X + puncte[i].X, puncteTranslatate[j + 1].Y + puncte[i].Y);
                       }
                   }

               }



               g.DrawLine(new Pen(Color.Green), varfuri[0], varfuri[1]);
               g.DrawLine(new Pen(Color.Green), varfuri[1], varfuri[2]);
               g.DrawLine(new Pen(Color.Green), varfuri[2], varfuri[0]);
               */



            //problema 3
            /*cerinta:
             * Se da o multime de puncte in plan
             * Sa se determine cercul de raza minima
             * care sa contina toate punctele data in interior(sau cel mult pe frontiera)
             * 
             */

            //rezolvare banala:

            /*
            int n = rnd.Next(10, 50);
            PointF[] puncte = new PointF[n];
            for (int i=0;i<n;i++) {
                puncte[i] = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
                
            }
            float razaMinima = float.MaxValue;
            PointF centru = new PointF((ClientSize.Width-padding)/2,(ClientSize.Height-padding)/2);
            for (int i=0;i<n-1;i++) {
                for (int j=i+1;j<n;j++) {
                    {
                        PointF mijloc = new PointF((puncte[i].X + puncte[j].X) / 2, (puncte[i].Y + puncte[j].Y)/2);
                        float razaCurenta = distanta(puncte[i], puncte[j])/2;
                        if (razaCurenta>=razaMinima) { continue; }
                        bool cuprindeToatePunctele = true;
                        for (int k=0;k<n;k++) {
                            if (k == j || k == i) { continue; }
                            if (distanta(puncte[k], mijloc) > razaCurenta) { cuprindeToatePunctele = false; }
                        }
                        if (cuprindeToatePunctele) {
                            razaMinima = razaCurenta;
                            centru = new PointF(mijloc.X,mijloc.Y);
                        }
                    }
                }
            }


            
            for (int i=0;i<n-2;i++) {
                float x1 = puncte[i].X, y1 = puncte[i].Y;
                for (int j=i+1;j<n-1;j++) {
                    float x2 = puncte[j].X, y2 = puncte[j].Y;
                    for (int k=j+1;k<n;k++) {
                        float x3 = puncte[k].X, y3 = puncte[k].Y;
                        float xCentruCurent, yCentruCurent;


                        //start calcul determinare centru curent
                        if (coliniare(puncte[i], puncte[j], puncte[k])) { continue; }
                        float t1 = x1 * x1 - x2 * x2 + y1 * y1 - y2 * y2;
                        float t2 = x1 * x1 - x3 * x3 + y1 * y1 - y3 * y3;
                        float p1 = 2 * (x2-x1);
                        float p2 = 2 * (x3-x1);
                        float s1 = 2 * (y2 - y1);
                        float s2 = 2 * (y3-y1);

                        yCentruCurent = (t1 * p2 - t2 * p1) / (s2*p1-s1*p2);
                        xCentruCurent = (-t2 - yCentruCurent * s2) / p2;

                        //sfarsit calcul determinare centru curent
                        PointF centruCurent = new PointF(xCentruCurent,yCentruCurent);
                        float razaCurenta = distanta(centruCurent, puncte[i]);
                        if (razaCurenta >= razaMinima) { continue; }
                        bool cuprindeToatePunctele = true;
                        for (int t=0;t<n;t++) {
                            if (t == i || t == j || t == k) { continue; }
                            if (distanta(puncte[t], centruCurent) > razaCurenta)
                            {

                                cuprindeToatePunctele = false;
                                break;
                            }
                        }
                        if (cuprindeToatePunctele) {
                            razaMinima = razaCurenta;
                            centru.X = centruCurent.X;
                            centru.Y = centruCurent.Y;
                        }

                    }
                }
            }
            g.FillEllipse(Brushes.Blue, centru.X - epsilon / 2, centru.Y - epsilon / 2, epsilon, epsilon);
            g.DrawEllipse(new Pen(Color.Green, 3), centru.X - razaMinima, centru.Y - razaMinima, 2 * razaMinima, 2 * razaMinima);
            for (int i = 0; i < n; i++)
            {
                g.FillEllipse(Brushes.Red, puncte[i].X - epsilon / 2, puncte[i].Y - epsilon / 2, epsilon, epsilon);

            }
            */




            //rezolvare cu metoda 2(problema 2):
            //(folosim algoritmul lui graham si convex hull)

            /*
            int n = rnd.Next(10,100);
            PointF[] puncte = new PointF[n];
            for (int i=0;i<n;i++) {
                puncte[i] = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
            }
            List<PointF> varfuriInvelitoare = invelitoareConvexa(puncte);
            PointF centru = new PointF((ClientSize.Width-padding)/2,ClientSize.Height-padding);
            float razaMinima = float.MaxValue; 
            int n2 = varfuriInvelitoare.Count();
            for (int i = 0; i < n2-1; i++) { 
                for (int j=i+1;j<n2;j++) {
                    PointF centruCurent = new PointF((varfuriInvelitoare[i].X + varfuriInvelitoare[j].X) / 2, (varfuriInvelitoare[i].Y + varfuriInvelitoare[j].Y)/2);
                    float razaCurenta = distanta(varfuriInvelitoare[i], varfuriInvelitoare[j])/2;
                    if (razaCurenta >= razaMinima) {continue;}
                    bool cuprindeToatePunctele = true;
                    for (int k=0;k<n2;k++) {
                        if (k==i || k==j) {continue;}
                        if (distanta(centruCurent, varfuriInvelitoare[k]) > razaCurenta) { cuprindeToatePunctele = false;break; }
                    }
                    if (cuprindeToatePunctele) {
                        centru = new PointF(centruCurent.X,centruCurent.Y);
                        razaMinima = razaCurenta;
                    }
                }
            }
            for (int i=0;i<n2-2;i++) {
                float x1 = varfuriInvelitoare[i].X, y1 = varfuriInvelitoare[i].Y;
                for (int j=i+1;j<n2-1;j++) {
                    float x2 = varfuriInvelitoare[j].X, y2 = varfuriInvelitoare[j].Y;
                    for (int k=j+1;k<n2;k++) {
                        float x3 = varfuriInvelitoare[k].X, y3 = varfuriInvelitoare[k].Y;


                        //start calcul determinare centru curent
                        if (coliniare(varfuriInvelitoare[i], varfuriInvelitoare[j], varfuriInvelitoare[k])) { continue; }
                        float t1 = x1 * x1 - x2 * x2 + y1 * y1 - y2 * y2;
                        float t2 = x1 * x1 - x3 * x3 + y1 * y1 - y3 * y3;
                        float p1 = 2 * (x2 - x1);
                        float p2 = 2 * (x3 - x1);
                        float s1 = 2 * (y2 - y1);
                        float s2 = 2 * (y3 - y1);

                        float yCentruCurent = (t1 * p2 - t2 * p1) / (s2 * p1 - s1 * p2);
                        float xCentruCurent = (-t2 - yCentruCurent * s2) / p2;

                        //sfarsit calcul determinare centru curent

                        PointF centruCurent = new PointF(xCentruCurent,yCentruCurent);
                        float razaCurenta = distanta(centruCurent, varfuriInvelitoare[i]);
                        if (razaCurenta >= razaMinima) { continue; }
                        bool cuprindeToatePunctele = true;
                        for (int t=0;t<n2;t++) {
                            if (t == i || t == j || t == k) { continue; }
                            if (distanta(varfuriInvelitoare[t], centruCurent) > razaCurenta) { cuprindeToatePunctele = false;break; }
                        }
                        if (cuprindeToatePunctele) {
                            centru = new PointF(centruCurent.X,centruCurent.Y);
                            razaMinima = razaCurenta;
                        }

                    }
                }
            }

            g.FillEllipse(Brushes.Blue,centru.X-epsilon/2,centru.Y-epsilon/2,epsilon,epsilon);
            g.DrawEllipse(new Pen(Color.Green,4),centru.X-razaMinima,centru.Y-razaMinima,2*razaMinima,2*razaMinima);
            for (int i=0;i<n;i++) {
                g.FillEllipse(Brushes.Red, puncte[i].X - epsilon / 2, puncte[i].Y-epsilon/2,epsilon,epsilon);
            }
            
            */


            //Solutie chiar mai eficienta(Algoritmul lui welzl)
            //rezolvare:
            List<PointF> puncte = new List<PointF>();
            List<PointF> puncteExcluse = new List<PointF>();
            int n = rnd.Next(10,100);
            for (int i=0;i<n;i++) {
                float x = rnd.Next(padding,ClientSize.Width-padding);
                float y = rnd.Next(padding,ClientSize.Height-padding);
                puncte.Add(new PointF(x,y));    
            }
            List<PointF> copiePuncte = new List<PointF>(puncte);
            Cerc cercMinim = Welzl(puncte,puncteExcluse);
            PointF centru = cercMinim.Centru;
            float raza = cercMinim.Raza;
            g.DrawEllipse(new Pen(Color.Green, 4), centru.X-raza,centru.Y-raza,2*raza,2*raza);
            foreach (PointF punct in copiePuncte) {
                g.FillEllipse(Brushes.Red, punct.X-epsilon/2,punct.Y-epsilon/2,epsilon,epsilon);
            }
            g.FillEllipse(Brushes.Blue,centru.X-epsilon/2,centru.Y-epsilon/2,epsilon,epsilon);



        }

    }
}


