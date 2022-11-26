using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RealTimeRegimeCalculationWallModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }        
        public double height_player { get; set; }
        public string posture { get; set; }
        public double time_distance { get; set; }
        public string blocker_left_lane { get; set; }
        public string blocker_right_lane { get; set; }
        public int blocker_top_height { get; set; }
        public int blocker_bottom_height { get; set; }        
        public double blocker_duration { get; set; }
        public string blocked_lane { get; set; }
        public string blocked_row { get; set; }

        public void OnGet()
        {
            ViewData["result"] = "Haven't done anything yet";
        }        
    }
}