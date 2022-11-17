using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snowFALL
{
    public partial class Form1 : Form
    {
        private readonly Bitmap snowImage;
        private readonly Bitmap backgroundImage;
        Bitmap scene;
        private readonly IList<Snow> snowList;
        private readonly Timer timer;
        private Graphics graph;
        public Form1()
        {
            InitializeComponent();
            snowList = new List<Snow>();
            backgroundImage = (Bitmap)Properties.Resources.background;
            snowImage = (Bitmap)Properties.Resources.snow;
            CreateSnow();
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            scene = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            graph = Graphics.FromImage(scene);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
        private void DrawScen()
        {
            graph.DrawImage(backgroundImage, 0, 0);

            foreach (var snow in snowList)
            {
                if (snow.Y > 0)
                {
                    graph.DrawImage(snowImage,
                       new Rectangle(
                     snow.X,
                     snow.Y,
                     snow.Size,
                     snow.Size));
                }
            }
            var control = CreateGraphics();
            control.DrawImage(scene, 0, 0);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach (var snow in snowList)
            {
                snow.Y += snow.Size;
                if (snow.Y > ClientRectangle.Height)
                {
                    snow.Y = -snow.Size;
                }
            }
            DrawScen();
            timer.Start();
        }
        private void CreateSnow()
        {
            var rand = new Random();
            for (var i = 0; i < 200; i++)
            {
                snowList.Add(new Snow
                {
                    X = rand.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    Y = -rand.Next(Screen.PrimaryScreen.WorkingArea.Height),
                    Size = rand.Next(1, 30)
                });
            }
        }
    }
}
