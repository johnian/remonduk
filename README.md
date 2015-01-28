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

for juliono:

1] optimize collision detection by removing redundant checks
2] refactor tether to be spring
3] refactor gravity to be electromagnetic?? need a better name
4] comments
5] default forces?
6] additional quad tree tests
7] anchors
8] randomize tests where possible
9] multiple quad trees, remove 'exists' from circle

implement a max velocity for the world
- utilize this in quad tree

store qtrees as an array

flag for whether the stage wraps around or has hard boundaries

circle should have a friction constant alongside elasticity
elasticity reduces the total magnitude, so the angle is unaffected
friction determines the angle of impact,
and then reduces the x and y values separately according to the angle of impact

should be able to have multiple quad trees
	- that way we can have only certain things interact with each other
		- bullets only collide with bullets
should only have circles contained in smallest containing node
	- if something falls on a boundary, it'll be contained in the parent node
	
when placing circles in the quad tree, place them with a modified size based on max velocity
max velocity - 128 -> every frame can move 128, + radius
	-> .25 seconds to travel distance of screen
	
figure out a decent max velocity

how to set up different quad trees
dictionary - give it a name:: "Bullets"
-- if we're worried about many layers, find a better way - use arrays

does it make sense to have a game that has certain things that don't collide with each other
things that are purely visual -- weather overlays

different depths / backgrounds

can our game handle things that stick to walls
negative friction value? if you assume -- cause circle to lose all kinetic energy
-.5 plus +.5 cancel to 0 to kill all kinetic energy
-- if negative, cap at value of 0

figure out how inelastic collisions typically work

<div id="1"></div>
# 1 basic graphics and i/o {
	implementation {
		draw circles {
			group circles {
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