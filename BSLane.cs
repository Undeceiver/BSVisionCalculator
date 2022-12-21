namespace BSVisionCalculator
{
    // This contains standard lanes
    public class BSLane
    {
        public double width; // This is the centre of the lane, where the bloq is

        public static BSLane FAR_LEFT = new BSLane(0);
        public static BSLane NEAR_LEFT = new BSLane(1);
        public static BSLane NEAR_RIGHT = new BSLane(2);
        public static BSLane FAR_RIGHT = new BSLane(3);        

        public BSLane(double width)
        {
            this.width = width;
        }

        public static BSLane fromString(String lane_str)
        {
            if (lane_str == "FAR_LEFT")
            {
                return FAR_LEFT;
            }
            else if (lane_str == "NEAR_LEFT")
            {
                return NEAR_LEFT;
            }
            else if (lane_str == "NEAR_RIGHT")
            {
                return NEAR_RIGHT;
            }
            else if (lane_str == "FAR_RIGHT")
            {
                return FAR_RIGHT;
            }
            else
            {
                throw new Exception("Lane description cannot be solved!");
            }
        }

        // 0-indexed, like the in-game maps
        public BSLane(int index) : this(GlobalParameters.width_lane * index + GlobalParameters.width_lane / 2)
        {

        }

        public int getIndex()
        {
            if (this.width == FAR_LEFT.width)
            {
                return 0;
            }
            else if (this.width == NEAR_LEFT.width)
            {
                return 1;
            }
            else if (this.width == NEAR_RIGHT.width)
            {
                return 2;
            }
            else if (this.width == FAR_RIGHT.width)
            {
                return 3;
            }
            else
            {
                throw new Exception("This lane does not have standard lane index.");
            }
        }
    }
}
