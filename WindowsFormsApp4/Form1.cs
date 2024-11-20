using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WindowsFormsApp4.Model;

namespace WindowsFormsApp4
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Info

        private Point ptDown, ptUp;

        private Rectangle rect = new Rectangle();

        private Color color = Color.Black;

        private List<Rectangle> rectA = new List<Rectangle>();
        private List<Color> colorA = new List<Color>();

        private List<Rectangle> rectE = new List<Rectangle>();
        private List<Color> colorE = new List<Color>();

        private List<Point> ptUpA = new List<Point>();
        private List<Point> ptDawnA = new List<Point>();
        private List<Color> colorL = new List<Color>();

        private List<Point> ptUpP = new List<Point>();
        private List<Point> ptDawnP = new List<Point>();
        private List<Color> colorP = new List<Color>();

        private Size size;

        #endregion

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int i = 0; i < rectA.Count; i++)
            {
                Pen pen = new Pen(colorA[i], 1);

                g.DrawRectangle(pen, rectA[i]);
            }
            for (int i = 0; i < rectE.Count; i++)
            {
                Pen pen = new Pen(colorE[i], 1);

                g.DrawEllipse(pen, rectE[i]);
            }
            for (int i = 0; i < ptDawnA.Count; i++)
            {
                Pen pen = new Pen(colorL[i], 1);

                g.DrawLine(pen, ptUpA[i], ptDawnA[i]);
            }
            for (int i = 0; i < ptDawnP.Count; i++)
            {
                Pen pen = new Pen(colorP[i], 1);

                g.DrawLine(pen, ptDawnP[i], ptUpP[i]);
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ptDown.X = e.X;
            ptDown.Y = e.Y;

            size.Width = 0;
            size.Height = 0;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            size.Width = e.X - ptDown.X;
            size.Height = e.Y - ptDown.Y;

            rect = new Rectangle(ptDown, size);

            ptUp = new Point(e.X, e.Y);

            switch (draw)
            {
                case 0:
                    rectA.Add(rect);
                    colorA.Add(color);
                    break;
                case 1:
                    rectE.Add(rect);
                    colorE.Add(color);
                    break;
                case 2:
                    ptUpA.Add(ptUp);
                    ptDawnA.Add(ptDown);
                    colorL.Add(color);
                    break;
                default:
                    break;
            }
            Invalidate();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                if (e.Button == MouseButtons.Left)
                {
                    Color color1 = this.BackColor;

                    switch (draw)
                    {
                        case 0:
                            rect = new Rectangle(ptDown, size);
                            g.DrawRectangle(new Pen(color1, 4), rect);
                            break;
                        case 1:
                            rect = new Rectangle(ptDown, size);
                            g.DrawEllipse(new Pen(color1, 4), rect);
                            break;
                        case 2:
                            g.DrawLine(new Pen(color1, 3), ptUp, ptDown);
                            break;
                        default:
                            MessageBox.Show("How?");
                            break;
                    }

                    size.Width = e.X - ptDown.X;
                    size.Height = e.Y - ptDown.Y;

                    Pen pen = new Pen(Color.Red, 2);
                    ptUp = new Point(e.X, e.Y);

                    switch (draw)
                    {
                        case 0:
                            rect = new Rectangle(ptDown, size);
                            g.DrawRectangle(pen, rect);
                            break;
                        case 1:
                            rect = new Rectangle(ptDown, size);
                            g.DrawEllipse(pen, rect);
                            break;
                        case 2:
                            g.DrawLine(pen, ptDown, ptUp);
                            break;
                        case 3:
                            ptUpP.Add(ptUp);
                            ptDawnP.Add(ptDown);
                            colorP.Add(color);
                            g.DrawLine(pen, ptDown, ptUp);
                            ptDown = ptUp;
                            break;
                        default:
                            MessageBox.Show("How?");
                            break;
                    }
                }
            }
        }
        private int draw = 0;
        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw = 0;
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw = 1;
        }

        private void penToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw = 3;
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw = 2;
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = Color.Red;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = Color.Blue;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = Color.Green;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rectA.Clear();
            colorA.Clear();

            rectE.Clear();
            colorE.Clear();

            ptUpA.Clear();
            ptDawnA.Clear();
            colorL.Clear();

            ptUpP.Clear();
            ptDawnP.Clear();
            colorP.Clear();

            Invalidate();
        }
        private void colorDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog1.Color;
            }
        }
        public Worker w = new Worker();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                w.colorA.Clear();
                w.rectA.Clear();

                w.rectE.Clear();
                w.colorE.Clear();

                w.ptUpA.Clear();
                w.ptDawnA.Clear();
                w.colorL.Clear();

                w.ptUpP.Clear();
                w.ptDawnP.Clear();
                w.colorP.Clear();

                FileStream fs = new FileStream("1.txt", FileMode.Create, FileAccess.Write);

                IFormatter bf = new BinaryFormatter();

                for (int i = 0; i != rectA.Count; i++)
                {
                    w.rectA.Add(rectA[i]);
                    w.colorA.Add(colorA[i]);
                }
                for (int i = 0; i != rectE.Count; i++)
                {
                    w.rectE.Add(rectE[i]);
                    w.colorE.Add(colorE[i]);
                }
                for (int i = 0; i != ptUpA.Count; i++)
                {
                    w.ptUpA.Add(ptUpA[i]);
                    w.ptDawnA.Add(ptDawnA[i]);
                    w.colorL.Add(colorL[i]);
                }
                for (int i = 0; i != ptDawnP.Count; i++)
                {
                    w.ptUpP.Add(ptUpP[i]);
                    w.ptDawnP.Add(ptDawnP[i]);
                    w.colorP.Add(colorP[i]);
                }

                bf.Serialize(fs, w);

                fs.Close();
            }
            catch
            {
                Application.Exit();
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream("1.txt", FileMode.Open, FileAccess.Read);

                IFormatter bf = new BinaryFormatter();

                w = (Worker)bf.Deserialize(fs);

                colorA.Clear();
                rectA.Clear();

                rectE.Clear();
                colorE.Clear();

                ptUpA.Clear();
                ptDawnA.Clear();
                colorL.Clear();

                ptUpP.Clear();
                ptDawnP.Clear();
                colorP.Clear();

                for (int i = 0; i != w.rectA.Count; i++)
                {
                    rectA.Add(w.rectA[i]);
                    colorA.Add(w.colorA[i]);
                }
                for (int i = 0; i != w.rectE.Count; i++)
                {
                    rectE.Add(w.rectE[i]);
                    colorE.Add(w.colorE[i]);
                }
                for (int i = 0; i != w.ptUpA.Count; i++)
                {
                    ptUpA.Add(w.ptUpA[i]);
                    ptDawnA.Add(w.ptDawnA[i]);
                    colorL.Add(w.colorL[i]);
                }
                for (int i = 0; i != w.ptDawnP.Count; i++)
                {
                    ptUpP.Add(w.ptUpP[i]);
                    ptDawnP.Add(w.ptDawnP[i]);
                    colorP.Add(w.colorP[i]);
                }
                fs.Close();

                Invalidate();
            }
            catch
            {
                Application.Exit();
            }
        }
    }
}
