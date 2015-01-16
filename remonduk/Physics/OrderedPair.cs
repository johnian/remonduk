using System;

namespace Remonduk.Physics
{
	/// <summary>
	/// 
	/// </summary>
	public class OrderedPair
	{
		/// <summary>
		/// 
		/// </summary>
		public double X;
		/// <summary>
		/// 
		/// </summary>
		public double Y;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public OrderedPair(double x = 0, double y = 0)
		{
			SetXY(x, y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void SetXY(double x, double y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="angle"></param>
		public void SetXY(double angle)
		{
			double magnitude = Magnitude();
			X = magnitude * Math.Cos(angle);
			Y = magnitude * Math.Sin(angle);
		}

		/// <summary>
		/// 
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
		/// <param name="y">The y value.</param>
		/// <param name="x">The x value.</param>
		/// <returns>tan(y, x) from 0 to 2PI</returns>
		public static double Angle(double y, double x)
		{
			if (Math.Round(y, Constants.PRECISION) == 0 &&
				Math.Round(x, Constants.PRECISION) == 0)
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
			return Angle(that.Y - Y, that.X - X);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public double Angle()
		{
			return Angle(Y, X);
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