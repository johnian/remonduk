2D
	stage 1:
		circles
			group circles
				following "leader"
				moving synchronously
			fuse circles
		physics
			motion
				position
				velocity
				acceleration (gravity / friction)
			collision detection
		basic i/o

	stage 2:
		development gui
		data
		state manager
			windows / menus	
		
	// stage 3:
	// 	physics
	// 		mass
	// 		density
	// 		momentum / inertia
	// 		elasticity
	// 	color
	// 	stroke width
	// 	intermediate i/o
	// 	
	// stage 4:
	// 	development gui
	// 
	// stage 5:
	// 	prettification
	// 	
	// stage :
	// 	advanced i/o
	// 		hotkey rebinding
	// 		"self" selection
	// 
	// 
	// stage :



content management
loading
activating textures / applying

animating sprites


2D

lighting
shadows
particle effects


**Fluid
**Flircle

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