namespace BSVisionCalculator
{
    public abstract class VisionCalculationSituation
    {
        // A vision calculation situation is a combination of an object potentially blocking, an object potentially blocked and a player state.
        // We assume the object potentially blocked is always a bloq (or bomb).

        public PlayerPosture posture;
        public double height_player;
        public double width_player;
        public double time_player;

        public BSObject blocker;
        public BSBloq blocked;
    }
}
