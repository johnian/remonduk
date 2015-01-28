using System;

namespace Remonduk.Physics
{
	/// <summary>
	/// Generic class for creating arbitrary forces for use in the physical system.
	/// </summary>
	public class Force
	{
		/// <summary>
		/// The function used to calculate the value of the force.
		/// </summary>
		public Func<Circle, Circle, OrderedPair> Calculate;

		/// <summary>
		/// Constructor for creating a force object.
		/// </summary>
		/// <param name="calculate">The function used to calculate the value of the force.</param>
		public Force(Func<Circle, Circle, OrderedPair> calculate)
		{
			Calculate = calculate;
		}
	}
}