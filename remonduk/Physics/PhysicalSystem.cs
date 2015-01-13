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
		public const double TIME_STEP = 1;

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<String, Force> FundamentalForces;
		/// <summary>
		/// 
		/// </summary>
		public Dictionary<String, Force> Forces;

		/// <summary>
		/// 
		/// </summary>
		public List<Circle> Circles;
		/// <summary>
		/// A list of all of the Interactions in this physical system.
		/// Iterate over this list to determine accelerations for all circles.
		/// </summary>
		public List<Interaction> Interactions;

		/// <summary>
		/// This physical system's dictionary of net Forces.
		/// Looking up a Circle will give the netforce exerted on that Circle from within the physical system.
		/// </summary>
		public Dictionary<Circle, OrderedPair> NetForces;
		/// <summary>
		/// 
		/// </summary>
		public Dictionary<Circle, List<Interaction>> InteractionMap;

		public double TimeStep;


		/// <summary>
		/// 
		/// </summary>
		public QuadTree<Circle> Tree;

		/// <summary>
		/// No-arg constructor to create a new physical system.  Inits lists, sets GravityOn to default.
		/// </summary>
		public PhysicalSystem(double timeStep = TIME_STEP)
		{
			Circles = new List<Circle>();
			NetForces = new Dictionary<Circle, OrderedPair>();

			Interactions = new List<Interaction>();
			InteractionMap = new Dictionary<Circle, List<Interaction>>();

			Forces = new Dictionary<String, Force>();
			FundamentalForces = new Dictionary<String, Force>();

			TimeStep = timeStep;

			Tree = new QuadTree<Circle>(new FRect(0, 0, 600, 600), 10);
		}

		/// <summary>
		/// Adds a new circle to this physical system.
		/// </summary>
		/// <param name="circle">The new circle to add.</param>
		public void AddCircle(Circle circle)
		{
			Circles.Add(circle);
			NetForces.Add(circle, new OrderedPair(0, 0));
			InteractionMap.Add(circle, new List<Interaction>());
			Tree.Insert(circle);
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
				foreach (Interaction interaction in associations)
				{
					Circle other = interaction.GetOther(circle);
					InteractionMap[other].Remove(interaction);
					Interactions.Remove(interaction);
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
			foreach (Circle circle in Circles)
			{
				UpdateNetForceOn(circle);
				double ax = NetForces[circle].X / circle.Mass;
				double ay = NetForces[circle].Y / circle.Mass;
				circle.SetAcceleration(ax, ay);
			}
		}

		/// <summary>
		/// Calculates the net Forces acting on a circle.
		/// </summary>
		/// <param name="circle">The circle to calculate net Forces for.</param>
		/// <returns>The net Forces on the given circle.</returns>
		public void UpdateNetForceOn(Circle circle)
		{
			OrderedPair f;
			double fx = 0;
			double fy = 0;
			foreach (Interaction interaction in InteractionMap[circle])
			{
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
		}

		public void UpdatePositions()
		{
			double time = TimeStep;
			bool overlapped = false;
			while (time > 0)
			{
				UpdateNetForces();
				Dictionary<Circle, List<Circle>> collisionMap = new Dictionary<Circle, List<Circle>>();
				double min = CheckCollisions(ref collisionMap, time, overlapped);
				overlapped = (min == 0);
				UpdateCircles(min, collisionMap);
				time -= min;
			}
		}

		public double CheckCollisions(ref Dictionary<Circle, List<Circle>> collisionMap,
			double time, bool overlapped)
		{
			double min = Double.PositiveInfinity;
			foreach (Circle circle in Circles)
			{
				List<Circle> collisions = new List<Circle>();
				foreach (Circle that in Circles) // use quad Tree to get the list of Circles to check against
				{
					double value = circle.Colliding(that, time);

					if (!Double.IsInfinity(value))
					{
						value = Math.Round(value, 8);
						if (value == 0 && overlapped)
						{
						}
						else if (value < min)
						{
							min = value;
							collisionMap = new Dictionary<Circle, List<Circle>>();
							collisions = new List<Circle>();
							collisionMap.Add(circle, collisions);
							collisions.Add(that);
						}
						else if (value == min)
						{
							if (collisionMap != null && !collisionMap.ContainsKey(circle))
							{
								collisionMap.Add(circle, collisions);
							}
							collisions.Add(that);
						}
					}
				}
			}
			if (Double.IsInfinity(min))
			{
				min = time;
			}
			return min;
		}

		public void UpdateCircles(double time,
			Dictionary<Circle, List<Circle>> collisionMap)
		{
			foreach (Circle circle in Circles)
			{
				circle.Update(time);
			}
			UpdateVelocities(collisionMap);
		}

		public void UpdateVelocities(Dictionary<Circle, List<Circle>> collisionMap)
		{
			Dictionary<Circle, OrderedPair> velocityMap = new Dictionary<Circle, OrderedPair>();
			foreach (Circle circle in collisionMap.Keys)
			{
				velocityMap.Add(circle, circle.CollideWith(collisionMap[circle]));
			}
			foreach (Circle circle in velocityMap.Keys)
			{
				Out.WriteLine(circle.Velocity + " => " + velocityMap[circle]);
				circle.SetVelocity(velocityMap[circle].X, velocityMap[circle].Y);
			}
			if (velocityMap.Keys.Count % 2 == 1)
			{
				Out.WriteLine("" + velocityMap.Keys.Count);
			}
		}

		public void Update()
		{
			UpdatePositions();
		}
	}
}