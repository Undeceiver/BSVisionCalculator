using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    public class VisionCalculationTestsModel : PageModel
    {
        public void OnGet()
        {
            ViewData["test1"] = test1();
            ViewData["test2"] = test2();
            ViewData["test3"] = test3();
            ViewData["test4"] = test4();
        }

        public String test1()
        {
            // Obvious vision block. Middle note and another middle note soon after.
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.35, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.5, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            bool isBlocked = situation.checkBlocked();

            return VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getTestResult(true,isBlocked);
        }

        public String test2()
        {
            // Obvious non-vision block. Different sides
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.35, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.5, BSLane.NEAR_LEFT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            bool isBlocked = situation.checkBlocked();

            return VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getTestResult(false, isBlocked);
        }

        public String test3()
        {
            // Obvious vision block. Middle note and note behind it soon after.
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality1();

            BSBloq blocker = new BSBloq(0.35, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(0.5, BSLane.FAR_RIGHT, BSRow.BOTTOM);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            bool isBlocked = situation.checkBlocked();

            return VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getTestResult(true, isBlocked);
        }

        public String test4()
        {
            // Obvious non-vision block. Very far notes
            VisionCalculationReality reality = VisionCalculationTestCommons.getTemplateReality2();

            BSBloq blocker = new BSBloq(0.05, BSLane.NEAR_RIGHT, BSRow.MID);
            BSBloq blocked = new BSBloq(2, BSLane.NEAR_RIGHT, BSRow.MID);

            BloqBloqSituation situation = new BloqBloqSituation(reality, PlayerPosture.CENTRED, 0, blocker, blocked);

            bool isBlocked = situation.checkBlocked();

            return VisionCalculationTestCommons.getBloqBloqSituationString(situation) + VisionCalculationTestCommons.getTestResult(false, isBlocked);
        }
    }
}
