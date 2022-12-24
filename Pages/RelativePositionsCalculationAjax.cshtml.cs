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

            // Centred
            // Bloq-bloq
            List<RelativeBloqBloqPositionSummary> centred_hardvb_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["centred_hardvb"] = RelativePositionsSummary.mergePositions(centred_hardvb_positions).toHtml(process);
            String centred_hardvb_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in centred_hardvb_positions)
            {
                centred_hardvb_positions_html += position.situationHtml(process);
            }
            ViewData["centred_hardvb_positions"] = centred_hardvb_positions_html;

            List<RelativeBloqBloqPositionSummary> centred_near_lanes_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.centralBloqBloqFilter));
            ViewData["centred_near_lanes"] = RelativePositionsSummary.mergePositions(centred_near_lanes_positions).toHtml(process);
            String centred_near_lanes_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in centred_near_lanes_positions)
            {
                centred_near_lanes_positions_html += position.situationHtml(process);
            }
            ViewData["centred_near_lanes_positions"] = centred_near_lanes_positions_html;

            List<RelativeBloqBloqPositionSummary> centred_inlines_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.strictInlineBloqBloqFilter));
            ViewData["centred_inlines"] = RelativePositionsSummary.mergePositions(centred_inlines_positions).toHtml(process);
            String centred_inlines_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in centred_inlines_positions)
            {
                centred_inlines_positions_html += position.situationHtml(process);
            }
            ViewData["centred_inlines_positions"] = centred_inlines_positions_html;

            List<RelativeBloqBloqPositionSummary> centred_other_bloqs_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.otherBloqBloqFilter));
            ViewData["centred_other_bloqs"] = RelativePositionsSummary.mergePositions(centred_other_bloqs_positions).toHtml(process);
            String centred_other_bloqs_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in centred_other_bloqs_positions)
            {
                centred_other_bloqs_positions_html += position.situationHtml(process);
            }
            ViewData["centred_other_bloqs_positions"] = centred_other_bloqs_positions_html;

            // Wall-bloq
            List<RelativeWallBloqPositionSummary> centred_full_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.fullWallBloqFilter));
            ViewData["centred_full_wall"] = RelativePositionsSummary.mergePositions(centred_full_wall_positions).toHtml(process);
            String centred_full_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in centred_full_wall_positions)
            {
                centred_full_wall_positions_html += position.situationHtml(process);
            }
            ViewData["centred_full_wall_positions"] = centred_full_wall_positions_html;

            List<RelativeWallBloqPositionSummary> centred_squat_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.squatWallBloqFilter));
            ViewData["centred_squat_wall"] = RelativePositionsSummary.mergePositions(centred_squat_wall_positions).toHtml(process);
            String centred_squat_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in centred_squat_wall_positions)
            {
                centred_squat_wall_positions_html += position.situationHtml(process);
            }
            ViewData["centred_squat_wall_positions"] = centred_squat_wall_positions_html;

            List<RelativeWallBloqPositionSummary> centred_side_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.sideWallBloqFilter));
            ViewData["centred_side_wall"] = RelativePositionsSummary.mergePositions(centred_side_wall_positions).toHtml(process);
            String centred_side_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in centred_side_wall_positions)
            {
                centred_side_wall_positions_html += position.situationHtml(process);
            }
            ViewData["centred_side_wall_positions"] = centred_side_wall_positions_html;

            List<RelativeWallBloqPositionSummary> centred_low_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.CENTRED), RelativePositionsSummary.lowWallBloqFilter));
            ViewData["centred_low_wall"] = RelativePositionsSummary.mergePositions(centred_low_wall_positions).toHtml(process);
            String centred_low_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in centred_low_wall_positions)
            {
                centred_low_wall_positions_html += position.situationHtml(process);
            }
            ViewData["centred_low_wall_positions"] = centred_low_wall_positions_html;

            // Left lean
            // Bloq-bloq
            List<RelativeBloqBloqPositionSummary> lean_left_hardvb_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["lean_left_hardvb"] = RelativePositionsSummary.mergePositions(lean_left_hardvb_positions).toHtml(process);
            String lean_left_hardvb_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_left_hardvb_positions)
            {
                lean_left_hardvb_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_hardvb_positions"] = lean_left_hardvb_positions_html;

            List<RelativeBloqBloqPositionSummary> lean_left_near_lanes_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.centralBloqBloqFilter));
            ViewData["lean_left_near_lanes"] = RelativePositionsSummary.mergePositions(lean_left_near_lanes_positions).toHtml(process);
            String lean_left_near_lanes_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_left_near_lanes_positions)
            {
                lean_left_near_lanes_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_near_lanes_positions"] = lean_left_near_lanes_positions_html;

            List<RelativeBloqBloqPositionSummary> lean_left_inlines_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.strictInlineBloqBloqFilter));
            ViewData["lean_left_inlines"] = RelativePositionsSummary.mergePositions(lean_left_inlines_positions).toHtml(process);
            String lean_left_inlines_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_left_inlines_positions)
            {
                lean_left_inlines_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_inlines_positions"] = lean_left_inlines_positions_html;

            List<RelativeBloqBloqPositionSummary> lean_left_other_bloqs_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.otherBloqBloqFilter));
            ViewData["lean_left_other_bloqs"] = RelativePositionsSummary.mergePositions(lean_left_other_bloqs_positions).toHtml(process);
            String lean_left_other_bloqs_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_left_other_bloqs_positions)
            {
                lean_left_other_bloqs_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_other_bloqs_positions"] = lean_left_other_bloqs_positions_html;

            // Wall-bloq
            List<RelativeWallBloqPositionSummary> lean_left_full_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.fullWallBloqFilter));
            ViewData["lean_left_full_wall"] = RelativePositionsSummary.mergePositions(lean_left_full_wall_positions).toHtml(process);
            String lean_left_full_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_left_full_wall_positions)
            {
                lean_left_full_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_full_wall_positions"] = lean_left_full_wall_positions_html;

            List<RelativeWallBloqPositionSummary> lean_left_squat_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.squatWallBloqFilter));
            ViewData["lean_left_squat_wall"] = RelativePositionsSummary.mergePositions(lean_left_squat_wall_positions).toHtml(process);
            String lean_left_squat_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_left_squat_wall_positions)
            {
                lean_left_squat_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_squat_wall_positions"] = lean_left_squat_wall_positions_html;

            List<RelativeWallBloqPositionSummary> lean_left_side_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.sideWallBloqFilter));
            ViewData["lean_left_side_wall"] = RelativePositionsSummary.mergePositions(lean_left_side_wall_positions).toHtml(process);
            String lean_left_side_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_left_side_wall_positions)
            {
                lean_left_side_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_side_wall_positions"] = lean_left_side_wall_positions_html;

            List<RelativeWallBloqPositionSummary> lean_left_low_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT), RelativePositionsSummary.lowWallBloqFilter));
            ViewData["lean_left_low_wall"] = RelativePositionsSummary.mergePositions(lean_left_low_wall_positions).toHtml(process);
            String lean_left_low_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_left_low_wall_positions)
            {
                lean_left_low_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_left_low_wall_positions"] = lean_left_low_wall_positions_html;

            // Right lean
            // Bloq-bloq
            List<RelativeBloqBloqPositionSummary> lean_right_hardvb_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["lean_right_hardvb"] = RelativePositionsSummary.mergePositions(lean_right_hardvb_positions).toHtml(process);
            String lean_right_hardvb_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_right_hardvb_positions)
            {
                lean_right_hardvb_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_hardvb_positions"] = lean_right_hardvb_positions_html;

            List<RelativeBloqBloqPositionSummary> lean_right_near_lanes_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.centralBloqBloqFilter));
            ViewData["lean_right_near_lanes"] = RelativePositionsSummary.mergePositions(lean_right_near_lanes_positions).toHtml(process);
            String lean_right_near_lanes_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_right_near_lanes_positions)
            {
                lean_right_near_lanes_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_near_lanes_positions"] = lean_right_near_lanes_positions_html;

            List<RelativeBloqBloqPositionSummary> lean_right_inlines_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.strictInlineBloqBloqFilter));
            ViewData["lean_right_inlines"] = RelativePositionsSummary.mergePositions(lean_right_inlines_positions).toHtml(process);
            String lean_right_inlines_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_right_inlines_positions)
            {
                lean_right_inlines_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_inlines_positions"] = lean_right_inlines_positions_html;

            List<RelativeBloqBloqPositionSummary> lean_right_other_bloqs_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.otherBloqBloqFilter));
            ViewData["lean_right_other_bloqs"] = RelativePositionsSummary.mergePositions(lean_right_other_bloqs_positions).toHtml(process);
            String lean_right_other_bloqs_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in lean_right_other_bloqs_positions)
            {
                lean_right_other_bloqs_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_other_bloqs_positions"] = lean_right_other_bloqs_positions_html;

            // Wall-bloq
            List<RelativeWallBloqPositionSummary> lean_right_full_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.fullWallBloqFilter));
            ViewData["lean_right_full_wall"] = RelativePositionsSummary.mergePositions(lean_right_full_wall_positions).toHtml(process);
            String lean_right_full_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_right_full_wall_positions)
            {
                lean_right_full_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_full_wall_positions"] = lean_right_full_wall_positions_html;

            List<RelativeWallBloqPositionSummary> lean_right_squat_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.squatWallBloqFilter));
            ViewData["lean_right_squat_wall"] = RelativePositionsSummary.mergePositions(lean_right_squat_wall_positions).toHtml(process);
            String lean_right_squat_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_right_squat_wall_positions)
            {
                lean_right_squat_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_squat_wall_positions"] = lean_right_squat_wall_positions_html;

            List<RelativeWallBloqPositionSummary> lean_right_side_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.sideWallBloqFilter));
            ViewData["lean_right_side_wall"] = RelativePositionsSummary.mergePositions(lean_right_side_wall_positions).toHtml(process);
            String lean_right_side_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_right_side_wall_positions)
            {
                lean_right_side_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_side_wall_positions"] = lean_right_side_wall_positions_html;

            List<RelativeWallBloqPositionSummary> lean_right_low_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT), RelativePositionsSummary.lowWallBloqFilter));
            ViewData["lean_right_low_wall"] = RelativePositionsSummary.mergePositions(lean_right_low_wall_positions).toHtml(process);
            String lean_right_low_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in lean_right_low_wall_positions)
            {
                lean_right_low_wall_positions_html += position.situationHtml(process);
            }
            ViewData["lean_right_low_wall_positions"] = lean_right_low_wall_positions_html;

            // Squat
            // Bloq-bloq
            List<RelativeBloqBloqPositionSummary> squat_hardvb_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["squat_hardvb"] = RelativePositionsSummary.mergePositions(squat_hardvb_positions).toHtml(process);
            String squat_hardvb_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_hardvb_positions)
            {
                squat_hardvb_positions_html += position.situationHtml(process);
            }
            ViewData["squat_hardvb_positions"] = squat_hardvb_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_near_lanes_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.centralBloqBloqFilter));
            ViewData["squat_near_lanes"] = RelativePositionsSummary.mergePositions(squat_near_lanes_positions).toHtml(process);
            String squat_near_lanes_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_near_lanes_positions)
            {
                squat_near_lanes_positions_html += position.situationHtml(process);
            }
            ViewData["squat_near_lanes_positions"] = squat_near_lanes_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_inlines_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.strictInlineBloqBloqFilter));
            ViewData["squat_inlines"] = RelativePositionsSummary.mergePositions(squat_inlines_positions).toHtml(process);
            String squat_inlines_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_inlines_positions)
            {
                squat_inlines_positions_html += position.situationHtml(process);
            }
            ViewData["squat_inlines_positions"] = squat_inlines_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_other_bloqs_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.otherBloqBloqFilter));
            ViewData["squat_other_bloqs"] = RelativePositionsSummary.mergePositions(squat_other_bloqs_positions).toHtml(process);
            String squat_other_bloqs_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_other_bloqs_positions)
            {
                squat_other_bloqs_positions_html += position.situationHtml(process);
            }
            ViewData["squat_other_bloqs_positions"] = squat_other_bloqs_positions_html;

            // Wall-bloq
            List<RelativeWallBloqPositionSummary> squat_full_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.fullWallBloqFilter));
            ViewData["squat_full_wall"] = RelativePositionsSummary.mergePositions(squat_full_wall_positions).toHtml(process);
            String squat_full_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_full_wall_positions)
            {
                squat_full_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_full_wall_positions"] = squat_full_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_squat_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.squatWallBloqFilter));
            ViewData["squat_squat_wall"] = RelativePositionsSummary.mergePositions(squat_squat_wall_positions).toHtml(process);
            String squat_squat_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_squat_wall_positions)
            {
                squat_squat_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_squat_wall_positions"] = squat_squat_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_side_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.sideWallBloqFilter));
            ViewData["squat_side_wall"] = RelativePositionsSummary.mergePositions(squat_side_wall_positions).toHtml(process);
            String squat_side_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_side_wall_positions)
            {
                squat_side_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_side_wall_positions"] = squat_side_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_low_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.SQUAT), RelativePositionsSummary.lowWallBloqFilter));
            ViewData["squat_low_wall"] = RelativePositionsSummary.mergePositions(squat_low_wall_positions).toHtml(process);
            String squat_low_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_low_wall_positions)
            {
                squat_low_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_low_wall_positions"] = squat_low_wall_positions_html;

            // Squat left
            // Bloq-bloq
            List<RelativeBloqBloqPositionSummary> squat_left_hardvb_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["squat_left_hardvb"] = RelativePositionsSummary.mergePositions(squat_left_hardvb_positions).toHtml(process);
            String squat_left_hardvb_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_left_hardvb_positions)
            {
                squat_left_hardvb_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_hardvb_positions"] = squat_left_hardvb_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_left_near_lanes_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.centralBloqBloqFilter));
            ViewData["squat_left_near_lanes"] = RelativePositionsSummary.mergePositions(squat_left_near_lanes_positions).toHtml(process);
            String squat_left_near_lanes_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_left_near_lanes_positions)
            {
                squat_left_near_lanes_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_near_lanes_positions"] = squat_left_near_lanes_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_left_inlines_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.strictInlineBloqBloqFilter));
            ViewData["squat_left_inlines"] = RelativePositionsSummary.mergePositions(squat_left_inlines_positions).toHtml(process);
            String squat_left_inlines_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_left_inlines_positions)
            {
                squat_left_inlines_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_inlines_positions"] = squat_left_inlines_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_left_other_bloqs_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.otherBloqBloqFilter));
            ViewData["squat_left_other_bloqs"] = RelativePositionsSummary.mergePositions(squat_left_other_bloqs_positions).toHtml(process);
            String squat_left_other_bloqs_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_left_other_bloqs_positions)
            {
                squat_left_other_bloqs_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_other_bloqs_positions"] = squat_left_other_bloqs_positions_html;

            // Wall-bloq
            List<RelativeWallBloqPositionSummary> squat_left_full_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.fullWallBloqFilter));
            ViewData["squat_left_full_wall"] = RelativePositionsSummary.mergePositions(squat_left_full_wall_positions).toHtml(process);
            String squat_left_full_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_left_full_wall_positions)
            {
                squat_left_full_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_full_wall_positions"] = squat_left_full_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_left_squat_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.squatWallBloqFilter));
            ViewData["squat_left_squat_wall"] = RelativePositionsSummary.mergePositions(squat_left_squat_wall_positions).toHtml(process);
            String squat_left_squat_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_left_squat_wall_positions)
            {
                squat_left_squat_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_squat_wall_positions"] = squat_left_squat_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_left_side_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.sideWallBloqFilter));
            ViewData["squat_left_side_wall"] = RelativePositionsSummary.mergePositions(squat_left_side_wall_positions).toHtml(process);
            String squat_left_side_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_left_side_wall_positions)
            {
                squat_left_side_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_side_wall_positions"] = squat_left_side_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_left_low_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.LEFT_SQUAT), RelativePositionsSummary.lowWallBloqFilter));
            ViewData["squat_left_low_wall"] = RelativePositionsSummary.mergePositions(squat_left_low_wall_positions).toHtml(process);
            String squat_left_low_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_left_low_wall_positions)
            {
                squat_left_low_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_left_low_wall_positions"] = squat_left_low_wall_positions_html;

            // Squat right
            // Bloq-bloq
            List<RelativeBloqBloqPositionSummary> squat_right_hardvb_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.hardVBBloqBloqFilter));
            ViewData["squat_right_hardvb"] = RelativePositionsSummary.mergePositions(squat_right_hardvb_positions).toHtml(process);
            String squat_right_hardvb_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_right_hardvb_positions)
            {
                squat_right_hardvb_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_hardvb_positions"] = squat_right_hardvb_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_right_near_lanes_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.centralBloqBloqFilter));
            ViewData["squat_right_near_lanes"] = RelativePositionsSummary.mergePositions(squat_right_near_lanes_positions).toHtml(process);
            String squat_right_near_lanes_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_right_near_lanes_positions)
            {
                squat_right_near_lanes_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_near_lanes_positions"] = squat_right_near_lanes_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_right_inlines_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.strictInlineBloqBloqFilter));
            ViewData["squat_right_inlines"] = RelativePositionsSummary.mergePositions(squat_right_inlines_positions).toHtml(process);
            String squat_right_inlines_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_right_inlines_positions)
            {
                squat_right_inlines_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_inlines_positions"] = squat_right_inlines_positions_html;

            List<RelativeBloqBloqPositionSummary> squat_right_other_bloqs_positions = summary.filterBloqBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureBloqBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.otherBloqBloqFilter));
            ViewData["squat_right_other_bloqs"] = RelativePositionsSummary.mergePositions(squat_right_other_bloqs_positions).toHtml(process);
            String squat_right_other_bloqs_positions_html = "";
            foreach (RelativeBloqBloqPositionSummary position in squat_right_other_bloqs_positions)
            {
                squat_right_other_bloqs_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_other_bloqs_positions"] = squat_right_other_bloqs_positions_html;

            // Wall-bloq
            List<RelativeWallBloqPositionSummary> squat_right_full_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.fullWallBloqFilter));
            ViewData["squat_right_full_wall"] = RelativePositionsSummary.mergePositions(squat_right_full_wall_positions).toHtml(process);
            String squat_right_full_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_right_full_wall_positions)
            {
                squat_right_full_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_full_wall_positions"] = squat_right_full_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_right_squat_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.squatWallBloqFilter));
            ViewData["squat_right_squat_wall"] = RelativePositionsSummary.mergePositions(squat_right_squat_wall_positions).toHtml(process);
            String squat_right_squat_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_right_squat_wall_positions)
            {
                squat_right_squat_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_squat_wall_positions"] = squat_right_squat_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_right_side_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.sideWallBloqFilter));
            ViewData["squat_right_side_wall"] = RelativePositionsSummary.mergePositions(squat_right_side_wall_positions).toHtml(process);
            String squat_right_side_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_right_side_wall_positions)
            {
                squat_right_side_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_side_wall_positions"] = squat_right_side_wall_positions_html;

            List<RelativeWallBloqPositionSummary> squat_right_low_wall_positions = summary.filterWallBloqPositions(RelativePositionsSummary.andFilter(RelativePositionsSummary.byPostureWallBloqFilter(PlayerPosture.RIGHT_SQUAT), RelativePositionsSummary.lowWallBloqFilter));
            ViewData["squat_right_low_wall"] = RelativePositionsSummary.mergePositions(squat_right_low_wall_positions).toHtml(process);
            String squat_right_low_wall_positions_html = "";
            foreach (RelativeWallBloqPositionSummary position in squat_right_low_wall_positions)
            {
                squat_right_low_wall_positions_html += position.situationHtml(process);
            }
            ViewData["squat_right_low_wall_positions"] = squat_right_low_wall_positions_html;
        }        
    }
}
