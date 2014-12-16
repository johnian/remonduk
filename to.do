implementation / gui cycle / refactor & optimize

# FLUID
=======
Flewis
Floid

should not be hyper realistic

rolling versus sliding -- tell the engine which way to render it
gravity
normal force versus surface tension

rotation should be purely graphical?
	doesn't affect any drawing calculations
		only roll when object is a single circle
		
gravitational point -- can be a specific point
	can be the boundaries -- should not inherently be a built in feature because
	it assumes the world will be drawn a certain way
	
	-- gravity calculator can be given a new function so you can make custom gravity
	-- decreases as cubic, decreases as linear, increases with distance
	
	attracting (gravity)
	repelling
	
	orbital platformer
	
	line of sight
	flag for whether the stage wraps around or has hard boundaries

2D {
	1 {
-		circles {
			group circles {
				// linked list - set a short leash for synchronous motion
-				chasing "leader"
-				tail "leader"
				move synchronously
			}
			fuse circles {
				add masses together
					percentage parameter to determine how much mass is absorbed
			}
		}
		physics {
			motion {
				position
				velocity
				acceleration {
					gravity {
						how to handle gravity - like to handle in the most organic way possible
					}
					friction
				}
			}
			collision detection {
				normal force
				surface tension
			}
		}
		basic i/o
	}

	2 {
		development gui
		data
		state manager {
			windows
			menus
		}
	}
		
	3 {
		physics {
			mass
			density
			momentum
			inertia
			elasticity
		}
		color
		stroke width
		intermediate i/o
	}
		
	4:
		development gui
	
	5:
		prettification
		
	:
		advanced i/o
			hotkey rebinding
			"self" selection
}


content management
loading
activating textures / applying

animating sprites


2D

lighting
shadows
particle effects

game engine {
	graphics {
		circles {
			grouping circles (link / unlink)
			fusing circles (construct / deconstruct)
			
			physics {
				position - cartesian
				velocity
				gravity / acceleration
				
				collision detection
				
				mass
				density
				
				momentum / inertia
				friction
				
				elasticity
				// fluid dynamics
			}
			motion
				following
				move together
					// lighting / shading
		color
		stroke width
		textures
	}
	time
		
		state manager
			windows / menus
			
	data
	
	development gui
		how should creating objects be handled
			explicitly tell the engine to draw specific linked objects
			or draw a curve / bounded shape and fill in details appropriately
			

	
	i/o

	sound	
	
	network
		social networking / leaderboards
		online multiplayer
		add ons / downloads
}


moveRoute class to store following information
	and handle following movement