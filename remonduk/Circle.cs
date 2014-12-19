using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace remonduk
{
	public class Circle
	{
		public int PRECISION = 8;

		public const double RADIUS = 1;
		public const double MASS = 1.0;

		public const double VELOCITY = 0;
		public const double VELOCITY_ANGLE = 0;
		public const double ACCELERATION = 0;
		public const double ACCELERATION_ANGLE = 0;
		public Color DEFAULT_COLOR = Color.Chartreuse;

		public const Circle TARGET = null;
		public double MIN_DIST = 0;
		public double MAX_DIST = 0;

		public double x, y, r, mass;
		public double velocity, vx, vy;
		public double acceleration, ax, ay;
		public double velocity_angle, acceleration_angle;

		public Circle target;
		public double min_dist;
		public double max_dist;

		public Color color;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		/// <param name="mass"></param>
		///

		public Circle() { }

		public Circle(Circle circle) :
			this(circle.x, circle.y, circle.r, circle.velocity, circle.velocity_angle, circle.acceleration, circle.acceleration_angle, circle.mass) { }

		public Circle(double x, double y, double r, double mass = MASS) :
			this(x, y, r,
				 VELOCITY, VELOCITY_ANGLE, mass) { }

		public Circle(double x, double y, double r,
			double velocity, double velocity_angle, double mass = MASS) :
			this(x, y, r,
				 velocity, velocity_angle,
				 ACCELERATION, ACCELERATION_ANGLE, mass) { }

		public Circle(double x, double y, double r,
			double velocity, double velocity_angle,
			double acceleration, double acceleration_angle, double mass = MASS)
		{
			this.x = x;
			this.y = y;
			setRadius(r);
			setMass(mass);

			setVelocity(velocity, velocity_angle);
			setAcceleration(acceleration, acceleration_angle);

			follow(TARGET);
			this.color = Color.Chartreuse;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="r"></param>
		/// 
		public void setRadius(double r)
		{
			if (r < RADIUS)
			{
				throw new ArgumentException("radius: " + r);
			}
			this.r = r;
		}

		public void setMass(double mass)
		{
			this.mass = mass;
		}

		public void setVelocity(double velocity, double velocity_angle)
		{
			vx = velocity * Math.Cos(velocity_angle);
			vy = velocity * Math.Sin(velocity_angle);
			this.velocity = magnitude(vx, vy);
			this.velocity_angle = angle(vy, vx);
		}

		public void setAcceleration(double acceleration, double acceleration_angle)
		{
			ax = acceleration * Math.Cos(acceleration_angle);
			ay = acceleration * Math.Sin(acceleration_angle);
			this.acceleration = magnitude(ax, ay);
			this.acceleration_angle = angle(ay, ax);
		}

		public void follow(Circle target = null)
		{
			if (target == null || target == this)
			{
				this.target = null;
				this.min_dist = MIN_DIST;
				this.max_dist = MAX_DIST;
			}
			else
			{
				follow(target, target.r + r, target.r + r);
			}
		}

		public void follow(Circle target, double min_dist, double max_dist)
		{
			if (target == null || target == this)
			{
				follow();
			}
			else
			{
				this.target = target;
				this.min_dist = min_dist;
				this.max_dist = max_dist;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		///
		public void updateAcceleration(double acceleration, double acceleration_angle)
		{
			ax += acceleration * Math.Cos(acceleration_angle);
			ay += acceleration * Math.Sin(acceleration_angle);
			this.acceleration = magnitude(ax, ay);
			this.acceleration_angle = angle(ay, ax);
		}

		public void updateVelocity()
		{
			vx += ax;
			vy += ay;
			velocity = magnitude(vx, vy);
			velocity_angle = angle(vy, vx);
		}

		public void updatePosition()
		{
			x += ax / 2 + vx;
			y += ay / 2 + vy;
			updateVelocity();
		}

		public bool colliding(Circle other) // do more robust collision detection here
		{
			//WE NEED TO REMEMBER VELOCITY EXISTS
			if (other == this)
			{
				return false;
			}
			if (distance(other) <= r + other.r) {
				return true;
			}
			else {
				return false;
				//double m1 = angle(vy, vx);
				//double m2 = angle(other.vy, other.vx);

				//double x1_new = (m2 * other.x - m1 * other.x - other.y + y) / (m2 - m1);
				//double y1_new = 

				//double t1_pos = (-1 * vx + Math.Sqrt(vx * vx - 2 * ax * (x - x1_new))) / ax;
				//double t1_neg = (-1 * vx - Math.Sqrt(vx * vx - 2 * ax * (x - x1_new))) / ax;

				//double x2_new = other.ax / 2 * t1_pos * t1_pos + other.vx * t1_pos + other.x;
				//double y2_new = other.ay / 2 * t1_pos * t1_pos + other.vy * t1_pos + other.y;

				//if (magnitude(y2_new - 
				


				//x, y values should be the same --
				//t values of when they reach those points might be different
				//so assuming t1, calculate new x2, y2
				//assuming t2, calculate new x1, y1

				//return magnitude(intersection_y2 - intersection_y1,  0) <= r + other.r;

				//intersection_t_pos

			}
			// find where they would intersect
			// if they intersect
			// how to check if they crossed paths
			//return 
		}

		public void move(HashSet<Circle> circles)
		{
			if (target != null)
			{
				// set up following in a better way ??
				//double dist = distance(target);
				setVelocity(velocity, angle(target.y - y, target.x - x));
				//double tau = 2.0 * Math.PI;
				//double diff = (velocity_angle % (tau)) - (target.velocity_angle % (tau));
			}

			updatePosition();
			foreach (Circle c in circles)
			{
				if (colliding(c))
				{
					double kinetic = mass * velocity * velocity / 2;
					//Debug.WriteLine(kinetic);
				}
			}
		}

		public void update(HashSet<Circle> circles) //revisit List for refactorization!!!!! rar i like my keyboard this
		//is fun kbye
		{
			move(circles);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="g"></param>
		public void draw(Graphics g)
		{

			Brush brush = new SolidBrush(Color.Chartreuse);
			g.FillEllipse(brush, (float)(x - r / 2), (float)(y - r / 2), (float)r, (float)r);
		}

		public double magnitude(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);

			//return Math.Round(Math.Sqrt(x * x + y * y), PRECISION);
		}

		public double distance(Circle other)
		{
			double distx = other.x - x;
			double disty = other.y - y;
			return magnitude(distx, disty);
		}

		public double angle(double y, double x)
		{
			if (Math.Round(y, PRECISION) == 0 &&
				Math.Round(x, PRECISION) == 0)
			{
				return 0;
			}
			double theta = Math.Atan2(y, x);
			//if (theta > 2 * Math.PI) {
			//	theta -= 2 * Math.PI;
			//}
			if (theta < 0)
			{
				theta += 2 * Math.PI;
			}
			return theta;
		}

		public String toString()
		{

			return "(" + x + ", " + y + ") radius: " + r + " mass: " + mass + "\n" +
				"velocity: " + velocity + " (" + vx + ", " + vy + "): " + velocity_angle + "\n" +
				"acceleration: " + acceleration + " (" + ax + ", " + ay + "): " + acceleration_angle + "\n" +
				"target: " + target + "[" + min_dist + ", " + max_dist + "\n" +
				"color: " + color;
		}
	}
}
