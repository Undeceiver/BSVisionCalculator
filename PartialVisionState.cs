namespace BSVisionCalculator
{
    // This is very similar to VisionRegime, but contains partial information.
    // It indicates what we know about the vision state of a situation at a given point.
    public class PartialVisionState
    {
        public bool vision_blocked;
        public bool spawning;

        // We have two separate variables here because we may not know whether it's parallel or sequential yet. Both cannot be true at the same time.
        public bool parallel;
        public bool sequential;

        public PartialVisionState(bool vision_blocked, bool spawning, bool parallel, bool sequential)
        {
            this.vision_blocked = vision_blocked;
            this.spawning = spawning;
            this.parallel = parallel;
            this.sequential = sequential;
        }
    }
}
