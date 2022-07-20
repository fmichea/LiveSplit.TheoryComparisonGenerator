using System;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.Options;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
	public class TheoryTimeComparisonGenerator : IComparisonGenerator
	{
		public TheoryTimeComparisonGenerator(IRun run, Time target)
		{
			Run = run;
			Target = target;
		}

		public IRun Run { get; set; }

		public virtual string Name
		{
			get
			{
				// FIXME: Handle timing method selection for title.
				return String.Format("Theory {0}", Target.RealTime.Value.ToString());
			}
		}

		public Time Target { get; set; }

		public virtual void Generate(ISettings settings)
		{
			// FIXME: Make a sub function that takes the timing method and computes the correct value,
			//  call it from this function with both available methods.

			var last_segment_times = new Time(TimeSpan.Zero, TimeSpan.Zero);
			double sob_ms;
			double sob_ms_igt;
			try
			{
				sob_ms = SumOfBest.CalculateSumOfBest(Run).Value.TotalMilliseconds;
			}
			catch
			{
				sob_ms = 0;
			}

			try
			{
				sob_ms_igt = SumOfBest.CalculateSumOfBest(Run, method: TimingMethod.GameTime).Value
					.TotalMilliseconds;
			}
			catch
			{
				sob_ms_igt = 0;
			}

			for (var ind = 0; ind < Run.Count; ind++)
			{
				double realTime;
				double igt;
				try
				{
					realTime = Run[ind].BestSegmentTime.RealTime.Value.TotalMilliseconds +
					           (Target.RealTime.Value.TotalMilliseconds - sob_ms) *
					           (Run[ind].BestSegmentTime.RealTime.Value.TotalMilliseconds / sob_ms);
				}
				catch
				{
					realTime = 0;
				}

				try
				{
					igt = Run[ind].BestSegmentTime.GameTime.Value.TotalMilliseconds +
					      (Target.GameTime.Value.TotalMilliseconds - sob_ms_igt) *
					      (Run[ind].BestSegmentTime.GameTime.Value.TotalMilliseconds / sob_ms_igt);
				}
				catch
				{
					igt = 0;
				}

				if (double.IsNaN(realTime)) realTime = 0;

				if (double.IsNaN(igt)) igt = 0;

				var segment_split_time = last_segment_times +
				                         new Time(TimeSpan.FromMilliseconds(realTime),
					                         TimeSpan.FromMilliseconds(igt));
				Run[ind].Comparisons[Name] = segment_split_time;
				last_segment_times = segment_split_time;
			}
		}
	}
}
