using System.Runtime.CompilerServices;

namespace BSVisionCalculator
{
    // This contains standard rows
    public class BSRow
    {
        public double height; // Relative to player effective position

        public static BSRow BOTTOM = new BSRow(0);
        public static BSRow MID = new BSRow(1);
        public static BSRow TOP = new BSRow(2);

        public BSRow(double height)
        {
            this.height = height;
        }

        // This is zero-indexed, like the game, 0 being bottom row.
        public BSRow(int index) : this(getRowHeight(index))
        {

        }

        public static BSRow fromString(String row_str)
        {
            if(row_str == "BOTTOM")
            {
                return BOTTOM;
            }
            else if(row_str == "MID")
            {
                return MID;
            }
            else if(row_str == "TOP")
            {
                return TOP;
            }
            else
            {
                throw new Exception("Row description cannot be solved!");
            }
        }

        private static double getRowHeight(int index)
        {
            double height = 0;

            if (index >= 2)
            {
                return height;
            }
            else
            {
                height -= GlobalParameters.height_row_mid_top;

                if (index >= 1)
                {
                    return height;
                }
                else
                {
                    height -= GlobalParameters.height_row_bottom_mid;

                    return height;
                }
            }
        }

        public double getRealHeight(VisionCalculationReality reality)
        {
            return reality.height_player_effective + this.height;
        }

        public int getIndex()
        {
            if (this.height == BSRow.BOTTOM.height)
            {
                return 0;
            }
            else if (this.height == BSRow.MID.height)
            {
                return 1;
            }
            else if (this.height == BSRow.TOP.height)
            {
                return 2;
            }
            else
            {
                throw new Exception("This row does not have standard row index.");
            }
        }
    }
}
