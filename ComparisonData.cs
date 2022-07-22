using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public class ComparisonData
    {

        public string Target { get; set; }
        public string SplitsName { get; set; }

        public string SecondaryName { get; set; }

        public ComparisonData(string split_name, string secondary_name, string target)
        {
            SplitsName = split_name;
            SecondaryName = secondary_name;
            Target = target;
        }
        public static ComparisonData FromXml(XmlNode node)
        {
            var element = (XmlElement)node;
            return new ComparisonData(
                element["SplitsName"].InnerText,
                element["SecondaryName"].InnerText,
                element["Target"].InnerText);
        }
        public int CreateElement(XmlDocument document, XmlElement element)
        {
            return 
            SettingsHelper.CreateSetting(document, element, "SplitsName", SplitsName) ^
            SettingsHelper.CreateSetting(document, element, "SecondaryName", SecondaryName) ^
            SettingsHelper.CreateSetting(document, element, "Target", Target);
        }
    }
}
