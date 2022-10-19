namespace BSVisionCalculator
{
    public class BSBloq : BSObject
    {        
        public BSLane lane;
        public BSRow row;
        public double width;
        public double height;
        public double depth; // We use the same object for the bloq as it moves over time. This value changes thus, as so do some others.
        public double time;
                        
        public BSBloq(double time, BSLane lane, BSRow row)
        {
            this.time = time;
            this.lane = lane;
            this.row = row;            
        }

        public double getRowHeight()
        {
            return this.row.getRealHeight(this.situation.reality);
        }

        public override void updatePosition(VisionCalculationSituation situation)
        {
            double height_row;

            this.width = this.lane.width;
            height_row = this.row.getRealHeight(situation.reality);

            // Spawn parabola
            this.height = height_row + (situation.reality.height_player_effective - GlobalParameters.height_row_mid_top - GlobalParameters.height_row_bottom_mid - height_row) * (this.depth * this.depth) / (situation.reality.process.jd * situation.reality.process.jd);
            /*System.Diagnostics.Debug.WriteLine("SOME BLOCK");
            System.Diagnostics.Debug.WriteLine("Target height:" + height_row);
            System.Diagnostics.Debug.WriteLine("Depth:" + this.depth);
            System.Diagnostics.Debug.WriteLine("JD:" + situation.reality.process.jd);
            System.Diagnostics.Debug.WriteLine("Current height:" + this.height);*/
        }

        // This works for both the blocker and the blocked
        public override void updateDepth(VisionCalculationSituation situation)
        {
            double time_delta = time - situation.time_player;
            this.depth = GlobalParameters.depth_playline + situation.reality.process.njs * time_delta;
        }
        public override void updateAngleValues(VisionCalculationSituation situation)
        {
            double width_left = this.width - GlobalParameters.size_bloq / 2;
            double width_right = this.width + GlobalParameters.size_bloq / 2;
            double height_top = this.height + GlobalParameters.size_bloq / 2;
            double height_bottom = this.height - GlobalParameters.size_bloq / 2;
            // Ignore depth
            double depth_start = this.depth; //- GlobalParameters.size_bloq / 2;
            double depth_end = this.depth; // + GlobalParameters.size_bloq / 2;

            double anglevalue_left_start = calculateAngleValue(width_left, situation.width_player, depth_start);
            double anglevalue_left_end = calculateAngleValue(width_left, situation.width_player, depth_end);
            double anglevalue_right_start = calculateAngleValue(width_right, situation.width_player, depth_start);
            double anglevalue_right_end = calculateAngleValue(width_right, situation.width_player, depth_end);
            double anglevalue_top_start = calculateAngleValue(height_top, situation.height_player, depth_start);
            double anglevalue_top_end = calculateAngleValue(height_top, situation.height_player, depth_end);
            double anglevalue_bottom_start = calculateAngleValue(height_bottom, situation.height_player, depth_start);
            double anglevalue_bottom_end = calculateAngleValue(height_bottom, situation.height_player, depth_end);

            this.anglevalue_left = Math.Min(anglevalue_left_start, anglevalue_left_end);
            this.anglevalue_right = Math.Max(anglevalue_right_start, anglevalue_right_end);
            this.anglevalue_top = Math.Max(anglevalue_top_start, anglevalue_top_end);
            this.anglevalue_bottom = Math.Max(anglevalue_bottom_start, anglevalue_bottom_end);
        }

        public override bool checkSpawning(VisionCalculationSituation situation)
        {
            double height_row = this.row.getRealHeight(situation.reality);

            double height_diff = Math.Abs(this.height - height_row);

            return (height_diff > Math.Max(GlobalParameters.height_row_bottom_mid, GlobalParameters.height_row_mid_top));
        }
    }
}
