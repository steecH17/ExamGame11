using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamGame11
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        List<Obstacle> obstacles = new List<Obstacle>();
        Random random = new Random();
        bool generated = true;
        Player player = new Player();
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            bmp = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
            Graphics canvas = Graphics.FromImage(bmp);
            SolidBrush brush = new SolidBrush(Color.Black);

            foreach (Obstacle obstacle in obstacles)
            {
                canvas.FillRectangle(brush, obstacle.x, obstacle.y, obstacle.width, obstacle.height);
            }

            canvas.FillRectangle(player.brush, player.x, player.y, player.width, player.height);
            

            e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(generated)
            {
                GeneratedObstacle();
            }
            foreach(Obstacle obstacle in obstacles)
            {
                if ((obstacle.y + 20) == player.y && obstacle.x == player.x)
                {
                    GameOver();
                    panel1.Invalidate();
                    return;
                }
                else
                {
                    obstacle.MoveParticle();
                    if (obstacle.y >= panel1.ClientSize.Height)
                    {
                        obstacle.y = 0;
                        obstacle.x = 20 * random.Next(0, 25);
                        generated = false;
                    }
                }
            }
            panel1.Invalidate();
        }
        
        private void GeneratedObstacle()
        {
            for(int i=0; i <= 3; i++)
            {
                Obstacle newObstacle = new Obstacle();
                obstacles.Add(newObstacle);
                
            }
        }

        private bool PlayerCrash(int y, int x)
        {
            foreach(Obstacle obstacle in obstacles)
            {
                if (obstacle.y == y && obstacle.x == x) return true;
            }
            return false;
        }
        private void GameOver()
        {
            timer.Stop();
            MessageBox.Show("Игра закончена вы проиграли!!!");
            obstacles.Clear();
            player = new Player();
            generated = true;
            panel1.Invalidate();
        }

        private void buttonStopGame_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A && player.x - 20 >= 0)
            {
                if (!PlayerCrash(player.y, player.x - 20)) player.x -= 20;
                //else GameOver();
            }
            else if (e.KeyCode == Keys.D && player.x + 20 <= 500)
            {
                if (!PlayerCrash(player.y, player.x + 20)) player.x += 20;
                //else GameOver();
            }
            else if (e.KeyCode == Keys.W && player.y - 20 >= 0)
            {
                if (!PlayerCrash(player.y - 20, player.x)) player.y -= 20;
                else GameOver();
                
            }
            else if (e.KeyCode == Keys.S && player.y + 20 < panel1.ClientSize.Height - 20)
            {
                if (!PlayerCrash(player.y + 20, player.x)) player.y += 20;
                else GameOver();
            }
            panel1.Invalidate();
        }
    }
}
