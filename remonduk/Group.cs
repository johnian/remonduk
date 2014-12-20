using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
    class Group
    {
        public HashSet<Circle> group;
        public HashSet<Circle> anchors;

        double x_min, x_min_y;
        double x_max, x_max_y;
        double y_min, y_min_x;
        double y_max, y_max_x;

        //public HashSet<Tether> tethers;

        public Group()
        {
            this.group = new HashSet<Circle>();
            this.anchors = new HashSet<Circle>();
            //this.tethers = new HashSet<Tether>();
        }

        public void tether(Circle c1, Circle c2, double max_dist, double k)
        {
            group.Add(c1);
            group.Add(c2);
			//tethers.Add(new Tether(c1,c2,max_dist,k));
        }

        public void draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Point[] points = new Point[4];
            points[0] = new Point((int)x_min, (int)x_min_y);
            points[1] = new Point((int)x_max, (int)x_max_y);
            points[2] = new Point((int)y_min_x, (int)y_min);
            points[3] = new Point((int)y_max_x, (int)y_max);
            g.DrawRectangle(pen, (int)x_min, (int)y_min, (int)(x_max - x_min), (int)(y_max - y_min));
            
        }

        public void update()
        {
            if (group.Count > 0)
            {
                x_min = group.ElementAt(0).x;
                x_max = x_min;
                y_min = group.ElementAt(0).y;
                y_max = y_min;
                y_min_x = x_min;
                y_max_x = x_max;
                x_max_y = y_max;
                x_min_y = y_min;
                foreach (Circle c in group)
                {
                    if (c.x > x_max)
                    {
                        x_max = c.x;
                        x_max_y = c.y;
                    }
                    if (c.x < x_min)
                    {
                        x_min = c.x;
                        x_min_y = c.y;
                    }
                    if (c.y > y_max)
                    {
                        y_max = c.y;
                        y_max_x = c.x;
                    }
                    if (c.y < y_min)
                    {
                        y_min = c.y;
                        y_min_x = c.x;
                    }
                }
            }
            //for (int i = 0; i < tethers.Count; i++ )
            //{
            //    System.Diagnostics.Debug.WriteLine("PULLING");
            //    tethers.ElementAt(i).pull();
            //}
        }
    }
}
