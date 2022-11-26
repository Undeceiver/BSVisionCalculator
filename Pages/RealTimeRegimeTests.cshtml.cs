using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    public class RealTimeRegimeTestsModel : PageModel
    {
        public void OnGet()
        {
            ViewData["test1"] = test1();
            ViewData["test2"] = test2();
            ViewData["test3"] = test3();
            ViewData["test4"] = test4();
            ViewData["test5"] = test5();
            ViewData["test6"] = test6();
        }

        public String test1()
        {
            // Spawn parallel - No vision block
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.2, BSLane.FAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.74, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>THIS SHOULD NOT HAVE ANY VISION BLOCK</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRealTimeRegimeCalculationString(calculation);
        }

        public String test2()
        {
            // Spawn blocked
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.73, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.74, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>THIS SHOULD BE VISION BLOCKED AT LEAST DURING SPAWN</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRealTimeRegimeCalculationString(calculation);
        }

        public String test3()
        {
            // Spawn blocked (top lane blocker!!)
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.73, BSLane.NEAR_RIGHT, BSRow.TOP);
            BSBloq blocked = new BSBloq(0.74, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>THIS SHOULD BE VISION BLOCKED AT LEAST DURING SPAWN</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRealTimeRegimeCalculationString(calculation);
        }

        public String test4()
        {
            // Parallel but far
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality2();

            BSBloq blocker = new BSBloq(3.0, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(4.0, BSLane.NEAR_RIGHT, BSRow.BOTTOM);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>THIS SHOULD BE PARALLEL FOR A LONG TIME</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRealTimeRegimeCalculationString(calculation);
        }
    
        public String test5()
        {
            // This should be a non-spawning block
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.2, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.3, BSLane.NEAR_RIGHT, BSRow.BOTTOM);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>THIS SHOULD BE BLOCKED THROUGHOUT</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRealTimeRegimeCalculationString(calculation);
        }
        public String test6()
        {
            // Sequential and close
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.1, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.25, BSLane.NEAR_RIGHT, BSRow.TOP);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>THIS SHOULD BE BLOCKED ONLY WHILE SPAWNING</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRealTimeRegimeCalculationString(calculation);
        }
    }
}
