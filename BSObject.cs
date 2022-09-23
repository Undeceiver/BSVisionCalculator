namespace BSVisionCalculator
{
    public abstract class BSObject
    {
        // The main purpose of this class is to be a common super class of walls and bloqs
        public double anglevalue_left;
        public double anglevalue_right;
        public double anglevalue_top;
        public double anglevalue_bottom;

        // An anglevalue is not an angle, but they can be compared like angles are. They are effectively the tangents of angles.
        public static double calculateAngleValue(double measure_object, double measure_player, double z)
        {
            return (measure_object - measure_player) / z;
        }

        public abstract void updateDepth(VisionCalculationSituation situation);
        public abstract void updateAngleValues(VisionCalculationSituation situation);

        public void updateSituation(VisionCalculationSituation situation)
        {
            this.updateDepth(situation);
            this.updateAngleValues(situation);
        }
    }
}
