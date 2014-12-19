using System;

namespace remonduk
{
	public class Force
	{
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