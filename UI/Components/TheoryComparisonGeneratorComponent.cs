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
        private IRun previousRun;

        public TheoryComparisonGeneratorComponent(LiveSplitState state)
        {
            CurrentState = state;

            Settings = new TheoryComparisonGeneratorSettings(state);
            Settings.OnChange += settings_OnChange;
            Settings.OnChangeComparison += settings_OnChangeComparison;
            Settings.OnChangePBComparison += settings_OnChangePBComparison;

            installedComparisons = new List<string>();
        }

        public TheoryComparisonGeneratorSettings Settings { get; set; }
        public LiveSplitState CurrentState { get; set; }

        private List<string> installedComparisons { get; }
        public string ComponentName => "Theory Comparison Generator";

        public IDictionary<string, Action> ContextMenuControls { get; protected set; }

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
                _updateAllComparisons(state);
                previousRun = state.Run;
            }
        }

        public void Dispose()
        {
            _removeAllComparisons(CurrentState);
        }

        public void Prepare(LiveSplitState state)
        {
            if (state != CurrentState)
            {
                _updateAllComparisons(state);
                CurrentState = state;
            }
        }

        public int GetSettingsHashCode()
        {
            return Settings.GetSettingsHashCode();
        }

        private void settings_OnChangeComparison(object sender, ComparisonSettingsChangeEventArgs e)
        {
            var generator = new TheoryTimeComparisonGenerator(CurrentState.Run, e.NewData);
            _updateComparisonInPlace(CurrentState, e.PrevData, e.NewData, generator);
        }

        private void settings_OnChangePBComparison(object sender, PBComparisonSettingsChangeEventArgs e)
        {
            var generator = new TheoryPBComparisonGenerator(CurrentState.Run, e.NewData);
            _updateComparisonInPlace(CurrentState, e.PrevData, e.NewData, generator);
        }

        private void settings_OnChange(object sender, EventArgs e)
        {
            _updateAllComparisons(CurrentState);
        }

        private void _updateComparisonInPlace(LiveSplitState state, ComparisonData prevData, ComparisonData newData, TheoryTimeComparisonGenerator generator)
        {
            var prevName = prevData.FormattedName;
            var newSelectedName = prevName;

            // First we remove the previous comparison generator from the list.
            _removeComparisonFromRun(state, prevName);

            // If this has new data and the new data is part of the splits currently used, add the comparison
            // generator. In case of PB theory time, this is true if enabled.
            if (_addComparisonToRun(state, generator))
            {
                newSelectedName = newData.FormattedName;
            }

            // If the comparison was previously selected, we update it with whichever name it now has. In the case
            // the comparison was only removed, this will default back to PB comparison.
            if (prevName == state.CurrentComparison)
                _updateSelectedComparison(state, newSelectedName);
        }

        private void _updateAllComparisons(LiveSplitState state)
        {
            var run = state.Run;

            _removeAllComparisons(state);

            _addComparisonToRun(state, new TheoryPBComparisonGenerator(run, Settings.TheoryPBData));

            foreach (var comparisonSetting in Settings.ComparisonsList)
            {
                var comparison = new TheoryTimeComparisonGenerator(run, comparisonSetting.Data);
                _addComparisonToRun(state, comparison);
            }

            _updateSelectedComparison(state, state.CurrentComparison);
        }

        private void _removeAllComparisons(LiveSplitState state)
        {
            foreach (var installedComparison in installedComparisons)
                _removeComparisonFromRun(state, installedComparison);
            installedComparisons.Clear();
        }

        private void _updateSelectedComparison(LiveSplitState state, string selectedComparison)
        {
            // We revert comparison to PB if the comparison that was selected is removed. (eg. file change
            // removes theory time currently selected).
            var currentlySelectedComparison =
                state.Run.ComparisonGenerators.FirstOrDefault(x => x.Name == selectedComparison);
            if (currentlySelectedComparison == null)
                state.CurrentComparison = Run.PersonalBestComparisonName;
            else
                state.CurrentComparison = selectedComparison;
        }

        private void _removeComparisonFromRun(LiveSplitState state, string generatorName)
        {
            var prevComparison = state.Run.ComparisonGenerators.FirstOrDefault(x => x.Name == generatorName);
            if (prevComparison != null) state.Run.ComparisonGenerators.Remove(prevComparison);
        }

        private bool _addComparisonToRun(LiveSplitState state, TheoryTimeComparisonGenerator generator)
        {
            if (!generator.ShouldAddToSplits(Settings.SplitsName))
                return false;

            installedComparisons.Add(generator.Name);

            // TODO: Find out why generate is only called after reset, forcing us to call it once on init.
            generator.Generate(state.Settings);

            // Add comparison generator to the run.
            state.Run.ComparisonGenerators.Add(generator);

            return true;
        }
    }
}
