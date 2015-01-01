using System;
using System.Drawing;

namespace remonduk
{
    /// <summary>
    /// An interaction represents an ongoing (or momentary?) connection between two circles (a tether).
    /// </summary>
	public class Interaction
	{
        /// <summary>
        /// The first circle involved in this interaction.
        /// </summary>
		public Circle first;
        /// <summary>
        /// The second circle involved in this interaction.
        /// </summary>
		public Circle second;
        /// <summary>
        /// The force exerted in this interaction.
        /// </summary>
		public Force force;
        /// <summary>
        /// A scalar to modify the force for this interaction.
        /// </summary>
		public double scalar;

		public Interaction(Circle first, Circle second, Force force, double scalar = 1) {
			this.first = first;
			this.second = second;
			this.force = force;
			this.scalar = scalar;
		}

		public Circle getOther(Circle circle) {
			if (circle == first) {
				return second;
			}
			return first;
		}

        /// <summary>
        /// Calculates the force exterted on the first circle.
        /// </summary>
        /// <returns>The force on the first circle.</returns>
		public OrderedPair forceOnFirst() {
			OrderedPair f = force.calculate(first, second);
			return new OrderedPair(scalar * f.x, scalar * f.y);
		}

        /// <summary>
        /// Calculates the force exerted on the second circle.
        /// </summary>
        /// <returns>The force on the second circle.</returns>
		public OrderedPair forceOnSecond() {
			return force.calculate(second, first);
		}

        /// <summary>
        /// Draws this interaction.
        /// </summary>
        /// <param name="g">The graphics object to draw this interaction on.</param>
        /// <param name="color">The color to draw this interaction.</param>
        public void draw(Graphics g, Color color)
        {
            Pen pen = new Pen(color);
            Point p1 = new Point((int)first.px, (int)first.py);
            Point p2 = new Point((int)second.px, (int)second.py);
            g.DrawLine(pen, p1, p2);
        }
	}
}