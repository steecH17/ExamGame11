using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ExamGame11
{
    public class Obstacle
    {
        Random random = new Random();
        public int x;
        public int y;
        public int width;
        public int height;
        public int speed;


        public Obstacle()
        {
            this.width = 20;
            this.height = 20;
            this.y = 0;
            this.x = 20 * random.Next(0, 25);
            this.speed = 20;
            
        }

        public void MoveParticle()
        {
            this.y+= speed;
        }
    }

    
}
