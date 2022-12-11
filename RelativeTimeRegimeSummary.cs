namespace BSVisionCalculator
{
    public class RelativeTimeRegimeSummary
    {
        public List<RelativeTimeRegime> regimes;

        public RelativeTimeRegimeSummary()
        {
            this.regimes = new List<RelativeTimeRegime>();
        }

        public String toString(VisionCalculationProcess process)
        {
            String result = "";

            for (int i = 0; i < regimes.Count; i++)
            {
                RelativeTimeRegime cur = regimes[i];

                double time_start_beats = process.secondsToBeats(cur.time_start);
                double time_end_beats = process.secondsToBeats(cur.time_end);

                result = result + "<p>When the objects are between " + time_start_beats + " and " + time_end_beats + " beats apart, " + okText(cur.ok) + "</p>";
            }

            return result;
        }

        public String okText(bool ok)
        {
            if(ok)
            {
                return "they are <span style=\"color:green\">correctly</span> within vision parameters.";
            }
            else
            {
                return "they <span style=\"color:red\">do not satisfy</span> vision parameters.";
            }
        }

        public RelativeTimeRegimeSummary merge(RelativeTimeRegimeSummary other)
        {
            RelativeTimeRegimeSummary result = new RelativeTimeRegimeSummary();

            int i = 0, j = 0;

            RelativeTimeRegime regime_this = this.regimes[i];
            RelativeTimeRegime regime_other = other.regimes[j];
            RelativeTimeRegime regime_new;

            double time_start_cur;
            bool ok_cur;
            bool this_cur;

            if(regime_this.time_start < regime_other.time_start)
            {
                time_start_cur = regime_this.time_start;
                ok_cur = regime_this.ok;
                this_cur = true;
            }
            else
            {
                time_start_cur = regime_other.time_start;
                ok_cur = regime_other.ok;
                this_cur = false;
            }

            while(i < this.regimes.Count && j < other.regimes.Count)
            {
                regime_this = this.regimes[i];
                regime_other = other.regimes[j];

                if(this_cur)
                {
                    if (ok_cur)
                    {
                        // We are extending an okay zone. Only extend if still okay.
                        if (regime_other.ok)
                        {
                            // Just extend.
                        }
                        else
                        {
                            // End the okay zone at the start of the other regime, and start a new not okay regime.
                            regime_new = new RelativeTimeRegime(ok_cur, time_start_cur);
                            regime_new.time_end = regime_other.time_start;

                            result.regimes.Add(regime_new);

                            time_start_cur = regime_other.time_start;
                            ok_cur = false;
                        }
                    }
                    else
                    {
                        // We are extending a not okay zone. Extend unless both regimes are okay in the new area.
                        if (regime_other.ok && regime_this.ok)
                        {
                            // We enter an okay zone. Note this can happen because the previous section might have been not okay due to a regime we left behind.
                            regime_new = new RelativeTimeRegime(ok_cur, time_start_cur);
                            regime_new.time_end = regime_other.time_start;

                            result.regimes.Add(regime_new);

                            time_start_cur = regime_other.time_start;
                            ok_cur = true;
                        }
                        else
                        {
                            // Just extend
                        }
                    }
                }
                else
                {
                    if (ok_cur)
                    {
                        // We are extending an okay zone. Only extend if still okay.
                        if (regime_this.ok)
                        {
                            // Just extend.
                        }
                        else
                        {
                            // End the okay zone at the start of the other regime, and start a new not okay regime.
                            regime_new = new RelativeTimeRegime(ok_cur, time_start_cur);
                            regime_new.time_end = regime_this.time_start;

                            result.regimes.Add(regime_new);

                            time_start_cur = regime_this.time_start;
                            ok_cur = false;
                        }
                    }
                    else
                    {
                        // We are extending a not okay zone. Extend unless both regimes are okay in the new area.
                        if (regime_other.ok && regime_this.ok)
                        {
                            // We enter an okay zone. Note this can happen because the previous section might have been not okay due to a regime we left behind.
                            regime_new = new RelativeTimeRegime(ok_cur, time_start_cur);
                            regime_new.time_end = regime_this.time_start;

                            result.regimes.Add(regime_new);

                            time_start_cur = regime_this.time_start;
                            ok_cur = true;
                        }
                        else
                        {
                            // Just extend
                        }
                    }
                }

                // Check which segment to update next
                if(regime_this.time_end < regime_other.time_end)
                {
                    i++;
                    this_cur = false;
                }
                else
                {
                    j++;
                    this_cur = true;
                }
            }

            // Close last regime
            regime_new = new RelativeTimeRegime(ok_cur, time_start_cur);
            if (this_cur)
            {                
                regime_new.time_end = this.regimes[this.regimes.Count - 1].time_end;
            }
            else
            {
                regime_new.time_end = other.regimes[other.regimes.Count - 1].time_end;
            }

            result.regimes.Add(regime_new);

            return result;
        }
    }
}
