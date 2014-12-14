using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace remonduk
{
    class Circle
    {
        float x, y, r;
        float vx, vy, speed;
        double direction;

        bool following;
        bool leashed;

        float min_leash;
        float max_leash;

        const float DEFAULT_S = 5;
        const double DEFAULT_TURN_RADIUS = Math.PI / 12.0;

        double turn_radius;
        
        Circle target; // following this target

        public Circle(float x, float y, float r)
        {
            this.x = x;
            this.y = y;
            this.r = r;

            speed = 0;
            vx = 0;
            vy = 0;
            direction = 0;
            target = null;

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

        public void setDirection(double direction)
        {
            this.direction = direction;
        }

        public void setV(float speed, double direction)
        {
            setDirection(direction);
            setVx(speed, direction);
            setVy(speed, direction);
            this.speed = speed;
        }

        public void setVx(float speed, double direction)
        {
            vx = speed * (float)Math.Cos(direction);
        }

        public void setVy(float speed, double direction)
        {
            vy = speed * (float)Math.Sin(direction);
        }

        public void update()
        {
            move();
        }

        public void move()
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
            if (leashed && dist > max_leash)
            {
                setV(target.speed, target.direction);
            }
            if (following)
            {
                direction = Math.Atan2(delta_y, delta_x);
                double tau = 2.0 * Math.PI;
                double diff = (direction % (tau)) - (target.direction % (tau));
                if (dist < min_leash && (diff > Math.PI) || (diff < -1 * Math.PI))
                {
                    speed = 0; // eventually, we'll use momentum
                }

                //double tau = 2.0 * Math.PI;
                //double diff = (direction % (tau)) - (target.direction % (tau));
                //if (diff > turn_radius)
                //{
                //    direction -= turn_radius;
                //}
                //else if (diff < turn_radius)
                //{
                //    direction += turn_radius;
                //}
                //else
                //{
                //    direction = target.direction;
                //}
            }
            setV(speed, direction);
            x += vx;
            y += vy;
        }

        public void draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color.Chartreuse);
            g.FillEllipse(brush, x, y, r, r);
        }
    }
}
