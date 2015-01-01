implementation / gui / refactor, comment, optimize / testing

# FLUID / ATOMIC
================
Flewis
Floid

[basic graphics and i/o] (#1)  
[basic physics] (#2)  
[intermediate graphics and i/o] (#3)  
[intermediate physics] (#4)  
[advanced graphics and i/o] (#5)  
[advanced physics] (#6)  
[networking] (#7)  
[sound] (#8)  

partial ghostiness? what if a circle can pass through some things, but not others
	ie. pass through people, but not through environment

test classes
make it work with quad tree

game update consists of two events
update accelerations based on forces
update positions of all circles

flag for whether the stage wraps around or has hard boundaries

3 types of physical interactions:
	collision (when actual boundaries are crossed)
	repulsion
	following

	grouping - leashing
		- anchoring (multiple anchoring pairings that connect two circles)

	tests for gui

<div id="1"></div>
# 1 basic graphics and i/o {
	implementation {
		draw circles {
			group circles {
				//chasing "leaders"
				//dragging "followers"
				//group motion
				tethering
			}
		}
		physics {
			motion {
				position
				velocity
				acceleration
				gravity
				forces
			}
			mass
			basic collision detection
		}
		basic i/o
	}
	gui {
		content management {
			create/modify/delete objects
			detailed windows {
				circle
				tether
				physical system {
					constants
				}
			}
		}
		saving/loading
		basic io
	}
	refactor, comment, optimize {
	}
	testing {
		implementation
		gui
		drawing
	}
}

<div id="2"></div>
# 2 basic physics {
	implementation {
		groups {
			fill {
				packing
					same size circles
					variable size
				auto-build tethers
					internal tethers
					"skin" tethers
				}
			compress? - hold together, may just be above
				I'm thinking you're referring to the "skin" here
			anchors
				anchor tethers
			differentiate
				fill color - shading?
				again the "skin" should help here
		}
		physics {
			friction
				sliding, rolling, fluid
				global for air friction, similar to gravity
			density - I think most of this will be handled by the density of the group, otherwise it's just m/v
			momentum - doesn't our kinetics handle this already?
			inertia -  same as above?
			elasticity
			intermediate collision detection
		}
	}
	gui {
		intermediate i/o {
			group selection / drawing
			keyboard shortcuts (delete, copy, paste, etc)
			menu bar {
				ensure all items are accessible
				define names
				shortcuts
			}
		}
		group detailed window
		global forces detailed window
		revamp lists {
			proper text
			multiple selections
			lock/unlock for modifications
		}
		generate frames - option to save each frame for replay
			might want to include a debug output too as in objects and their values
			always save the last # of frames - able to save and review if something looks weird

	}
	refactor & optimize {
		main window
		check for redundant lists
	}
	testing {

	}
}

<div id="3"></div>
# 3 intermediate graphics and i/o {
	implementation {
		intermediate i/o {
			hotkey rebinding
			"self" selection
		}
	}
	gui {

	}
	refactor & optimize {

	}
	testing {

	}
}

<div id="4"></div>
# 4 intermediate physics {
	implementation {

		physics {

			line of sight
		}
	}
	gui {

	}
	refactor & optimize {

	}
	testing {

	}
}

<div id="5"></div>
# 5 advanced graphics and i/o {
	implementation {
		textures
		particle effects
	}
	gui {

	}
	refactor & optimize {

	}
	testing {

	}
}

<div id="6"></div>
# 6 advanced physics {
	implementation {
		physics {
			lighting
			shadows

		}
	}
	gui {

	}
	refactor & optimize {

	}
	testing {

	}
}

<div id="7"></div>
# 7 sound {

}

<div id="8"></div>
# 8 networking {
	social networking / leaderboards
	online multiplayer
	add ons / downloads
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using remonduk.QuadTreeTest;

namespace remonduk
{
	/// <summary>
	/// Building blocks.
	/// </summary>
	public class Circle
	{
		/// <summary>
		/// Default value for a circle's radius.
		/// </summary>
		public const double RADIUS = 1;
		/// <summary>
		/// Default value for a circle's mass.
		/// </summary>
		public const double MASS = 1;

		/// <summary>
		/// Default value for the x component of a circle's position.
		/// </summary>
		public const double PX = 0;
		/// <summary>
		/// Default value for the y component of a circle's position.
		/// </summary>
		public const double PY = 0;
		/// <summary>
		/// Default value for the x component of a circle's velocity.
		/// </summary>
		public const double VX = 0;
		/// <summary>
		/// Default value for the y component of a circle's velocity.
		/// </summary>
		public const double VY = 0;
		/// <summary>
		/// Default value for the x component of a circle's acceleration.
		/// </summary>
		public const double AX = 0;
		/// <summary>
		/// Default value for the y component of a circle's acceleration.
		/// </summary>
		public const double AY = 0;

		public bool EXISTS = true;

		/// <summary>
		/// Default value for a circle's target. Used for non-physics interactions.
		/// </summary>
		public const Circle TARGET = null;
		/// <summary>
		/// Default value for the minimum distance to follow the target at.
		/// </summary>
		public double MIN_DIST = 0;
		/// <summary>
		/// Default value for the maximum distance to follow the target at.
		/// </summary>
		public double MAX_DIST = 0;

		/// <summary>
		/// Default value for a circle's color.
		/// </summary>
		//Need to rework this for loading / saving.  Save to string and load from there
		public Color COLOR = Color.Chartreuse;

		/// <summary>
		/// The circle's radius and mass.
		/// </summary>
		public double radius, mass;
		/// <summary>
		/// The circle's position, velocity, and acceleration vectors.
		/// </summary>
		public OrderedPair position, velocity, acceleration;
		public double px { get { return position.x; } set { position.x = value; } }
		public double py { get { return position.y; } set { position.y = value; } }
		public double vx { get { return velocity.x; } set { velocity.x = value; } }
		public double vy { get { return velocity.y; } set { velocity.y = value; } }
		public double ax { get { return acceleration.x; } set { acceleration.x = value; } }
		public double ay { get { return acceleration.y; } set { acceleration.y = value; } }

		/// <summary>
		/// The circle's target. Used for non-physics interactions.
		/// </summary>
		public Circle target;
		/// <summary>
		/// The minimum distance this circle will follow the target to.
		/// </summary>
		public double min_dist;
		/// <summary>
		/// The maximum distance this circle will follow the target from.
		/// </summary>
		public double max_dist;

		/// <summary>
		/// The circle's color.
		/// </summary>
		public Color color;

		/// <summary>
		/// If this circle exists (if it should interact with other objects in the physical system).
		/// </summary>
		bool exists;

		public QuadTreeTest.QuadTreePositionItem<Circle> q_tree_pos;

		/// <summary>
		/// Empty constructor. No values are set.
		/// </summary>
		public Circle() { }

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="other">The other circle to copy.</param>
		public Circle(Circle that) :
		this(that.radius, that.mass, that.position.x, that.position.y,
			that.velocity.x, that.velocity.y, that.acceleration.x, that.acceleration.y) { }

			public Circle(double radius) :
			this(radius, MASS) { }

			public Circle(double radius, double mass) :
			this(radius, mass, PX, PY) { }

			public Circle(double radius, double mass, double px, double py) :
			this(radius, mass, px, py, VX, VY) { }

			public Circle(double radius, double mass, double px, double py, double vx, double vy) :
			this(radius, mass, px, py, vx, vy, AX, AY) { }

			public Circle(double radius, double mass, double px, double py,
				double vx, double vy, double ax, double ay)
				{
					setRadius(radius);
					setMass(mass);

					setPosition(px, py);
					setVelocity(vx, vy);
					setAcceleration(ax, ay);

					exists = EXISTS;
					follow(TARGET);

					color = COLOR;
					//q_tree_pos = new QuadTreeTest.QuadTreePositionItem<Circle>(this, new Tuple<double, double>(position.x, position.y), new Tuple<double, double>(radius, radius));
				}

				/// <summary>
				///
				/// </summary>
				/// <param name="x"></param>
				/// <param name="y"></param>
				/// <param name="radius"></param>
				/// <param name="mass"></param>
				///
				//public Circle(double x, double y, double radius, double mass = MASS) :
				//	this(x, y, radius,
					//		 VELOCITY, VELOCITY_ANGLE, mass) { }

					///// <summary>
					/////
					///// </summary>
					///// <param name="x"></param>
					///// <param name="y"></param>
					///// <param name="radius"></param>
					///// <param name="velocity"></param>
					///// <param name="velocity_angle"></param>
					///// <param name="mass"></param>
					//public Circle(double x, double y, double radius,
						//	double velocity, double velocity_angle, double mass = MASS) :
						//	this(x, y, radius,
							//		 velocity, velocity_angle,
							//		 ACCELERATION, ACCELERATION_ANGLE, mass) { }

							/// <summary>
							///
							/// </summary>
							/// <param name="x"></param>
							/// <param name="y"></param>
							/// <param name="radius"></param>
							/// <param name="velocity"></param>
							/// <param name="velocity_angle"></param>
							/// <param name="acceleration"></param>
							/// <param name="acceleration_angle"></param>
							/// <param name="mass"></param>
							//public Circle(double x, double y, double radius,
								//	double velocity, double velocity_angle,
								//	double acceleration, double acceleration_angle, double mass = MASS)
								//{
									//	this.x = x;
									//	this.y = y;
									//	setRadius(radius);
									//	setMass(mass);

									//	setVelocity(velocity, velocity_angle);
									//	setAcceleration(acceleration, acceleration_angle);

									//	follow(TARGET);
									//	this.color = Color.Chartreuse;
									//	exists = true;
									//	q_tree_pos = new QuadTreeTest.QuadTreePositionItem<Circle>(this, new Tuple<double, double>(x, y), new Tuple<double, double>(radius, radius));
									//	//forces = new List<Force>();
									//}

									/// <summary>
									/// Sets the radius.
									/// </summary>
									/// <param name="radius">The new value for this circle's radius.</param>
									public void setRadius(double radius)
									{
										if (radius < RADIUS)
										{
											throw new ArgumentException("radius: " + radius);
										}
										this.radius = radius;
									}

									/// <summary>
									/// Set this circles mass.
									/// </summary>
									/// <param name="mass">The value to set this circles mass to.</param>
									public void setMass(double mass)
									{
										this.mass = mass;
									}

									/// <summary>
									///
									/// </summary>
									/// <param name="px"></param>
									/// <param name="py"></param>
									public void setPosition(double px, double py)
									{
										position.x = px;
										position.y = py;
									}

									/// <summary>
									/// Sets this circles velocity.  Recalculates vx and vy values.
									/// </summary>
									/// <param name="velocity">The new velocity vector's magnitude.</param>
									/// <param name="velocity_angle">The new velocity vector's angle.</param>
									//public void setVelocity(double velocity, double velocity_angle)
									//{
										//	vx = velocity * Math.Cos(velocity_angle);
										//	vy = velocity * Math.Sin(velocity_angle);
										//	//I know we went over these once before but why are we rounding here?  For the tests?
										//	if (Math.Round(vx, PRECISION) == 0)
										//	{
											//		vx = 0;
											//	}
											//	if (Math.Round(vy, PRECISION) == 0)
											//	{
												//		vy = 0;
												//	}
												//	this.velocity = magnitude(vx, vy);
												//	this.velocity_angle = angle(vy, vx);
												//}

												public void setVelocity(double vx, double vy)
												{
													velocity.x = vx;
													velocity.y = vy;
												}

												public void changeDirection(double angle)
												{
													velocity.set(velocity.magnitude(), angle);
												}

												/// <summary>
												/// Sets this circles acceleration.  Recalculates ax and ay values.
												/// </summary>
												/// <param name="acceleration">The new acceleration vector's magnitude.</param>
												/// <param name="acceleration_angle">The new acceleration vector's angle.</param>
												//public void setAcceleration(double acceleration, double acceleration_angle)
												//{
													//	ax = acceleration * Math.Cos(acceleration_angle);
													//	ay = acceleration * Math.Sin(acceleration_angle);
													//	//again I know we went over these once before but why are we rounding here?  For the tests?
													//	if (Math.Round(ax, PRECISION) == 0)
													//	{
														//		ax = 0;
														//	}
														//	if (Math.Round(ay, PRECISION) == 0)
														//	{
															//		ay = 0;
															//	}
															//	this.acceleration = magnitude(ax, ay);
															//	this.acceleration_angle = angle(ay, ax);
															//}

															public void setAcceleration(double ax, double ay)
															{
																acceleration.x = ax;
																acceleration.y = ay;
															}

															/// <summary>
															/// Sets the target circle for this circle to follow.  Uses default min and max distances.
															/// Somethings a little weird - we should talk about this one.
															/// </summary>
															/// <param name="target">The target circle for this circle to follow.</param>
															public void follow(Circle target = null)
															{
																if (target == null || target == this)
																{
																	this.target = null;
																	this.min_dist = MIN_DIST;
																	this.max_dist = MAX_DIST;
																}
																else
																{
																	follow(target, target.radius + radius, target.radius + radius);
																}
															}

															/// <summary>
															/// Sets a circle for this circle to follow.
															/// </summary>
															/// <param name="target">The target circle this circle should follow.</param>
															/// <param name="min_dist">The minimum distance this circle will start following at.</param>
															/// <param name="max_dist">The maximum distance this circle will start following at.</param>
															public void follow(Circle target, double min_dist, double max_dist)
															{
																if (target == null || target == this)
																{
																	follow();
																}
																else
																{
																	this.target = target;
																	this.min_dist = min_dist;
																	this.max_dist = max_dist;
																}
															}

															/// <summary>
															/// Updates this circles acceleration.  Adds an acceleration vector to the current acceleration.
															/// </summary>
															/// <param name="acceleration">The acceleration vector's magnitude to be added.</param>
															/// <param name="acceleration_angle">The acceleration vector's angle to be added.</param>
															//public void updateAcceleration(double magnitude, double angle)
															//{
																//	// should remove this - acceleration shouldn't be changed, just forces
																//	acceleration.x += magnitude * Math.Cos(angle);
																//	acceleration.y += magnitude * Math.Sin(angle);
																//	//this.acceleration = magnitude(ax, ay);
																//	//this.acceleration_angle = angle(ay, ax);
																//}

																/// <summary>
																/// Updates this circles velocity by adding its acceleration values to velocity values.  Occurs once per frame.
																/// </summary>
																public void updateVelocity(double time)
																{
																	velocity.x += acceleration.x * time;
																	velocity.y += acceleration.y * time;
																	//setVelocity(acceleration.x * time, acceleration.y * time);
																	//vx += ax * time;
																	//vy += ay * time;
																	//velocity = magnitude(vx, vy);
																	//velocity_angle = angle(vy, vx);
																}

																/// <summary>
																/// Updates this circles position based on current velocity and acceleration values.
																/// </summary>
																public void updatePosition(double time)
																{
																	position.x += acceleration.x * time * time / 2 + velocity.x * time;
																	position.y += acceleration.y * time * time / 2 + velocity.y * time;
																	q_tree_pos.Position = new Tuple<double, double>(position.x, position.y);
																	updateVelocity(time);
																}

																public OrderedPair potentialPosition()
																{
																	double potential_x = acceleration.x / 2 + velocity.x + position.x;
																	double potential_y = acceleration.y / 2 + velocity.y + position.y;
																	return new OrderedPair(potential_x, potential_y);
																}

																/// <summary>
																/// The beginings of elastic collisions.
																/// </summary>
																/// <param name="that">The other circle this circle is colliding with.</param>
																/// <returns></returns>
																public bool elastic(Circle that)
																{
																	// should make this take a "combined mass" circle
																	if (!that.exists)
																	return false;
																	//double this_vx = (that.vx * (this.mass - that.mass) + 2 * that.mass * that.vx) / (this.mass + that.mass);
																	//double that_vx = (this.vx * (this.mass - that.mass) + 2 * this.mass * this.vx) / (this.mass + that.mass);
																	//double this_vy = (that.vy * (this.mass - that.mass) + 2 * that.mass * that.vy) / (this.mass + that.mass);
																	//double that_vy = (this.vy * (this.mass - that.mass) + 2 * this.mass * this.vy) / (this.mass + that.mass);
																	//this.setVelocity(Circle.magnitude(this_vx, this_vy), Circle.angle(this_vy, this_vx));
																	//that.setVelocity(Circle.magnitude(that_vx, that_vy), Circle.angle(that_vy, that_vx));

																	// this should only set velocity for "this" because it'll be automatically handled when looping through
																	// also, simultaneous collisions won't work with this version
																	return true;
																}

																/// <summary>
																/// Determines if this circle is colliding with that circle.
																/// </summary>
																/// <param name="that">The circle to check for collision with.</param>
																/// <returns>Returns the angle of impact if colliding.  -1 if not.  </returns>
																public double colliding(Circle that, double time)
																{
																	double center = OrderedPair.angle(that.position.y - position.y, that.position.x - position.x);
																	// use squared instead of square root for efficiency
																	//Check the easy  one first
																	if (distanceSquared(that.position) <= (that.radius + radius) * (that.radius + radius))
																	{
																		Out.WriteLine("overlapping");
																		if (that != this)
																		{
																			return 0; // what do we do in this case
																		}
																		return Double.PositiveInfinity;
																		//return -1;
																	}
																	else
																	{
																		return crossing(that, time);



																		//double reference_angle = Circle.angle(ay / 2 + vy - that.ay / 2 - that.vy, ax / 2 + vx - that.ax / 2 - that.vx);
																		//double direction = Circle.angle(that.y - y, that.x - x);
																		//if neither are moving or if they're moving in opposite directions.
																		//if ((acceleration + velocity == 0 && that.acceleration + that.velocity == 0) ||
																		//	Math.Abs(direction - reference_angle) > Math.PI / 2)
																		//{
																			//	Out.WriteLine("not moving or wrong direction");
																			//	return -1;
																			//}
																			//check if we will cross
																			//double cross = crossing(that);
																			//if (cross != null)
																			//{
																				//	center = Circle.angle(that.y - cross.Item2, that.x - cross.Item1);
																				//	return center;
																				//}
																				//return -1;
																			}
																		}

																		/// <summary>
																		/// Treats that circle as stationary and the frame of perspective as this.
																		/// </summary>
																		/// <param name="that">The circle to check if this circle is crossing.</param>
																		/// <returns>The center of this circle when the two circles collide.</returns>
																		public double crossing(Circle that, double time)
																		{
																			double this_vx = acceleration.x * time / 2 + velocity.x;
																			double this_vy = acceleration.y * time / 2 + velocity.y;
																			double that_vx = that.acceleration.x * time / 2 + that.velocity.x;
																			double that_vy = that.acceleration.y * time / 2 + that.velocity.y;
																			double reference_vx = this_vx - that_vx;
																			double reference_vy = this_vy - that_vy;
																			Out.WriteLine("");
																			Out.WriteLine("reference_vx: " + reference_vx);
																			Out.WriteLine("reference_vy: " + reference_vy);

																			OrderedPair point = closestPoint(that.position.x, that.position.y, reference_vx, reference_vy);
																			if (point == null)
																			{
																				Out.WriteLine("point is null");
																				return Double.PositiveInfinity;
																				//return null;
																			}
																			double distance_from_that = OrderedPair.magnitude(point.x - that.position.x, point.y - that.position.y);
																			double radii_sum = that.radius + radius;

																			Out.WriteLine("distance_from_that: " + distance_from_that);
																			Out.WriteLine("radii_sum: " + radii_sum);
																			if (distance_from_that > radii_sum)
																			{
																				Out.WriteLine("too far");
																				return Double.PositiveInfinity;
																				//return null;
																			}
																			else
																			{
																				double distance_from_collision_squared = radii_sum * radii_sum - distance_from_that * distance_from_that;
																				double distance_from_collision = Math.Sqrt(distance_from_collision_squared);

																				double reference_velocity = OrderedPair.magnitude(reference_vx, reference_vy);
																				double collision_x = point.x - distance_from_collision * reference_vx / reference_velocity;
																				double collision_y = point.y - distance_from_collision * reference_vy / reference_velocity;
																				double time_x = collision_x / reference_vx;
																				double time_y = collision_y / reference_vy;
																				if (time_x >= 0 && time_x == time_y)
																				{
																					return time_x;
																				}
																				else
																				{
																					return Double.PositiveInfinity;
																				}
																				//return Tuple.Create(collision_x, collision_y);
																			}
																		}

																		/// <summary>
																		/// Calculates the closest point on the this' movement vector to that.
																		/// </summary>
																		/// <param name="that_x">That circles x value.</param>
																		/// <param name="that_y">That circles y value</param>
																		/// <param name="reference_vx">This circles reference vx.</param>
																		/// <param name="reference_vy">This circles reference vy.</param>
																		/// <returns>The closest point on this' movement vector to that_x and that_y</returns>
																		public OrderedPair closestPoint(double that_x, double that_y,
																			double reference_vx, double reference_vy)
																			{
																				double x = position.x;
																				double y = position.y;
																				double constant_1 = reference_vy * x - reference_vx * y;
																				double constant_2 = reference_vx * that_x + reference_vy * that_y;

																				double determinant = reference_vx * reference_vx + reference_vy * reference_vy;

																				double intersection_x;
																				double intersection_y;
																				if (determinant == 0)
																				{
																					intersection_x = x;
																					intersection_y = y;
																				}
																				else
																				{
																					intersection_x = (constant_1 * reference_vy + constant_2 * reference_vx) / determinant;
																					intersection_y = (constant_2 * reference_vy - constant_1 * reference_vx) / determinant;
																				}

																				Out.WriteLine("intersection_x: " + intersection_x);
																				Out.WriteLine("intersection_y: " + intersection_y);

																				double delta_x = intersection_x - x;
																				double delta_y = intersection_y - y;
																				if (OrderedPair.magnitude(intersection_x - x, intersection_y - y) >
																				OrderedPair.magnitude(reference_vx, reference_vy))
																				{
																					Out.WriteLine("too far");
																					return null;
																				}
																				// have this also return the time of impact, double between [0, 1]
																				// this might automatically take care of the above case because time would be > 1
																				return new OrderedPair(intersection_x, intersection_y);
																			}

																			public OrderedPair collideWith(List<Circle> circles)
																			{
																				double total_mass = 0;
																				double total_vx = 0;
																				double total_vy = 0;
																				foreach (Circle circle in circles)
																				{
																					total_mass += circle.mass;
																					total_vx += circle.velocity.x;
																					total_vy += circle.velocity.y;
																				}


																				double vx = (total_vx * (mass - total_mass) + 2 * total_mass * total_vx) / (mass + total_mass);
																				double vy = (total_vy * (mass - total_mass) + 2 * total_mass * total_vy) / (mass + total_mass);

																				return new OrderedPair(vx, vy);
																				//setVelocity(vx, vy);

																				//return velocity;
																				//elastic(new Circle(0, 0, 1, total_mass))
																			}

																			/// <summary>
																			/// This circles move method.  Updates the velocity, checks for collisions then updates the (x,y) coordinates.
																			/// </summary>
																			/// <param name="time">For some reason a silly keycollection of circles, change me.</param>
																			public void move(double time)
																			{
																				if (target != null)
																				{
																					// changeDirection(target.y - y, target.x - x);
																					velocity.set(velocity.magnitude(), position.angle(target.position));
																				}
																				//if (this.exists)
																				//{
																					//	foreach (Circle c in circles)
																					//	{
																						//		if (colliding(c) >= 0)
																						//		{
																							//			elastic(c);
																							//		}
																							//	}
																							//}
																							updatePosition(time);
																						}

																						/// <summary>
																						/// Calculates the distance from this circle to that circle.
																						/// </summary>
																						/// <param name="other">The other circle to calculate distance to.</param>
																						/// <returns>The distance to the other circle.</returns>
																						public double distance(Circle other)
																						{
																							return position.magnitude(other.position);
																						}

																						public double distanceSquared(OrderedPair that)
																						{
																							double distx = that.x - position.x;
																							double disty = that.y - position.y;
																							return distx * distx + disty * disty;
																						}

																						/// <summary>
																						/// This circles update method.  See move()
																						/// </summary>
																						/// <param name="circles">Some silly keycollection...change me</param>
																						//revisit List for refactorization!!!!! rar i like my keyboard this
																						public void update(double time)
																						{
																							move(time);
																						}

																						/// <summary>
																						/// Draws this circle.
																						/// </summary>
																						/// <param name="g">The graphics object to draw this circle on.</param>
																						public void draw(Graphics g)
																						{
																							Brush brush = new SolidBrush(color);
																							g.FillEllipse(brush, (float)(position.x - radius), (float)(position.y - radius), (float)(2 * radius), (float)(2 * radius));
																						}

																						/// <summary>
																						/// This circles to string method.
																						/// </summary>
																						/// <returns>
																						/// (523, 316) radius: 5 mass: 1
																						/// velocity: 0 (0, 0): 0
																						/// acceleration: 0 (0, 0): 0
																						/// target: [0, 0
																						/// color: Color [Chartreuse]
																						/// </returns>
																						public String toString()
																						{
																							return "radius: " + radius + ", mass: " + mass + "\n" +
																							"position: " + position + "\n" +
																							"velocity: " + velocity + "\n" +
																							"acceleration: " + acceleration + "\n" +
																							"target: " + target + "[" + min_dist + ", " + max_dist + "]\n" +
																							"color: " + color;
																						}
																					}
																				}

																				// need to handle following differently
																				// make crossing check if following target first, if so, update velocity first
