using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class InlineLastMaxAjaxModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }        

        public void OnGet()
        {
        }

        public void OnPost()
        {
            double rt = Math.Round(1000*60*hjd/bpm);

            ViewData["time_inline_last_max_default"] = 1000*GlobalParameters.time_inline_last_max_wrt_reaction_time_default + rt;
        }
    }
}
