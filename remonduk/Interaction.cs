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

        /// <summary>
        /// Calculates the force exterted on the first circle.
        /// </summary>
        /// <returns>The force on the first circle.</returns>
		public Tuple<double, double> forceOnFirst() {
			Tuple<double, double> f = force.calculate(first, second);
			return Tuple.Create(scalar * f.Item1, scalar * f.Item2);
		}

        /// <summary>
        /// Calculates the force exerted on the second circle.
        /// </summary>
        /// <returns>The force on the second circle.</returns>
		public Tuple<double, double> forceOnSecond() {
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
            Point p1 = new Point((int)first.x, (int)first.y);
            Point p2 = new Point((int)second.x, (int)second.y);
            g.DrawLine(pen, p1, p2);
        }
	}
}