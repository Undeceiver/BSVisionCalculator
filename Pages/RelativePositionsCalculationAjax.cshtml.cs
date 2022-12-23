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
            ViewData["left_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT))).toHtml(process);
            ViewData["left_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT))).toHtml(process);
            ViewData["right_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT))).toHtml(process);
            ViewData["right_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT))).toHtml(process);
            ViewData["squat_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.SQUAT))).toHtml(process);
            ViewData["squat_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.SQUAT))).toHtml(process);
            ViewData["squat_left_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT_SQUAT))).toHtml(process);
            ViewData["squat_left_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT_SQUAT))).toHtml(process);
            ViewData["squat_right_bloqbloq"] = RelativePositionsSummary.mergePositions(summary.filterBloqBloqPositions(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT_SQUAT))).toHtml(process);
            ViewData["squat_right_wallbloq"] = RelativePositionsSummary.mergePositions(summary.filterWallBloqPositions(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT_SQUAT))).toHtml(process);

            List<RelativeBloqBloqPositionSummary> hardVBPositions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["centred_hardvb"] = RelativePositionsSummary.mergePositions(hardVBPositions).toHtml(process);
            String centred_hardvb_positions = "";
            foreach (RelativeBloqBloqPositionSummary hardVBPosition in hardVBPositions)
            {
                centred_hardvb_positions += hardVBPosition.situationHtml(process);
            }
            ViewData["centred_hardvb_positions"] = centred_hardvb_positions;
        }        
    }
}
