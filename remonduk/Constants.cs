using System;

namespace remonduk {
	public class Constants {

        public static Constants instance;

		public double GRAVITY = 1;
		public double GRAVITY_ANGLE = Math.PI / 2.0;
        public bool GRAVITY_ACTIVE = false;

        public double DEFAULT_RADIUS = 5;
        public double MIN_RADIUS = 1;
        public double MAX_RADIUS = 40;
        public bool STATIC_RADIUS = false;

        public Constants()
        { }

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