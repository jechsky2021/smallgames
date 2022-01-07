using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSmallGames.models
{
    public class Gamers
    {
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int selected { get; set; }
        public PictureBox pic { get; set; }
        public int selectedLineIndex { get; set; }
        public List<Line> lines { get; set; }
        public int walkcount { get; set; }
        public Gamers(List<Line> lines,string name)
        {
            this.lines = lines;
            this.name = name;
            this.pic = new PictureBox();
            this.selectedLineIndex = 1;
        }
        public void walk(int r)
        {
            this.x = this.pic.Location.X + (r-1) * 16+8;
            this.pic.Location = new System.Drawing.Point(this.x,this.pic.Location.Y);
            this.lines[selectedLineIndex].count = this.lines[selectedLineIndex].count - r < 0 ? 0 : this.lines[selectedLineIndex].count - r;
        }
        public int result()
        {
            return this.lines[selectedLineIndex].count;
        }
        public int getTh
        {
            get { return this.name == "玩家1" ? 1 : 2; }
            set { }
        }
    }
}
