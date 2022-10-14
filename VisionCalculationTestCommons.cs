namespace BSVisionCalculator
{
    public class VisionCalculationTestCommons
    {

        // Simple and to the point.
        public static VisionCalculationProcess getTemplateProcess1()
        {
            VisionCalculationProcess process = new VisionCalculationProcess(120,16,1.5);

            return process;
        }

        // Very long offset
        public static VisionCalculationProcess getTemplateProcess2()
        {
            VisionCalculationProcess process = new VisionCalculationProcess(120,16,10);
            
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
