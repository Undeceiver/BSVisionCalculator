﻿namespace BSVisionCalculator
{
    public class VisionCalculationProcess
    {
        // This class represents one big calculation of vision blocks, on a single map but including multiple variable parameters.

        // Normally it just uses default values. The values can be modified manually.
        public VisionCalculationProcess(double bpm, double njs, double hjd)
        {
            this.height_player_min = GlobalParameters.height_player_min_default;
            this.height_player_max = GlobalParameters.height_player_max_default;
            this.height_squat = GlobalParameters.height_squat_default;
            this.width_lean = GlobalParameters.width_lean_default;
            this.width_squatlean = GlobalParameters.width_squatlean_default;
            this.time_granularity = GlobalParameters.time_granularity_default;
            this.proportion_spawn = GlobalParameters.proportion_spawn_default;

            this.size_bloq = GlobalParameters.size_bloq;

            this.bpm = bpm;
            this.njs = njs;
            this.hjd = hjd;

            this.time_reaction = this.hjd * 60 / this.bpm;
            this.jd = this.njs * this.time_reaction;

            this.time_hardvb_first_min = GlobalParameters.time_hardvb_first_min_wrt_reaction_time_default + this.time_reaction;
            this.time_hardvb_last_max = GlobalParameters.time_hardvb_last_max_default;
            this.time_hardvb_process_min = GlobalParameters.time_hardvb_process_min_default;
            this.time_inline_first_min = GlobalParameters.time_inline_first_min_default;
            this.time_inline_last_max = GlobalParameters.time_inline_last_max_wrt_reaction_time_default + this.time_reaction;
            this.time_inline_process_min = GlobalParameters.time_inline_process_min_default;
            this.time_last_counted = GlobalParameters.time_last_counted_default;
        }

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
        public double time_last_counted; // Any vision happening closer than this to the player is not counted for the above parameters.
        public double time_granularity; // Granularity of time to consider in calculations and algorithms.
        public double proportion_spawn; // Proportion of the lane distance that the note must be away from its final position to consider it to still be spawning.

        public double size_bloq;

        /*******************************
        // Map parameters
        *******************************/
        public double bpm; // BPM of the song. We assume this is constant for now (variations later).
        public double njs; // Note jump speed of the map.
        public double hjd; // Half jump duration of the map, after considering offset.
        public double jd; // Jump distance (real one, from the player to the spawn position).
        public double time_reaction; // Reaction time, in the sense of time between an object spawns and it reaches the current time.

        public double beatsToSeconds(double beats)
        {
            return beats * 60 / bpm;
        }

        public double secondsToBeats(double seconds)
        {
            return seconds * bpm / 60;
        }

        public double secondsToMeters(double seconds)
        {
            return seconds * njs;
        }

        public double metersToSeconds(double meters)
        {
            return meters / njs;
        }

        public void recalculatePostures()
        {
            PlayerPosture.CENTRED = new PlayerPosture(GlobalParameters.width_centre, 0);
            PlayerPosture.LEFT = new PlayerPosture(GlobalParameters.width_centre - this.width_lean, 0);
            PlayerPosture.RIGHT = new PlayerPosture(GlobalParameters.width_centre + this.width_lean, 0);
            PlayerPosture.SQUAT = new PlayerPosture(GlobalParameters.width_centre, -this.height_squat);
            PlayerPosture.LEFT_SQUAT = new PlayerPosture(GlobalParameters.width_centre - this.width_squatlean, -this.height_squat);
            PlayerPosture.RIGHT_SQUAT = new PlayerPosture(GlobalParameters.width_centre + this.width_squatlean, -this.height_squat);    
        }
    }
}
