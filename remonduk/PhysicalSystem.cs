using System;
using System.Linq;
using System.Collections.Generic;
using remonduk.QuadTreeTest;

namespace remonduk
{
	/// <summary>
	/// The physical system represents the current existence the interactions are taking place in.  
	/// It contains all of the circles and interactions.
	/// </summary>
	public class PhysicalSystem
	{
		/// <summary>
		/// This physical system's dictionary of net forces.
		/// Looking up a circle will give the netforce exerted on that circle from within the physical system.
		/// </summary>
		public Dictionary<Circle, OrderedPair> netForces;
		public List<Circle> circles;
		/// <summary>
		/// A list of all of the interactions in this physical system.
		/// </summary>
		public List<Interaction> interactions; // ideally, i'd like to index into this by circle to get interactions associated with that circle
		public Dictionary<Circle, List<Interaction>> interactionMap;

		public Dictionary<String, Force> forces;

		public QuadTree<Circle> tree;

		/// <summary>
		/// Default value for gravity.
		/// </summary>
		public const bool GRAVITY = true;
		/// <summary>
		/// Global flag for gravity in this physical system.
		/// </summary>
		public bool gravity;

		/// <summary>
		/// No-arg constructor to create a new physical system.  Inits lists, sets gravity to default.
		/// </summary>
		public PhysicalSystem()
		{
			circles = new List<Circle>();
			netForces = new Dictionary<Circle, OrderedPair>();
			interactions = new List<Interaction>();
			interactionMap = new Dictionary<Circle, List<Interaction>>();

			forces = new Dictionary<String, Force>();

			tree = new QuadTree<Circle>(new FRect(0, 0, 600, 600), 10);

			gravity = GRAVITY;
			forces.Add("Gravity", new Gravity(Gravity.GRAVITY, Gravity.ANGLE));
		}

		/// <summary>
		/// Adds a new circle to this physical system.
		/// </summary>
		/// <param name="circle">The new circle to add.</param>
		public void addCircle(Circle circle)
		{
			//Out.WriteLine("1");
			circles.Add(circle);
			//Out.WriteLine("2");
			// need to figure out how to handle gravity
			if (false /*gravity*/)
			{
				//Out.WriteLine("3");
				interactions.Add(new Interaction(circle, null, forces["Gravity"]));
			}

			//Out.WriteLine("4");
			netForces.Add(circle, new OrderedPair(0, 0));

			//Out.WriteLine("5");
			interactionMap.Add(circle, new List<Interaction>());
			//Out.WriteLine("6");

			// why does this throw a null pointer exception
			tree.Insert(circle.q_tree_pos);
			//Out.WriteLine("7");
		}

		/// <summary>
		/// Removes a circle from this physical system.
		/// </summary>
		/// <param name="circle">The circle to remove from this system.</param>
		public bool removeCircle(Circle circle)
		{
			if (circles.Remove(circle))
			{
				netForces.Remove(circle);
				List<Interaction> associations = interactionMap[circle];
				foreach (Interaction interaction in associations)
				{
					Circle other = interaction.getOther(circle);
					interactionMap[other].Remove(interaction);
					interactions.Remove(interaction);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Adds an interaction to this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to add.</param>
		public void addInteraction(Interaction interaction)
		{
			interactions.Add(interaction);
			// do a try get value here, else create a new list, then add
			interactionMap[interaction.first].Add(interaction);
			interactionMap[interaction.second].Add(interaction);
		}

		/// <summary>
		/// Removes an interaction from this physical system.
		/// </summary>
		/// <param name="interaction">The interaction to remove.</param>
		public void removeInteraction(Interaction interaction)
		{
			interactions.Remove(interaction);
			interactionMap[interaction.first].Remove(interaction);
			interactionMap[interaction.second].Remove(interaction);
		}

		/// <summary>
		/// Updates circle velocities based on the netforces in the dictionary.
		/// </summary>
		public void updateNetForces()
		{
			for (int i = 0; i < netForces.Count; i++)
			{
				Circle circle = netForces.ElementAt(i).Key;
				//Out.WriteLine(circle.GetHashCode() + "");

				updateNetForceOn(circle);
				//netForces[circle] = updateNetForceOn(circle);
				double ax = netForces[circle].x / circle.mass;
				double ay = netForces[circle].y / circle.mass;
				//Out.WriteLine(netForces[circle] + "");
				circle.setAcceleration(ax, ay);
			}
		}

		public void update()
		{
			updateNetForces();
			updatePositions();
		}

		/// <summary>
		/// Calculates the net forces acting on a circle.
		/// </summary>
		/// <param name="circle">The circle to calculate net forces for.</param>
		/// <returns>The net forces on the given circle.</returns>
		public void updateNetForceOn(Circle circle)
		{
			double fx = 0;
			double fy = 0;
			//Out.WriteLine(circle.GetHashCode() + "");
			foreach (Interaction interaction in interactionMap[circle])
			{
				OrderedPair f;
				if (interaction.first == circle)
				{
					f = interaction.forceOnFirst();
				}
				else
				{
					f = interaction.forceOnSecond();
				}
				fx += f.x;
				fy += f.y;
			}
			netForces[circle].set(fx, fy);
			//Out.WriteLine("f: " + fx + ", " + fy);
		}

		public void updatePositions()
		{
			Dictionary<Circle, OrderedPair>.KeyCollection circles = netForces.Keys;
			double time = 1;
			Dictionary<Circle, List<Circle>> collisionMap;
			while (time > 0)
			{
				collisionMap = new Dictionary<Circle, List<Circle>>();
				double min = Double.PositiveInfinity;
				foreach (Circle circle in circles)
				{
					List<Circle> collisions = new List<Circle>();
					foreach (Circle that in circles)
					{
						// use quad tree to get the list of circles to check against
						// for now, just use circles
						
						double value = circle.colliding(that, time);
						//Out.WriteLine("collision time for " + this.GetHashCode() + "> " + that.GetHashCode() + ": " + value);
						if (!Double.IsInfinity(value))
						{
							if (value < min)
							{
								min = value;
								collisionMap = new Dictionary<Circle, List<Circle>>();
								collisions = new List<Circle>();
								collisionMap.Add(circle, collisions);
								collisions.Add(that);
							}
							else if (value == min)
							{
								collisionMap.Add(circle, collisions);
								collisions.Add(that);
							}
						}
						//Out.WriteLine("value: " + value);
						//Out.WriteLine("min: " + min);
					}
				}
				if (Double.IsInfinity(min))
				{
					min = time;
				}
				foreach (Circle circle in circles)
				{
					circle.update(min);
				}
				updateVelocities(collisionMap);
				time -= min;
				//Out.WriteLine("time: " + time);
				//Out.WriteLine("number of collisions: " + collisionMap.Count + "");
			}
		}

		public void updateVelocities(Dictionary<Circle, List<Circle>> collisionMap)
		{
			Dictionary<Circle, OrderedPair> velocityMap = new Dictionary<Circle, OrderedPair>();
			foreach (Circle circle in collisionMap.Keys)
			{
				velocityMap.Add(circle, circle.collideWith(collisionMap[circle]));

				//Out.WriteLine("updated velocity " + circle.GetHashCode() + " " + velocityMap[circle]);
			}
			foreach (Circle circle in velocityMap.Keys)
			{
				//Out.WriteLine(velocityMap[circle] + "");
				circle.setVelocity(velocityMap[circle].x, velocityMap[circle].y);
			}
		}
	}
}