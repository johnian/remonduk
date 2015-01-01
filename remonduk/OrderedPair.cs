using System;

namespace remonduk
{
	public class OrderedPair
	{

		/// <summary>
		/// Precision for rounding to 0.
		/// </summary>
		public const int PRECISION = 8;
		public double x, y;

		public OrderedPair(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public void set(double angle)
		{
			double magnitude = this.magnitude();
			x = magnitude * Math.Cos(angle);
			y = magnitude * Math.Sin(angle);
		}

		public void set(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public void get()
		{

		}


		public override String ToString()
		{
			return "(" + x + ", " + y + ")";
		}

		public override bool Equals(Object obj)
		{
			if (obj == null) return false;
			if (obj == this) return true;
			OrderedPair that = (OrderedPair)obj;
			return x == that.x && y == that.y;
		}

		/// <summary>
		/// Returns the magnitude of a vector based on it's x and y components.
		/// </summary>
		/// <param name="x">The magnitude of the x component.</param>
		/// <param name="y">The magnitude of the y component.</param>
		/// <returns>The magnitude.</returns>
		public static double magnitude(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);
		}

		public double magnitude()
		{
			return magnitude(x, y);
		}

		public double magnitude(OrderedPair that)
		{
			return magnitude(that.x - x, that.y - y);
		}

		/// <summary>
		/// Returns the angle of a vector between [0, 2PI].
		/// </summary>
		/// <param name="y">The y value.</param>
		/// <param name="x">The x value.</param>
		/// <returns>tan(y,x) from 0 to 2PI</returns>
		public static double angle(double y, double x)
		{
			if (Math.Round(y, PRECISION) == 0 &&
				Math.Round(x, PRECISION) == 0)
			{
				return 0;
			}
			double theta = Math.Atan2(y, x);
			if (theta < 0)
			{
				theta += 2 * Math.PI;
			}
			return theta;
		}

		public double angle()
		{
			return angle(y, x);
		}

		public double angle(OrderedPair that)
		{
			return angle(that.y - y, that.x - x);
		}
	}
}