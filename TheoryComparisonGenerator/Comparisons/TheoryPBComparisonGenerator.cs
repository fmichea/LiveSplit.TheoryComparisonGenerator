using LiveSplit.Model;
using LiveSplit.Options;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
    public class TheoryPBComparisonGenerator : TheoryTimeComparisonGenerator
    {

        public TheoryPBComparisonGenerator(IRun run, ComparisonData data) : base(run, data)
        {
        }

        public override string Name
        {
            get
            {
                if (Data.SecondaryName != "") return Data.SecondaryName;
                return "Theory PB";
            }
        }

        public override void Generate(ISettings settings)
        {
            // This is wrong if the game uses different timing method, so we need to dig that out of the state.
            Data.Target = Run[Run.Count - 1].PersonalBestSplitTime[TimingMethod.RealTime].ToString();
            base.Generate(settings);
        }
    }
}
