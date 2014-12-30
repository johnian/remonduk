using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace remonduk
{
    /// <summary>
    /// Building blocks.
    /// </summary>
	public class Circle
	{
        /// <summary>
        /// Precision for rounding to 0.
        /// </summary>
		public const int PRECISION = 8;

        /// <summary>
        /// The default value for a circles radius.
        /// </summary>
		public const double RADIUS = 1;
        /// <summary>
        /// The default value for a circles mass.
        /// </summary>
		public const double MASS = 1.0;

        /// <summary>
        /// The default value for a circles velocity vector magnitude.
        /// </summary>
		public const double VELOCITY = 0;
        /// <summary>
        /// The default value for a circles velocity vector angle.
        /// </summary>
		public const double VELOCITY_ANGLE = 0;
        /// <summary>
        /// The default value for a circles acceleration vector magnitude.
        /// </summary>
		public const double ACCELERATION = 0;
        /// <summary>
        /// The default value for a circles acceleration vector angle.
        /// </summary>
		public const double ACCELERATION_ANGLE = 0;

		/// <summary>
        /// The default value for a circles color.
        /// </summary>
        //Need to rework this for loading / saving.  Save to string and load from there
        public Color DEFAULT_COLOR = Color.Chartreuse;

        /// <summary>
        /// The default value for a circles target.  Used for non-physics interactions.
        /// </summary>
		public const Circle TARGET = null;
        /// <summary>
        /// The default value for the minimum distance to follow the target at.
        /// </summary>
		public double MIN_DIST = 0;
        /// <summary>
        /// The default value for the maximum distance to follow the target at.
        /// </summary>
		public double MAX_DIST = 0;

        //r = d!
        /// <summary>
        /// This circles position, radius, and mass.
        /// </summary>
		public double x, y, r, mass;
        /// <summary>
        /// This circles velocity vector magnitude and angle and (x,y) components.
        /// </summary>
        public double velocity, velocity_angle, vx, vy;
        /// <summary>
        /// This circles acceleration vector magnitude and angle and (x,y) components.
        /// </summary>
		public double acceleration, acceleration_angle, ax, ay;

        /// <summary>
        /// This circles target.  Used for non-physics interactions.
        /// </summary>
        public Circle target;
        /// <summary>
        /// The minimum distance this circle will follow at.
        /// </summary>
		public double min_dist;
        /// <summary>
        /// The maximum distance this circle will follow at.
        /// </summary>
		public double max_dist;

        /// <summary>
        /// This circles color.
        /// </summary>
		public Color color;

        /// <summary>
        /// If this circle exists (if it should interact with other objects in the physical system).
        /// </summary>
		bool exists;

        public QuadTreeTest.QuadTreePositionItem<Circle> q_tree_pos;

		//public List<Force> forces;

        /// <summary>
        /// Empty constructor.  No values are set.
        /// </summary>
		public Circle() { }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">The other circle to copy.</param>
		public Circle(Circle other) :
			this(other.x, other.y, other.r, other.velocity, other.velocity_angle, other.acceleration,
				 other.acceleration_angle, other.mass) { }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="mass"></param>
        ///
		public Circle(double x, double y, double r, double mass = MASS) :
			this(x, y, r,
				 VELOCITY, VELOCITY_ANGLE, mass) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="velocity"></param>
        /// <param name="velocity_angle"></param>
        /// <param name="mass"></param>
		public Circle(double x, double y, double r,
			double velocity, double velocity_angle, double mass = MASS) :
			this(x, y, r,
				 velocity, velocity_angle,
				 ACCELERATION, ACCELERATION_ANGLE, mass) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="velocity"></param>
        /// <param name="velocity_angle"></param>
        /// <param name="acceleration"></param>
        /// <param name="acceleration_angle"></param>
        /// <param name="mass"></param>
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
            q_tree_pos = new QuadTreeTest.QuadTreePositionItem<Circle>(this, new Tuple<double, double>(x, y), new Tuple<double, double>(r, r));
            //forces = new List<Force>();
		}

		/// <summary>
		/// Sets this circles radius.
		/// </summary>
		/// <param name="r">The new value for this circles radius.</param>
		/// 
		public void setRadius(double r)
		{
			if (r < RADIUS)
			{
				throw new ArgumentException("radius: " + r);
			}
			this.r = r;
		}

        /// <summary>
        /// Set this circles mass.
        /// </summary>
        /// <param name="mass">The value to set this circles mass to.</param>
		public void setMass(double mass)
		{
			this.mass = mass;
		}

        /// <summary>
        /// Sets this circles velocity.  Recalculates vx and vy values.
        /// </summary>
        /// <param name="velocity">The new velocity vector's magnitude.</param>
        /// <param name="velocity_angle">The new velocity vector's angle.</param>
		public void setVelocity(double velocity, double velocity_angle)
		{
			vx = velocity * Math.Cos(velocity_angle);
			vy = velocity * Math.Sin(velocity_angle);
            //I know we went over these once before but why are we rounding here?  For the tests?
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

        /// <summary>
        /// Sets this circles acceleration.  Recalculates ax and ay values.
        /// </summary>
        /// <param name="acceleration">The new acceleration vector's magnitude.</param>
        /// <param name="acceleration_angle">The new acceleration vector's angle.</param>
		public void setAcceleration(double acceleration, double acceleration_angle)
		{
			ax = acceleration * Math.Cos(acceleration_angle);
            ay = acceleration * Math.Sin(acceleration_angle);
            //again I know we went over these once before but why are we rounding here?  For the tests?
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

        /// <summary>
        /// Sets the target circle for this circle to follow.  Uses default min and max distances.
        /// Somethings a little weird - we should talk about this one.
        /// </summary>
        /// <param name="target">The target circle for this circle to follow.</param>
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

        /// <summary>
        /// Sets a circle for this circle to follow.
        /// </summary>
        /// <param name="target">The target circle this circle should follow.</param>
        /// <param name="min_dist">The minimum distance this circle will start following at.</param>
        /// <param name="max_dist">The maximum distance this circle will start following at.</param>
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
        /// Updates this circles acceleration.  Adds an acceleration vector to the current acceleration.
        /// </summary>
        /// <param name="acceleration">The acceleration vector's magnitude to be added.</param>
        /// <param name="acceleration_angle">The acceleration vector's angle to be added.</param>
		public void updateAcceleration(double acceleration, double acceleration_angle)
		{
			// this might not be necessary anymore
			ax += acceleration * Math.Cos(acceleration_angle);
			ay += acceleration * Math.Sin(acceleration_angle);
			this.acceleration = magnitude(ax, ay);
			this.acceleration_angle = angle(ay, ax);
		}

        /// <summary>
        /// Updates this circles velocity by adding it's acceleration values to velocity values.  Occurs once per frame.
        /// </summary>
		public void updateVelocity()
		{
			vx += ax;
			vy += ay;
			velocity = magnitude(vx, vy);
			velocity_angle = angle(vy, vx);
		}

        /// <summary>
        /// Updates this circles position based on current velocity and acceleration values.
        /// </summary>
		public void updatePosition()
		{
			x += ax / 2 + vx;
			y += ay / 2 + vy;
            q_tree_pos.Position = new Tuple<double, double>(x, y);
			updateVelocity();
		}

        /// <summary>
        /// The beginings of elastic collisions.
        /// </summary>
        /// <param name="that">The other circle this circle is colliding with.</param>
        /// <returns></returns>
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
		/// <summary>
		/// Determines if this circle is colliding with that circle.
		/// </summary>
		/// <param name="that">The circle to check for collision with.</param>
		/// <returns>Returns the angle of impact if colliding.  -1 if not.  </returns>
        public double colliding(Circle that)
		{
			double center = Circle.angle(that.y - this.y, that.x - this.x);
			// use squared instead of square root for efficiency
            //Check the easy  one first
			if (distance(that) <= that.r + r)
			{
				Out.WriteLine("overlapping");
				if (that != this)
					return center;
				return -1;
			}
			else
			{
				double reference_angle = Circle.angle(ay / 2 + vy - that.ay / 2 - that.vy, ax / 2 + vx - that.ax / 2 - that.vx);
				double direction = Circle.angle(that.y - y, that.x - x);
                //if neither are moving or if they're moving in opposite directions.
				if ((acceleration + velocity == 0 && that.acceleration + that.velocity == 0) ||
					Math.Abs(direction - reference_angle) > Math.PI / 2)
				{
                    Out.WriteLine("not moving or wrong direction");
					return -1;
				}
                //check if we will cross
				Tuple<double, double> cross = crossing(that);
				if (cross != null)
				{
					center = Circle.angle(that.y - cross.Item2, that.x - cross.Item1);
					return center;
				}
				return -1;
			}
		}

        /// <summary>
        /// Treats that circle as stationary and the frame of perspective as this.
        /// </summary>
        /// <param name="that">The circle to check if this circle is crossing.</param>
        /// <returns>The center of this circle when the two circles collide.</returns>
		public Tuple<double, double> crossing(Circle that)
		{
			double reference_vx = ax / 2 + vx - (that.ax / 2 + that.vx);
			double reference_vy = ay / 2 + vy - (that.ay / 2 + that.vy);
			Out.WriteLine("");
			Out.WriteLine("reference_vx: " + reference_vx);
			Out.WriteLine("reference_vy: " + reference_vy);

			Tuple<double, double> point = closestPoint(that.x, that.y, reference_vx, reference_vy);
			if (point == null)
			{
				Out.WriteLine("point is null");
				return null;
			}
			double distance_from_that = Circle.magnitude(point.Item1 - that.x, point.Item2 - that.y);
			double radii_sum = that.r + r;

			Out.WriteLine("distance_from_that: " + distance_from_that);
			Out.WriteLine("radii_sum: " + radii_sum);
			if (distance_from_that > radii_sum)
			{
                Out.WriteLine("too far");
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

        /// <summary>
        /// Calculates the closest point on the this' movement vector to that.
        /// </summary>
        /// <param name="point_x">That circles x value.</param>
        /// <param name="point_y">That circles y value</param>
        /// <param name="reference_vx">This circles reference vx.</param>
        /// <param name="reference_vy">This circles reference vy.</param>
        /// <returns>The closest point on this' movement vector to point_x and point_y</returns>
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

            Out.WriteLine("intersection_x: " + intersection_x);
            Out.WriteLine("intersection_y: " + intersection_y);

			double delta_x = intersection_x - x;
			double delta_y = intersection_y - y;
			if (Circle.magnitude(intersection_x - x, intersection_y - y) >
				Circle.magnitude(reference_vx, reference_vy))
			{
                Out.WriteLine("too far");
				return null;
			}
			// have this also return the time of impact, double between [0, 1]
			// this might automatically take care of the above case because time would be > 1
			return Tuple.Create(intersection_x, intersection_y);
		}

        /// <summary>
        /// This circles move method.  Updates the velocity, checks for collisions then updates the (x,y) coordinates.
        /// </summary>
        /// <param name="circles">For some reason a silly keycollection of circles, change me.</param>
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
		}

        /// <summary>
        /// Calculates the distance from this circle to that circle.
        /// </summary>
        /// <param name="other">The other circle to calculate distance to.</param>
        /// <returns>The distance to the other circle.</returns>
		public double distance(Circle other)
		{
			double distx = other.x - x;
			double disty = other.y - y;
			return magnitude(distx, disty);
		}

        /// <summary>
        /// This circles update method.  See move()
        /// </summary>
        /// <param name="circles">Some silly keycollection...change me</param>
		public void update(Dictionary<Circle, Tuple<double, double>>.KeyCollection circles) //revisit List for refactorization!!!!! rar i like my keyboard this
		//is fun kbye
		{
			move(circles);
		}

		/// <summary>
		/// Draws this circle.
		/// </summary>
		/// <param name="g">The graphics object to draw this circle on.</param>
		public void draw(Graphics g)
		{
			Brush brush = new SolidBrush(color);
			g.FillEllipse(brush, (float)(x - r), (float)(y - r), (float)(2F * r), (float)(2F * r));
		}

        /// <summary>
        /// Returns the magnitude of a vector based on it's x and y components.
        /// </summary>
        /// <param name="x">The magnitude of the x component.</param>
        /// <param name="y">The magnitude of the y component.</param>
        /// <returns>The magnitude.</returns>
		public static double magnitude(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);
		}

        /// <summary>
        /// Julians atan2.  Always positive with some precision thing he needs to explain to me.
        /// </summary>
        /// <param name="y">The y value.</param>
        /// <param name="x">The x value.</param>
        /// <returns>tan(y,x) from 0 to 2PI</returns>
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

        /// <summary>
        /// This circles to string method.
        /// </summary>
        /// <returns>
        /// (523, 316) radius: 5 mass: 1
        /// velocity: 0 (0, 0): 0
        /// acceleration: 0 (0, 0): 0
        /// target: [0, 0
        /// color: Color [Chartreuse]
        /// </returns>
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
