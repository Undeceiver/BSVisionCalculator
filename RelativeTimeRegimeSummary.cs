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
    }
}
