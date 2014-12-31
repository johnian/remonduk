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
			fill(circle packing, autobuild tethers)
			compress? - hold together, may just be above
			anchors
			differentiate - either through color or maybe bounding shape or something
		}
		physics {
			friction
			density
			momentum
			inertia
			elasticity
			intermediate collision detection
		}
	}
	gui {
		intermediate i/o {
			group selection
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
