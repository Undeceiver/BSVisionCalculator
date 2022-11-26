namespace BSVisionCalculator
{
    public class RealTimeRegimeCalculation
    {
        // We use this situation as a working object, modifying it but keeping the important parts of it.
        public VisionCalculationSituation situation;
        public List<RealTimeVisionState> states;

        public RealTimeRegimeCalculation(VisionCalculationSituation situation)
        {
            this.situation = situation;
            this.states = new List<RealTimeVisionState>();
        }

        // Initialize it to the player being at the time the blocked spawns and at the time the blocker disappears. We assume the objects are ordered adequately.
        public void initialize()
        {
            // First possible vision block: Time the blocked object spawns, which is the blocked's time minus the reaction time.            
            RealTimeVisionState blocked_spawn = this.getState(GlobalParameters.getBlockedTime(this.situation.blocked) - this.situation.reality.process.time_reaction);

            // Last possible vision block: Time the blocker goes past the player. We account here for the play line distance, by accounting for the NJS.
            // We slightly adjust the playline to prevent negative values that screw everything up.            
            RealTimeVisionState blocker_past = this.getState(GlobalParameters.getBlockerTime(this.situation.blocker) + GlobalParameters.depth_playline * 0.95 / this.situation.reality.process.njs);

            this.states.Add(blocked_spawn);
            this.states.Add(blocker_past);            
        }

        public RealTimeVisionState getState(double time)
        {
            this.situation.time_player = time;
            return new RealTimeVisionState(this.situation);
        }

        // Main loop.
        public void processAll()
        {
            Random rnd = new Random();
            this.initialize();
            bool some_change = true;
            while(some_change)
            {
                //System.Diagnostics.Debug.WriteLine("ANOTHER ROUND DOWN THE LOOP");
                this.updateStates();
                //System.Diagnostics.Debug.WriteLine("States updated. " + this.states.Count + " states.");
                this.mergeAll();
                //System.Diagnostics.Debug.WriteLine("States merged. " + this.states.Count + " states.");
                //System.Diagnostics.Debug.WriteLine(VisionCalculationTestCommons.getRealTimeRegimeCalculationString(this));
                some_change = false;
                // We use random module here to decide where we start the check on each round, to hopefully be more likely to find vision block states earlier.
                int offset = rnd.Next(this.states.Count);
                //System.Diagnostics.Debug.WriteLine("Offset: " + offset);
                for (int i = mathmod(offset + 1,this.states.Count), n = 0; !some_change && n < this.states.Count; i = mathmod(i+1,this.states.Count), n++)
                {
                    // We process pairs, so the start we do not compute.
                    if(i == 0)
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

        public RealTimeRegimeSummary getSummary()
        {
            RealTimeRegimeSummary result = new RealTimeRegimeSummary();
            RealTimeRegime previous = new RealTimeRegime(this.states[0].spawning, this.states[0].vision_blocked, this.states[0].time);
            result.regimes.Add(previous);
            for(int i = 1; i < this.states.Count; i++)
            {
                RealTimeRegime current = new RealTimeRegime(this.states[i].spawning, this.states[i].vision_blocked, this.states[i].time);
                if(current.spawning != previous.spawning || current.vision_blocked != previous.vision_blocked)
                {
                    previous.time_end = this.states[i].time;
                    previous = current;
                    result.regimes.Add(current);
                }
            }
            previous.time_end = this.states[this.states.Count - 1].time;
            
            return result;
        }

        // This returns true if and only if some change is performed on the list of states (and thus we have to restart the checks).
        // i should be the index of next, which is also where we would insert the middle point.
        // This function may add a middle point, but nothing else.
        // Other functions remove redundant points and update preblock/postblock state.
        public bool processPair(int i)
        {
            RealTimeVisionState previous = this.states[i - 1];
            RealTimeVisionState next = this.states[i];
            RealTimeVisionState middle;

            // First check if they are not already within the granularity. If they are, pass.
            if (next.time - previous.time <= this.situation.reality.process.time_granularity)
            {
                return false;
            }

            if(previous.spawning)
            {
                // Was spawning
                if(next.spawning)
                {
                    // Is still spawning
                    if(previous.vision_blocked)
                    {
                        // Was vision blocked
                        if(next.vision_blocked)
                        {
                            // Still vision blocked.
                            // Nothing to do.
                            return false;
                        }
                        else
                        {
                            // No longer vision blocked.
                            middle = this.getState((next.time + previous.time) / 2);
                            this.states.Insert(i, middle);
                            return true;
                        }
                    }
                    else
                    { 
                        // Was not vision blocked
                        if(next.vision_blocked)
                        {
                            // Is now vision blocked.
                            middle = this.getState((next.time + previous.time) / 2);
                            this.states.Insert(i, middle);
                            return true;
                        }
                        else
                        {
                            // Still not vision blocked.
                            if(previous.preblock)
                            {
                                // Was preblock.
                                // Next one must also be preblock.
                                if(next.preblock)
                                {
                                    // Nothing to do.
                                    return false;
                                }
                                else
                                {
                                    // This may not happen. ERROR!!
                                    throw new Exception("Pre-block non-vision block followed by non-pre-block non-vision block, how is that possible?!");
                                }
                            }
                            else if(previous.postblock)
                            {                                
                                // Was postblock.
                                // Next one must also be postblock.
                                if(next.postblock)
                                {
                                    // Nothing to do.
                                    return false;
                                }
                                else
                                {
                                    // This may not happen. ERROR!!
                                    throw new Exception("Post-block non-vision block followed by non-post-block non-vision block, how is that possible?!");
                                }
                            }
                            else
                            {
                                // Neither pre nor post-block, we must calculate middle as there may be a vision block inbetween.
                                middle = this.getState((next.time + previous.time) / 2);
                                this.states.Insert(i, middle);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    // It's no longer spawning. Definitely need to calculate inbetween.
                    middle = this.getState((next.time + previous.time) / 2);
                    this.states.Insert(i, middle);
                    return true;
                }
            }
            else
            {
                // Was not spawning
                // Next one cannot be spawning!
                if(next.spawning)
                {
                    throw new Exception("Non-spawning situation followed by spawning situation!!");
                }
                else
                {
                    // Nothing is spawning.
                    if(previous.vision_blocked)
                    {
                        // Was vision blocked
                        if(next.vision_blocked)
                        {
                            // Still vision blocked
                            // Nothing to do.
                            return false;
                        }
                        else
                        {
                            // No longer vision blocked
                            middle = this.getState((next.time + previous.time) / 2);
                            this.states.Insert(i, middle);
                            return true;
                        }
                    }
                    else
                    {
                        // Was not vision blocked
                        if(next.vision_blocked)
                        {
                            // Is now vision blocked
                            middle = this.getState((next.time + previous.time) / 2);
                            this.states.Insert(i, middle);
                            return true;
                        }
                        else
                        {
                            // Still not vision blocked
                            if (previous.preblock)
                            {
                                // Was preblock.
                                // Next one must also be preblock.
                                if (next.preblock)
                                {
                                    // Nothing to do.
                                    return false;
                                }
                                else
                                {
                                    // This may not happen. ERROR!!
                                    throw new Exception("Pre-block non-vision block followed by non-pre-block non-vision block, how is that possible?!");
                                }
                            }
                            else if (previous.postblock)
                            {
                                // Was postblock.
                                // Next one must also be postblock.
                                if (next.postblock)
                                {
                                    // Nothing to do.
                                    return false;
                                }
                                else
                                {
                                    // This may not happen. ERROR!!
                                    throw new Exception("Post-block non-vision block followed by non-post-block non-vision block, how is that possible?!");
                                }
                            }
                            else
                            {
                                // Neither pre nor post-block, we must calculate middle as there may be a vision block inbetween.
                                middle = this.getState((next.time + previous.time) / 2);
                                this.states.Insert(i, middle);
                                return true;
                            }
                        }
                    }
                }
            }
        }

        public void mergeAll()
        {
            // We assume there is always at least two states.
            RealTimeVisionState last = this.states[0];
            int equal_count = 1;
            for (int i = 1; i < this.states.Count; i++)
            {
                RealTimeVisionState cur = this.states[i];
                // We can only assume they're equal if we know if they're pre-, post- or during block.
                if(cur.spawning == last.spawning && cur.vision_blocked == last.vision_blocked && cur.postblock == last.postblock && cur.preblock == last.preblock && (cur.vision_blocked || cur.postblock || last.postblock))
                {
                    equal_count++;
                    if(equal_count > 2)
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

        public void updateStates()
        {
            // We assume spawning and non-spawning always work properly (All spawning before all spawning).

            // It's simple really:
            //  - Find states that are vision_blocked. For each of those, they're either spawning or non-spawning.
            //  - Every non-vision block state before a spawning vision block is pre-block.
            //  - Every non-vision block spawning state after a spawning vision block is post-block.
            //  - Every non-vision block non-spawning state before a non-spawning vision block is pre-block.
            //  - Every non-vision block state after a non-spawning vision block is post-block.
            for(int i = 0; i < this.states.Count; i++)
            {
                RealTimeVisionState cur = this.states[i];
                if(cur.vision_blocked)
                {
                    if(cur.spawning)
                    {
                        for(int j = 0; j < i; j++)
                        {
                            RealTimeVisionState pre = this.states[j];
                            if(!pre.vision_blocked)
                            {
                                pre.preblock = true;
                            }
                        }
                        for(int j = i+1; j < this.states.Count; j++)
                        {
                            RealTimeVisionState post = this.states[j];
                            if(!post.vision_blocked && post.spawning)
                            {
                                post.postblock = true;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < i; j++)
                        {
                            RealTimeVisionState pre = this.states[j];
                            if (!pre.vision_blocked && !pre.spawning)
                            {
                                pre.preblock = true;
                            }
                        }
                        for (int j = i + 1; j < this.states.Count; j++)
                        {
                            RealTimeVisionState post = this.states[j];
                            if (!post.vision_blocked)
                            {
                                post.postblock = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
