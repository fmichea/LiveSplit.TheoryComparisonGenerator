using System;
using System.Collections.Generic;
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
            this.label1.Text = $"Saved times for {SplitsName}";

            AutoTheoryPB = true;

            // FIXME: checking/unchecking the box doesn't seem to have any effect?
            checkboxAutomaticPBComp.DataBindings.Add(
                "Checked",
                this,
                "AutoTheoryPB",
                false,
                DataSourceUpdateMode.OnPropertyChanged
            );
        }

        public LiveSplitState CurrentState { get; set; }

        public bool AutoTheoryPB { get; set; }

        public string SplitsName { get; set; }

        public List<Comparison> Comparisons { get; set; }

        public string ComparisonsString
        {

            get { return GetComparisonString(); }
            set { SetComparisonString(value); }
        }
        public string GetComparisonString()
        {
            List<string> comparisons = new List<string>();
            Comparisons.ForEach(x => comparisons.Add(x.ToString()));
            return String.Join(Comparison.ListDivider, comparisons);
        }
        public void SetComparisonString(string value)
        {
            List<string> comparisons = value.Split('|').ToList();
            List<Comparison> comps = new List<Comparison>();
            comparisons.ForEach(x => comps.Add(new Comparison(x)));
            Comparisons = comps;
        }
        public event EventHandler OnChange;

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            AutoTheoryPB = SettingsHelper.ParseBool(element["AutoTheoryPB"]);
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
            return hashCode;
        }

        private void buttonAddTheoryTime_Click(object sender, EventArgs e)
        {
          //TODO: Add logic for adding theory times to the Comparison list  
        }
    }
}
