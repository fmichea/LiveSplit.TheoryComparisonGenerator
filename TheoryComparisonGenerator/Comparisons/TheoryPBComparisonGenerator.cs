using LiveSplit.Model;
using LiveSplit.Options;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
    public class TheoryPBComparisonGenerator : TheoryTimeComparisonGenerator
    {

        public TheoryPBComparisonGenerator(IRun run, PBComparisonData data) : base(run, data)
        {
            PBData = data;
        }

        public override void Generate(ISettings settings)
        {
            Data.TargetT = Run[Run.Count - 1].PersonalBestSplitTime;
            base.Generate(settings);
        }

        public PBComparisonData PBData { get; protected set; }

        public override bool ShouldAddToSplits(string splitsName)
        {
            return PBData != null && PBData.Enabled;
        }
    }
}
