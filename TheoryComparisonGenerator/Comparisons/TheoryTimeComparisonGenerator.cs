using System;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.Options;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
	public class TheoryTimeComparisonGenerator : IComparisonGenerator
	{
		public TheoryTimeComparisonGenerator(IRun run, ComparisonData data)
		{
			Run = run;
			Data = data;
		}

		public IRun Run { get; set; }

		public ComparisonData Data { get; protected set; }

		public virtual string Name => Data.FormattedName;

		public virtual void Generate(ISettings settings)
		{
			Generate(TimingMethod.RealTime);
			Generate(TimingMethod.GameTime);
		}

		public void Generate(TimingMethod method)
		{
			// Variable theorySplitTime represents the time at which the segment will have to be split.
			var theorySplitTime = TimeSpan.Zero;

			// For this comparison, we need a full sum of best available to base calculation on.
			var sob = SumOfBest.CalculateSumOfBest(Run, method: method);
			if (sob == null) return;

			// Target time must also be available.
			var target = Data.TargetT[method];
			if (target == null) return;

			// Variable multiplier is the amount we need to multiple every gold to get theory split time.
			//   eg. Gold = 1:00 (60000ms), Theory = 1:10 (70000ms) => Multiplier 1.1666... (aka. 116 %)
			var goldMultiplier = target.Value.TotalMilliseconds / sob.Value.TotalMilliseconds;

			// For each segment in the run, find the split time for this segment using the multiplier.
			for (var idx = 0; idx < Run.Count; idx++)
			{
				if (idx == Run.Count - 1)
				{
					// Last split gets the exact target to avoid floating point inaccuracy. The
					// difference should be very minimal with what would have resulted in "else"
					// computation, however this avoids displaying "18:59.99" when user chooses
					// a 19:00 target.
					theorySplitTime = target.Value;
				}
				else
				{
					// Fetch the segment gold, this should never fail since we already computed the SOB.
					var gold = Run[idx].BestSegmentTime[method];
					if (gold == null) continue;

					// Variable theorySegmentTime is the expected segment duration for theory.
					var theorySegmentTime = gold.Value.TotalMilliseconds * goldMultiplier;

					// Variable theorySplitTime is the cumulative time to the end of this segment from run
					// start, in other words the deadline by which to split this segment.
					theorySplitTime += TimeSpan.FromMilliseconds(theorySegmentTime);
				}

				// Add this split time to the run comparison on the correct timing method.
				var comparisonTime = Time.Zero;
				if (Run[idx].Comparisons.ContainsKey(Name))
					comparisonTime = Run[idx].Comparisons[Name];

				comparisonTime[method] = theorySplitTime;
				Run[idx].Comparisons[Name] = comparisonTime;
			}
		}
	}
}
