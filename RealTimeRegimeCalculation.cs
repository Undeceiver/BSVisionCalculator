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
    }

    // Missing here: Merge more than two consecutive with same situation.
    // Missing here: Update pre-block and post-block.
}
