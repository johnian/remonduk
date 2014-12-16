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
        public float x, y, r;
        float ax, ay, vx, vy, speed;
        float magnitude; // CHANGE ME
        double direction_velocity, direction_acceleration;
        double mass;

        bool following;
        bool leashed;

        float min_leash;
        float max_leash;

        const float DEFAULT_SPEED = 0;
        const double DEFAULT_TURN_RADIUS = Math.PI / 12.0;
        const float GRAVITY_CONSTANT = 9.8F;
        const double DEFAULT_MASS = 1.0;

        double turn_radius;

        Circle target; // following this target

        public Circle(float x, float y, float r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.mass = DEFAULT_MASS;
            speed = DEFAULT_SPEED;
            magnitude = 0;
            vx = 0;
            vy = 0;
            direction_velocity = 0;
            direction_acceleration = 0;
            target = null;

            min_leash = 2 * r;
            max_leash = 3 * r;

            turn_radius = DEFAULT_TURN_RADIUS;
        }

        public Circle(float x, float y, float r, float speed)  : this(x, y, r)
        {
            this.speed = speed;
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


        public void setA(float magnitude, double direction_acceleration)
        {
            this.magnitude = magnitude;
            this.direction_acceleration = direction_acceleration;
            ax = magnitude * (float)Math.Cos(direction_acceleration);
            ay = -1 * magnitude * (float)Math.Sin(direction_acceleration) + GRAVITY_CONSTANT;
        }

        public void setV(float speed, double direction_velocity)
        {
            this.speed = speed + magnitude;
            this.direction_velocity = direction_velocity;
            vx = speed * (float)Math.Cos(direction_velocity) + ax;
            vy = speed * (float)Math.Sin(direction_velocity) + ay;
        }

        public void update(List<Circle> circles) //revisit List for refactorization!!!!! rar i like my keyboard this
            //is fun kbye
        {
            move(circles);
        }

        public Circle collide(Circle other)
        {
            //WE NEED TO REMEMBER VELOCITY EXISTS
            float distx = x - other.x;
            float disty = y - other.y;
            double dist = Math.Sqrt(disty*disty + distx * distx);

            if (other.GetHashCode() == this.GetHashCode())
            {
                return null;
            }

            if(dist <= r+other.r)
            {
                return other;
            }

            return null;
        }


        public void move(List<Circle> circles)
        {
            float delta_y = 0.0F;
            float delta_x = 0.0F;
            double dist = 0.0F;
            if (target != null)
            {
                delta_y = target.y - y;
                delta_x = target.x - x;
                dist = Math.Sqrt(delta_y * delta_y + delta_x * delta_x);
            }
            if (leashed && dist > max_leash && target.speed > speed)
            {
                setV(target.speed, target.direction_velocity);
            }
            if (following)
            {
                direction_velocity = Math.Atan2(delta_y, delta_x);
                double tau = 2.0 * Math.PI;
                double diff = (direction_velocity % (tau)) - (target.direction_velocity % (tau));
                //if (dist < min_leash && ((diff > Math.PI) || (diff < -1 * Math.PI)))
                //{
                //    return;
                //}
            }

            setA(magnitude, direction_acceleration);
            setV(speed, direction_velocity);
            x += vx;
            y += vy;
            //speed = temp_speed;
            foreach(Circle c in circles)
            {
                if (collide(c) != null)
                {
                    double kinetic = mass * speed * speed/2;
                    Debug.WriteLine(kinetic);
                }
            }

        }

        public void draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Chartreuse);
            g.FillEllipse(brush, x, y, r, r);
        }
    }
}
