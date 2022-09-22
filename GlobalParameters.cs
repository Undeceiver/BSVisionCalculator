using System.Runtime.ConstrainedExecution;

namespace BSVisionCalculator
{
    public class GlobalParameters
    {
        // This class only contains true constants and default values. Parameters that are configurable and/or vary are part of other classes.

        /*******************************
        // True constants
        *******************************/

        // Player height
        const double height_player_std = 1.8; // Base height from which calculations are made.
        const double height_player_mult = 0.5; // Multiplier for the height difference from the standard height.
        const double height_player_absolute_min = 1.6; // Minimum effective player height that is ever considered.
        const double height_player_absolute_max = 2.4; // Maximum effective player height that is ever considered.
        const double height_player_above_camera = 0.1; // Distance between the camera and the player's real height.

        // Other aspects related to the player
        const double length_playline = 0.65; // Distance at which the line corresponding to objects on the current time is from the player (forward).

        // Wall geometry
        const double height_wall_min = 0.1; // Minimum height at which the base of a wall is.
        const double height_wall_level = 0.6; // Height difference per level for walls.

        // Bloq geometry
        const double size_bloq = 0.5; // Size of bloqs.

        // Spawn geometry
        const double height_bloq_spawn_level = 0.6; // Vertical distance between bloqs when they spawn and are on same column at the same time.

        // Lane geometry
        const double width_lane = 0.6; // Width of each lane.

        // Row geometry
        const double height_row_bottom_mid = 0.55; // Vertical distance between the bottom and mid rows.
        const double height_row_mid_top = 0.5; // Vertical distance between the mid and top rows.                

        /*******************************
        // Default values
        *******************************/
        const double height_player_min_default = 1.1; // Minimum height of players to consider.
        const double height_player_max_default = 2.2; // Maximum height of players to consider.
        const double height_squat_default = 0.5; // Vertical displacement due to squatting.
        const double width_lean_default = 0.6; // Horizontal displacement due to leaning.
        const double width_squatlean_default = 0.4; // Horizontal displacement due to leaning.
        const double time_hardvb_first_min_wrt_reaction_time_default = 0; // Default for the minimum first visualization time, with respect to the reaction time.
        const double time_hardvb_last_max_default = 0.1; // Maximum time that the object can disappear before having to hit it.
        const double time_hardvb_process_min_default = 0.3; // Minimum amount of time that we need an object to remain in vision to process it.
        const double time_inline_first_min_default = 0; // Minimum amount of time for first visualization for inlines.
        const double time_inline_last_max_wrt_reaction_time_default = 0; // Maximum time that the object can disappear before having to hit it, with respect to the reaction time.
        const double time_inline_process_min_default = 0; // Minimum amount of time that we need an object to remain in vision to process it, in inline circumstances.
        const double time_granularity_default = 0.025; // Granularity of time to consider in calculations and algorithms.

    }
}
