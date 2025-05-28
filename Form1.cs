using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _7.Drawing_of_independent_functions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int n = 50, i, j;
        int yes_1, yes_2, yes_3;
        double Z_3 = 1, a = -25, b = 25, c = -25, d = 25, xmax, xmin, ymax, ymin, mx, my, x0, y0;

        

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        double G_Z_1, G_Z_2;
        int counter = 0;
        

        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox3.Text = "0";
            textBox1.Text = "1"; //начальное
            textBox2.Text = "100"; //конечное
            textBox4.Text = "0";
            textBox5.Text = Convert.ToString(n);
        }

        double F_z(double x, double y)
        {
            //return Math.Sin(x) * Math.Cos(y) * Math.Cos(x);
            //return x * x + y * y;
            return Math.Sin(x) + Math.Cos(y);
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (G_Z_1 < G_Z_2)
            {
                G_Z_1 += 0.05;
                Z_3 = G_Z_1;
                Go_draw();
            } else
            {
                timer1.Enabled = false;
            }
            counter++;
            textBox3.Text = Convert.ToString(counter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            counter = 0;
            textBox3.Text = "0";
            pictureBox1.Image = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            G_Z_1 = Convert.ToDouble(textBox1.Text);
            G_Z_2 = Convert.ToDouble(textBox2.Text);
            timer1.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double temp_draw;
            G_Z_1 = Convert.ToDouble(textBox1.Text);
            G_Z_2 = Convert.ToDouble(textBox2.Text);
            temp_draw = Convert.ToDouble(textBox4.Text);
            if (temp_draw < G_Z_1) { temp_draw = G_Z_1 + (G_Z_2 - G_Z_1) / 2; textBox4.Text = Convert.ToString(temp_draw); }
            Z_3 = G_Z_1;
            n = Convert.ToInt32(textBox5.Text);
            double[,] T1 = new double[n, n];
            double[,] T2 = new double[n, n];
            double CNT, dt = 0.01, dx = 0.1, dy = 0.1, λ = 1.0;
            double kt = 1.1;
            double[] x = new double[n];
            double[] y = new double[n];

            double temperature_control(double cNt/*, int step*/)
            {
                double ret = Math.Cos(cNt);
                //if (((cNt % step) % 2) == 0) { ret = G_Z_2 / 8; } else { ret = G_Z_1; }
                //if (((cNt % step) % 2) == 0) { ret = 10; } else { ret = -10; }
                for (int s = 1; s < 500; s++)
                {
                    ret *= Math.Cos(cNt);
                }
                return ret;
            }
            /*
            for (j = 0; j < n; j++)
            {
                for (i = 0; i < n; i++)
                {
                    T1[i, j] = G_Z_1;
                }
            }*/
            CNT = dt;
            for (int cnt = 0; cnt < (G_Z_2 - G_Z_1) / (CNT); cnt++)
            {
                for (j = 1; j < n - 1; j++)
                {
                    for (i = 1; i < n - 1; i++)
                    {
                        
                        T2[i, j] = T1[i,j] + dt * ((T1[i - 1, j] - 2 * T1[i, j] + T1[i + 1, j]) / dx + (T1[i, j - 1] - 2 * T1[i, j] + T1[i, j + 1]) / dy) * λ;
                    }
                    T2[i, j] += temperature_control(cnt); // += dt * kt; ////нагреваем
                }
                for (j = 0; j < n; j++)
                {
                    for (i = 0; i < n; i++)
                    {
                        T1[i, j] = T2[i, j];
                    }
                }
            }


            Bitmap btmp; btmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox2.Image = btmp; Graphics gr = Graphics.FromImage(pictureBox2.Image);
            Pen p1 = new Pen(Color.Red, 5);
            Pen p2 = new Pen(Color.Blue, 5);
            Pen p3 = new Pen(Color.Orange, 5);


            for (i = 0; i < n; i++)
            {
                x[i] = i;
                for (j = 0; j < n; j++)
                    y[j] = j;
            }

            xmax = x[0];
            xmin = x[0];
            ymax = y[0];
            ymin = y[0];
            for (i = 0; i < n; i++)
            {
                if (x[i] > xmax) xmax = x[i]; if (x[i] < xmin) xmin = x[i]; if (y[i] > ymax) ymax = y[i]; if (y[i] < ymin) ymin = y[i];
            }

            //коэффициенты масштаба
            mx = pictureBox2.Width / (xmax - xmin);
            my = pictureBox2.Height / (ymax - ymin);

            
            Z_3 = G_Z_1;
            for (i = 0; i < n - 1; i++)
            {
                for (j = 0; j < n - 1; j++)
                {
                    double z_x0_y0, z_x1_y0, z_x0_y1, z_x1_y1;

                    int[] X_3 = new int[2];
                    int[] Y_3 = new int[2];

                    z_x0_y0 = T1[i, j];
                    z_x1_y0 = T1[i + 1, j];
                    z_x0_y1 = T1[i, j + 1];
                    z_x1_y1 = T1[i + 1, j + 1];

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 <= Z_3) && (z_x0_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x0_y1 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x0_y1 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y1 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p2, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p2, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p2, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 <= Z_3) && (z_x1_y0 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y0 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x1_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x1_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p2, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p2, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p2, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                }
            }
            Z_3 = G_Z_2 - 1;
            for (i = 0; i < n - 1; i++)
            {
                for (j = 0; j < n - 1; j++)
                {
                    double z_x0_y0, z_x1_y0, z_x0_y1, z_x1_y1;

                    int[] X_3 = new int[2];
                    int[] Y_3 = new int[2];

                    z_x0_y0 = T1[i, j];
                    z_x1_y0 = T1[i + 1, j];
                    z_x0_y1 = T1[i, j + 1];
                    z_x1_y1 = T1[i + 1, j + 1];

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 <= Z_3) && (z_x0_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x0_y1 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x0_y1 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y1 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 <= Z_3) && (z_x1_y0 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y0 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x1_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x1_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                }
            }
            Z_3 = temp_draw;
            for (i = 0; i < n - 1; i++)
            {
                for (j = 0; j < n - 1; j++)
                {
                    double z_x0_y0, z_x1_y0, z_x0_y1, z_x1_y1;

                    int[] X_3 = new int[2];
                    int[] Y_3 = new int[2];

                    z_x0_y0 = T1[i, j];
                    z_x1_y0 = T1[i + 1, j];
                    z_x0_y1 = T1[i, j + 1];
                    z_x1_y1 = T1[i + 1, j + 1];

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 <= Z_3) && (z_x0_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x0_y1 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x0_y1 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y1 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p3, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p3, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p3, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 <= Z_3) && (z_x1_y0 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y0 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x1_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x1_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p3, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p3, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p3, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                }
            }

            /*
            x0 = mx * (-xmin);
            y0 = my * (ymax);
            gr.DrawLine(p6, 0, (int)y0, pictureBox1.Width, (int)y0);
            gr.DrawLine(p6, (int)x0, 0, (int)x0, pictureBox1.Height);*/
        }

        private void button6_Click(object sender, EventArgs e)
        {
            G_Z_1 = Convert.ToDouble(textBox1.Text);
            G_Z_2 = Convert.ToDouble(textBox2.Text);
            Z_3 = Convert.ToDouble(G_Z_1);
            n = Convert.ToInt32(textBox5.Text);

            int k = 3;

            double[] x = new double[n];
            double[] y = new double[n];

            Bitmap btmp; btmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox2.Image = btmp; Graphics gr = Graphics.FromImage(pictureBox2.Image);
            Pen p1 = new Pen(Color.Blue, 5);
            Pen p2 = new Pen(Color.Black, 3);

            for (i = 0; i < n; i++)
            {
                x[i] = a + i * (b - a) / n;
                for (j = 0; j < n; j++)
                    y[j] = c + j * (d - c) / n;
            }

            xmax = x[0];
            xmin = x[0];
            ymax = y[0];
            ymin = y[0];
            for (i = 0; i < n; i++)
            {
                if (x[i] > xmax) xmax = x[i]; if (x[i] < xmin) xmin = x[i]; if (y[i] > ymax) ymax = y[i]; if (y[i] < ymin) ymin = y[i];
            }

            mx = pictureBox1.Width / (xmax - xmin);
            my = pictureBox1.Height / (ymax - ymin);

            for (double cnt = (G_Z_1 * k); cnt < (G_Z_2 * k); cnt++)
            {
                Z_3 = (cnt / k);
                for (i = 0; i < n - 1; i++)
                {
                    for (j = 0; j < n - 1; j++)
                    {
                        double z_x0_y0, z_x1_y0, z_x0_y1, z_x1_y1;

                        int[] X_3 = new int[2];
                        int[] Y_3 = new int[2];

                        z_x0_y0 = F_z(x[i], y[j]);
                        z_x1_y0 = F_z(x[i + 1], y[j]);
                        z_x0_y1 = F_z(x[i], y[j + 1]);
                        z_x1_y1 = F_z(x[i + 1], y[j + 1]);

                        yes_1 = 0; yes_2 = 0; yes_3 = 0;
                        if (((z_x0_y0 <= Z_3) && (z_x0_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x0_y1 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                        if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                        if (((z_x0_y1 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y1 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                        if ((yes_1 + yes_2) == 2)
                        {
                            X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (x[i] - x[i]) + x[i]) - xmin));
                            Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                            X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                            Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                            gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                        }
                        if ((yes_2 + yes_3) == 2)
                        {
                            X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                            Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                            X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                            Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                            gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                        }
                        if ((yes_1 + yes_3) == 2)
                        {
                            X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (x[i] - x[i]) + x[i]) - xmin));
                            Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                            X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                            Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                            gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                        }

                        yes_1 = 0; yes_2 = 0; yes_3 = 0;
                        if (((z_x0_y0 <= Z_3) && (z_x1_y0 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y0 <= Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                        if (((z_x0_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x0_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                        if (((z_x1_y0 <= Z_3) && (z_x1_y1 >= Z_3)) || ((z_x1_y0 >= Z_3) && (z_x1_y1 <= Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                        if ((yes_1 + yes_2) == 2)
                        {
                            X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                            Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (y[j] - y[j]) + y[j])));
                            X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                            Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                            gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                        }
                        if ((yes_2 + yes_3) == 2)
                        {
                            X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                            Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                            X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                            Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                            gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                        }
                        if ((yes_1 + yes_3) == 2)
                        {
                            X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                            Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (y[j] - y[j]) + y[j])));
                            X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                            Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (y[j + 1] - y[j]) + y[j])));
                            gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                        }
                    }
                }

            }

            x0 = mx * (-xmin);
            y0 = my * (ymax);
            gr.DrawLine(p2, 0, (int)y0, pictureBox1.Width, (int)y0);
            gr.DrawLine(p2, (int)x0, 0, (int)x0, pictureBox1.Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Z_3 = Convert.ToDouble(textBox4.Text);
            Go_draw();
        }

        /*void Draw_line()
        {

        }*/

        void Go_draw()
        {
            textBox4.Text = Convert.ToString(Z_3);
            n = Convert.ToInt32(textBox5.Text);

            double[] x = new double[n];
            double[] y = new double[n];

            Bitmap btmp; btmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = btmp; Graphics gr = Graphics.FromImage(pictureBox1.Image);
            Pen p1 = new Pen(Color.BlueViolet, 5);
            Pen p2 = new Pen(Color.Black, 3);

            for (i = 0; i < n; i++)
            {
                x[i] = a + i * (b - a) / n;
                for (j = 0; j < n; j++)
                    y[j] = c + j * (d - c) / n;
            }

            xmax = x[0];
            xmin = x[0];
            ymax = y[0];
            ymin = y[0];
            for (i = 0; i < n; i++)
            {
                if (x[i] > xmax)
                    xmax = x[i];
                if (x[i] < xmin)
                    xmin = x[i];

                if (y[i] > ymax)
                    ymax = y[i];
                if (y[i] < ymin)
                    ymin = y[i];
            }

            mx = pictureBox1.Width / (xmax - xmin);
            my = pictureBox1.Height / (ymax - ymin);

            for (i = 0; i < n - 1; i++)
            {
                for (j = 0; j < n - 1; j++)
                {
                    double z_x0_y0, z_x1_y0, z_x0_y1, z_x1_y1;

                    int[] X_3 = new int[2];
                    int[] Y_3 = new int[2];

                    z_x0_y0 = F_z(x[i], y[j]);
                    z_x1_y0 = F_z(x[i + 1], y[j]);
                    z_x0_y1 = F_z(x[i], y[j + 1]);
                    z_x1_y1 = F_z(x[i + 1], y[j + 1]);

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 < Z_3) && (z_x0_y1 > Z_3)) || ((z_x0_y0 > Z_3) && (z_x0_y1 < Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 < Z_3) && (z_x1_y1 > Z_3)) || ((z_x0_y0 > Z_3) && (z_x1_y1 < Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x0_y1 < Z_3) && (z_x1_y1 > Z_3)) || ((z_x0_y1 > Z_3) && (z_x1_y1 < Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x0_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y1 - z_x1_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (x[i] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x0_y0 - z_x0_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y1) / (z_x1_y1 - z_x0_y1)) * (y[j + 1] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }

                    yes_1 = 0; yes_2 = 0; yes_3 = 0;
                    if (((z_x0_y0 < Z_3) && (z_x1_y0 > Z_3)) || ((z_x0_y0 > Z_3) && (z_x1_y0 < Z_3))) { yes_1 = 1; } else { yes_1 = 0; };
                    if (((z_x0_y0 < Z_3) && (z_x1_y1 > Z_3)) || ((z_x0_y0 > Z_3) && (z_x1_y1 < Z_3))) { yes_2 = 1; } else { yes_2 = 0; };
                    if (((z_x1_y0 < Z_3) && (z_x1_y1 > Z_3)) || ((z_x1_y0 > Z_3) && (z_x1_y1 < Z_3))) { yes_3 = 1; } else { yes_3 = 0; };

                    if ((yes_1 + yes_2) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y0 - z_x0_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (x[i + 1] - x[i]) + x[i]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x0_y0) / (z_x1_y1 - z_x0_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_2 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x0_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y1) / (z_x1_y0 - z_x1_y1)) * (y[j] - y[j + 1]) + y[j + 1])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                    if ((yes_1 + yes_3) == 2)
                    {
                        X_3[0] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (x[i] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[0] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x0_y0 - z_x1_y0)) * (y[j] - y[j]) + y[j])));
                        X_3[1] = Convert.ToInt32(mx * ((((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (x[i + 1] - x[i + 1]) + x[i + 1]) - xmin));
                        Y_3[1] = Convert.ToInt32(my * (ymax - (((Z_3 - z_x1_y0) / (z_x1_y1 - z_x1_y0)) * (y[j + 1] - y[j]) + y[j])));
                        gr.DrawLine(p1, X_3[0], Y_3[0], X_3[1], Y_3[1]);
                    }
                }
            }
            x0 = mx * (-xmin);
            y0 = my * (ymax);
            gr.DrawLine(p2, 0, (int)y0, pictureBox1.Width, (int)y0);
            gr.DrawLine(p2, (int)x0, 0, (int)x0, pictureBox1.Height);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
