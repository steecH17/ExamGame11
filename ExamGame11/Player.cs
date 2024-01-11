using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamGame11
{
    public class Player
    {
        Random random = new Random();
        public int x;
        public int y;
        public int width;
        public int height;
        public SolidBrush brush = new SolidBrush(Color.Red);

        public Player()
        {
            this.width = 20;
            this.height = 20;
            this.y = 400;
            this.x = 20 * random.Next(0, 25);
        }
    }
}
