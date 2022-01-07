using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinSmallGames.models;

namespace WinSmallGames
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Gamers> listgamers = new List<Gamers>();
        int increment = 8;
        int startGames = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Lines lines1 = new Lines();
            lines1.sort = null;
            lines1.GenLines();
            DrawLines(lines1, 1);
            Lines lines2 = new Lines();
            lines2.sort = null;
            lines2.GenLines();
            DrawLines(lines2, 2);

            Gamers gamers1 = new Gamers(lines1.linelist, "玩家1");
            gamers1.selected = 1;
            DrawGamers(gamers1, gamers1.selectedLineIndex, 1);
            listgamers.Add(gamers1);
            
            Gamers gamers2 = new Gamers(lines2.linelist, "玩家2");
            gamers2.selected = 0;
            DrawGamers(gamers2, gamers2.selectedLineIndex, 2);
            listgamers.Add(gamers2);

        }

        void DrawLines(Lines list, int th)
        {
            for (int i = 0; i < list.linelist.Count; i++)
            {
                for (int j = 0; j < list.linelist[i].count; j++)
                {
                    int x = this.Width / 4 + 2 * j * increment;
                    int y = this.Height / 4 + (th - 1) * increment*9 + 2 * i * increment;
                    PictureBox pic = new PictureBox();
                    pic.BackColor = Color.Black;
                    pic.Width = increment;
                    pic.Height = increment;
                    pic.Location = new Point(x, y);
                    
                    this.Controls.Add(pic);
                }
            }
        }


        void DrawGamers(Gamers gamers, int lineIndex, int th)
        {
            int gamerx = this.Width / 4 - increment*4;
            int gamery = this.Height / 4 + (th - 1) * increment*9 + 2 * lineIndex * increment;
            gamers.pic.BackColor = Color.Red;
            gamers.pic.Width = increment;
            gamers.pic.Height = increment;
            gamers.pic.Location = new Point(gamerx, gamery);
            gamers.pic.MouseDown += Pic_MouseDown;
            this.Controls.Add(gamers.pic);
            gamers.x = gamerx;
            gamers.y = gamery;
        }

        private void Pic_MouseDown(object sender, MouseEventArgs e)
        {
            listgamers.ForEach(r =>
            {
                PictureBox pic = (PictureBox)sender;
                int x = pic.Location.X;
                int y = pic.Location.Y;
                if (x>= r.x && x <= r.x + increment * 2 && y >= r.y && y <= r.y + increment * 2)
                {
                    r.selected = 1;
                }
                else
                {
                    r.selected = 0;
                }
            });
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(startGames==0)
            {
                Gamers gamers = listgamers.Where(r => r.selected == 1).FirstOrDefault();

                int y = this.Height / 4 + (gamers.getTh - 1) * increment * 9 + 2 * gamers.selectedLineIndex * increment;
                if (e.KeyCode == Keys.Left)
                {
                    if (gamers.x <= 0)
                    {
                        gamers.x = 0;
                    }
                    else
                    {
                        gamers.x -= increment * 2;
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    gamers.x += increment * 2;
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (gamers.y < y)
                    {
                        gamers.y = gamers.pic.Location.Y;
                    }
                    else
                    {
                        gamers.y -= increment * 2;
                        if (gamers.y == y-increment*2)
                        {
                            gamers.selectedLineIndex = 0;
                        }
                    }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (gamers.y > y)
                    {
                        gamers.y = gamers.pic.Location.Y;
                    }
                    else
                    {
                        gamers.y += increment * 2;
                        if (gamers.y == y + increment * 2)
                        {
                            gamers.selectedLineIndex = 2;
                        }
                    }
                    
                }
                if (gamers.x >= this.Width/4)
                {
                    MessageBox.Show("请开始游戏");
                    gamers.x = gamers.pic.Location.X;
                }
                gamers.pic.Location = new Point(gamers.x, gamers.y);
            }
            else
            {
                listgamers.ForEach(r =>
                {
                    if (e.KeyCode == Keys.Left)
                    {
                        r.x = r.x;
                    }
                    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                    {
                        r.y = r.y;
                    }
                    if (e.KeyCode == Keys.Right)
                    {
                        Random random = new Random();
                        int ran = random.Next(1, 8);
                        r.walkcount += ran;
                        r.walk(ran);
                        for (int i = 0; i < r.walkcount; i++)
                        {
                            foreach (Control item in this.Controls)
                            {
                                PictureBox pbx = item as PictureBox;
                                if (pbx != null)
                                {
                                    if (pbx.BackColor == Color.Black
                                                                 && pbx.Location.X == this.Width / 4 + 2 * i * increment
                                                            && pbx.Location.Y == this.Height / 4 + (r.getTh - 1) * increment * 9 + 2 * r.selectedLineIndex * increment)
                                    {
                                        this.Controls.Remove(pbx);
                                    }
                                }
                                 
                            }

                        }

                        if (r.result() == 0)
                        {
                            MessageBox.Show(r.name + "获胜");
                            r.walkcount = 0;
                            Application.Exit();
                        }

                    }
                });
                
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            startGames = 1;
            listgamers.ForEach(r=>{
                int gamerx = this.Width / 4- increment;
                r.pic.Location = new Point(gamerx,r.y);
            });
        }
    }
}
