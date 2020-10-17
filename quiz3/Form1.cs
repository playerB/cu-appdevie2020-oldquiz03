using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quiz3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double counttime = 0;
        Point current = new Point();
        Point clickname = new Point(300,400);
        Point clicklogo = new Point(300,400);
        Point clickad = new Point(300,400);
        Bitmap logopic;
        String namebox, addressbox;
        int editMode = 0;

        private void button1_Click(object sender, EventArgs e) //name
        {
            this.BackColor = Color.Yellow;
            editMode = 1;
            namebox = textBox1.Text;
            counttime = 0;
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e) //logo
        {
            this.BackColor = Color.Yellow;
            editMode = 2;
            counttime = 0;
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e) //address
        {
            this.BackColor = Color.Yellow;
            editMode = 3;
            addressbox = textBox3.Text;
            counttime = 0;
            timer1.Start();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            current.X = e.X;
            current.Y = e.Y;
            Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (editMode == 1)
            {
                clickname.X = e.X;
                clickname.Y = e.Y;
                clickInPicbox();
            }
            else if (editMode == 2)
            {
                clicklogo.X = e.X;
                clicklogo.Y = e.Y;
                clickInPicbox();
            }
            else if (editMode == 3)
            {
                clickad.X = e.X;
                clickad.Y = e.Y;
                clickInPicbox();
            }
            Refresh();
        }

        private void clickInPicbox()
        {
            timer1.Stop();
            counttime = 0;
            label1.Text = "not in editmode";
            editMode = 0;
            this.BackColor = DefaultBackColor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "editmode ends in " + ((100 - counttime) / 10).ToString() + " s";
            counttime += 1;
            if (counttime >= 100)
            {
                timer1.Stop();
                editMode = 0;
                this.BackColor = Color.White;
                counttime = 0;
                label1.Text = "not in editmode";
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawString(namebox, DefaultFont, Brushes.Black, clickname);
            if (logopic != null) g.DrawImage(logopic, clicklogo);
            g.DrawString(addressbox, DefaultFont, Brushes.Black, clickad);
            if (editMode == 1)
            {
                g.DrawString(namebox, DefaultFont, Brushes.Black, current);
            }
            else if (editMode == 2 && logopic != null)
            {
                g.DrawImage(logopic, current);
            }
            else if (editMode == 3)
            {
                g.DrawString(addressbox, DefaultFont, Brushes.Black, current);
            }

        }

        private void button6_Click(object sender, EventArgs e) //logo choosefile
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK) return;
            Bitmap bm = new Bitmap(openFileDialog2.FileName);
            Bitmap bm2 = new Bitmap(openFileDialog2.FileName);
            for (int i = 0; i<bm.Size.Width; i++)
                for (int j = 0; j < bm.Size.Height; j++)
                {
                    int r = bm.GetPixel(i, j).R;
                    int g = bm.GetPixel(i, j).G;
                    int b = bm.GetPixel(i, j).B;
                    int grey = (r + g + b) / 3;
                    bm2.SetPixel(i, j, Color.FromArgb(grey, grey, grey));
                }
            logopic = bm2;
            textBox2.Text = openFileDialog2.FileName;
        }

        private void button4_Click(object sender, EventArgs e) //save
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
            sw.WriteLine("{0},{1},{2}", clickname.X, clickname.Y, namebox);
            sw.WriteLine("{0},{1},{2}", clickad.X, clickad.Y, addressbox);
            sw.Close();
        }

        private void button5_Click(object sender, EventArgs e) //open
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            clickname = new Point(300, 400);
            clickad = new Point(300, 400);
            StreamReader sr = new StreamReader(openFileDialog1.FileName);
            String[] name = sr.ReadLine().Split(',');
            clickname.X = Convert.ToInt32(name[0]);
            clickname.Y = Convert.ToInt32(name[1]);
            namebox = name[2];
            textBox1.Text = name[2];
            String[] address = sr.ReadToEnd().Split(',');
            clickad.X = Convert.ToInt32(address[0]);
            clickad.Y = Convert.ToInt32(address[1]);
            addressbox = address[2];
            textBox3.Text = address[2];
            Refresh();
        }
    }
}
