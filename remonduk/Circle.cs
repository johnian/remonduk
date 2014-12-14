using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace remonduk
{
    class Circle
    {
        float x, y, r;
        float vx, vy, s;
        double heading;

        Circle target;


        public Circle(float x, float y, float r)
        {
            this.x = x;
            this.y = y;
            this.r = r;

            s = 5;
            vx = 0;
            vy = 0;
            heading = Math.PI;
            target = null;
        }

        public void update()
        {
            if(target != null)
            {
                double diff = (heading % (2.0 * Math.PI)) - (target.heading % (2.0 * Math.PI));
                if (diff > Math.PI / 12.0)
                {
                    heading -= Math.PI / 12.0;
                }
                else if (diff < Math.PI / 12.0)
                {
                    heading += Math.PI / 12.0;
                }
                else
                {
                    heading = target.heading;
                }
            }
            vx = (float)Math.Cos(heading) * s;
            vy = (float)Math.Sin(heading) * s;
            x += vx;
            y += vy;
        }

        public void draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Chartreuse);
            g.FillEllipse(brush, x, y, r, r);
        }
    }
}
