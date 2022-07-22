using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public partial class ComparisonSettings : UserControl
    {
        public ComparisonData Data { get; set; }
        public string SplitsName { get { return Data.SplitsName; } set { Data.SplitsName = value; } }
        public string SecondaryName { get { return Data.SecondaryName; } set { Data.SecondaryName = value; } }

        public string Target { get { return Data.Target; } set { Data.Target = value; } }
        protected IList<ComparisonSettings> ComparisonsList { get; set; }
        protected LiveSplitState CurrentState { get; set; }
        protected int ComparisonIndex => ComparisonsList.IndexOf(this);
        protected int TotalComparisons => ComparisonsList.Count;

        public event EventHandler ComparisonRemoved;

        public ComparisonSettings(LiveSplitState State, string splits_name, IList<ComparisonSettings> comparisonSettings)
        {
            InitializeComponent();
            Data = new ComparisonData(splits_name, "", "");
            CurrentState = State;
            ComparisonsList = comparisonSettings;
        }

        private void txtAltName_TextChanged(object sender, EventArgs e)
        {
            SecondaryName = txtAltName.Text;
        }

        private void txtTargetTime_TextChanged(object sender, EventArgs e)
        {
            Target = txtTargetTime.Text;
        }

        private void ComparisonSettings_Load(object sender, EventArgs e)
        {
            txtName.DataBindings.Clear();
            txtAltName.DataBindings.Clear();
            txtTargetTime.DataBindings.Clear();

            txtName.DataBindings.Add("Text", this, "SplitsName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtAltName.DataBindings.Add("Text", this, "SecondaryName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtTargetTime.DataBindings.Add("Text", this, "Target", false, DataSourceUpdateMode.OnPropertyChanged);



        }
        public void SelectControl()
        {
            btnRemoveColumn.Select();
        }
        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            ComparisonRemoved?.Invoke(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO Add functionality for Attach comparison
        }
    }
}
