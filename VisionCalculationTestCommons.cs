namespace BSVisionCalculator
{
    public class VisionCalculationTestCommons
    {

        // Simple and to the point.
        public static VisionCalculationProcess getTemplateProcess1()
        {
            VisionCalculationProcess process = new VisionCalculationProcess();

            process.height_player_min = 1.8;
            process.height_player_max = 1.8;
            process.height_squat = GlobalParameters.height_squat_default;
            process.width_lean = GlobalParameters.width_lean_default;
            process.width_squatlean = GlobalParameters.width_squatlean_default;
            process.time_hardvb_first_min = 0.5;
            process.time_hardvb_last_max = 0.1;
            process.time_hardvb_process_min = 0.4;
            process.time_inline_first_min = 0;
            process.time_inline_last_max = 100;
            process.time_inline_process_min = 0;
            process.time_granularity = GlobalParameters.time_granularity_default;

            process.bpm = 120;
            process.njs = 16;
            process.hjd = 1.5;
            process.jd = 12; // This should be calculated.
            process.time_reaction = 0.75; // This should be calculated too.

            return process;
        }

        // Very long offset
        public static VisionCalculationProcess getTemplateProcess2()
        {
            VisionCalculationProcess process = new VisionCalculationProcess();

            process.height_player_min = 1.8;
            process.height_player_max = 1.8;
            process.height_squat = GlobalParameters.height_squat_default;
            process.width_lean = GlobalParameters.width_lean_default;
            process.width_squatlean = GlobalParameters.width_squatlean_default;
            process.time_hardvb_first_min = 0.5;
            process.time_hardvb_last_max = 0.1;
            process.time_hardvb_process_min = 0.4;
            process.time_inline_first_min = 0;
            process.time_inline_last_max = 100;
            process.time_inline_process_min = 0;
            process.time_granularity = GlobalParameters.time_granularity_default;

            process.bpm = 120;
            process.njs = 16;
            process.hjd = 10;
            process.jd = 80; // This should be calculated.
            process.time_reaction = 5; // This should be calculated too.

            return process;
        }

        public static VisionCalculationReality getTemplateReality1()
        {
            VisionCalculationReality reality = new VisionCalculationReality(getTemplateProcess1(),1.8);

            return reality;
        }
        
        public static VisionCalculationReality getTemplateReality2()
        {
            VisionCalculationReality reality = new VisionCalculationReality(getTemplateProcess2(),1.8);

            return reality;
        }

        public static String getBloqBloqSituationString(BloqBloqSituation situation)
        {
            return "<p>Player X: " + situation.width_player + ", Y: " + situation.height_player + "</p>" + 
                "<p>Blocker X: " + situation.blocker_bloq.width + ", Y: " + situation.blocker_bloq.height + ", Z: " + situation.blocker_bloq.depth + ", H. ANGLES: " + situation.blocker_bloq.anglevalue_left + "//" + situation.blocker_bloq.anglevalue_right + ", V. ANGLES: " + situation.blocker_bloq.anglevalue_top + "//" + situation.blocker_bloq.anglevalue_bottom + "</p>" + 
                "<p>Blocked X: " + situation.blocked.width + ", Y: " + situation.blocked.height + ", Z: " + situation.blocked.depth + ", H. ANGLES: " + situation.blocked.anglevalue_left + "//" + situation.blocked.anglevalue_right + ", V. ANGLES: " + situation.blocked.anglevalue_top + "//" + situation.blocked.anglevalue_bottom + "</p>" + 
                "BLOCKED: " + situation.checkBlocked();
        }

        public static String getTestResult(Object expected, Object obtained)
        {
            if(expected.Equals(obtained))
            {
                return "<p style=\"color:green\"><strong>OK</strong></p>";
            }
            else
            {
                return "<p style=\"color:red\"><strong>ERROR</strong></p>";
            }
        }
    }
}
