using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class ReactionTimeAjaxModel : PageModel
    {
        public double bpm { get; set; }
        public double njs { get; set; }
        public double hjd { get; set; }        

        public void OnGet()
        {
        }

        public void OnPost()
        {
            ViewData["rt"] = Math.Round(1000*60*hjd/bpm);
        }
    }
}
