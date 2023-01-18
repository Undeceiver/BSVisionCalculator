using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RelativePositionsCalculationModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }        
        public double height_player_min { get; set; }
        public double height_player_max { get; set; }
        public double time_last_counted { get; set; }
        public double time_hardvb_first_min { get; set; }
        public double time_hardvb_last_max { get; set; }
        public double time_hardvb_process_min { get; set; }
        public double time_inline_first_min { get; set; }
        public double time_inline_last_max { get; set; }
        public double time_inline_process_min { get; set; }

        // Advanced parameters
        public double height_squat { get; set; } // Vertical displacement due to squatting.
        public double width_lean { get; set; } // Horizontal displacement due to leaning.
        public double width_squatlean { get; set; } // Horizontal displacement due to leaning while also squatting.
        public double time_granularity { get; set; } // Granularity of time to consider in calculations and algorithms.
        public double proportion_spawn_default { get; set; } // Proportion of the lane distance that the note must be away from its final position to consider it to still be spawning.

        public double size_bloq { get; set; }


        public void OnGet()
        {
            ViewData["time_last_counted_default"] = GlobalParameters.time_last_counted_default * 1000;
            ViewData["time_hardvb_first_min_default"] = GlobalParameters.time_hardvb_first_min_wrt_reaction_time_default * 1000 + 625;
            ViewData["time_hardvb_last_max_default"] = GlobalParameters.time_hardvb_last_max_default * 1000;
            ViewData["time_hardvb_process_min_default"] = GlobalParameters.time_hardvb_process_min_default * 1000;
            ViewData["time_inline_first_min_default"] = GlobalParameters.time_inline_first_min_default * 1000;
            ViewData["time_inline_last_max_default"] = GlobalParameters.time_inline_last_max_wrt_reaction_time_default * 1000 + 625;
            ViewData["time_inline_process_min_default"] = GlobalParameters.time_inline_process_min_default * 1000;

            ViewData["height_squat_default"] = GlobalParameters.height_squat_default;
            ViewData["width_lean_default"] = GlobalParameters.width_lean_default;
            ViewData["width_squatlean_default"] = GlobalParameters.width_squatlean_default;
            ViewData["time_granularity_default"] = GlobalParameters.time_granularity_default;
            ViewData["proportion_spawn_default"] = GlobalParameters.proportion_spawn_default;
            ViewData["size_bloq_default"] = GlobalParameters.size_bloq;
        }        
    }
}