using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RealTimeRegimeCalculationModel : PageModel
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
            ViewData["height_squat_default"] = GlobalParameters.height_squat_default;
            ViewData["width_lean_default"] = GlobalParameters.width_lean_default;
            ViewData["width_squatlean_default"] = GlobalParameters.width_squatlean_default;
            ViewData["time_granularity_default"] = GlobalParameters.time_granularity_default;
            ViewData["proportion_spawn_default"] = GlobalParameters.proportion_spawn_default;
            ViewData["size_bloq_default"] = GlobalParameters.size_bloq;
        }        
    }
}