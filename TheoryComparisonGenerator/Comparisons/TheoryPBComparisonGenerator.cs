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
            Data.TargetT = Run[Run.Count - 1].PersonalBestSplitTime;
            base.Generate(settings);
        }
    }
}
