namespace BSVisionCalculator
{
    public class VisionCalculationProcess
    {
        // This class represents one big calculation of vision blocks, on a single map but including multiple variable parameters.

        /*******************************
        // Configurable parameters (minimums/maximums/thresholds/etc)
        *******************************/
        public double height_player_min; // Minimum player height to consider.
        public double height_player_max; // Maximum player height to consider.
        public double height_squat; // Vertical displacement due to squatting.
        public double width_lean; // Horizontal displacement due to leaning.
        public double width_squatlean; // Horizontal displacement due to leaning while also squatting.
        public double time_hardvb_first_min; // Minimum amount of time before having to hit a note, that we need to see an object for the first time.
        public double time_hardvb_last_max; // Maximum amount of time before having to hit a note, for the last time we see an object.
        public double time_hardvb_process_min; // Minimum amount of time an object needs to remain in vision to be able to process it.
        public double time_inline_first_min; // Minimum amount of time before having to hit a note, that we need to see an object for the first time, before it reaches us.
        public double time_inline_last_max; // Maximum amount of time before having to hit a note, for the last time we see an object due to an inline.
        public double time_inline_process_min; // Minimum amount of time that an object needs to remain in vision to be able to process it, in inline cases.
        public double time_granularity; // Granularity of time to consider in calculations and algorithms.

        /*******************************
        // Map parameters
        *******************************/
        public double bpm; // BPM of the song. We assume this is constant for now (variations later).
        public double njs; // Note jump speed of the map.
        public double hjd; // Half jump duration of the map, after considering offset.
        public double jd; // Jump distance (real one, from the player to the spawn position).
        public double time_reaction; // Reaction time, in the sense of time between an object spawns and it reaches the current time.
    }
}
