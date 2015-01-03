using System;

namespace remonduk
{
	public class OrderedPair
	{

		/// <summary>
		/// Precision for rounding to 0.
		/// </summary>
		public const int PRECISION = 8;
		public double X, Y;

		public OrderedPair(double x, double y)
		{
			X = x;
			Y = y;
		}

		public void Set(double angle)
		{
			double magnitude = Magnitude();
			X = magnitude * Math.Cos(angle);
			Y = magnitude * Math.Sin(angle);
		}

		public void Set(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}

		public void Get()
		{

		}


		public override String ToString()
		{
			return "(" + X + ", " + Y + ")";
		}

		public override bool Equals(Object obj)
		{
			if (obj == null) return false;
			if (obj == this) return true;
			OrderedPair that = (OrderedPair)obj;
			return X == that.X && Y == that.Y;
		}

		/// <summary>
		/// Returns the magnitude of a vector based on its x and y components.
		/// </summary>
		/// <param name="x">The magnitude of the x component.</param>
		/// <param name="y">The magnitude of the y component.</param>
		/// <returns>The magnitude.</returns>
		public static double Magnitude(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);
		}

		public double Magnitude()
		{
			return Magnitude(X, Y);
		}

		public double Magnitude(OrderedPair that)
		{
			return Magnitude(that.X - X, that.Y - Y);
		}

		/// <summary>
		/// Returns the angle of a vector between [0, 2PI].
		/// </summary>
		/// <param name="y">The y value.</param>
		/// <param name="x">The x value.</param>
		/// <returns>tan(y, x) from 0 to 2PI</returns>
		public static double Angle(double y, double x)
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

		public double Angle()
		{
			return Angle(Y, X);
		}

		public double Angle(OrderedPair that)
		{
			return Angle(that.Y - Y, that.X - X);
		}
	}
}