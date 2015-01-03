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
		public Circle First;
        /// <summary>
        /// The second circle involved in this interaction.
        /// </summary>
		public Circle Second;
        /// <summary>
        /// The force exerted in this interaction.
        /// </summary>
		public Force ActingForce;
        /// <summary>
        /// A scalar to modify the force for this interaction.
        /// </summary>
		public double Scalar;

		public Interaction(Circle first, Circle second, Force force, double scalar = 1) {
			First = first;
			Second = second;
			ActingForce = force;
			Scalar = scalar;
		}

		public Circle GetOther(Circle circle) {
			if (circle == First) {
				return Second;
			}
			return First;
		}

        /// <summary>
        /// Calculates the force exterted on the first circle.
        /// </summary>
        /// <returns>The force on the first circle.</returns>
		public OrderedPair ForceOnFirst() {
			OrderedPair f = ActingForce.Calculate(First, Second);
			return new OrderedPair(Scalar * f.X, Scalar * f.Y);
		}

        /// <summary>
        /// Calculates the force exerted on the second circle.
        /// </summary>
        /// <returns>The force on the second circle.</returns>
		public OrderedPair ForceOnSecond() {
			return ActingForce.Calculate(Second, First);
		}

        /// <summary>
        /// Draws this interaction.
        /// </summary>
        /// <param name="g">The graphics object to draw this interaction on.</param>
        /// <param name="color">The color to draw this interaction.</param>
        public void Draw(Graphics g, Color color)
        {
            Pen pen = new Pen(color);
            Point p1 = new Point((int)First.Px, (int)First.Py);
            Point p2 = new Point((int)Second.Px, (int)Second.Py);
            g.DrawLine(pen, p1, p2);
        }
	}
}