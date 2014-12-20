using System;
using System.Drawing;

namespace remonduk
{
	public class Interaction
	{
		public Circle first;
		public Circle second;
		public Force force;
		public double scalar;

		public Interaction(Circle first, Circle second, Force force, double scalar = 1) {
			this.first = first;
			this.second = second;
			this.force = force;
			this.scalar = scalar;
		}

		public Tuple<double, double> forceOnFirst() {
			Tuple<double, double> f = force.calculate(first, second);
			return Tuple.Create(scalar * f.Item1, scalar * f.Item2);
		}

		public Tuple<double, double> forceOnSecond() {
			return force.calculate(second, first);
		}

        public void draw(Graphics g, Color color)
        {
            Pen pen = new Pen(color);
            Point p1 = new Point((int)first.x, (int)first.y);
            Point p2 = new Point((int)second.x, (int)second.y);
            g.DrawLine(pen, p1, p2);
        }
	}
}