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

		public const bool FOLLOWING = false;
		public const Circle TARGET = null;
		public const double MIN_DIST = 0F;
		public const double MAX_DIST = 0F;

		public float x, y, r, mass;
		public float velocity, vx, vy;
		public float acceleration, ax, ay;
		public double velocity_angle, acceleration_angle;
  
		public bool following;
		public Circle target;
		public double min_dist;
		public double max_dist;

        public Color color;

        public Circle()
        { }

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

			target = TARGET;
			following = FOLLOWING;
			min_dist = MIN_DIST * r;
			max_dist = MAX_DIST * r;

            //add me somewhere else
            this.color = DEFAULT_COLOR;
        }

        public void follow(Circle target)
        {
            following = true;
            this.target = target;
        }

		public void setRadius(float r) {
			if (r < RADIUS) {
				this.r = RADIUS;
			}
			else {
				this.r = r;
			}
		}

		public void setMass(float mass) {
			if (mass < MASS) {
				this.mass = MASS;
			}
			else {
				this.mass = mass;
			}
		}

        public void setVelocity(float velocity, double velocity_angle)
        {
			this.velocity = velocity;
			this.velocity_angle = velocity_angle;
            vx = velocity * (float)Math.Cos(velocity_angle);
            vy = velocity * (float)Math.Sin(velocity_angle);
        }

		public double getVelocityAngle() {
			return Math.Atan2(vy, vx);
		}

		public void updateVelocity() {
			vx += ax;
			vy += ay;
			velocity = magnitude(vx, vy);
			velocity_angle = Math.Atan2(vy, vx);
		}

		public void setAcceleration(float acceleration, double acceleration_angle) {
			this.acceleration = acceleration;
			this.acceleration_angle = acceleration_angle;
			ax = acceleration * (float)Math.Cos(acceleration_angle);
			ay = acceleration * (float)Math.Sin(acceleration_angle);
		}

		public void updateAcceleration(float acceleration, double acceleration_angle) {
			// should we update velocity here?
			ax += acceleration * (float)Math.Cos(acceleration_angle);
			ay += acceleration * (float)Math.Sin(acceleration_angle);
			acceleration = magnitude(ax, ay);
			acceleration_angle = Math.Atan2(ay, ax);
		}


        public void updatePosition()
        {
            updateVelocity();
            x += vx;
            y += vy;
        }

        public Circle collide(Circle other)
        {
            //WE NEED TO REMEMBER VELOCITY EXISTS
            if (other.GetHashCode() == this.GetHashCode())
            {
                return null;
            }
            double dist = distance(other);
            if(dist <= r+other.r)
            {
                return other;
            }
            return null;
        }

        public void move(HashSet<Circle> circles)
        {
            float delta_x = 0F;
            float delta_y = 0F;
            double dist = 0F;
            if (target != null)
            {
                delta_x = target.x - x;
                delta_y = target.y - y;
                dist = distance(target);
            }
            if (following)
            {
                velocity_angle = Math.Atan2(delta_y, delta_x);
                double tau = 2.0 * Math.PI;
                double diff = (velocity_angle % (tau)) - (target.velocity_angle % (tau));
            }

            updatePosition();
            foreach(Circle c in circles)
            {
                if (collide(c) != null)
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

        public void draw(Graphics g)
        {
            Brush brush = new SolidBrush(color);
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
    }
}
