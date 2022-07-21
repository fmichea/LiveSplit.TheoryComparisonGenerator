using System;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.Options;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
	public class TheoryTimeComparisonGenerator : IComparisonGenerator
	{
		public TheoryTimeComparisonGenerator(IRun run, string name, Time target)
		{
			Run = run;
			Target = target;
			alias = name;
		}

		public Time Target { get; set; }

		public IRun Run { get; set; }

		public virtual string Name => formatName();

		private string alias { get; set; }

		private string formatName()
		{
			if (alias != "") return alias;

			// FIXME: Handle timing method selection for title. Currently selected timing method is
			//   available in LiveSplitState under CurrentTimingMethod however the name of the comparison
			//   does not currently update when the timing method is changed. Target currently set so
			//   target for both timing methods are the same.
			var timeSpan = Target[TimingMethod.RealTime];
			if (timeSpan == null)
			{
				return "Theory ???";
			}

			return $"Theory {formatTimeSpan(timeSpan.Value)}";
		}

		private string formatTimeSpan(TimeSpan timeSpan)
		{
			string value = timeSpan.ToString();

			// Remove prefixes which are not needed. "00:" becomes "" and "01:" becomes "1:".
			value = value.TrimStart('0').TrimStart(':');
			value = value.TrimStart('0').TrimStart(':');

			return value;
		}

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
			var target = Target[method];
			if (target == null) return;

			// Variable multiplier is the amount we need to multiple every gold to get theory split time.
			//   eg. Gold = 1:00 (60000ms), Theory = 1:10 (70000ms) => Multiplier 1.1666... (aka. 116 %)
			var goldMultiplier = target.Value.TotalMilliseconds / sob.Value.TotalMilliseconds;

			// For each segment in the run, find the split time for this segment using the multiplier.
			for (var idx = 0; idx < Run.Count; idx++)
			{
				// Fetch the segment gold, this should never fail since we already computed the SOB.
				var gold = Run[idx].BestSegmentTime[method];
				if (gold == null) continue;

				// Variable theorySegmentTime is the expected segment duration for theory.
				var theorySegmentTime = gold.Value.TotalMilliseconds * goldMultiplier;

				// Variable theorySplitTime is the cumulative time to the end of this segment from run
				// start, in other words the deadline by which to split this segment.
				theorySplitTime += TimeSpan.FromMilliseconds(theorySegmentTime);

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
