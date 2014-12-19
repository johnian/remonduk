using System;

namespace remonduk
{
	public class Force
	{
		public Func<Circle, Circle, double> forceFunction;

		public Force(Func<Circle, Circle, double> forceFunction) {
			this.forceFunction = forceFunction;
		}

		public double calculate(Circle first, Circle second)
		{
			return forceFunction(first, second);
		}
	}
}