using System;

namespace Remonduk.Physics
{
	/// <summary>
	/// 
	/// </summary>
	public class OrderedPair
	{
		/// <summary>
		/// The x component of the ordered pair.
		/// </summary>
		public double X;
		/// <summary>
		/// The y component of the ordered pair.
		/// </summary>
		public double Y;

		/// <summary>
		/// Constructor that takes x and y values.
		/// </summary>
		/// <param name="x">The x component of the ordered pair.</param>
		/// <param name="y">The y component of the ordered pair.</param>
		public OrderedPair(double x = 0, double y = 0)
		{
			SetXY(x, y);
		}

		/// <summary>
		/// Sets both the x and y values of the ordered pair.
		/// </summary>
		/// <param name="x">The x component of the ordered pair.</param>
		/// <param name="y">The y component of the ordered pair.</param>
		public void SetXY(double x, double y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Adjusts the x and y components of the ordered pair according to the angle.
		/// </summary>
		/// <param name="angle">The angle to use for readjusting the x and y components.</param>
		public void SetXY(double angle)
		{
			double magnitude = Magnitude();
			X = magnitude * Math.Cos(angle);
			Y = magnitude * Math.Sin(angle);
		}

		/// <summary>
		/// Returns the magnitude of the x and y values but squared.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static double MagnitudeSquared(double x, double y)
		{
			return x * x + y * y;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public double MagnitudeSquared(OrderedPair that)
		{
			return MagnitudeSquared(that.X - X, that.Y - Y);
		}

		/// <summary>
		/// Returns the magnitude of a vector based on its x and y components.
		/// </summary>
		/// <param name="x">The magnitude of the x component.</param>
		/// <param name="y">The magnitude of the y component.</param>
		/// <returns>The magnitude.</returns>
		public static double Magnitude(double x, double y)
		{
			return Math.Sqrt(MagnitudeSquared(x, y));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public double Magnitude(OrderedPair that)
		{
			return Magnitude(that.X - X, that.Y - Y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public double Magnitude()
		{
			return Magnitude(X, Y);
		}

		/// <summary>
		/// Returns the angle of a vector between [0, 2PI].
		/// </summary>
		/// <param name="x">The x value.</param>
		/// <param name="y">The y value.</param>
		/// <returns>tan(y, x) from 0 to 2PI</returns>
		public static double Angle(double x, double y)
		{
			if (Math.Abs(x) <= Constants.EPSILON &&
				Math.Abs(y) <= Constants.EPSILON)
			//if (Math.Round(y, Constants.PRECISION) == 0 &&
			//	Math.Round(x, Constants.PRECISION) == 0)
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public double Angle(OrderedPair that)
		{
			return Angle(that.X - X, that.Y - Y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public double Angle()
		{
			return Angle(X, Y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			return "(" + X + ", " + Y + ")";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(Object obj)
		{
			if (obj == null) return false;
			if (obj == this) return true;
			OrderedPair that = (OrderedPair)obj;
			return X == that.X && Y == that.Y;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}