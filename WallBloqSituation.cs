namespace BSVisionCalculator
{
    public class WallBloqSituation : VisionCalculationSituation
    {
        public BSWall blocker_wall;

        public WallBloqSituation(VisionCalculationReality reality, PlayerPosture posture, double time_player, BSWall blocker, BSBloq blocked) : base(reality, posture, time_player, blocker, blocked)
        {
            this.blocker_wall = blocker;
        }
    }
}
