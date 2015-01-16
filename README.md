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

target: platformer genre

Remonduck

basic pool of forces
	1] electromagnetic (em)
	2] spring
	3] constant
	
	-> implement damping forces so system doesn't blow up

for juliono:

	tethers have a max distance they can extend
	update comments
	add file comments at top

	tethers are like springs - try to keep at an equilibrium point -
	pull when too far away - push when too close

	things that don't move
	anchor them to boundaries
	world boundaries

	randomize tests
	optimize code
	finish test cases
	change lists to hashsets where it makes sense
	
	
have the ONLY force be spring and gravity

if the spring force is what holds everything together,
	we should at least design the system to handle that properly

partial ghostiness? what if a circle can pass through some things, but not others
	ie. pass through people, but not through environment

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