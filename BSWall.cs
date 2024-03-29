﻿namespace BSVisionCalculator
{
    public class BSWall : BSObject
    {

        public BSLane lane_left;
        public BSLane lane_right;
        public BSWallHeight height_level_top;
        public BSWallHeight height_level_bottom;

        public double width_left;
        public double width_right;
        public double height_top;
        public double height_bottom;
        public double depth_start;
        public double depth_end;
        public double time_start;
        public double time_end;

        public BSWall(double time_start, double time_end, BSLane lane_left, BSLane lane_right, BSWallHeight height_level_top, BSWallHeight height_level_bottom)
        {
            this.time_start = time_start;
            this.time_end = time_end;
            this.lane_left = lane_left;
            this.lane_right = lane_right;
            this.height_level_top = height_level_top;
            this.height_level_bottom = height_level_bottom;
        }

        public override void updatePosition(VisionCalculationSituation situation)
        {
            this.width_left = this.lane_left.width - GlobalParameters.width_lane/2;
            this.width_right = this.lane_right.width + GlobalParameters.width_lane / 2;
            this.height_top = this.height_level_top.getWallHeight(situation.reality);
            this.height_bottom = this.height_level_bottom.getWallHeight(situation.reality);

            /*
            System.Diagnostics.Debug.WriteLine("WIDTH LEFT: " + this.width_left);
            System.Diagnostics.Debug.WriteLine("WIDTH RIGHT: " + this.width_right);
            System.Diagnostics.Debug.WriteLine("HEIGHT TOP: " + this.height_top);
            System.Diagnostics.Debug.WriteLine("HEIGHT BOTTOM: " + this.height_bottom);*/
        }

        public override void updateDepth(VisionCalculationSituation situation)
        {
            double time_delta_start = time_start - situation.time_player;
            double time_delta_end = time_end - situation.time_player;

            this.depth_start = Math.Min(situation.reality.process.jd,Math.Max(0,GlobalParameters.depth_playline + situation.reality.process.njs * time_delta_start));
            this.depth_end = Math.Min(situation.reality.process.jd, Math.Max(0,GlobalParameters.depth_playline + situation.reality.process.njs * time_delta_end));
        }
        public override void updateAngleValues(VisionCalculationSituation situation)
        {
            /*
            System.Diagnostics.Debug.WriteLine("CALCULATING ANGLE VALUE FOR WALL");
            System.Diagnostics.Debug.WriteLine("Width right: " + this.width_right);
            System.Diagnostics.Debug.WriteLine("Depth: " + this.depth_start);
            */

            double anglevalue_left_start = calculateAngleValue(this.width_left, situation.width_player, this.depth_start);
            double anglevalue_left_end = calculateAngleValue(this.width_left, situation.width_player, this.depth_end);
            double anglevalue_right_start = calculateAngleValue(this.width_right, situation.width_player, this.depth_start);
            double anglevalue_right_end = calculateAngleValue(this.width_right, situation.width_player, this.depth_end);
            double anglevalue_top_start = calculateAngleValue(this.height_top, situation.height_player, this.depth_start);
            double anglevalue_top_end = calculateAngleValue(this.height_top, situation.height_player, this.depth_end);
            double anglevalue_bottom_start = calculateAngleValue(this.height_bottom, situation.height_player, this.depth_start);
            double anglevalue_bottom_end = calculateAngleValue(this.height_bottom, situation.height_player, this.depth_end);

            /*
            System.Diagnostics.Debug.WriteLine("Angle value right start: " + anglevalue_right_start);
            System.Diagnostics.Debug.WriteLine("Angle value right end: " + anglevalue_right_end);
            */

            this.anglevalue_left = Math.Min(anglevalue_left_start, anglevalue_left_end);
            this.anglevalue_right = Math.Max(anglevalue_right_start, anglevalue_right_end);
            this.anglevalue_top = Math.Max(anglevalue_top_start, anglevalue_top_end);
            this.anglevalue_bottom = Math.Max(anglevalue_bottom_start, anglevalue_bottom_end);

            /*
            System.Diagnostics.Debug.WriteLine("Angle value right: " + this.anglevalue_right);
            */
        }

        public override bool checkSpawning(VisionCalculationSituation situation)
        {
            return false;
        }
    }
}
