using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSmallGames.models
{
    public class Lines
    {
        public int? sort { get; set; }
        public List<Line> linelist { get; set; }

        public void GenLines()
        {
            linelist = new List<Line>
                {
                    new Line(){ toothpick = new Toothpick() ,count = 3 },
                    new Line(){ toothpick = new Toothpick() ,count = 5 },
                    new Line(){ toothpick = new Toothpick() ,count = 7 }
                };
            if (!sort.HasValue)
            {
                Random random = new Random();
                sort = random.Next(0, 2);
            }
            linelist.Sort((x, y) => sort == 0 ? x.count.CompareTo(y.count) : -x.count.CompareTo(y.count));
            
        }

    }
}
