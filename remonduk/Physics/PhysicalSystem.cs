using System;
using System.Collections.Generic;
using Remonduk.Physics.CollisionPruning;

namespace Remonduk.Physics
{
	/// <summary>
	/// The physical system represents the current existence the Interactions are taking place in.  
	/// It contains all of the Circles and Interactions.
	/// </summary>
	public class PhysicalSystem
	{
		/// <summary>
		/// 
		/// </summary>
		public const double WIDTH = 800;
		/// <summary>
		/// 
		/// </summary>
		public const double HEIGHT = 800;
		/// <summary>
		/// 
		/// </summary>
		public const double TIME_STEP = 1;
		/// <summary>
		/// 
		/// </summary>
		public const double MAX_SPEED = 150;

		/// <summary>
		/// 
		/// </summary>
		public List<Circle> Circles;
		/// <summary>
		/// 
		/// </summary>
		public Dictionary<String, Force> Forces;

		/// <summary>
		/// A list of all of the Interactions in this physical system.
		/// Iterate over this list to determine accelerations for all circles.
		/// </summary>
		public List<Interaction> Interactions;
		/// <summary>
		/// 
		/// </summary>
		public Dictionary<Circle, List<Interaction>> InteractionMap;

		/// <summary>
		/// 
		/// </summary>
		public double TimeStep;
		/// <summary>
		/// 
		/// </summary>
		public OrderedPair Dimensions;
		/// <summary>
		/// 
		/// </summary>
		public QuadTree Tree;

		/// <summary>
		/// The max speed a circle in the Physical System can travel.
		/// </summary>
		public double MaxSpeed;

		/// <summary>
		/// No-arg constructor to create a new physical system.  Inits lists, sets GravityOn to default.
		/// </summary>
		public PhysicalSystem(double width = WIDTH, double height = HEIGHT, double timeStep = TIME_STEP, double maxSpeed = MAX_SPEED)
		{
			Circles = new List<Circle>();
			Forces = new Dictionary<String, Force>();
			Interactions = new List<Interaction>();
			InteractionMap = new Dictionary<Circle, List<Interaction>>();

			TimeStep = timeStep;
			Dimensions = new OrderedPair(width, height);
			Tree = new QuadTree(this);
		}

		/// <summary>
		/// Adds a new circle to this physical system.
		/// </summary>
		/// <param name="circle">The new circle to add.</param>
		public void AddCircle(Circle circle)
		{
			Circles.Add(circle);
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
				foreach (Interaction interaction in InteractionMap[circle])
				{
					Circle other = interaction.GetOther(circle);
					InteractionMap[other].Remove(interaction);
					Interactions.Remove(interaction);
				}
				Tree.Remove(circle);
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
			InteractionMap[interaction.First].Add(interaction);
			InteractionMap[interaction.Second].Add(interaction);
		}

		/// <summary>
		/// Removes an interaction from this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to remove.</param>
		public bool RemoveInteraction(Interaction interaction)
		{
			if (Interactions.Remove(interaction))
			{
				InteractionMap[interaction.First].Remove(interaction);
				return InteractionMap[interaction.Second].Remove(interaction);
			}
			return false;
		}

		// CollisionVelocities
		public void CollisionVelocities(Dictionary<Circle, List<Circle>> collisionMap)
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

		public void UpdateCircles(double time,
			Dictionary<Circle, List<Circle>> collisionMap)
		{
			foreach (Circle circle in Circles)
			{
				circle.Update(time);
				Tree.Move(circle);
			}
			CollisionVelocities(collisionMap);
		}

		public void UpdateCollisionMap(ref Dictionary<Circle, List<Circle>> collisionMap,
			ref List<Circle> collisions, Circle circle, Circle that)
		{
			if (/*collisionMap != null && */!collisionMap.ContainsKey(circle))
			{
				collisionMap.Add(circle, collisions);
			}
			if (!collisionMap[circle].Contains(that))
			{
				collisionMap[circle].Add(that);
			}
			// check to see if the collision is already in the dictionary

			if (!collisionMap.ContainsKey(that))
			{
				collisionMap.Add(that, new List<Circle>());
			}
			if (!collisionMap[that].Contains(circle))
			{
				collisionMap[that].Add(circle);
			}
		}

		public void NewCollisionMap(ref Dictionary<Circle, List<Circle>> collisionMap,
			ref List<Circle> collisions, Circle circle, Circle that)
		{
			collisionMap = new Dictionary<Circle, List<Circle>>();
			collisions = new List<Circle>();
			collisionMap.Add(circle, collisions);
			collisions.Add(that);
		}

		public double FirstCollision(ref Dictionary<Circle, List<Circle>> collisionMap,
			double time, bool overlapped, Circle circle, double min)
		{
			List<Circle> collisions = new List<Circle>();
			foreach (Circle that in Tree.Possible(circle, time))
			//foreach (Circle that in Circles)
			{
				double collisionTime = circle.Colliding(that, time);
				if (!Double.IsInfinity(collisionTime) && (collisionTime != 0 || !overlapped))
				{
					double delta = collisionTime - min;
					if (delta < -Constants.EPSILON)
					{
						min = collisionTime;
						NewCollisionMap(ref collisionMap, ref collisions, circle, that);
					}
					else if (Math.Abs(delta) <= Constants.EPSILON)
					{
						UpdateCollisionMap(ref collisionMap, ref collisions, circle, that);
					}
				}
			}
			return min;
		}

		public double FirstCollision2(ref Dictionary<Circle, List<Circle>> collisionMap,
			double time, bool overlapped, Circle circle, double min)
		{
			List<Circle> collisions = new List<Circle>();
			//foreach (Circle that in Tree.Possible(circle, time))
			foreach (Circle that in Circles)
			{
				double collisionTime = circle.Colliding(that, time);
				if (!Double.IsInfinity(collisionTime) && (collisionTime != 0 || !overlapped))
				{
					double delta = collisionTime - min;
					if (delta < -Constants.EPSILON)
					{
						min = collisionTime;
						NewCollisionMap(ref collisionMap, ref collisions, circle, that);
					}
					else if (Math.Abs(delta) <= Constants.EPSILON)
					{
						UpdateCollisionMap(ref collisionMap, ref collisions, circle, that);
					}
				}
			}
			return min;
		}

		public double CheckCollisions(ref Dictionary<Circle, List<Circle>> collisionMap,
			double time, bool overlapped)
		{
			double min = Double.PositiveInfinity;
			foreach (Circle circle in Circles)
			{
				min = FirstCollision(ref collisionMap, time, overlapped, circle, min);
			}
			if (Double.IsInfinity(min))
			{
				min = time;
			}
			return min;
		}

		/// <summary>
		/// Calculates the net Forces acting on a circle.
		/// </summary>
		/// <param name="circle">The circle to calculate net Forces for.</param>
		/// <returns>The net Forces on the given circle.</returns>
		public void CalculateAcceleration(Circle circle)
		{
			OrderedPair f;
			double fx = 0;
			double fy = 0;
			List<Interaction> removable = new List<Interaction>();
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
				if (f == null)
				{
					removable.Add(interaction);
				}
				else
				{
					fx += f.X;
					fy += f.Y;
				}
			}
			foreach (Interaction interaction in removable)
			{
				RemoveInteraction(interaction);
			}
			circle.SetAcceleration(fx / circle.Mass, fy / circle.Mass);
		}

		/// <summary>
		/// Updates circle velocities based on the netforces in the dictionary.
		/// </summary>
		public void CalculateAccelerations()
		{
			foreach (Circle circle in Circles)
			{
				CalculateAcceleration(circle);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Update()
		{
			double time = TimeStep;
			bool overlapped = false;
			while (time > 0)
			{
				CalculateAccelerations();
				Dictionary<Circle, List<Circle>> collisionMap = new Dictionary<Circle, List<Circle>>();
				double min = CheckCollisions(ref collisionMap, time, overlapped);
				overlapped = (min == 0);

				UpdateCircles(min, collisionMap);
				time -= min;
			}
		}
	}
}