namespace BSVisionCalculator
{
    public abstract class VisionCalculationSituation
    {
        // A vision calculation situation is a combination of an object potentially blocking, an object potentially blocked and a player state.
        // We assume the object potentially blocked is always a bloq (or bomb).
        public VisionCalculationReality reality;

        public PlayerPosture posture;
        public double height_player;
        public double width_player;
        public double time_player;

        public BSObject blocker;
        public BSBloq blocked;

        public bool is_blocked;

        public VisionCalculationSituation(VisionCalculationReality reality, PlayerPosture posture, double time_player, BSObject blocker, BSBloq blocked)
        {
            this.reality = reality;
            this.posture = posture;
            this.height_player = this.posture.y;
            this.width_player = this.posture.x;
            this.time_player = time_player;
            this.blocker = blocker;            
            this.blocked = blocked;

            this.updateObjects();
        }

        public void updateObjects()
        {
            this.blocker.updateSituation(this);
            this.blocked.updateSituation(this);
        }

        public bool checkBlocked()
        {
            bool blocked_horizontal;
            bool blocked_vertical;

            if (blocked.anglevalue_left >= blocker.anglevalue_left && blocked.anglevalue_left <= blocker.anglevalue_right)
            {
                blocked_horizontal = true;
            }
            else if (blocked.anglevalue_right >= blocker.anglevalue_left && blocked.anglevalue_right <= blocker.anglevalue_right)
            {
                blocked_horizontal = true;
            }
            else
            {
                blocked_horizontal = false;
            }

            if (blocked.anglevalue_top >= blocker.anglevalue_bottom && blocked.anglevalue_top <= blocker.anglevalue_top)
            {
                blocked_vertical = true;
            }
            else if (blocked.anglevalue_bottom >= blocker.anglevalue_bottom && blocked.anglevalue_bottom <= blocker.anglevalue_top)
            {
                blocked_vertical = true;
            }
            else
            {
                blocked_vertical = false;
            }

            this.is_blocked = blocked_horizontal && blocked_vertical;

            return this.is_blocked;
        }
    }
}
