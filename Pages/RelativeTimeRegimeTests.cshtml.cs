using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    public class RelativeTimeRegimeTestsModel : PageModel
    {
        public void OnGet()
        {
            ViewData["test1"] = test1();
            ViewData["test2"] = test2();
            ViewData["test3"] = test3();
            ViewData["test4"] = test4();
            ViewData["test5"] = test5();
            ViewData["test6"] = test6();
            ViewData["test7"] = test7();
            ViewData["test8"] = test8();
            ViewData["test9"] = test9();
            ViewData["test10"] = test10();
            ViewData["test11"] = test11();
            ViewData["test12"] = test12();
            ViewData["test13"] = test13();
        }

        public String test1()
        {
            // Basic situation: Both middle.
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Both blocks in middle</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        
        public String test2()
        {
            // Blocker middle, blocked far right.
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Blocker middle, blocked side</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test3()
        {
            // Blocker middle, blocked bottom
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.BOTTOM);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Blocker middle, blocked bottom</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test4()
        {
            // Blocker middle, blocked top
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.TOP);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Blocker middle, blocked top</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }
    
        public String test5()
        {
            // Blocker top, blocked mid
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.TOP);
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Blocker top, blocked mid</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }
        public String test6()
        {
            // Blocker top, blocked bottom
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.TOP);
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.BOTTOM);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Blocker top, blocked bottom</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test7()
        {
            // Bottom row inline
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.BOTTOM);
            BSBloq blocked = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.BOTTOM);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, false);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Bottom row inline</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test8()
        {
            // Mid row inline
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, false); ;

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Mid row inline</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test9()
        {
            // Bottom row inline
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.TOP);
            BSBloq blocked = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.TOP);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, false);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Top row inline</h3>" + VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test10()
        {
            // Side wall centre bloq
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSWall blocker = new BSWall(0, 0, BSLane.NEAR_RIGHT, BSLane.FAR_RIGHT, new BSWallHeight(5), new BSWallHeight(0));
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);

            WallBloqSituation situation = new WallBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Side wall with centre bloq</h3>" + VisionCalculationTestCommons.getWallBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test11()
        {
            // Side wall centre bloq, leaning
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSWall blocker = new BSWall(0, 0, BSLane.NEAR_RIGHT, BSLane.FAR_RIGHT, new BSWallHeight(5), new BSWallHeight(0));
            BSBloq blocked = new BSBloq(0, BSLane.NEAR_RIGHT, BSRow.MID);

            WallBloqSituation situation = new WallBloqSituation(reality, PlayerPosture.LEFT, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Side wall with centre bloq, leaning</h3>" + VisionCalculationTestCommons.getWallBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test12()
        {
            // Side wall far bloq
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSWall blocker = new BSWall(0, 0, BSLane.NEAR_RIGHT, BSLane.FAR_RIGHT, new BSWallHeight(5), new BSWallHeight(0));
            BSBloq blocked = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.MID);

            WallBloqSituation situation = new WallBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Side wall with far bloq</h3>" + VisionCalculationTestCommons.getWallBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }

        public String test13()
        {
            // Side wall far bloq, leaning
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSWall blocker = new BSWall(0, 0, BSLane.NEAR_RIGHT, BSLane.FAR_RIGHT, new BSWallHeight(5), new BSWallHeight(0));
            BSBloq blocked = new BSBloq(0, BSLane.FAR_RIGHT, BSRow.MID);

            WallBloqSituation situation = new WallBloqSituation(reality, PlayerPosture.LEFT, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();
            //System.Diagnostics.Debug.WriteLine("Processing finished!!");

            return "<h3>Side wall with far bloq, leaning</h3>" + VisionCalculationTestCommons.getWallBloqSituationString(situation) + VisionCalculationTestCommons.getRelativeTimeRegimeCalculationString(calculation);
        }
    }
}
