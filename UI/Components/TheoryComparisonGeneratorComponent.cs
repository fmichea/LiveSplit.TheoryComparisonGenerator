using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private List<string> installedComparisons { get; set; }

        public TheoryComparisonGeneratorComponent(LiveSplitState state)
        {
            CurrentState = state;

            Settings = new TheoryComparisonGeneratorSettings(state);
            Settings.OnChange += settings_OnChange;

            installedComparisons = new List<string>();
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
            updateComparisons(CurrentState);
        }

        void updateComparisons(LiveSplitState state)
        {
            IRun run = state.Run;

            foreach (var installedComparison in installedComparisons)
            {
                removeComparisonFromRun(state, installedComparison);
            }
            installedComparisons.Clear();

            if (Settings.AutoTheoryPB)
            {
                addComparisonToRun(state, new TheoryPBComparisonGenerator(run, Time.Zero));
            }

            foreach (var comparisonSetting in Settings.ComparisonsList)
            {
                // This is a theory time for a different split file.
                if (comparisonSetting.SplitsName != Path.GetFileNameWithoutExtension(CurrentState?.Run.FilePath))
                {
                    continue;
                }

                var timeSpan = TimeSpan.Zero;
                try
                {
                    timeSpan = TimeSpan.Parse(comparisonSetting.Target);
                }
                catch
                {
                    continue;
                }
                var comparisonName = string.IsNullOrEmpty(comparisonSetting?.SecondaryName) ?
                    string.Concat("Theory ", timeSpan.ToString("hh\\:mm\\:ss")) :
                    comparisonSetting.SecondaryName;

                var comparison = new TheoryTimeComparisonGenerator(
                    run,
                    comparisonName,
                    new Time(timeSpan, timeSpan)
                );
                addComparisonToRun(state, comparison);
            }

            // We revert comparison to PB if the comparison that was selected is removed. (eg. file change
            // removes theory time currently selected).
            var currentlySelectedComparison =
                run.ComparisonGenerators.FirstOrDefault(x => x.Name == state.CurrentComparison);
            if (currentlySelectedComparison == null)
            {
                state.CurrentComparison = Run.PersonalBestComparisonName;
            }
        }

        private void removeComparisonFromRun(LiveSplitState state, string generatorName)
        {
            var prevComparison = state.Run.ComparisonGenerators.FirstOrDefault(x => x.Name == generatorName);
            if (prevComparison != null)
            {
                state.Run.ComparisonGenerators.Remove(prevComparison);
            }
        }

        private void addComparisonToRun(LiveSplitState state, IComparisonGenerator generator)
        {
            installedComparisons.Add(generator.Name);

            // TODO: Find out why generate is only called after reset, forcing us to call it once on init.
            generator.Generate(state.Settings);

            // Add comparison generator to the run.
            state.Run.ComparisonGenerators.Add(generator);
        }

        private IRun previousRun;
    }
}
