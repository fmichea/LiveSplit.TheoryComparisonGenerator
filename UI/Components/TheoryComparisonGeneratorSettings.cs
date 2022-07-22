using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    public partial class TheoryComparisonGeneratorSettings : UserControl
    {        

        public TheoryComparisonGeneratorSettings(LiveSplitState state)
        {
            InitializeComponent();

            CurrentState = state;
            SplitsName = Path.GetFileNameWithoutExtension(CurrentState?.Run.FilePath);
            AutoTheoryPB = true;
            StartingSize = Size;
            StartingTableLayoutSize = tableComparisons.Size;
            ComparisonsList = new List<ComparisonSettings>();

            // FIXME: checking/unchecking the box doesn't seem to have any effect?
            checkboxAutomaticPBComp.DataBindings.Add(
                "Checked",
                this,
                "AutoTheoryPB",
                false,
                DataSourceUpdateMode.OnPropertyChanged
            );
        }
        public Size StartingSize { get; set; }
        public Size StartingTableLayoutSize { get; set; }
        public LiveSplitState CurrentState { get; set; }

        public bool AutoTheoryPB { get; set; }

        public string SplitsName { get; set; }

        public IList<ComparisonSettings> ComparisonsList { get; set; }


        public event EventHandler OnChange;

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            AutoTheoryPB = SettingsHelper.ParseBool(element["AutoTheoryPB"]);

            var comparisonsElement = element["Comparisons"];
            ComparisonsList.Clear();
            foreach (var child in comparisonsElement.ChildNodes)
            {
                var comparisonData = ComparisonData.FromXml((XmlNode)child);
                ComparisonsList.Add(new ComparisonSettings(CurrentState, comparisonData.SplitsName, ComparisonsList) { Data = comparisonData });
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
            var hashCode = SettingsHelper.CreateSetting(document, parent, "AutoTheoryPB", AutoTheoryPB);
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
        }

        void column_ColumnRemoved(object sender, EventArgs e)
        {
            var comparison = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(comparison);
            ComparisonsList.Remove(comparison);
            ResetComparisons();
            if (ComparisonsList.Count > 0)
                ComparisonsList.Last().SelectControl();
          
        }
        private void ResetComparisons()
        {
            ClearLayout();
            var index = 1;
            foreach (var comparison in ComparisonsList)
            {
                UpdateLayoutForColumn();
                AddColumnToLayout(comparison, index);
                index++;
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
            ComparisonsList.Add(comparisonControl);
            AddColumnToLayout(comparisonControl, ComparisonsList.Count);

        }

        private void TheoryComparisonGeneratorSettings_Load(object sender, EventArgs e)
        {
            ResetComparisons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TODO Add functionality for show all button.
        }
    }
}
