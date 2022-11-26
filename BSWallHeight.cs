namespace BSVisionCalculator
{
    // These are the heights at which a wall can be. Similar to rows but for walls.
    public class BSWallHeight
    {
        public int index; // Stored as index because the value depends heavily on player height.

        public BSWallHeight(int index)
        {
            this.index = index;
        }

        // Index starts from the bottom, up to 5 (in principle)
        public double getWallHeight(VisionCalculationReality reality)
        {
            double height = GlobalParameters.height_wall_min + this.index * GlobalParameters.height_wall_level + reality.height_player_effective - GlobalParameters.height_player_std;

            height = Math.Max(GlobalParameters.height_wall_min, height);

            return height;
        }
    }
}
