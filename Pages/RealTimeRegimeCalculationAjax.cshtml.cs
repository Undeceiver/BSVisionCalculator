using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RealTimeRegimeCalculationAjaxModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }
        public double height_player { get; set; }
        public string posture { get; set; }
        public double time_distance { get; set; }
        public string blocker_lane { get; set; }
        public string blocker_row { get; set; }
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
            BSBloq blocker = new BSBloq(0, BSLane.fromString(blocker_lane), BSRow.fromString(blocker_row));

            // Blocked
            BSBloq blocked = new BSBloq(process.beatsToSeconds(time_distance), BSLane.fromString(blocked_lane), BSRow.fromString(blocked_row));

            VisionCalculationSituation situation = new BloqBloqSituation(reality, posture_obj, 0, blocker, blocked);

            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(situation);

            calculation.processAll();

            //ViewData["result"] = VisionCalculationTestCommons.getRealTimeRegimeSummaryString(calculation.getSummary());
            ViewData["result"] = calculation.getSummary().toString();
        }        
    }
}
