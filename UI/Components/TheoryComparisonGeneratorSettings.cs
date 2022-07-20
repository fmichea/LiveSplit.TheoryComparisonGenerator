using System;
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
	}
}
