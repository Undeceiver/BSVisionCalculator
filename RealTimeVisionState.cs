using System.Runtime.CompilerServices;

namespace BSVisionCalculator
{
    // This is very similar to VisionRegime, but contains partial information.
    // It indicates what we know about the vision state of a situation at a given point.
    public class RealTimeVisionState
    {
        public class RealTimeVisionStateObject
        {
            public double anglevalue_left;
            public double anglevalue_right;
            public double anglevalue_top;
            public double anglevalue_bottom;

            public RealTimeVisionStateObject(double anglevalue_left, double anglevalue_right, double anglevalue_top, double anglevalue_bottom)
            {                
                this.anglevalue_left = anglevalue_left;
                this.anglevalue_right = anglevalue_right;
                this.anglevalue_top = anglevalue_top;
                this.anglevalue_bottom = anglevalue_bottom;
            }
        }

        public RealTimeVisionStateObject blocker;
        public RealTimeVisionStateObject blocked;

        // This is the player's time at which this state happens
        public double time;

        public bool vision_blocked;
        public bool spawning;
                
        // Both of these can be false, but not both can be true. We use these because we sometimes do not know, but we assume there is only one spawning and one non-spawning vision block over time for the same pair of objects.
        // These are only relevant for non-blocked states, and they refer to a vision block that is also either spawning or non-spawning as this one.
        // Has there been a vision block before.
        public bool postblock;
        // Is there a vision block after.
        public bool preblock;

        // Takes a snapshot of the situation into the RealTimeVisionStateObject classes
        public RealTimeVisionState(VisionCalculationSituation situation)
        {
            situation.updateObjects();

            this.blocker = new RealTimeVisionStateObject(situation.blocker.anglevalue_left, situation.blocker.anglevalue_right, situation.blocker.anglevalue_top, situation.blocker.anglevalue_bottom);
            this.blocked = new RealTimeVisionStateObject(situation.blocked.anglevalue_left, situation.blocked.anglevalue_right, situation.blocked.anglevalue_top, situation.blocked.anglevalue_bottom);

            this.time = situation.time_player;

            this.vision_blocked = situation.checkBlocked();
            this.spawning = situation.checkSpawning();

            this.postblock = false;
            this.preblock = false;
        }

        // This does not work.
        /*
        public static bool getSequential(VisionCalculationSituation situation)
        {
            // First save the original player time.
            double time_player_original = situation.time_player;

            // Get the angle value differences before.
            double anglevalue_before_left_right = Math.Abs(situation.blocker.anglevalue_left - situation.blocked.anglevalue_right);
            double anglevalue_before_right_left = Math.Abs(situation.blocker.anglevalue_right - situation.blocked.anglevalue_left);
            double anglevalue_before_top_bottom = Math.Abs(situation.blocker.anglevalue_top - situation.blocked.anglevalue_bottom);
            double anglevalue_before_bottom_top = Math.Abs(situation.blocker.anglevalue_bottom - situation.blocked.anglevalue_top);

            // Now increase it by a little bit, to check how things change.
            situation.time_player += situation.reality.process.time_granularity;
            situation.updateObjects();

            // Get the angle value differences now.
            double anglevalue_after_left_right = Math.Abs(situation.blocker.anglevalue_left - situation.blocked.anglevalue_right);
            double anglevalue_after_right_left = Math.Abs(situation.blocker.anglevalue_right - situation.blocked.anglevalue_left);
            double anglevalue_after_top_bottom = Math.Abs(situation.blocker.anglevalue_top - situation.blocked.anglevalue_bottom);
            double anglevalue_after_bottom_top = Math.Abs(situation.blocker.anglevalue_bottom - situation.blocked.anglevalue_top);

            System.Diagnostics.Debug.WriteLine("GETSEQUENTIAL\n");
            System.Diagnostics.Debug.WriteLine("left_right before: " + anglevalue_before_left_right);
            System.Diagnostics.Debug.WriteLine("left_right after: " + anglevalue_after_left_right);
            System.Diagnostics.Debug.WriteLine("right_left before: " + anglevalue_before_right_left);
            System.Diagnostics.Debug.WriteLine("right_left after: " + anglevalue_after_right_left);
            System.Diagnostics.Debug.WriteLine("top_bottom before: " + anglevalue_before_top_bottom);
            System.Diagnostics.Debug.WriteLine("top_bottom after: " + anglevalue_after_top_bottom);
            System.Diagnostics.Debug.WriteLine("bottom_top before: " + anglevalue_before_bottom_top);
            System.Diagnostics.Debug.WriteLine("bottom_top after: " + anglevalue_after_bottom_top);


            bool result = true;
            // We assume here that the angle values never stay exactly the same. This may be a problem if they do!!!
            // Check if they are separating horizontally.
            if((anglevalue_before_left_right > anglevalue_after_left_right) || (anglevalue_before_right_left > anglevalue_after_right_left))
            {
                result = false;
            }

            // Check if they are separating vertically.
            if((anglevalue_before_top_bottom > anglevalue_after_top_bottom) || (anglevalue_before_bottom_top > anglevalue_after_bottom_top))
            {
                result = false;
            }

            System.Diagnostics.Debug.WriteLine("Sequential: " + result);            

            // Restore the time of the player and update.
            situation.time_player = time_player_original;
            situation.updateObjects();

            return result;
        }
        */        
    }
}
