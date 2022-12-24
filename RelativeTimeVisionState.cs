namespace BSVisionCalculator
{
    // This class represents a relative positioning of two objects and the timing ranges for vision associated with them in relation to the max/min parameters.
    public class RelativeTimeVisionState
    {
        // Time distance between the blocker and the blocked.
        public double time_relative;

        public double time_first;
        public double time_last;
        public double time_process;

        public bool ok_first;
        public bool ok_last;
        public bool ok_process;            

        public RelativeTimeVisionState(double time_relative, RealTimeRegimeSummary regimes, double time_blocked, VisionCalculationProcess process, bool hardvb)
        {
            this.time_relative = time_relative;
            this.time_first = 0;
            this.time_last = process.time_reaction;
            this.time_process = 0;

            foreach(RealTimeRegime regime in regimes.regimes)                
            {
                if(!regime.vision_blocked)                {               
                    
                    double cur_time_first = time_blocked - regime.time_start;
                    if(cur_time_first > this.time_first)
                    {
                        this.time_first = cur_time_first;
                    }

                    // Adjust to not count stuff that is too close to the time when you need to hit the block.
                    double time_end_adjusted = Math.Min(regime.time_end, time_blocked - process.time_last_counted);

                    // Check that the adjusted time is still within this regime.
                    if (time_end_adjusted > regime.time_start)
                    {
                        double cur_time_last = time_blocked - time_end_adjusted;
                        if (cur_time_last < this.time_last)
                        {
                            this.time_last = cur_time_last;
                        }

                        this.time_process += time_end_adjusted - regime.time_start;
                    }                    
                }
            }

            // After the first object disappears, we now see the object, this is like an extra regime.
            // This goes from the moment the first object disappears to the maximum counted time (assuming that is positive time).
            double last_time_end = regimes.regimes[regimes.regimes.Count - 1].time_end;
            if(time_blocked - last_time_end > process.time_last_counted)
            {
                double extra_time_first = time_blocked - last_time_end;
                if(extra_time_first > this.time_first)
                {
                    this.time_first = extra_time_first;
                }

                double extra_time_last = process.time_last_counted;
                if(extra_time_last < this.time_last)
                {
                    this.time_last = extra_time_last;
                }

                this.time_process += (time_blocked - process.time_last_counted) - last_time_end;
            }

            if (hardvb)
            {
                this.ok_first = (this.time_first >= process.time_hardvb_first_min);
                this.ok_last = (this.time_last <= process.time_hardvb_last_max);
                this.ok_process = (this.time_process >= process.time_hardvb_process_min);
            }
            else
            {
                this.ok_first = (this.time_first >= process.time_inline_first_min);
                this.ok_last = (this.time_last <= process.time_inline_last_max);
                this.ok_process = (this.time_process >= process.time_inline_process_min);
            }
        }

        public bool isOk()
        {
            return ok_first && ok_last && ok_process;
        }
    }
}
