using System;

namespace remonduk
{
	public class Force
	{
		// if we have a separate class that manages all the forces
		// iterate over it, building a map of circle to net forces
		// at the end, iterate over the map, updating accelerations of all circles

		public Func<Circle, Circle, Tuple<double, double>> forceFunction;

		public Force(Func<Circle, Circle, Tuple<double, double>> forceFunction) {
			this.forceFunction = forceFunction;
		}

		public Tuple<double, double> calculate(Circle first, Circle second)
		{
			return forceFunction(first, second);
		}
	}
}