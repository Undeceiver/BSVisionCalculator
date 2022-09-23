using System.Transactions;

namespace BSVisionCalculator
{
    public class BloqBloqSituation : VisionCalculationSituation
    {
        public BSBloq blocker_bloq;

        public BloqBloqSituation(VisionCalculationReality reality, PlayerPosture posture, double time_player, BSBloq blocker, BSBloq blocked) : base(reality, posture, time_player, blocker, blocked)
        {
            this.blocker_bloq = blocker;            
        }
    }
}
