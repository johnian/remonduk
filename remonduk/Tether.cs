using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remonduk
{
    class Tether
    {
        public Circle c1, c2;
        public double max_dist;
        public double k;

        public Tether(Circle c1, Circle c2, double max_dist, double k)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.max_dist = max_dist;
            this.k = k;
        }

        public void pull(Circle original_c1, Circle original_c2)
        {
            double delta_x = original_c2.x - original_c1.x;
			double delta_y = original_c2.y - original_c1.y;
            double dist = Math.Sqrt(delta_x * delta_x + delta_y * delta_y);
			if(dist > max_dist)
			{
				double spring_force = k * (dist - max_dist);
				double accel1 = spring_force / original_c1.mass;
                double accel2 = spring_force / original_c2.mass;
				double theta = Math.Atan2(delta_y, delta_x);

				c1.updateAcceleration(accel1, theta);
				c2.updateAcceleration(accel2, theta + Math.PI);



				//if(accel1 < 0)
				//{
				//}
				//else
				//{
				//	c1.updateAcceleration(Math.Abs(accel1), theta1);
				//}

				//if(accel2 < 0)
				//{
				//	c2.updateAcceleration(Math.Abs(accel2), theta1);
				//}
				//else
				//{
				//	c2.updateAcceleration(Math.Abs(accel2), theta2);
				//}
				System.Diagnostics.Debug.WriteLine("Acceleration[" + c1.acceleration + ", " + original_c1.acceleration);

			}
        }
    }
}
