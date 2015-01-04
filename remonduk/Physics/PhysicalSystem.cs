using System;
using System.Linq;
using System.Collections.Generic;
using Remonduk.QuadTreeTest;

namespace Remonduk.Physics
{
	/// <summary>
	/// The physical system represents the current existence the Interactions are taking place in.  
	/// It contains all of the Circles and Interactions.
	/// </summary>
	public class PhysicalSystem
	{
		/// <summary>
		/// This physical system's dictionary of net Forces.
		/// Looking up a Circle will give the netforce exerted on that Circle from within the physical system.
		/// </summary>
		public Dictionary<Circle, OrderedPair> NetForces;
		public List<Circle> Circles;
		/// <summary>
		/// A list of all of the Interactions in this physical system.
		/// </summary>
		public List<Interaction> Interactions; // ideally, i'd like to index into this by Circle to get Interactions associated with that Circle
		public Dictionary<Circle, List<Interaction>> InteractionMap;

		public Dictionary<String, Force> Forces;

		public QuadTree<Circle> Tree;

		/// <summary>
		/// Default value for Gravity.
		/// </summary>
		public const bool GRAVITY_ON = true;
		/// <summary>
		/// Global flag for GravityOn in this physical system.
		/// </summary>
		public bool GravityOn;

		/// <summary>
		/// No-arg constructor to create a new physical system.  Inits lists, sets GravityOn to default.
		/// </summary>
		public PhysicalSystem()
		{
			Circles = new List<Circle>();
			NetForces = new Dictionary<Circle, OrderedPair>();
			Interactions = new List<Interaction>();
			InteractionMap = new Dictionary<Circle, List<Interaction>>();

			Forces = new Dictionary<String, Force>();

			Tree = new QuadTree<Circle>(new FRect(0, 0, 600, 600), 10);

			GravityOn = GRAVITY_ON;
			Forces.Add("Gravity", new Gravity(Gravity.GRAVITY, Gravity.ANGLE));
		}

		/// <summary>
		/// Adds a new circle to this physical system.
		/// </summary>
		/// <param name="circle">The new circle to add.</param>
		public void AddCircle(Circle circle)
		{
			//Out.WriteLine("1");
			Circles.Add(circle);
			//Out.WriteLine("2");
			// need to figure out how to handle GravityOn
			if (false /*GravityOn*/)
			{
				//Out.WriteLine("3");
				Interactions.Add(new Interaction(circle, null, Forces["Gravity"]));
			}

			//Out.WriteLine("4");
			NetForces.Add(circle, new OrderedPair(0, 0));

			//Out.WriteLine("5");
			InteractionMap.Add(circle, new List<Interaction>());
			//Out.WriteLine("6");

			// why does this throw a null pointer exception
			Tree.Insert(circle);
			//Out.WriteLine("7");
		}

		/// <summary>
		/// Removes a circle from this physical system.
		/// </summary>
		/// <param name="circle">The circle to remove from this system.</param>
		public bool RemoveCircle(Circle circle)
		{
			if (Circles.Remove(circle))
			{
				NetForces.Remove(circle);
				List<Interaction> associations = InteractionMap[circle];
				foreach (Interaction Interaction in associations)
				{
					Circle other = Interaction.GetOther(circle);
					InteractionMap[other].Remove(Interaction);
					Interactions.Remove(Interaction);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Adds an interaction to this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to add.</param>
		public void AddInteraction(Interaction interaction)
		{
			Interactions.Add(interaction);
			// do a try get value here, else create a new list, then add
			InteractionMap[interaction.First].Add(interaction);
			InteractionMap[interaction.Second].Add(interaction);
		}

		/// <summary>
		/// Removes an interaction from this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to remove.</param>
		public void RemoveInteraction(Interaction interaction)
		{
			Interactions.Remove(interaction);
			InteractionMap[interaction.First].Remove(interaction);
			InteractionMap[interaction.Second].Remove(interaction);
		}

		/// <summary>
		/// Updates circle velocities based on the netforces in the dictionary.
		/// </summary>
		public void UpdateNetForces()
		{
			for (int i = 0; i < NetForces.Count; i++)
			{
				Circle circle = NetForces.ElementAt(i).Key;
				//Out.WriteLine(circle.GetHashCode() + "");

				UpdateNetForceOn(circle);
				//NetForces[circle] = UpdateNetForceOn(circle);
				double ax = NetForces[circle].X / circle.Mass;
				double ay = NetForces[circle].Y / circle.Mass;
				//Out.WriteLine(NetForces[circle] + "");
				circle.SetAcceleration(ax, ay);
			}
		}

		public void update()
		{
			UpdateNetForces();
			UpdatePositions();
		}

		/// <summary>
		/// Calculates the net Forces acting on a circle.
		/// </summary>
		/// <param name="circle">The circle to calculate net Forces for.</param>
		/// <returns>The net Forces on the given circle.</returns>
		public void UpdateNetForceOn(Circle circle)
		{
			double fx = 0;
			double fy = 0;
			//Out.WriteLine(Circle.GetHashCode() + "");
			foreach (Interaction interaction in InteractionMap[circle])
			{
				OrderedPair f;
				if (interaction.First == circle)
				{
					f = interaction.ForceOnFirst();
				}
				else
				{
					f = interaction.ForceOnSecond();
				}
				fx += f.X;
				fy += f.Y;
			}
			NetForces[circle].SetXY(fx, fy);
			//Out.WriteLine("f: " + fx + ", " + fy);
		}

		public void UpdatePositions()
		{
			double time = 1;
			bool overlapped = false;

			while (time > 0)
			{
				Dictionary<Circle, List<Circle>> collisionMap = new Dictionary<Circle, List<Circle>>();
				double min = CheckCollisions(ref collisionMap, ref Circles, time, overlapped);
				if (collisionMap.Count != 0)
				{
					//Out.WriteLine("collision count: " + collisionMap.Count);
					//Out.WriteLine("circle count: " + Circles.Count);
					//Out.WriteLine("number of collisions: " + collisionMap.Count + "");
					Out.WriteLine("");
				}
				overlapped = (min == 0);

				UpdateCircles(min, time, collisionMap);
				time -= min;
			}
		}

		public double CheckCollisions(ref Dictionary<Circle, List<Circle>> collisionMap, ref List<Circle> near,
			double time, bool overlapped)
		{
			//Out.WriteLine("");
			double min = Double.PositiveInfinity;
			foreach (Circle circle in Circles)
			{
				List<Circle> collisions = new List<Circle>();
				foreach (Circle that in near)
				{
					// use quad Tree to get the list of Circles to check against
					// for now, just use Circles
					double value = circle.Colliding(that, time);

					if (!Double.IsInfinity(value))
					{
						Out.WriteLine("collision time for [" + circle.GetHashCode() + " & " + that.GetHashCode() + "]: " + value + " < " + min);
						Out.WriteLine(circle.Velocity + " : " + that.Velocity);
						if (value == 0 && overlapped)
						{
							//continue;
						}
						else if (value < min)
						{
							//Out.WriteLine("updating collision map: " + value);
							min = value;
							collisionMap = new Dictionary<Circle, List<Circle>>();
							collisions = new List<Circle>();
							collisionMap.Add(circle, collisions);
							collisions.Add(that);
						}
						else if (value == min)
						{
							//Out.WriteLine("adding to collision map: " + value);
							if (collisionMap != null && !collisionMap.ContainsKey(circle)) {
								// is this needed in the first place
								collisionMap.Add(circle, collisions);
							}
							collisions.Add(that);
						}
					}
				}
			}
			return min;
		}

		public void UpdateCircles(double min, double time,
			Dictionary<Circle, List<Circle>> collisionMap)
		{
			if (Double.IsInfinity(min))
			{
				min = time;
			}
			foreach (Circle circle in Circles)
			{
				circle.Update(min);
			}
			UpdateVelocities(collisionMap);
		}

		public void UpdateVelocities(Dictionary<Circle, List<Circle>> collisionMap)
		{
			Dictionary<Circle, OrderedPair> velocityMap = new Dictionary<Circle, OrderedPair>();
			foreach (Circle circle in collisionMap.Keys)
			{
				velocityMap.Add(circle, circle.CollideWith(collisionMap[circle]));

				//Out.WriteLine("updated velocity " + Circle.GetHashCode() + " " + velocityMap[Circle]);
			}
			foreach (Circle circle in velocityMap.Keys)
			{
				//Out.WriteLine(velocityMap[circle] + " <= " + circle.Velocity);
				circle.SetVelocity(velocityMap[circle].X, velocityMap[circle].Y);
			}
		}
	}
}