namespace BSVisionCalculator
{
    public class BSBloq : BSObject
    {
        public double width;
        public double height;
        public double depth; // We use the same object for the bloq as it moves over time. This value changes thus, as so do some others.
        public double time;
    }
}
