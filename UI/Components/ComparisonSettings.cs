using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveSplit.TheoryComparisonGenerator.Comparisons;

namespace LiveSplit.UI.Components
{
    public partial class ComparisonSettings : UserControl
    {
        public ComparisonData Data { get; set; }
        protected IList<ComparisonSettings> ComparisonsList { get; set; }
        protected LiveSplitState CurrentState { get; set; }
        protected int ComparisonIndex => ComparisonsList.IndexOf(this);
        protected int TotalComparisons => ComparisonsList.Count;

        public event EventHandler ComparisonRemoved;
        public event EventHandler MovedUp;
        public event EventHandler MovedDown;

        public event EventHandler<ComparisonSettingsChangeEventArgs> OnChange;

        public ComparisonSettings(LiveSplitState State, string splits_name, IList<ComparisonSettings> comparisonSettings)
        {
            InitializeComponent();
            Data = new ComparisonData(splits_name, "", "");
            CurrentState = State;
            ComparisonsList = comparisonSettings;
        }

        private void txtAltName_TextChanged(object sender, EventArgs e)
        {
            var newText = txtAltName.Text;

            if (Data.SecondaryName == newText)
            {
                return;
            }

            var prevData = Data;
            Data = new ComparisonData(Data) { SecondaryName = newText };
            OnChange?.Invoke(this, new ComparisonSettingsChangeEventArgs(prevData, Data));
        }

        private void txtTargetTime_TextChanged(object sender, EventArgs e)
        {

            var newText = txtTargetTime.Text;

            if (Data.Target == newText)
            {
                return;
            }

            var prevData = Data;
            Data = new ComparisonData(Data) { Target = newText};
            OnChange?.Invoke(this, new ComparisonSettingsChangeEventArgs(prevData, Data));
        }

        private void ComparisonSettings_Load(object sender, EventArgs e)
        {
            txtName.Text = Data.SplitsName;
            txtAltName.Text = Data.SecondaryName;
            txtTargetTime.Text = Data.Target;
        }

        public void SelectControl()
        {
            btnRemoveColumn.Select();
        }
        public void UpdateEnabledButtons()
        {
            btnMoveDown.Enabled = ComparisonIndex < TotalComparisons - 1;
            btnMoveUp.Enabled = ComparisonIndex > 0;
        }
        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            ComparisonRemoved?.Invoke(this, null);
        }

        private void btnAttachToSplits_Click(object sender, EventArgs e)
        {
            var newSplitsName = Path.GetFileNameWithoutExtension(CurrentState?.Run.FilePath);

            var prevData = Data;
            Data = new ComparisonData(Data) { SplitsName = newSplitsName };
            OnChange?.Invoke(this, new ComparisonSettingsChangeEventArgs(prevData, Data));

            txtName.Text = newSplitsName;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MovedUp?.Invoke(this, null);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MovedDown?.Invoke(this, null);
        }
    }

    public class ComparisonSettingsChangeEventArgs : EventArgs
    {
        public ComparisonSettingsChangeEventArgs(ComparisonData prevData, ComparisonData newData)
        {
            PrevData = prevData;
            NewData = newData;
        }

        public ComparisonData PrevData { get; protected set; }
        public ComparisonData NewData { get; protected set; }
    }
}
