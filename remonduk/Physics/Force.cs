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
		public Func<Circle, Circle, OrderedPair> ForceFunction;

		/// <summary>
		/// Constructor for creating a force object.
		/// </summary>
		/// <param name="forceFunction">The function used to calculate the value of the force.</param>
		public Force(Func<Circle, Circle, OrderedPair> forceFunction)
		{
			ForceFunction = forceFunction;
		}

		/// <summary>
		/// Returns the force components as an ordered pair.
		/// </summary>
		/// <param name="first">The first circle in the force interaction.</param>
		/// <param name="second">The second circle in the force interaction.</param>
		/// <returns>The force components as anm ordered pair.</returns>
		public OrderedPair Calculate(Circle first, Circle second)
		{
			return ForceFunction(first, second);
		}
	}
}