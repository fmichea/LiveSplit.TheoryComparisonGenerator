using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.TheoryComparisonGenerator.Comparisons;

namespace LiveSplit.UI.Components
{
    public partial class TheoryComparisonGeneratorSettings : UserControl
    {

        public TheoryComparisonGeneratorSettings(LiveSplitState state)
        {
            InitializeComponent();

            CurrentState = state;
            StartingSize = Size;
            StartingTableLayoutSize = tableComparisons.Size;
            ComparisonsList = new List<ComparisonSettings>();
            ShowAll = false;
            TheoryPBData = new PBComparisonData(true, "");

        }

        public Size StartingSize { get; set; }
        public Size StartingTableLayoutSize { get; set; }

        public LiveSplitState CurrentState
        {
            get { return _currentState; }
            set {
                _currentState = value;
                SplitsName = Path.GetFileNameWithoutExtension(value?.Run.FilePath);
            }
        }
        private LiveSplitState _currentState { get; set; }
        public string SplitsName { get; set; }

        public PBComparisonData TheoryPBData { get; set; }


        public IList<ComparisonSettings> ComparisonsList { get; set; }


        public event EventHandler OnChange;

        public event EventHandler<PBComparisonSettingsChangeEventArgs> OnChangePBComparison;
        public event EventHandler<ComparisonSettingsChangeEventArgs> OnChangeComparison;

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            TheoryPBData = new PBComparisonData(TheoryPBData)
            {
                Enabled = SettingsHelper.ParseBool(element["AutoTheoryPB"]),
                SecondaryName = SettingsHelper.ParseString(element["AutoTheoryDisplayName"])
            };

            var comparisonsElement = element["Comparisons"];
            ComparisonsList.Clear();
            foreach (var child in comparisonsElement.ChildNodes)
            {
                var comparisonData = ComparisonData.FromXml((XmlNode)child);

                var comparisonControl = new ComparisonSettings(CurrentState, comparisonData.SplitsName, ComparisonsList)
                { Data = comparisonData };
                comparisonControl.OnChange += comparisonSettings_OnChange;
                ComparisonsList.Add(comparisonControl);
            }
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            var hashCode = SettingsHelper.CreateSetting(document, parent, "AutoTheoryPB", TheoryPBData.Enabled);
            hashCode ^= SettingsHelper.CreateSetting(document, parent, "AutoTheoryDisplayName", TheoryPBData.SecondaryName);

            XmlElement comparisonsElement = null;
            if (document != null)
            {
                comparisonsElement = document.CreateElement("Comparisons");
                parent.AppendChild(comparisonsElement);
            }

            var count = 1;
            foreach (var comparisonData in ComparisonsList.Select(x => x.Data))
            {
                XmlElement settings = null;
                if (document != null)
                {
                    settings = document.CreateElement("Settings");
                    comparisonsElement.AppendChild(settings);
                }
                hashCode ^= comparisonData.CreateElement(document, settings) * count;
                count++;
            }
            return hashCode;
        }

        private void AddColumnToLayout(ComparisonSettings comparison, int index)
        {
            tableComparisons.Controls.Add(comparison, 0, index);
            tableComparisons.SetColumnSpan(comparison, 4);
            comparison.ComparisonRemoved -= column_ColumnRemoved;
            comparison.ComparisonRemoved += column_ColumnRemoved;
            comparison.MovedUp -= column_MovedUp;
            comparison.MovedDown -= column_MovedDown;
            comparison.MovedUp += column_MovedUp;
            comparison.MovedDown += column_MovedDown;

        }
        void column_MovedDown(object sender, EventArgs e)
        {
            var column = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(column);
            ComparisonsList.Remove(column);
            ComparisonsList.Insert(index + 1, column);
            ResetComparisons();
            column.SelectControl();
            OnChange?.Invoke(this, null);

        }

        void column_MovedUp(object sender, EventArgs e)
        {
            var column = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(column);
            ComparisonsList.Remove(column);
            ComparisonsList.Insert(index - 1, column);
            ResetComparisons();
            column.SelectControl();
            OnChange?.Invoke(this, null);

        }
        void column_ColumnRemoved(object sender, EventArgs e)
        {
            var comparison = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(comparison);
            ComparisonsList.Remove(comparison);
            ResetComparisons();
            if (ComparisonsList.Count > 0)
                ComparisonsList.Last().SelectControl();
            OnChange?.Invoke(this, null);
        }

        private void ResetComparisons()
        {
            ClearLayout();
            var index = 1;
            foreach (var comparison in ComparisonsList)
            {
                if (comparison.Data.SplitsName == SplitsName || ShowAll)
                {
                    UpdateLayoutForColumn();
                    AddColumnToLayout(comparison, index);
                    comparison.UpdateEnabledButtons();
                    index++;
                }
            }
        }
        private void ClearLayout()
        {
            tableComparisons.RowCount = 1;
            tableComparisons.RowStyles.Clear();
            tableComparisons.RowStyles.Add(new RowStyle(SizeType.Absolute, 29f));
            tableComparisons.Size = StartingTableLayoutSize;
            foreach (var control in tableComparisons.Controls.OfType<ComparisonSettings>().ToList())
            {
                tableComparisons.Controls.Remove(control);
            }
            Size = StartingSize;
        }
        private void UpdateLayoutForColumn()
        {
            tableComparisons.RowCount++;
            tableComparisons.RowStyles.Add(new RowStyle(SizeType.Absolute, 179f));
            tableComparisons.Size = new Size(tableComparisons.Size.Width, tableComparisons.Size.Height + 179);
            Size = new Size(Size.Width, Size.Height + 179);
            groupComparisons.Size = new Size(groupComparisons.Size.Width, groupComparisons.Size.Height + 179);
        }
        private void btnAddComparison_Click(object sender, EventArgs e)
        {
            var comparisonControl = new ComparisonSettings(CurrentState, SplitsName, ComparisonsList);
            comparisonControl.OnChange += comparisonSettings_OnChange;
            ComparisonsList.Add(comparisonControl);
            AddColumnToLayout(comparisonControl, ComparisonsList.Count);
            foreach (var comparison in ComparisonsList)
                comparison.UpdateEnabledButtons();
            OnChange?.Invoke(this, null);
        }

        private void TheoryComparisonGeneratorSettings_Load(object sender, EventArgs e)
        {
            SplitsName = Path.GetFileNameWithoutExtension(CurrentState?.Run.FilePath);
            checkboxAutomaticPBComp.Checked = TheoryPBData.Enabled;
            txtTheoryPBAltName.Text = TheoryPBData.SecondaryName;
            ResetComparisons();
        }

        private bool ShowAll { get; set; }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAll = !ShowAll;
            if (ShowAll)
            {
                btnShowAll.Text = "Hide other splits comparisons";
            }
            else
            {
                btnShowAll.Text = "Show All";
            }
            ResetComparisons();
        }

        private void checkboxAutomaticPBComp_CheckedChanged(object sender, EventArgs e)
        {
            PBComparisonData prevData = TheoryPBData;
            TheoryPBData = new PBComparisonData(TheoryPBData) { Enabled = checkboxAutomaticPBComp.Checked };
            OnChangePBComparison?.Invoke(this, new PBComparisonSettingsChangeEventArgs(prevData, TheoryPBData));
        }

        private void txtTheoryPBAltName_TextChanged(object sender, EventArgs e)
        {
            PBComparisonData prevData = TheoryPBData;
            TheoryPBData = new PBComparisonData(TheoryPBData) { SecondaryName = txtTheoryPBAltName.Text };
            OnChangePBComparison?.Invoke(this, new PBComparisonSettingsChangeEventArgs(prevData, TheoryPBData));
        }

        private void comparisonSettings_OnChange(object sender, ComparisonSettingsChangeEventArgs e)
        {
            OnChangeComparison?.Invoke(this, e);
        }
    }

    public class PBComparisonSettingsChangeEventArgs : EventArgs
    {
        public PBComparisonSettingsChangeEventArgs(PBComparisonData prevData, PBComparisonData newData)
        {
            PrevData = prevData;
            NewData = newData;
        }

        public PBComparisonData PrevData { get; protected set; }
        public PBComparisonData NewData { get; protected set; }
    }
}
