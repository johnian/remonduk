using System;

namespace remonduk {
    /// <summary>
    /// Constants to be used by the PhysicalSystem.  Eventually to be loaded from a file.
    /// </summary>
	public class Constants {

        /// <summary>
        /// The singleton instance of the Constants.
        /// </summary>
        public static Constants instance;

        /// <summary>
        /// Magnitude used for calculating gravity.
        /// </summary>
		public double GRAVITY = 1;
        /// <summary>
        /// Angle used for calculating gravity.
        /// </summary>
		public double GRAVITY_ANGLE = Math.PI / 2.0;
        /// <summary>
        /// Global flag for enabling or diasbling gravity.
        /// </summary>
        public bool GRAVITY_ACTIVE = false;

        /// <summary>
        /// The default radius for a circle..
        /// </summary>
        public double DEFAULT_RADIUS = 5;
        /// <summary>
        /// The minimum radius for a circle.
        /// </summary>
        public double MIN_RADIUS = 1;
        /// <summary>
        /// The maximum radius for a circle.
        /// </summary>
        public double MAX_RADIUS = 40;
        /// <summary>
        /// Forces all circles to have the same size radius.
        /// </summary>
        public bool STATIC_RADIUS = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Constants()
        { }

        /// <summary>
        /// Returns the singleton instance of constants or creates one if this is the first call.
        /// </summary>
        public static Constants Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Constants();
                }
                return instance;
            }
        }

        
	}
}