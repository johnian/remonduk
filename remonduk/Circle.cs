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
		public const int PRECISION = 8;

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

		bool exists;

		//public List<Force> forces;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="r"></param>
		/// <param name="mass"></param>
		///
		public Circle() { }

		public Circle(Circle other) :
			this(other.x, other.y, other.r, other.velocity, other.velocity_angle, other.acceleration,
				 other.acceleration_angle, other.mass) { }

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
			exists = true;
			//forces = new List<Force>();
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
			if (Math.Round(vx, PRECISION) == 0)
			{
				vx = 0;
			}
			if (Math.Round(vy, PRECISION) == 0)
			{
				vy = 0;
			}
			this.velocity = magnitude(vx, vy);
			this.velocity_angle = angle(vy, vx);
		}

		public void setAcceleration(double acceleration, double acceleration_angle)
		{
			ax = acceleration * Math.Cos(acceleration_angle);
			ay = acceleration * Math.Sin(acceleration_angle);
			if (Math.Round(ax, PRECISION) == 0)
			{
				ax = 0;
			}
			if (Math.Round(ay, PRECISION) == 0)
			{
				ay = 0;
			}
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
			// this might not be necessary anymore
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

		public bool elastic(Circle that)
		{
			// should make this take a "combined mass" circle
			if (!that.exists)
				return false;
			double this_vx = (that.vx * (this.mass - that.mass) + 2 * that.mass * that.vx) / (this.mass + that.mass);
			double that_vx = (this.vx * (this.mass - that.mass) + 2 * this.mass * this.vx) / (this.mass + that.mass);
			double this_vy = (that.vy * (this.mass - that.mass) + 2 * that.mass * that.vy) / (this.mass + that.mass);
			double that_vy = (this.vy * (this.mass - that.mass) + 2 * this.mass * this.vy) / (this.mass + that.mass);
			this.setVelocity(Circle.magnitude(this_vx, this_vy), Circle.angle(this_vy, this_vx));
			that.setVelocity(Circle.magnitude(that_vx, that_vy), Circle.angle(that_vy, that_vx));

			// this should only set velocity for "this" because it'll be automatically handled when looping through
			// also, simultaneous collisions won't work with this version
			return true;
		}

		// make this return the angle of impact
		public double colliding(Circle that)
		{
			double center = Circle.angle(that.y - this.y, that.x - this.x);
			// use squared instead of square root for efficiency
			if (distance(that) <= that.r + r)
			{
				Debug.WriteLine("overlapping");
				if (that != this)
					return center;
				return -1;
			}
			else
			{
				double reference_angle = Circle.angle(ay / 2 + vy - that.ay / 2 - that.vy, ax / 2 + vx - that.ax / 2 - that.vx);
				double direction = Circle.angle(that.y - y, that.x - x);
				if ((acceleration + velocity == 0 && that.acceleration + that.velocity == 0) ||
					Math.Abs(direction - reference_angle) > Math.PI / 2)
				{
					Debug.WriteLine("not moving or wrong direction");
					return -1;
				}
				Tuple<double, double> cross = crossing(that);
				if (cross != null)
				{
					center = Circle.angle(that.y - cross.Item2, that.x - cross.Item1);
					return center;
				}
				return -1;
			}
		}

		public Tuple<double, double> crossing(Circle that)
		{
			double reference_vx = ax / 2 + vx - (that.ax / 2 + that.vx);
			double reference_vy = ay / 2 + vy - (that.ay / 2 + that.vy);
			Debug.WriteLine("");
			Debug.WriteLine("reference_vx: " + reference_vx);
			Debug.WriteLine("reference_vy: " + reference_vy);

			Tuple<double, double> point = closestPoint(that.x, that.y, reference_vx, reference_vy);
			if (point == null)
			{
				Debug.WriteLine("point is null");
				return null;
			}
			double distance_from_that = Circle.magnitude(point.Item1 - that.x, point.Item2 - that.y);
			double radii_sum = that.r + r;

			Debug.WriteLine("distance_from_that: " + distance_from_that);
			Debug.WriteLine("radii_sum: " + radii_sum);
			if (distance_from_that > radii_sum)
			{
				Debug.WriteLine("too far");
				return null;
			}
			else
			{
				double distance_from_collision_squared = radii_sum * radii_sum - distance_from_that * distance_from_that;
				double distance_from_collision = Math.Sqrt(distance_from_collision_squared);

				double reference_velocity = Circle.magnitude(reference_vx, reference_vy);
				double collision_x = point.Item1 - distance_from_collision * reference_vx / reference_velocity;
				double collision_y = point.Item2 - distance_from_collision * reference_vy / reference_velocity;
				return Tuple.Create(collision_x, collision_y);
			}
		}

		public Tuple<double, double> closestPoint(double point_x, double point_y,
			double reference_vx, double reference_vy)
		{
			double constant_1 = reference_vy * x - reference_vx * y;
			double constant_2 = reference_vx * point_x + reference_vy * point_y;

			double determinant = reference_vx * reference_vx + reference_vy * reference_vy;

			double intersection_x;
			double intersection_y;
			if (determinant == 0)
			{
				intersection_x = x;
				intersection_y = y;
			}
			else
			{
				intersection_x = (constant_1 * reference_vy + constant_2 * reference_vx) / determinant;
				intersection_y = (constant_2 * reference_vy - constant_1 * reference_vx) / determinant;
			}

			Debug.WriteLine("intersection_x: " + intersection_x);
			Debug.WriteLine("intersection_y: " + intersection_y);

			double delta_x = intersection_x - x;
			double delta_y = intersection_y - y;
			if (Circle.magnitude(intersection_x - x, intersection_y - y) >
				Circle.magnitude(reference_vx, reference_vy))
			{
				Debug.WriteLine("too far");
				return null;
			}
			// have this also return the time of impact, double between [0, 1]
			// this might automatically take care of the above case because time would be > 1
			return Tuple.Create(intersection_x, intersection_y);
		}

		public void move(Dictionary<Circle, Tuple<double, double>>.KeyCollection circles)
		{
			if (target != null)
			{
				setVelocity(velocity, angle(target.y - y, target.x - x));
			}
			if (this.exists)
			{
				foreach (Circle c in circles)
				{
					if (colliding(c) >= 0)
					{
						// need to pass this the crossing point
						// need to update position in two steps
						// push them to the exact point of collision
						// and then have them move according to the remaining time
						elastic(c);
					}
				}
			}
			updatePosition();
			//foreach (Circle c in circles)
			//{
			//	if (colliding(c))
			//	{
			//		double kinetic = mass * velocity * velocity / 2;
			//	}
			//}
		}

		public double distance(Circle other)
		{
			double distx = other.x - x;
			double disty = other.y - y;
			return magnitude(distx, disty);
		}

		public void update(Dictionary<Circle, Tuple<double, double>>.KeyCollection circles) //revisit List for refactorization!!!!! rar i like my keyboard this
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
			Brush brush = new SolidBrush(color);
			g.FillEllipse(brush, (float)(x - r), (float)(y - r), (float)(2F * r), (float)(2F * r));
		}

		public static double magnitude(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);
			//return Math.Round(Math.Sqrt(x * x + y * y), PRECISION);
		}

		public static double angle(double y, double x)
		{
			if (Math.Round(y, PRECISION) == 0 &&
				Math.Round(x, PRECISION) == 0)
			{
				return 0;
			}
			double theta = Math.Atan2(y, x);
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
