namespace BSVisionCalculator
{
    public class RelativeTimeRegimeCalculation
    {
        // We use this situation as a working object, modifying it but keeping the important parts of it.
        public VisionCalculationSituation situation;
        public List<RelativeTimeVisionState> states;
        public bool hardvb;

        public RelativeTimeRegimeCalculation(VisionCalculationSituation situation, bool hardvb)
        {
            this.situation = situation;
            this.states = new List<RelativeTimeVisionState>();
            this.hardvb = hardvb;
        }

        // Initialize it to the objects being one HJD away or being at the same time.
        public void initialize()
        {
            // Minimum relative distance: Same time (granularity difference to avoid weird behaviours)
            RelativeTimeVisionState same_time = this.getState(this.situation.reality.process.time_granularity);

            // Maximum relative distance: HJD
            RelativeTimeVisionState hjd_away = this.getState(this.situation.reality.process.time_reaction - this.situation.reality.process.time_granularity);

            this.states.Add(same_time);
            this.states.Add(hjd_away);
        }

        public RelativeTimeVisionState getState(double time_relative)
        {
            this.situation.blocked.time = GlobalParameters.getBlockerTime(this.situation.blocker) + time_relative;
            RealTimeRegimeCalculation calculation = new RealTimeRegimeCalculation(this.situation);
            calculation.processAll();
            RealTimeRegimeSummary summary = calculation.getSummary();
            return new RelativeTimeVisionState(time_relative, summary, this.situation.blocked.time, this.situation.reality.process, this.hardvb);
        }

        // Main loop.
        public void processAll()
        {
            Random rnd = new Random();
            this.initialize();
            bool some_change = true;
            while (some_change)
            {
                //System.Diagnostics.Debug.WriteLine("ANOTHER ROUND DOWN THE LOOP");
                //System.Diagnostics.Debug.WriteLine("States updated. " + this.states.Count + " states.");
                //this.mergeAll();
                //System.Diagnostics.Debug.WriteLine("States merged. " + this.states.Count + " states.");
                //System.Diagnostics.Debug.WriteLine(VisionCalculationTestCommons.getRealTimeRegimeCalculationString(this));
                some_change = false;
                // We use random module here to decide where we start the check on each round, to hopefully be more likely to find vision block states earlier.
                int offset = rnd.Next(this.states.Count);
                //System.Diagnostics.Debug.WriteLine("Offset: " + offset);
                for (int i = mathmod(offset + 1, this.states.Count), n = 0; !some_change && n < this.states.Count; i = mathmod(i + 1, this.states.Count), n++)
                {
                    // We process pairs, so the start we do not compute.
                    if (i == 0)
                    {
                        continue;
                    }
                    //System.Diagnostics.Debug.WriteLine("About to process pair: " + i);
                    some_change = this.processPair(i);
                    //System.Diagnostics.Debug.WriteLine("Pair processed: " + some_change);
                }
            }
        }

        public static int mathmod(int a, int b)
        {
            return ((a % b) + b) % b;
        }

        public RelativeTimeRegimeSummary getSummary()
        {
            RelativeTimeRegimeSummary result = new RelativeTimeRegimeSummary();
            RelativeTimeRegime previous = new RelativeTimeRegime(this.states[0].isOk(), this.states[0].time_relative);
            result.regimes.Add(previous);
            for (int i = 1; i < this.states.Count; i++)
            {
                RelativeTimeRegime current = new RelativeTimeRegime(this.states[i].isOk(), this.states[i].time_relative);
                if (current.ok != previous.ok)
                {
                    previous.time_end = this.states[i].time_relative;
                    previous = current;
                    result.regimes.Add(current);
                }
            }
            previous.time_end = this.states[this.states.Count - 1].time_relative;

            return result;
        }

        // This returns true if and only if some change is performed on the list of states (and thus we have to restart the checks).
        // i should be the index of next, which is also where we would insert the middle point.
        // This function may add a middle point, but nothing else.
        // Other functions remove redundant points and update preblock/postblock state.
        public bool processPair(int i)
        {
            RelativeTimeVisionState previous = this.states[i - 1];
            RelativeTimeVisionState next = this.states[i];
            RelativeTimeVisionState middle;

            /*System.Diagnostics.Debug.WriteLine("Previous relative time: " + previous.time_relative);
            System.Diagnostics.Debug.WriteLine("Next relative time: " + next.time_relative);*/

            // First check if they are not already within the granularity. If they are, pass.
            if (next.time_relative - previous.time_relative <= this.situation.reality.process.time_granularity)
            {
                return false;
            }

            /*
            System.Diagnostics.Debug.WriteLine("Previous OK first: " + previous.ok_first);
            System.Diagnostics.Debug.WriteLine("Next OK first: " + next.ok_first);
            System.Diagnostics.Debug.WriteLine("Previous OK last: " + previous.ok_last);
            System.Diagnostics.Debug.WriteLine("Next OK last: " + next.ok_last);
            System.Diagnostics.Debug.WriteLine("Previous OK process: " + previous.ok_process);
            System.Diagnostics.Debug.WriteLine("Next OK process: " + next.ok_process);
            */
            // Create a middle point if for all parameters, at least one of the two is within parameter (ok).
            // If for just one parameter, both previous and next are not okay, then the middle point will still be not okay for that parameter, and thus won't need to be checked.
            if ((previous.ok_first || next.ok_first) && (previous.ok_last || next.ok_last) && (previous.ok_process || next.ok_process))
            {
                // We could use weighted average here: The further from the parameter it is, the further away (approximately) we need to move away from it.
                // However, the details are tedious and probably not worth it since binary search should be good enough in practice.
                middle = this.getState((next.time_relative + previous.time_relative) / 2);
                /*System.Diagnostics.Debug.WriteLine("Generating middle!");
                System.Diagnostics.Debug.WriteLine("Middle time: " + middle.time_relative);*/
                this.states.Insert(i, middle);
                return true;
            }

            return false;
        }

        // Merge conditions are a little complicated for now so we just don't do it.
        /*
        public void mergeAll()
        {
            // We assume there is always at least two states.
            RelativeTimeVisionState last = this.states[0];
            int equal_count = 1;
            for (int i = 1; i < this.states.Count; i++)
            {
                RelativeTimeVisionState cur = this.states[i];
                
                if (cur.ok_first == last.ok_first && cur.ok_last == last.ok_last && cur.ok_process == last.ok_process)
                {
                    equal_count++;
                    if (equal_count > 2)
                    {
                        // Remove the previous one and reduce the count.
                        this.states.RemoveAt(i - 1);
                        // In place modification of the list, adjust the index.
                        i--;
                        equal_count--;
                    }
                }
                else
                {
                    equal_count = 1;
                }

                last = cur;
            }
        }
        */
    }
}
