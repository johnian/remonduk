using System.Drawing;

namespace Remonduk.Physics
{
	/// <summary>
	/// Interaction is the pairing of two circles and the force acting between them.
	/// </summary>
	public class Interaction
	{
		/// <summary>
		/// The first circle involved in the interaction.
		/// </summary>
		public Circle First;
		/// <summary>
		/// The second circle involved in the interaction.
		/// </summary>
		public Circle Second;
		/// <summary>
		/// The force acting between the two circles.
		/// </summary>
		public Force ActingForce;
		/// <summary>
		/// The scalar to modify the force on the first circle.
		/// </summary>
		public double Scalar;

		/// <summary>
		/// Constructor for creating an interaction.
		/// </summary>
		/// <param name="first">The first circle in the interaction.</param>
		/// <param name="second">The second circle in the interaction.</param>
		/// <param name="force">The force acting between the two circles.</param>
		/// <param name="scalar">The scalar modifying the force on the first circle.</param>
		public Interaction(Circle first, Circle second, Force force, double scalar = 1)
		{
			First = first;
			Second = second;
			ActingForce = force;
			Scalar = scalar;
		}

		/// <summary>
		/// Returns the other circle in the interaction not specified.
		/// </summary>
		/// <param name="circle">The circle to not return.</param>
		/// <returns>The other circle in the interaction not specified.</returns>
		public Circle GetOther(Circle circle)
		{
			if (circle == First)
			{
				return Second;
			}
			return First;
		}

		/// <summary>
		/// Returns the values of the force on the first circle modified by the scalar.
		/// </summary>
		/// <returns>The values of the fortce on the first circle.</returns>
		public OrderedPair ForceOnFirst()
		{
			OrderedPair f = ActingForce.Calculate(First, Second);
			return new OrderedPair(Scalar * f.X, Scalar * f.Y);
		}

		/// <summary>
		/// Returns the values of the force on the second circle.
		/// </summary>
		/// <returns>The values of the force on the second circle.</returns>
		public OrderedPair ForceOnSecond()
		{
			return ActingForce.Calculate(Second, First);
		}

		/// <summary>
		/// Draws the interaction.
		/// </summary>
		/// <param name="graphics">The graphics object to draw this interaction on.</param>
		/// <param name="color">The color to use for drawing the interaction.</param>
		public void Draw(Graphics graphics, Color color)
		{
			Pen pen = new Pen(color);
			Point p1 = new Point((int)First.Px, (int)First.Py);
			Point p2 = new Point((int)Second.Px, (int)Second.Py);
			graphics.DrawLine(pen, p1, p2);
		}
	}
}