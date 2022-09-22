namespace BSVisionCalculator
{
    public class VisionCalculationProcess
    {
        // This class represents one big calculation of vision blocks, on a single map but including multiple variable parameters.

        /*******************************
        // Configurable parameters (minimums/maximums/thresholds/etc)
        *******************************/
        double height_player_min; // Minimum player height to consider.
        double height_player_max; // Maximum player height to consider.
        double height_squat; // Vertical displacement due to squatting.
        double width_lean; // Horizontal displacement due to leaning.
        double width_squatlean; // Horizontal displacement due to leaning while also squatting.
        double time_hardvb_first_min; // Minimum amount of time before having to hit a note, that we need to see an object for the first time.
        double time_hardvb_last_max; // Maximum amount of time before having to hit a note, for the last time we see an object.
        double time_hardvb_process_min; // Minimum amount of time an object needs to remain in vision to be able to process it.
        double time_inline_first_min; // Minimum amount of time before having to hit a note, that we need to see an object for the first time, before it reaches us.
        double time_inline_last_max; // Maximum amount of time before having to hit a note, for the last time we see an object due to an inline.
        double time_inline_process_min; // Minimum amount of time that an object needs to remain in vision to be able to process it, in inline cases.
        double time_granularity; // Granularity of time to consider in calculations and algorithms.

        /*******************************
        // Map parameters
        *******************************/
        double bpm; // BPM of the song. We assume this is constant for now (variations later).
        double njs; // Note jump speed of the map.
        double hjd; // Half jump duration of the map, after considering offset.
        double jd; // Jump distance (real one, from the player to the spawn position).
        double time_reaction; // Reaction time, in the sense of time between an object spawns and it reaches the current time.
    }
}
