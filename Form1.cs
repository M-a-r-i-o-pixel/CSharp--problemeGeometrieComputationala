using System.Collections.Generic;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace setProbleme3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public Random rnd = new Random();
        public int padding = 20;
        int epsilon = 8;
        private float Distanta(PointF a, PointF b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //problema 1:
            /*cerinta:
             ------2n puncte in plan
            -----sa se uneasca doua cate doua a.i. suma lungimilor
             segmentelor obtinute sa fie minima
            (problema de cuplaj)
             */


            /*am folosit o strategie greedy,gasind lungimea minima pentru cuplajul unui punct cu unul din toate celelalte puncte,
             * si facand asta pentru toate punctele,
             * e logic ca suma cuplajelor se minimizeaza pentu ca termenii sumei,adica lungimile cuplajelor individuale,
             * se aleg minimi
             * 
             */

            //rezolvare grosiera:
            /*
            int n = 2 * rnd.Next(10,50);
            List<PointF> puncte = new List<PointF>();
            Dictionary<PointF, PointF> cuplaje = new Dictionary<PointF,PointF>();
            for (int i=0;i<n;i++) {
                puncte.Add(new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding)));
                
            }
            
            int l = puncte.Count;
            for (int i=0;i<l-1;i++) {
                float lungimeMinima = Distanta(puncte[i], puncte[i+1]);
                int eliminateJ = i + 1;
                for (int j=i+2;j<l;j++) {
                    float lungimeCurenta = Distanta(puncte[i], puncte[j]);
                    if (lungimeCurenta < lungimeMinima) {
                        lungimeMinima = lungimeCurenta;
                        eliminateJ = j;
                    }
                }
                cuplaje[puncte[i]]= puncte[eliminateJ];

                puncte.RemoveAt(eliminateJ);
                puncte.RemoveAt(i);

                l -= 2;
            }
            Pen greenPen = new Pen(Color.Green, 3);
            foreach (KeyValuePair<PointF, PointF> kvp in cuplaje) {
                PointF capat1 = kvp.Key;
                PointF capat2 = kvp.Value;
                g.DrawLine(greenPen,capat1,capat2);
                g.FillEllipse(Brushes.Red,capat1.X-epsilon/2,capat1.Y-epsilon/2,epsilon,epsilon);
                g.FillEllipse(Brushes.Red,capat2.X-epsilon/2,capat2.Y-epsilon/2,epsilon,epsilon);
            }
            */

            



            




            //problema 2

            /*cerinta:
             * o multime de S de segmente de dreapta inchise
             * se cer toate intersectiile dintre segmentele din s
             * 
             * 
             */

            //rezolvare ineficienta:

            /*
            int n = rnd.Next(2,50);//numar de segmente
            PointF[,] segmente= new PointF[n, 2];
            List<PointF> puncteIntersectie = new List<PointF>();
            for (int i=0;i<n;i++) {
                PointF punct1 = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
                PointF punct2 = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
                while (punct1.Equals(punct2)) {
                    punct2 = new PointF(rnd.Next(padding,ClientSize.Width-padding),rnd.Next(padding,ClientSize.Height-padding));
                }
                segmente[i, 0] = punct1; segmente[i, 1] = punct2;
                float x1 = punct1.X, x2 = punct2.X;
                float y1 = punct1.Y, y2 = punct2.Y;
                g.FillEllipse(Brushes.Red, x1 - epsilon / 2, y1 - epsilon / 2, epsilon,epsilon);
                g.FillEllipse(Brushes.Red,x2-epsilon/2,y2-epsilon/2,epsilon,epsilon);
                g.DrawLine(new Pen(Color.Black,4),x1,y1,x2,y2);

            }
            for (int i=0;i<n-1;i++) {
                for (int j=i+1;j<n;j++) {
                    PointF punct1 = segmente[i, 0];
                    PointF punct2 = segmente[i, 1];
                    PointF punct3 = segmente[j, 0];
                    PointF punct4 = segmente[j, 1];
                    float x1=punct1.X, x2=punct2.X, x3=punct3.X, x4=punct4.X;
                    float y1=punct1.Y, y2=punct2.Y, y3=punct3.Y, y4=punct4.Y;
                    float r1 = (y2 - y1) / (x2-x1);
                    float r2 = (y4-y3) / (x4-x3);
                    float x = (y3-y1+r1*x1-r2*x3) / (r1-r2);
                    float y = r1 * x - r1 * x1 + y1;
                    bool punctDeIntersectie = true;
                    if (Math.Min(x1, x2) <= x && x <= Math.Max(x1, x2)) {; } else { punctDeIntersectie = false; }
                    if (Math.Min(x3, x4) <= x && x <= Math.Max(x3, x4))
                    {
                        ;
                    }
                    else { punctDeIntersectie = false; }
                    if (Math.Min(y1,y2) <= y && y <= Math.Max(y1,y2)) {; } else { punctDeIntersectie = false; }
                    if (Math.Min(y3,y4) <= y && y <= Math.Max(y3,y4)) {; } else { punctDeIntersectie = false; }
                    if (punctDeIntersectie) {
                        puncteIntersectie.Add(new PointF(x,y));
                    }

                    
                    
                    
                    

                }
            }

            for (int i=0;i<puncteIntersectie.Count;i++) {
                float x = puncteIntersectie[i].X;
                float y = puncteIntersectie[i].Y;
                g.FillEllipse(Brushes.Blue,x-epsilon/2,y-epsilon/2,epsilon,epsilon);
            }
            */
        }
    }
}
