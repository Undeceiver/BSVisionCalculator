using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RelativeTimeRegimeCalculationWallAjaxModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }
        public double height_player { get; set; }
        public string posture { get; set; }
        public string blocker_left_lane { get; set; }
        public string blocker_right_lane { get; set; }
        public int blocker_top_height { get; set; }
        public int blocker_bottom_height { get; set; }
        public double blocker_duration { get; set; }
        public string blocked_lane { get; set; }
        public string blocked_row { get; set; }
        public double time_last_counted { get; set; }
        public double time_hardvb_first_min { get; set; }
        public double time_hardvb_last_max { get; set; }
        public double time_hardvb_process_min { get; set; }

        // Advanced parameters
        public double height_squat { get; set; } // Vertical displacement due to squatting.
        public double width_lean { get; set; } // Horizontal displacement due to leaning.
        public double width_squatlean { get; set; } // Horizontal displacement due to leaning while also squatting.
        public double time_granularity { get; set; } // Granularity of time to consider in calculations and algorithms.
        public double proportion_spawn_default { get; set; } // Proportion of the lane distance that the note must be away from its final position to consider it to still be spawning.

        public double size_bloq { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {            
            VisionCalculationProcess process = new VisionCalculationProcess(bpm, njs, hjd);

            process.time_last_counted = time_last_counted/1000;
            process.time_hardvb_first_min = time_hardvb_first_min/1000;
            process.time_hardvb_last_max = time_hardvb_last_max/1000;
            process.time_hardvb_process_min = time_hardvb_process_min/1000;

            process.height_squat = height_squat;
            process.width_lean = width_lean;
            process.width_squatlean = width_squatlean;
            process.time_granularity = time_granularity;
            process.proportion_spawn = proportion_spawn_default;
            process.size_bloq = size_bloq;

            process.recalculatePostures();

            VisionCalculationReality reality = new VisionCalculationReality(process, height_player);

            // Posture
            PlayerPosture posture_obj = PlayerPosture.fromString(posture);

            // Blocker
            BSWall blocker = new BSWall(0, process.beatsToSeconds(blocker_duration), BSLane.fromString(blocker_left_lane), BSLane.fromString(blocker_right_lane), new BSWallHeight(blocker_top_height), new BSWallHeight(blocker_bottom_height));

            // Blocked
            BSBloq blocked = new BSBloq(0, BSLane.fromString(blocked_lane), BSRow.fromString(blocked_row));

            VisionCalculationSituation situation = new WallBloqSituation(reality, posture_obj, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation = new RelativeTimeRegimeCalculation(situation, true);

            calculation.processAll();

            ViewData["result"] = calculation.getSummary().toHtml(process);
        }        
    }
}
