namespace BSVisionCalculator
{
    public class VisionCalculationReality
    {
        // This class represents one particular "reality" for a vision calculation process, where parameters that we consider to not change in the actual reality are fixed.
        // Note that we do not include parameters that appear in the general process here, only those that vary.

        public VisionCalculationProcess process;

        public VisionCalculationReality(VisionCalculationProcess process, double height_player_real)
        {
            this.process = process;
            this.height_player_real = height_player_real;
            this.height_player_effective = getEffectivePlayerHeight(height_player_real);
            this.height_camera = height_player_real - GlobalParameters.height_player_above_camera;
        }

        public static double getEffectivePlayerHeight(double height_player_real)
        {
            double difference = height_player_real - GlobalParameters.height_player_std;
            double prop_difference = difference * GlobalParameters.height_player_mult;
            double height_effective = GlobalParameters.height_player_std + prop_difference;

            return Math.Min(GlobalParameters.height_player_absolute_max, Math.Max(GlobalParameters.height_player_absolute_min, height_effective));
        }

        /*******************************
        // Player parameters
        *******************************/
        public double height_player_real; // Real height of the player (or rather, what they configured their game to).
        public double height_player_effective; // Effective player height, as recalculated by the game.
        public double height_camera; // Height at which the camera is.


    }
}
