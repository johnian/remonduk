using System;

namespace remonduk {
	public class Constants {

        public static Constants instance;

		public float GRAVITY = 9.8F;
		public float GRAVITY_ANGLE = (float)Math.PI / 2.0F;
        public bool GRAVITY_ACTIVE = false;

        public float DEFAULT_RADIUS = 5F;
        public float MIN_RADIUS = 1F;
        public float MAX_RADIUS = 40F;
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