namespace BSVisionCalculator
{
    public class RelativePositionsSummary
    {
        public VisionCalculationProcess process;

        public VisionCalculationReality reality_maxheight;
        public VisionCalculationReality reality_minheight;

        public List<RelativeBloqBloqPositionSummary> positions_bloqbloq;
        public List<RelativeWallBloqPositionSummary> positions_wallbloq;

        public RelativePositionsSummary(VisionCalculationProcess process)
        {
            this.process = process;

            this.reality_maxheight = new VisionCalculationReality(process, process.height_player_max);
            this.reality_minheight = new VisionCalculationReality(process, process.height_player_min);

            this.positions_bloqbloq = new List<RelativeBloqBloqPositionSummary>();
            this.positions_wallbloq = new List<RelativeWallBloqPositionSummary>();
        }

        // The list must be non-empty. We could use a "zero" summary, but this is just a slippery slope.
        public static RelativeTimeRegimeSummary mergePositions(List<RelativeBloqBloqPositionSummary> positions)
        {
            if(positions.Count == 0)
            {
                throw new Exception("Trying to merge an empty list of bloqbloq positions!");
            }

            RelativeTimeRegimeSummary result = positions[0].regimes;

            for(int i = 1; i < positions.Count; i++)
            {
                result = result.merge(positions[i].regimes);
            }

            return result;
        }

        // The list must be non-empty. We could use a "zero" summary, but this is just a slippery slope.
        public static RelativeTimeRegimeSummary mergePositions(List<RelativeWallBloqPositionSummary> positions)
        {
            if (positions.Count == 0)
            {
                throw new Exception("Trying to merge an empty list of wallbloq positions!");
            }

            RelativeTimeRegimeSummary result = positions[0].regimes;

            for (int i = 1; i < positions.Count; i++)
            {
                result = result.merge(positions[i].regimes);
            }

            return result;
        }

        public List<RelativeBloqBloqPositionSummary> filterBloqBloqPositions(Predicate<RelativeBloqBloqPositionSummary> filter)
        {
            return this.positions_bloqbloq.FindAll(filter);
        }

        public List<RelativeWallBloqPositionSummary> filterWallBloqPositions(Predicate<RelativeWallBloqPositionSummary> filter)
        {
            return this.positions_wallbloq.FindAll(filter);
        }

        // Some predicates
        public static bool allBloqBloq(RelativeBloqBloqPositionSummary pos)
        {
            return true;
        }

        public static bool hardVBBloqBloqFilter(RelativeBloqBloqPositionSummary pos)
        {
            return (pos.blocker_bloq.lane.width == BSLane.NEAR_LEFT.width && pos.blocker_bloq.row.height == BSRow.MID.height);
        }
        
        public static bool centralBloqBloqFilter(RelativeBloqBloqPositionSummary pos)
        {
            return (pos.blocker_bloq.lane.width == BSLane.NEAR_LEFT.width);
        }

        public static bool strictInlineBloqBloqFilter(RelativeBloqBloqPositionSummary pos)
        {
            return (!hardVBBloqBloqFilter(pos) && pos.blocker_bloq.lane.width == pos.blocked_bloq.lane.width && pos.blocker_bloq.row.height == pos.blocked_bloq.row.height);
        }

        // Complement of the three above.
        public static bool otherBloqBloqFilter(RelativeBloqBloqPositionSummary pos)
        {
            return (!hardVBBloqBloqFilter(pos) && !centralBloqBloqFilter(pos) && !strictInlineBloqBloqFilter(pos));
        }
        
        public static bool byPostureBloqBloqFilter(PlayerPosture posture, RelativeBloqBloqPositionSummary pos)
        {
            return (pos.posture.x == posture.x && pos.posture.y == posture.y);
        }

        public static Predicate<RelativeBloqBloqPositionSummary> byPostureBloqBloqFilter(PlayerPosture posture)
        {
            return ((pos) => byPostureBloqBloqFilter(posture, pos));
        }               

        public static bool allWallBloq(RelativeWallBloqPositionSummary pos)
        {
            return true;
        }
        public static bool fullWallBloqFilter(RelativeWallBloqPositionSummary pos)
        {
            return (pos.blocker_wall.height_level_bottom.index == 0 && pos.blocker_wall.height_level_top.index >= 3 && pos.blocker_wall.lane_right.width >= BSLane.NEAR_LEFT.width);
        }

        public static bool squatWallBloqFilter(RelativeWallBloqPositionSummary pos)
        {
            return (pos.blocker_wall.height_level_bottom.index == 2 && pos.blocker_wall.height_level_top.index >= 3 && pos.blocker_wall.lane_right.width >= BSLane.NEAR_LEFT.width);
        }

        public static bool sideWallBloqFilter(RelativeWallBloqPositionSummary pos)
        {
            return (pos.blocker_wall.lane_right.width <= BSLane.FAR_LEFT.width && pos.blocker_wall.height_level_top.index >= 3);
        }

        public static bool lowWallBloqFilter(RelativeWallBloqPositionSummary pos)
        {
            return (pos.blocker_wall.height_level_top.index <= 2);
        }

        public static Predicate<T> andFilter<T>(Predicate<T> p1, Predicate<T> p2)
        {
            return ((t) => p1(t) && p2(t));
        }

        // The three above should cover everything relevant, so no need for the complement.

        public static bool byPostureWallBloqFilter(PlayerPosture posture, RelativeWallBloqPositionSummary pos)
        {
            return (pos.posture.x == posture.x && pos.posture.y == posture.y);
        }

        public static Predicate<RelativeWallBloqPositionSummary> byPostureWallBloqFilter(PlayerPosture posture)
        {
            return ((pos) => byPostureWallBloqFilter(posture, pos));
        }

        public void processAll()
        {
            this.process.recalculatePostures();

            this.positions_bloqbloq.Clear();
            this.positions_wallbloq.Clear();

            // To iterate
            BSLane[] lanes = new BSLane[2] { BSLane.FAR_LEFT, BSLane.NEAR_LEFT }; // Use symmetry
            BSRow[] rows = new BSRow[3] { BSRow.BOTTOM, BSRow.MID, BSRow.TOP };

            // Main loop for all player postures
            PlayerPosture[] postures = new PlayerPosture[6] { PlayerPosture.CENTRED, PlayerPosture.LEFT, PlayerPosture.RIGHT, PlayerPosture.SQUAT, PlayerPosture.LEFT_SQUAT, PlayerPosture.RIGHT_SQUAT };

            foreach (PlayerPosture posture in postures)
            {
                // Bloq-bloq positions
                // Centre lane
                foreach(BSRow row_blocker in rows)
                {
                    foreach(BSLane lane_blocked in lanes)
                    {
                        foreach(BSRow row_blocked in rows)
                        {
                            this.positions_bloqbloq.Add(this.processBloqBloqPosition(posture, BSLane.NEAR_LEFT, row_blocker, lane_blocked, row_blocked));
                        }
                    }
                }

                // Side lane
                foreach(BSRow row_blocker in rows)
                {
                    foreach(BSRow row_blocked in rows)
                    {
                        this.positions_bloqbloq.Add(this.processBloqBloqPosition(posture, BSLane.FAR_LEFT, row_blocker, BSLane.FAR_LEFT, row_blocked));
                    }
                }

                // Wall-bloq positions
                // Full walls
                foreach(BSLane lane_blocked in lanes)
                {
                    foreach(BSRow row_blocked in rows)
                    {
                        this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.FAR_LEFT, BSLane.NEAR_LEFT, new BSWallHeight(0), new BSWallHeight(5), lane_blocked, row_blocked));
                    }
                }

                // Squat walls
                foreach (BSLane lane_blocked in lanes)
                {
                    foreach (BSRow row_blocked in rows)
                    {
                        this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.FAR_LEFT, BSLane.NEAR_LEFT, new BSWallHeight(2), new BSWallHeight(5), lane_blocked, row_blocked));
                    }
                }

                // Full side
                foreach (BSRow row_blocked in rows)
                {
                    this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.FAR_LEFT, BSLane.FAR_LEFT, new BSWallHeight(0), new BSWallHeight(5), BSLane.FAR_LEFT, row_blocked));
                }

                // Half side
                foreach (BSRow row_blocked in rows)
                {
                    this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.FAR_LEFT, BSLane.FAR_LEFT, new BSWallHeight(2), new BSWallHeight(5), BSLane.FAR_LEFT, row_blocked));
                }

                // Full central
                foreach (BSLane lane_blocked in lanes)
                {
                    foreach (BSRow row_blocked in rows)
                    {
                        this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.NEAR_LEFT, BSLane.NEAR_LEFT, new BSWallHeight(0), new BSWallHeight(5), lane_blocked, row_blocked));
                    }
                }

                // Half central
                foreach (BSLane lane_blocked in lanes)
                {
                    foreach (BSRow row_blocked in rows)
                    {
                        this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.NEAR_LEFT, BSLane.NEAR_LEFT, new BSWallHeight(2), new BSWallHeight(5), lane_blocked, row_blocked));
                    }
                }

                // Low walls
                for(int height_bottom = 0; height_bottom <= 1; height_bottom++)
                {
                    for(int height_top = height_bottom + 1; height_top <= 2; height_top++)
                    {
                        // Pure left
                        foreach (BSRow row_blocked in rows)
                        {
                            this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.FAR_LEFT, BSLane.FAR_LEFT, new BSWallHeight(height_bottom), new BSWallHeight(height_top), BSLane.FAR_LEFT, row_blocked));
                        }
                                                
                        foreach(BSLane lane_blocked in lanes)
                        {
                            foreach(BSRow row_blocked in rows)
                            {
                                // Pure centre
                                this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.NEAR_LEFT, BSLane.NEAR_LEFT, new BSWallHeight(height_bottom), new BSWallHeight(height_top), lane_blocked, row_blocked));

                                // Double lane
                                this.positions_wallbloq.Add(this.processWallBloqPosition(posture, BSLane.FAR_LEFT, BSLane.NEAR_LEFT, new BSWallHeight(height_bottom), new BSWallHeight(height_top), lane_blocked, row_blocked));
                            }
                        }
                    }
                }
            }
        }

        public RelativeBloqBloqPositionSummary processBloqBloqPosition(PlayerPosture posture, BSLane lane_blocker, BSRow row_blocker, BSLane lane_blocked, BSRow row_blocked)
        {
            BSBloq blocker = new BSBloq(0, lane_blocker, row_blocker);
            BSBloq blocked = new BSBloq(1, lane_blocked, row_blocked);

            VisionCalculationSituation situation_maxheight = new BloqBloqSituation(this.reality_maxheight, posture, 0, blocker, blocked);
            VisionCalculationSituation situation_minheight = new BloqBloqSituation(this.reality_minheight, posture, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation_maxheight = new RelativeTimeRegimeCalculation(situation_maxheight, isHardVBBloq(lane_blocker, row_blocker));
            RelativeTimeRegimeCalculation calculation_minheight = new RelativeTimeRegimeCalculation(situation_minheight, isHardVBBloq(lane_blocker, row_blocker));

            calculation_maxheight.processAll();
            calculation_minheight.processAll();

            RelativeTimeRegimeSummary summary_maxheight = calculation_maxheight.getSummary();
            RelativeTimeRegimeSummary summary_minheight = calculation_minheight.getSummary();
            RelativeTimeRegimeSummary summary_combined = summary_maxheight.merge(summary_minheight);

            RelativeBloqBloqPositionSummary result = new RelativeBloqBloqPositionSummary(blocker, blocked, posture, summary_combined);

            return result;
        }

        public RelativeWallBloqPositionSummary processWallBloqPosition(PlayerPosture posture, BSLane left_lane_blocker, BSLane right_lane_blocker, BSWallHeight bottom_wall_height_blocker, BSWallHeight top_wall_height_blocker, BSLane lane_blocked, BSRow row_blocked)
        {
            BSWall blocker = new BSWall(0, 0, left_lane_blocker, right_lane_blocker, top_wall_height_blocker, bottom_wall_height_blocker);
            BSBloq blocked = new BSBloq(1, lane_blocked, row_blocked);

            VisionCalculationSituation situation_maxheight = new WallBloqSituation(this.reality_maxheight, posture, 0, blocker, blocked);
            VisionCalculationSituation situation_minheight = new WallBloqSituation(this.reality_minheight, posture, 0, blocker, blocked);

            RelativeTimeRegimeCalculation calculation_maxheight = new RelativeTimeRegimeCalculation(situation_maxheight, isHardVBWall(right_lane_blocker, top_wall_height_blocker));
            RelativeTimeRegimeCalculation calculation_minheight = new RelativeTimeRegimeCalculation(situation_minheight, isHardVBWall(right_lane_blocker, top_wall_height_blocker));

            calculation_maxheight.processAll();
            calculation_minheight.processAll();

            RelativeTimeRegimeSummary summary_maxheight = calculation_maxheight.getSummary();
            RelativeTimeRegimeSummary summary_minheight = calculation_minheight.getSummary();
            RelativeTimeRegimeSummary summary_combined = summary_maxheight.merge(summary_minheight);

            RelativeWallBloqPositionSummary result = new RelativeWallBloqPositionSummary(blocker, blocked, posture, summary_combined);

            return result;
        }

        // This is left-focused!!!!!
        public bool isHardVBBloq(BSLane lane_blocker, BSRow row_blocker)
        {
            return (row_blocker.height == BSRow.MID.height && lane_blocker.width == BSLane.NEAR_LEFT.width);
        }

        // This is left-focused!!!!!
        public bool isHardVBWall(BSLane right_lane_blocker, BSWallHeight top_wall_height_blocker)
        {
            return (top_wall_height_blocker.index >= 3 && right_lane_blocker.width >= BSLane.NEAR_LEFT.width);
        }
    }
}
