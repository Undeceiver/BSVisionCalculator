using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BSVisionCalculator.Pages
{
    [BindProperties]
    public class RelativePositionsCalculationAjaxModel : PageModel
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
            process.time_inline_first_min = time_inline_first_min/1000;
            process.time_inline_last_max = time_inline_last_max/1000;
            process.time_inline_process_min = time_inline_process_min/1000;

            RelativePositionsSummary summary = new RelativePositionsSummary(process);

            summary.processAll();

            ViewData["overall_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.allBloqBloq)).toHtml(process);
            ViewData["overall_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.allWallBloq)).toHtml(process);
            ViewData["centred_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.CENTRED))).toHtml(process);
            ViewData["centred_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.CENTRED))).toHtml(process);
        }        
    }
}
