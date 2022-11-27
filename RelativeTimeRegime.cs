namespace BSVisionCalculator
{
    public class RelativeTimeRegime
    {
        public bool ok;

        public double time_start;
        public double time_end;

        public RelativeTimeRegime(bool ok, double time_start)
        {
            this.ok = ok;
            this.time_start = time_start;            
        }
    }
}
