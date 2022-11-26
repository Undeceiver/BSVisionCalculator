namespace BSVisionCalculator
{
    public class PlayerPosture
    {
        // Don't change these...
        public double x;
        public double y;

        public static PlayerPosture CENTRED = new PlayerPosture(GlobalParameters.width_centre, 0);
        public static PlayerPosture LEFT = new PlayerPosture(GlobalParameters.width_centre - GlobalParameters.width_lean_default, 0);
        public static PlayerPosture RIGHT = new PlayerPosture(GlobalParameters.width_centre + GlobalParameters.width_lean_default, 0);
        public static PlayerPosture SQUAT = new PlayerPosture(GlobalParameters.width_centre, - GlobalParameters.height_squat_default);
        public static PlayerPosture LEFT_SQUAT = new PlayerPosture(GlobalParameters.width_centre - GlobalParameters.width_squatlean_default, -GlobalParameters.height_squat_default);
        public static PlayerPosture RIGHT_SQUAT = new PlayerPosture(GlobalParameters.width_centre + GlobalParameters.width_squatlean_default, -GlobalParameters.height_squat_default);

        private PlayerPosture(double x, double y) // y is relative to camera position
        {
            this.x = x;
            this.y = y;
        }

        public static PlayerPosture fromString(String posture_str)
        {
            if(posture_str == "CENTRED")
            {
                return CENTRED;
            }
            else if(posture_str == "LEFT")
            {
                return LEFT;
            }
            else if(posture_str == "RIGHT")
            {
                return RIGHT;
            }
            else if(posture_str == "SQUAT")
            {
                return SQUAT;
            }
            else if(posture_str == "LEFT_SQUAT")
            {
                return LEFT_SQUAT;
            }
            else if(posture_str == "RIGHT_SQUAT")
            {
                return RIGHT_SQUAT;
            }
            else
            {
                throw new Exception("Posture description could not be matched!");
            }
        }

        public static PlayerPosture[] getPostures()
        {
            return new PlayerPosture[] { CENTRED, LEFT, RIGHT, SQUAT, LEFT_SQUAT, RIGHT_SQUAT };
        }
    }
}
