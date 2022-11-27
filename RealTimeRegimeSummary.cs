namespace BSVisionCalculator
{
    public class RealTimeRegimeSummary
    {
        public List<RealTimeRegime> regimes;
        
        public RealTimeRegimeSummary()
        {
            this.regimes = new List<RealTimeRegime>();
        }

        public String toString()
        {
            String result = "";

            RealTimeRegime start = regimes[0];
            double time_start_total = start.time_start;
            double time_start_last = start.time_start;
            double time_end_last = start.time_end;
            bool vision_blocked_last = start.vision_blocked;
            bool spawning_last = start.spawning;

            result = result + "<p>When the second object spawns, it is " + blockedString(start.vision_blocked) + ".</p>";

            for(int i = 1; i < regimes.Count; i++)
            {
                RealTimeRegime cur = regimes[i];

                double time_diff = cur.time_start - time_start_last;
                double total_time_diff = cur.time_start - time_start_total;

                result = result + "<p>" + changeString(time_diff, total_time_diff);

                if (vision_blocked_last && (!cur.vision_blocked))
                {
                    result = result + ", the vision block ends.</p>";                    
                }
                else if((!vision_blocked_last) && cur.vision_blocked)
                {
                    result = result + ", the object becomes vision blocked.</p>";
                }
                else if(spawning_last && (!cur.spawning))
                {
                    result = result + ", the object finishes its spawning animation.</p>";
                }

                time_start_last = cur.time_start;
                time_end_last = cur.time_end;
                vision_blocked_last = cur.vision_blocked;
                spawning_last = cur.spawning;
            }

            result = result + "<p>" + timeString(time_end_last - time_start_last) + " later (" + timeString(time_end_last - time_start_total) + " total), the first object reaches the player.</p>";

            return result;
        }

        public String blockedString(bool vision_blocked)
        {
            if(vision_blocked)
            {
                return "vision blocked";
            }
            else
            {
                return "not vision blocked";
            }
        }

        public String timeString(double time)
        {
            return Math.Round(time * 1000) + "ms";
        }

        public String changeString(double time_diff, double total_time_diff)
        {
            return "After approximately " + timeString(time_diff) + " (" + timeString(total_time_diff) + " total)";
        }        
    }
}
