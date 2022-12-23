using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BSVisionCalculator
{
    public class RelativeBloqBloqPositionSummary
    {
        public BSBloq blocker_bloq;
        public BSBloq blocked_bloq;

        public PlayerPosture posture;

        public RelativeTimeRegimeSummary regimes;

        public RelativeBloqBloqPositionSummary(BSBloq blocker_bloq, BSBloq blocked_bloq, PlayerPosture posture, RelativeTimeRegimeSummary regimes)
        {
            this.blocker_bloq = blocker_bloq;
            this.blocked_bloq = blocked_bloq;
            this.posture = posture;

            this.regimes = regimes;
        }

        public String situationHtml(VisionCalculationProcess process)
        {
            String result = "";
            int blocker_row_reversed = 3 - this.blocker_bloq.row.getIndex();
            int blocked_row_reversed = 3 - this.blocked_bloq.row.getIndex();
            int blocker_lane_p1 = this.blocker_bloq.lane.getIndex() + 1;
            int blocked_lane_p1 = this.blocked_bloq.lane.getIndex() + 1;

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
                                if (row == blocker_row_reversed && column == blocker_lane_p1)
                                {
                                    result += "<div class='situation_cell'><img class='situation_bloq' src='images/blue.jpg'/></div>";
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
