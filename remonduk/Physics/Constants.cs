using System;

namespace Remonduk.Physics
{
	/// <summary>
	/// Constants for the physical system.
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// The singleton instance of the Constants.
		/// </summary>
		private static Constants instance;

		/// <summary>
		/// The maximum acceptable error value for calculations.
		/// </summary>
		public const double EPSILON = .00000001;

		/// <summary>
		/// Magnitude used for calculating gravity.
		/// </summary>
		public double GRAVITY = 1;
		/// <summary>
		/// Angle used for calculating gravity.
		/// </summary>
		public double GRAVITY_ANGLE = Math.PI / 2.0;
		/// <summary>
		/// Global flag for enabling or diasbling gravity.
		/// </summary>
		public bool GRAVITY_ACTIVE = false;

		/// <summary>
		/// The default radius for a circle..
		/// </summary>
		public double DEFAULT_RADIUS = 5;
		/// <summary>
		/// The minimum radius for a circle.
		/// </summary>
		public double MIN_RADIUS = 1;
		/// <summary>
		/// The maximum radius for a circle.
		/// </summary>
		public double MAX_RADIUS = 40;
		/// <summary>
		/// Forces all circles to have the same size radius.
		/// </summary>
		public bool STATIC_RADIUS = false;

		/// <summary>
		/// Private constructor to prevent manual instantiation.
		/// </summary>
		private Constants() { }

		/// <summary>
		/// Returns the singleton instance of constants or creates one if this is the first call.
		/// </summary>
		public static Constants Instance
		{
			// make this read from a file
			get
			{
				if (instance == null)
				{
					instance = new Constants();
				}
				return instance;
			}
		}
	}
}