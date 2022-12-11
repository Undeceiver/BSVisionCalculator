namespace BSVisionCalculator
{
    public class RelativeWallBloqPositionSummary
    {
        public BSWall blocker_wall;
        public BSBloq blocked_bloq;

        public PlayerPosture posture;

        public RelativeTimeRegimeSummary regimes;

        public RelativeWallBloqPositionSummary(BSWall blocker_wall, BSBloq blocked_bloq, PlayerPosture posture, RelativeTimeRegimeSummary regimes)
        {
            this.blocker_wall = blocker_wall;
            this.blocked_bloq = blocked_bloq;
            this.posture = posture;

            this.regimes = regimes;
        }
    }
}
