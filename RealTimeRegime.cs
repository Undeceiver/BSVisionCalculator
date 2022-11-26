namespace BSVisionCalculator
{
    // Used for summaries
    public class RealTimeRegime
    {
        public bool spawning;
        public bool vision_blocked;

        public double time_start;
        public double time_end;

        public RealTimeRegime(bool spawning, bool vision_blocked, double time_start)
        {
            this.spawning = spawning;
            this.vision_blocked = vision_blocked;
            this.time_start = time_start;
        }
    }
}
