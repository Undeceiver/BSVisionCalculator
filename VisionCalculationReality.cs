namespace BSVisionCalculator
{
    public class VisionCalculationReality
    {
        // This class represents one particular "reality" for a vision calculation process, where parameters that we consider to not change in the actual reality are fixed.
        // Note that we do not include parameters that appear in the general process here, only those that vary.

        /*******************************
        // Player parameters
        *******************************/
        public double height_player_real; // Real height of the player (or rather, what they configured their game to).
        public double height_player_effective; // Effective player height, as recalculated by the game.
        public double height_camera; // Height at which the camera is.                
    }
}
