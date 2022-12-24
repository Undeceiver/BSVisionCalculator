namespace BSVisionCalculator
{
    public class RelativeWallBloqPositionSummary
    {
        public BSWall blocker_wall;
        public BSBloq blocked_bloq;

        public PlayerPosture posture;

        public RelativeTimeRegimeSummary regimes;

        public RelativeWallBloqPositionSummary(BSWall blocker_wall, BSBloq blocked_bloq, PlayerPosture posture, RelativeTimeRegimeSummary regimes)
        {
            this.blocker_wall = blocker_wall;
            this.blocked_bloq = blocked_bloq;
            this.posture = posture;

            this.regimes = regimes;
        }

        public String situationHtml(VisionCalculationProcess process)
        {
            String result = "";
            int blocker_bottom_row_reversed = 3 - this.blocker_wall.height_level_bottom.index;
            int blocker_top_row_reversed = 3 - this.blocker_wall.height_level_top.index;
            int blocked_row_reversed = 3 - this.blocked_bloq.row.getIndex();
            int blocker_lane_left_p1 = this.blocker_wall.lane_left.getIndex() + 1;
            int blocker_lane_right_p1 = this.blocker_wall.lane_right.getIndex() + 1;
            int blocked_lane_p1 = this.blocked_bloq.lane.getIndex() + 1;

            //result += "<div>Bottom is: " + this.blocker_wall.height_level_bottom.index + ", top is: " + this.blocker_wall.height_level_top.index + ", bottom reversed is: " + blocker_bottom_row_reversed + ", top reversed is: " + blocker_top_row_reversed + "</div>";

            result += "<div class='situation_summary'>";
            {
                result += "<div class='situation'>";
                {
                    result += "<div class='front_grid'>";
                    {
                        for (int row = 1; row <= 3; row++)
                        {
                            for (int column = 1; column <= 4; column++)
                            {
                                if (row > blocker_top_row_reversed && row <= blocker_bottom_row_reversed && column >= blocker_lane_left_p1 && column <= blocker_lane_right_p1)
                                {
                                    result += "<div class='situation_cell'><img class='situation_wall' src='images/wall.png'/></div>";
                                }
                                else
                                {
                                    result += "<div class='situation_cell'></div>";
                                }
                            }
                        }
                    }
                    result += "</div>";
                    result += "<div class='situation_arrow'><span>&#8594;</span></div>";
                    result += "<div class='back_grid'>";
                    {
                        for (int row = 1; row <= 3; row++)
                        {
                            for (int column = 1; column <= 4; column++)
                            {
                                if (row == blocked_row_reversed && column == blocked_lane_p1)
                                {
                                    result += "<div class='situation_cell'><img class='situation_bloq' src='images/red.jpg'/></div>";
                                }
                                else
                                {
                                    result += "<div class='situation_cell'></div>";
                                }
                            }
                        }
                    }
                    result += "</div>";
                }
                result += "</div>";

                result += "<div>";
                {
                    result += this.regimes.toHtml(process);
                }
                result += "</div>";
            }
            result += "</div>";

            return result;
        }
    }
}
