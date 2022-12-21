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

        public String situationHtml()
        {
            String result = "";
            int blocker_row_reversed = 3 - this.blocker_bloq.row.getIndex();
            int blocked_row_reversed = 3 - this.blocked_bloq.row.getIndex();
            int blocker_lane_p1 = this.blocker_bloq.lane.getIndex() + 1;
            int blocked_lane_p1 = this.blocked_bloq.lane.getIndex() + 1;

            result += "<div class=\"situation\">";
            {
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";
                result += "<div class=\"situation_cell\"/>";

                result += "<div class=\"front_bloq\" style=\"grid-area: " + blocker_row_reversed.ToString() + "/" + blocker_lane_p1.ToString() + "/" + blocker_row_reversed.ToString() + "/" + blocker_lane_p1.ToString() + ";\"><img class=\"situation_bloq\" src=\"images/blue.jpg\"/></div>";                    
                result += "<div class=\"back_bloq\" style=\"grid-area: " + blocked_row_reversed.ToString() + "/" + blocked_lane_p1.ToString() + "/" + blocked_row_reversed.ToString() + "/" + blocked_lane_p1.ToString() + ";'><img class=\"situation_bloq\" src=\"images/red.jpg\"/></div>";                
            }
            result += "</div>";

            return result;
        }        
    }
}
