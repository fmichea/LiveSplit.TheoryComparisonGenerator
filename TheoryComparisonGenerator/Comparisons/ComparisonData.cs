using System;
using System.Xml;
using LiveSplit.UI;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
    public class ComparisonData
    {
        public ComparisonData(string split_name, string secondary_name, string target)
        {
            SplitsName = split_name;
            SecondaryName = secondary_name;
            Target = target;
        }

        public ComparisonData(ComparisonData other)
        {
            SplitsName = other.SplitsName;
            SecondaryName = other.SecondaryName;
            Target = other.Target;
        }

        public string Target { get; set; }

        public TimeSpan? TargetT
        {
            get
            {
                try
                {
                    return TimeSpan.Parse(Target);
                }
                catch
                {
                    return null;
                }
            }
        }

        public string SplitsName { get; set; }

        public string SecondaryName { get; set; }

        public string FormattedName => formatName();

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

        private string formatName()
        {
            if (SecondaryName != "") return SecondaryName;

            var timeSpan = TargetT;
            if (timeSpan == null) return "Theory ???";

            return $"Theory {formatTimeSpan(timeSpan.Value)}";
        }

        private string formatTimeSpan(TimeSpan timeSpan)
        {
            var value = timeSpan.ToString();

            // Remove prefixes which are not needed. "00:" becomes "" and "01:" becomes "1:".
            value = value.TrimStart('0').TrimStart(':');
            value = value.TrimStart('0').TrimStart(':');

            return value;
        }
    }
}
