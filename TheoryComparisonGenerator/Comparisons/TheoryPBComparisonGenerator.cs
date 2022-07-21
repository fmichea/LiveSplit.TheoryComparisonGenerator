using LiveSplit.Model;
using LiveSplit.Options;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
	public class TheoryPBComparisonGenerator : TheoryTimeComparisonGenerator
	{
		public override string Name
		{
			get { return "Theory PB";  }
		}

		public TheoryPBComparisonGenerator(IRun run, Time target) : base(run, "", target) {}

		public override void Generate(ISettings settings)
		{
			Target = Run[Run.Count - 1].PersonalBestSplitTime;
			base.Generate(settings);
		}
	}
}
