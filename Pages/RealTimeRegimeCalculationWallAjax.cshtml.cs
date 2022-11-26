using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RealTimeRegimeCalculationWallAjaxModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }
        public double height_player { get; set; }
        public string posture { get; set; }
        public double time_distance { get; set; }
        public string blocker_left_lane { get; set; }
        public string blocker_right_lane { get; set; }
        public int blocker_bottom_height { get; set; }
        public int blocker_top_height { get; set; }
        public double blocker_duration { get; set; }
        public string blocked_lane { get; set; }
        public string blocked_row { get; set; }
        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            VisionCalculationProcess process = new VisionCalculationProcess(bpm, njs, hjd);
            VisionCalculationReality reality = new VisionCalculationReality(process, height_player);

            // Posture
            PlayerPosture posture_obj = PlayerPosture.fromString(posture);

            // Blocker
            BSWall blocker = new BSWall(0, process.beatsToSeconds(blocker_duration), BSLane.fromString(blocker_left_lane), BSLane.fromString(blocker_right_lane), new BSWallHeight(blocker_top_height), new BSWallHeight(blocker_bottom_height));

            // Blocked
            BSBloq blocked = new BSBloq(process.beatsToSeconds(time_distance), BSLane.fromString(blocked_lane), BSRow.fromString(blocked_row));

            VisionCalculationSituation situation = new WallBloqSituation(reality, posture_obj, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();

            //ViewData["result"] = VisionCalculationTestCommons.getRealTimeRegimeSummaryString(calculation.getSummary());
            ViewData["result"] = calculation.getSummary().toString();
        }        
    }
}
