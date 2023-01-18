using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RelativeTimeRegimeCalculationWallModel : PageModel
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
        
        public void OnGet()
        {
            ViewData["time_last_counted_default"] = GlobalParameters.time_last_counted_default * 1000;
            ViewData["time_hardvb_first_min_default"] = GlobalParameters.time_hardvb_first_min_wrt_reaction_time_default * 1000 + 625;            
            ViewData["time_hardvb_last_max_default"] = GlobalParameters.time_hardvb_last_max_default * 1000;
            ViewData["time_hardvb_process_min_default"] = GlobalParameters.time_hardvb_process_min_default * 1000;

            ViewData["height_squat_default"] = GlobalParameters.height_squat_default;
            ViewData["width_lean_default"] = GlobalParameters.width_lean_default;
            ViewData["width_squatlean_default"] = GlobalParameters.width_squatlean_default;
            ViewData["time_granularity_default"] = GlobalParameters.time_granularity_default;
            ViewData["proportion_spawn_default"] = GlobalParameters.proportion_spawn_default;
            ViewData["size_bloq_default"] = GlobalParameters.size_bloq;
        }        
    }
}