implementation / gui / refactor & optimize / testing

# FLUID / ATOMIC
================
Flewis
Floid

should not be hyper realistic

gravitational point -- can be a specific point
	can be the boundaries -- should not inherently be a built in feature because
	it assumes the world will be drawn a certain way
	
	-- gravity calculator can be given a new function so you can make custom gravity
	-- decreases as cubic, decreases as linear, increases with distance
	
	flag for whether the stage wraps around or has hard boundaries

# 1 basic graphics and i/o {
	implementation {
		draw circles {
			group circles {
				chasing "leaders"
				dragging "followers"
				group motion
			}
		}
		physics {
			motion {
				position
				velocity
				acceleration
				gravity	
			}
			mass
			basic collision detection
		}
		basic i/o
	}
	gui {
		content management
		loading
	}
	refactor & optimize {
	
	}
	testing {
	
	}
}

# 2 basic physics {
	implementation {
		groups {
			
		}
		physics {
			friction
			forces
			density
			momentum
			inertia
			elasticity
			intermediate collision detection
		}
		intermediate i/o
	}
	gui {
	
	}
	refactor & optimize {
	
	}
	testing {
	
	}
}

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

# 5 advanced graphics {
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

# 7 sound {
	
}

# 8 networking {
	social networking / leaderboards
	online multiplayer
	add ons / downloads
}