using System;

namespace remonduk
{
	public class Force
	{
		public Func<Circle, Circle, OrderedPair> ForceFunction;

		public Force(Func<Circle, Circle, OrderedPair> forceFunction) {
			ForceFunction = forceFunction;
		}

		public OrderedPair Calculate(Circle first, Circle second)
		{
			return ForceFunction(first, second);
		}
	}
}