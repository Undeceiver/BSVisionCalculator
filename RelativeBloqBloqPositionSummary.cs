namespace BSVisionCalculator
{
    public class RelativeBloqBloqPositionSummary
    {
        public BSBloq blocker_bloq;
        public BSBloq blocked_bloq;

        public PlayerPosture posture;

        public RelativeTimeRegimeSummary regimes;

        public RelativeBloqBloqPositionSummary(BSBloq blocker_bloq, BSBloq blocked_bloq, PlayerPosture posture, RelativeTimeRegimeSummary regimes)
        {
            this.blocker_bloq = blocker_bloq;
            this.blocked_bloq = blocked_bloq;
            this.posture = posture;

            this.regimes = regimes;
        }
    }
}
