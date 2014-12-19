using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
    class Tether
    {
        Circle c1, c2;
        public double max_dist;
        public double k;

        public Tether(Circle c1, Circle c2, double max_dist, double k)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.max_dist = max_dist;
            this.k = k;
        }

        public void pull()
        {
            double delta_x = c1.x - c2.x;
            double delta_y = c1.y - c2.y;
            double dist = Math.Sqrt(delta_x * delta_x + delta_y * delta_y);
            System.Diagnostics.Debug.WriteLine("Distance = " + dist);
            if(dist > max_dist)
            {
                double theta1 = Math.Atan2(-delta_y, -delta_x);
                double theta2 = Math.Atan2(delta_y, delta_x);
                double accel1 = k * (dist - max_dist) - c1.acceleration;
                double accel2 = k * (dist - max_dist) - c2.acceleration;

                if(accel1 < 0)
                {
                    c1.updateAcceleration(Math.Abs(k * (dist - max_dist) - c1.acceleration), theta2);
                }
                else
                {
                    c1.updateAcceleration(Math.Abs(k * (dist - max_dist) - c1.acceleration), theta1);
                }

                if(accel2 < 0)
                {
                    c2.updateAcceleration(Math.Abs(k * (dist - max_dist) - c2.acceleration), theta1);
                }
                else
                {
                    c2.updateAcceleration(Math.Abs(k * (dist - max_dist) - c2.acceleration), theta2);
                }
                System.Diagnostics.Debug.WriteLine("TETHERING");
            }
        }
    }
}
