using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace probleme1
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen redPen = new Pen(Color.Red, 3);
            Pen bluePen = new Pen(Color.Blue, 3);
            Random rnd = new Random();

            //problema1:Se da o multime de puncte in plan.Sa se determine dreptunghiul de arie minima
            //care sa contina toate punctele in interior.
            /*
            int n = rnd.Next(4, 100);
            float[] x = new float[n];
            float[] y = new float[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = rnd.Next(10, 500);
                y[i] = rnd.Next(10, 200);
            }
            float epsilon = 10;
            for (int i = 0; i < n; i++)
            {
                g.FillEllipse(Brushes.Green, x[i], y[i], epsilon, epsilon);
            }
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            for (int i = 0; i < n; i++)
            {
                if (x[i] > maxX) { maxX = x[i]; }
                if (x[i] < minX) { minX = x[i]; }
                if (y[i] > maxY) { maxY = y[i]; }
                if (y[i] < minY) { minY = y[i]; }
            }
            g.DrawRectangle(bluePen, minX, minY, maxX - minX + 2 * epsilon, maxY - minY + 2 * epsilon);

            */
            
            //problema2:Se dau doua multimi de puncte in plan.Pentru fiecare punct din prima multime 
            //sa se gaseasca cel mai apropiat punct din ceea de a doua multime.
          
            /*int padding = 20;
            int n1 = rnd.Next(200,300);
            int n2 = rnd.Next(50,60);
            float[] x1 = new float[n1];
            float[] x2 = new float[n2];
            float[] y1 = new float[n1];
            float[] y2 = new float[n2];
            for (int i = 0; i < n1; i++) {
                x1[i] = rnd.Next(padding,ClientSize.Width-padding);
                y1[i] = rnd.Next(padding,ClientSize.Height-padding);
            }
            for (int i = 0; i < n2; i++) {
                x2[i] = rnd.Next(padding,ClientSize.Width-padding);
                y2[i] = rnd.Next(padding,ClientSize.Height-padding);
            }
            float epsilon = 10;
            for (int i = 0; i < n1; i++) {
                g.FillEllipse(Brushes.Red, x1[i], y1[i],epsilon,epsilon);
            }
            for (int i = 0; i < n2; i++) {
                g.FillEllipse(Brushes.Blue, x2[i], y2[i],epsilon,epsilon);
            }
            for (int i = 0; i < n1; i++) {
                float minDist = float.MaxValue;
                float closestX = x1[i], closestY = y1[i];
                for (int j = 0; j < n2; j++) {
                    float distance = (float)Math.Sqrt(Math.Pow(x1[i] - x2[j], 2) + Math.Pow(y1[i] - y2[j],2));
                    if (distance < minDist) { minDist = distance; closestX = x2[j]; closestY = y2[j]; }
                }
                g.DrawLine(new Pen(Color.Green, 3), x1[i], y1[i], closestX, closestY);
            }
            */
            //problema3
            //Se dau n puncte in plan.pentru un punct dat q sa se determine cercul cu centrul in q si de raza
            //care sa nu contina nici un punct din multimea data
            
                        int n = rnd.Next(40,50);
                        float[] xPoints = new float[n];
                        float[] yPoints = new float[n];
                        float padding = 10;
                        float epsilon = 5;
                        for (int i = 0; i < n; i++) {
                            xPoints[i] = rnd.Next((int)padding, ClientSize.Width - (int)padding);
                            yPoints[i] = rnd.Next((int)padding, ClientSize.Height - (int)padding);
                            g.FillEllipse(Brushes.Red, xPoints[i]-epsilon/2, yPoints[i]-epsilon/2,epsilon,epsilon);
                        }
                        float[] q = new float[2];
                        q[0] = rnd.Next((int)padding,ClientSize.Width-(int)padding);
                        q[1] = rnd.Next((int)padding,ClientSize.Height-(int)padding);
                        g.FillEllipse(Brushes.Blue, q[0]-epsilon, q[1]-epsilon,2*epsilon,2*epsilon);
                        float minRaza = Math.Abs((float)Math.Sqrt(Math.Pow(xPoints[0] - q[0], 2) + Math.Pow(yPoints[0] - q[1],2)));
                        for (int i = 1; i < n; i++) {
                            float raza = (float)Math.Sqrt(Math.Pow(xPoints[i] - q[0], 2) + Math.Pow(yPoints[i] - q[1],2));
                            if (raza < minRaza) { minRaza = raza; }
                        }
                        minRaza -= 2*epsilon;
                        g.DrawEllipse(bluePen, q[0]-minRaza, q[1]-minRaza,2*minRaza,2*minRaza);
                        
        }
    }
}
