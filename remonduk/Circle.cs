using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace remonduk
{
    class Circle
    {
        const float GRAVITY_CONSTANT = 9.8F; // should maybe move to global location?

        const float DEFAULT_VELOCITY = 0;
        const float DEFAULT_VELOCITY_ANGLE = 0F;
        const float DEFAULT_ACCELERATION = 0;
        const float DEFAULT_ACCELERATION_ANGLE = 0F;

        const double DEFAULT_TURN_RADIUS = Math.PI / 12.0;
        const double DEFAULT_MASS = 1.0;

        public float r, x, y;
        float velocity, vx, vy;
        float acceleration, ax, ay;
        double velocity_angle, acceleration_angle;

        double mass;

        bool following;
        bool leashed;

        float min_leash;
        float max_leash;

        double turn_radius;

        Circle target; // following this target

        public Circle(float x, float y, float r) : this(x, y, r,
            DEFAULT_VELOCITY, DEFAULT_VELOCITY_ANGLE) { }

        public Circle(float x, float y, float r, float velocity, double velocity_angle) : this(x, y, r, velocity, velocity_angle,
            DEFAULT_ACCELERATION, DEFAULT_ACCELERATION_ANGLE) { }

        public Circle(float x, float y, float r, float velocity, double velocity_angle, float acceleration, double acceleration_angle)
        {
            this.x = x;
            this.y = y;
            this.r = r;

            setV(velocity, velocity_angle);
            setA(acceleration, acceleration_angle);

            mass = DEFAULT_MASS;

            target = null;
            leashed = false;
            following = false;

            min_leash = 2 * r;
            max_leash = 3 * r;

            turn_radius = DEFAULT_TURN_RADIUS;
        }

        public void leash(Circle target)
        {
            this.target = target;
            leashed = true;
        }

        public void follow(Circle target)
        {
            following = true;
            this.target = target;
        }

        public void setA(float acceleration, double acceleration_angle)
        {
            this.acceleration = acceleration;
            this.acceleration_angle = acceleration_angle;
            ax = acceleration * (float)Math.Cos(acceleration_angle);
            ay = -1 * acceleration * (float)Math.Sin(acceleration_angle) + GRAVITY_CONSTANT;
            Debug.WriteLine("" + ax + ", " + ay);
        }

        public void setV(float velocity, double velocity_angle)
        {
            this.velocity = velocity + acceleration;
            this.velocity_angle = velocity_angle;
            vx = velocity * (float)Math.Cos(velocity_angle) + ax;
            vy = velocity * (float)Math.Sin(velocity_angle) + ay;
        }

        public void updatePosition()
        {
            setA(acceleration, acceleration_angle);
            setV(velocity, velocity_angle);
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

        public void move(List<Circle> circles)
        {
            float delta_x = 0.0F;
            float delta_y = 0.0F;
            double dist = 0.0F;
            if (target != null)
            {
                delta_x = target.x - x;
                delta_y = target.y - y;
                dist = distance(target);
            }
            if (leashed && dist > max_leash && target.velocity > velocity)
            {
                setV(target.velocity, target.velocity_angle);
            }
            if (following)
            {
                velocity_angle = Math.Atan2(delta_y, delta_x);
                double tau = 2.0 * Math.PI;
                double diff = (velocity_angle % (tau)) - (target.velocity_angle % (tau));
                //if (dist < min_leash && ((diff > Math.PI) || (diff < -1 * Math.PI)))
                //{
                //    return;
                //}
            }

            updatePosition();
            //velocity = temp_speed;
            foreach(Circle c in circles)
            {
                if (collide(c) != null)
                {
                    double kinetic = mass * velocity * velocity / 2;
                    //Debug.WriteLine(kinetic);
                }
            }
        }

        public void update(List<Circle> circles) //revisit List for refactorization!!!!! rar i like my keyboard this
        //is fun kbye
        {
            move(circles);
        }

        public void draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Chartreuse);
            g.FillEllipse(brush, x, y, r, r);
        }

        private double distance(Circle other)
        {
            float distx = other.x - x;
            float disty = other.y - y;
            return Math.Sqrt(distx * distx + disty * disty);
        }
    }
}
