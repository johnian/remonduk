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
		public const float RADIUS = 1F;
		public const float MASS = 1.0F;

		public const float VELOCITY = 0;
		public const float VELOCITY_ANGLE = 0F;
		public const float ACCELERATION = Constants.GRAVITY;
		public const float ACCELERATION_ANGLE = Constants.GRAVITY_ANGLE;
        public Color DEFAULT_COLOR = Color.Chartreuse;

		public const Circle TARGET = null;
		public float MIN_DIST = 0F;
		public float MAX_DIST = 0F;

		public float x, y, r, mass;
		public float velocity, vx, vy;
		public float acceleration, ax, ay;
		public double velocity_angle, acceleration_angle;
  
		public Circle target;
		public float min_dist;
		public float max_dist;

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

		public Circle(float x, float y, float r, float mass = MASS):
			this(x, y, r,
				VELOCITY, VELOCITY_ANGLE, mass) { }

		public Circle(float x, float y, float r,
			float velocity, double velocity_angle, float mass = MASS):
			this(x, y, r,
				velocity, velocity_angle,
				ACCELERATION, ACCELERATION_ANGLE, mass) { }

		public Circle(float x, float y, float r,
			float velocity, double velocity_angle,
			float acceleration, double acceleration_angle, float mass = MASS)
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
		public void setRadius(float r) {
			if (r < RADIUS) {
				throw new ArgumentException("radius: " + r);
			}
			this.r = r;
		}

		public void setMass(float mass) {
			this.mass = mass;
		}

		public void setVelocity(float velocity, double velocity_angle)
        {
			this.velocity = velocity;
			this.velocity_angle = velocity_angle;
            vx = velocity * (float)Math.Cos(velocity_angle);
            vy = velocity * (float)Math.Sin(velocity_angle);
        }

		public void setAcceleration(float acceleration, double acceleration_angle) {
			this.acceleration = acceleration;
			this.acceleration_angle = acceleration_angle;
			ax = acceleration * (float)Math.Cos(acceleration_angle);
			ay = acceleration * (float)Math.Sin(acceleration_angle);
		}

		public void follow(Circle target = null) {
			if (target == null) {
				this.target = null;
				this.min_dist = MIN_DIST;
				this.max_dist = MAX_DIST;
			}
			else {
				follow(target, target.r + r, target.r + r);
			}
		}

        public void follow(Circle target, float min_dist, float max_dist)
        {
			if (target == null) {
				follow();
			}
			else {
				this.target = target;
				this.min_dist = min_dist;
				this.max_dist = max_dist;
			}
        }

		/// <summary>
		/// 
		/// </summary>
		///
		public void updateAcceleration(float acceleration, double acceleration_angle) {
			// should we update velocity here?
			this.ax += acceleration * (float)Math.Cos(acceleration_angle);
			this.ay += acceleration * (float)Math.Sin(acceleration_angle);
			this.acceleration = magnitude(ax, ay);
			this.acceleration_angle = angle(ay, ax);
		}

		public void updateVelocity() {
			vx += ax;
			vy += ay;
			velocity = magnitude(vx, vy);
			velocity_angle = angle(vy, vx);
		}

		public void updatePosition()
        {
            updateVelocity();
            x += vx;
            y += vy;
        }

        public bool colliding(Circle other) // do more robust collision detection here
        {
            //WE NEED TO REMEMBER VELOCITY EXISTS
            if (other == this)
            {
                return false;
            }
            return distance(other) <= r + other.r;
        }

        public void move(HashSet<Circle> circles)
        {
            if (target != null)
            {
				// set up following in a better way ??
				//double dist = distance(target);
				setVelocity(velocity, angle(target.x - x, target.y - y));
				//double tau = 2.0 * Math.PI;
				//double diff = (velocity_angle % (tau)) - (target.velocity_angle % (tau));
            }

            updatePosition();
            foreach(Circle c in circles)
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
            g.FillEllipse(brush, x-r/2, y-r/2, r, r);
        }

		public float magnitude(float x, float y) {
			return (float)Math.Sqrt(x * x + y * y);
		}

        public double distance(Circle other)
        {
            float distx = other.x - x;
            float disty = other.y - y;
            return magnitude(distx, disty);
        }

		public double angle(float y, float x) {
			return Math.Atan2(y, x);
		}
		
		public String toString() {

			return "(" + x + ", " + y + ") radius: " + r + " mass: " + mass + "\n" +
				"velocity: " + velocity + " (" + vx + ", " + vy + "): " + velocity_angle + "\n" +
				"acceleration: " + acceleration + " (" + ax + ", " + ay + "): " + acceleration_angle + "\n" +
				"target: " + target + "[" + min_dist + ", " + max_dist + "\n" +
				"color: " + color;
		}
	}
}
