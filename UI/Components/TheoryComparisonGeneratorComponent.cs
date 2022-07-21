using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.TheoryComparisonGenerator.Comparisons;

namespace LiveSplit.UI.Components
{
	public class TheoryComparisonGeneratorComponent : IComponent
	{
		public string ComponentName => "Theory Comparison Generator";

		public IDictionary<string, Action> ContextMenuControls { get; protected set; }

		public TheoryComparisonGeneratorSettings Settings { get; set; }
		public LiveSplitState CurrentState { get; set; }

		public TheoryComparisonGeneratorComponent(LiveSplitState state)
		{
			CurrentState = state;

			Settings = new TheoryComparisonGeneratorSettings(state);
			Settings.OnChange += settings_OnChange;
		}

		public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
		{
			Prepare(state);
		}

		public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
		{
			Prepare(state);
		}

		public float VerticalHeight => 0;
		public float MinimumWidth => 0;
		public float HorizontalWidth => 0;
		public float MinimumHeight => 0;
		public float PaddingTop => 0;
		public float PaddingBottom => 0;
		public float PaddingLeft => 0;
		public float PaddingRight => 0;

		public void Prepare(LiveSplitState state)
		{
			if (state != CurrentState)
			{
				CurrentState = state;
			}

		}

		public XmlNode GetSettings(XmlDocument document)
		{
			return Settings.GetSettings(document);
		}

		public Control GetSettingsControl(LayoutMode mode)
		{
			return Settings;
		}

		public void SetSettings(XmlNode settings)
		{
			Settings.SetSettings(settings);
		}

		public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
		{
			// FIXME: why does this not use CurrentState?
			if (previousRun != state.Run)
			{
				updateComparisons(state);
				previousRun = state.Run;
			}
		}

		public void Dispose()
		{
		}

		public int GetSettingsHashCode()
		{
			return Settings.GetSettingsHashCode();
		}

		void settings_OnChange(object sender, EventArgs e)
		{
			Console.WriteLine("Settings have changed!");
		}

		void updateComparisons(LiveSplitState state)
		{
			IRun run = state.Run;

			// FIXME: remove all currently added theory times here? Easier to handle theory times changes.

			if (Settings.AutoTheoryPB)
			{
				addComparisonToRun(state, new TheoryPBComparisonGenerator(run, Time.Zero));
			}

			// FIXME: Here we would need to use the settings tied to the run to figure out which theory
			//    times are configured and add all of them.

			// FIXME: Remove hard coded theory time for testing.
			addComparisonToRun(state, new TheoryTimeComparisonGenerator(run, new Time(TimeSpan.Parse("00:20:00.000"), TimeSpan.Zero)));
		}

		private void removeComparisonFromRun(LiveSplitState state, IComparisonGenerator generator)
		{
			var prevComparison = state.Run.ComparisonGenerators.FirstOrDefault(x => x.Name == generator.Name);
			if (prevComparison != null)
			{
				state.Run.ComparisonGenerators.Remove(prevComparison);
			}
		}

		private void addComparisonToRun(LiveSplitState state, IComparisonGenerator generator)
		{
			removeComparisonFromRun(state, generator);

			// TODO: Find out why generate is only called after reset, forcing us to call it once on init.
			generator.Generate(state.Settings);

			// Add comparison generator to the run.
			state.Run.ComparisonGenerators.Add(generator);
		}

		private IRun previousRun;
	}
}
