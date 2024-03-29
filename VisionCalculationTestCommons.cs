﻿namespace BSVisionCalculator
{
    public class VisionCalculationTestCommons
    {

        // Simple and to the point.
        public static VisionCalculationProcess getTemplateProcess1()
        {
            VisionCalculationProcess process = new VisionCalculationProcess(120,16,1.25);

            return process;
        }

        // Very long offset
        public static VisionCalculationProcess getTemplateProcess2()
        {
            VisionCalculationProcess process = new VisionCalculationProcess(120,16,100);
            
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
                "<p>BLOCKED: " + situation.checkBlocked() + "</p>";
        }

        public static String getWallBloqSituationString(WallBloqSituation situation)
        {
            return "<p>Player X: " + situation.width_player + ", Y: " + situation.height_player + "</p>" +
                "<p>Blocker left X: " + situation.blocker_wall.width_left + ", right X: " + situation.blocker_wall.width_right + ", top Y: " + situation.blocker_wall.height_top + ", bottom Y: " + situation.blocker_wall.height_bottom + ", start Z: " + situation.blocker_wall.depth_start + ", end Z: " + situation.blocker_wall.depth_end + ", H. ANGLES: " + situation.blocker.anglevalue_left + "//" + situation.blocker.anglevalue_right + ", V. ANGLES: " + situation.blocker.anglevalue_top + "//" + situation.blocker.anglevalue_bottom + "</p>" +
                "<p>Blocked X: " + situation.blocked.width + ", Y: " + situation.blocked.height + ", Z: " + situation.blocked.depth + ", H. ANGLES: " + situation.blocked.anglevalue_left + "//" + situation.blocked.anglevalue_right + ", V. ANGLES: " + situation.blocked.anglevalue_top + "//" + situation.blocked.anglevalue_bottom + "</p>" +
                "<p>BLOCKED: " + situation.checkBlocked() + "</p>";
        }

        public static String getRealTimeVisionStateString(RealTimeVisionState state)
        {
            return
                "<p>Player time: " + state.time + "</p>" +
                "<p>Vision blocked: " + state.vision_blocked + "</p>" +
                "<p>Spawning: " + state.spawning + "</p>" +
                "<p>Preblock: " + state.preblock + "</p>" + 
                "<p>Postblock: " + state.postblock + "</p>";
        }

        public static String getRealTimeRegimeCalculationString(RealTimeRegimeCalculation calculation)
        {
            String result = "";

            result += "<p>Regime summary:</p>";
            result += getRealTimeRegimeSummaryString(calculation.getSummary());
            result += "<p>Regime calculation. " + calculation.states.Count + " states total: </p><ul>";
            for(int i = 0; i < calculation.states.Count; i++)
            {
                result += "<li>" + getRealTimeVisionStateString(calculation.states[i]) + "</li>";
            }
            result += "</ul>";

            return result;
        }        

        public static String getRealTimeRegimeSummaryString(RealTimeRegimeSummary summary)
        {
            String result = "";

            for(int i = 0; i < summary.regimes.Count; i++)
            {
                result += "<p>Time:" + summary.regimes[i].time_start + " - " + summary.regimes[i].time_end + " | Spawning: " + summary.regimes[i].spawning + "| Vision blocked: " + summary.regimes[i].vision_blocked + "</p>";
            }

            return result;
        }

        public static String getRelativeTimeVisionStateString(RelativeTimeVisionState state)
        {
            return
                "<p>Relative time: " + state.time_relative + "</p>" +
                "<p>First vision: " + state.time_first + "</p>" +
                "<p>OK? => " + state.ok_first + "</p>" +
                "<p>Last vision: " + state.time_last + "</p>" +
                "<p>OK? => " + state.ok_last + "</p>" +
                "<p>Total process time: " + state.time_process + "</p>" +
                "<p>OK? => " + state.ok_process + "</p>";
        }

        public static String getRelativeTimeRegimeCalculationString(RelativeTimeRegimeCalculation calculation)
        {
            String result = "";

            result += "<p>Regime summary:</p>";
            result += getRelativeTimeRegimeSummaryString(calculation.getSummary());
            result += "<p>Regime calculation. " + calculation.states.Count + " states total: </p><ul>";
            for (int i = 0; i < calculation.states.Count; i++)
            {
                result += "<li>" + getRelativeTimeVisionStateString(calculation.states[i]) + "</li>";
            }
            result += "</ul>";

            return result;
        }

        public static String getRelativeTimeRegimeSummaryString(RelativeTimeRegimeSummary summary)
        {
            String result = "";

            for (int i = 0; i < summary.regimes.Count; i++)
            {
                result += "<p>Time:" + summary.regimes[i].time_start + " - " + summary.regimes[i].time_end + " | OK: " + summary.regimes[i].ok + "</p>";
            }

            return result;
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
