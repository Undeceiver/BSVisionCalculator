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

        // 0-indexed, like the in-game maps
        public BSLane(int index) : this(GlobalParameters.width_lane * index + GlobalParameters.width_lane / 2)
        {

        }
    }
}
