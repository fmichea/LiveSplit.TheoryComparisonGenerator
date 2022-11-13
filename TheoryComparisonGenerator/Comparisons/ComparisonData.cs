using System;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;

namespace LiveSplit.TheoryComparisonGenerator.Comparisons
{
    public class ComparisonData
    {
        public ComparisonData(string splitName, string secondaryName, string target)
            : this(splitName, secondaryName, makeTimeFromString(target))
        {
        }

        public ComparisonData(string splitName, string secondaryName, Time target)
        {
            SplitsName = splitName;
            SecondaryName = secondaryName;
            TargetT = target;
        }

        public ComparisonData(ComparisonData other)
        {
            SplitsName = other.SplitsName;
            SecondaryName = other.SecondaryName;
            TargetT = other.TargetT;
        }

        public string Target
        {
            get => TargetT[TimingMethod.RealTime]?.ToString(@"hh\:mm\:ss\.fff");
            set => TargetT = makeTimeFromString(value);
        }

        public Time TargetT { get; set; }

        public string SplitsName { get; set; }

        public string SecondaryName { get; set; }

        public virtual string FormattedName => formatName();

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

            var timeSpan = TargetT[TimingMethod.RealTime];
            if (timeSpan == null) return "Theory ???";
            return $"Theory {formatTimeSpan(timeSpan.Value)}";
        }

        private string formatTimeSpan(TimeSpan timeSpan)
        {
            var value = timeSpan.ToString();

            // Remove prefixes which are not needed. "00:" becomes "" and "01:" becomes "1:".
            value = value.TrimStart('0').TrimStart(':');
            value = value.TrimStart('0').TrimStart(':');

            // Remove suffix which are not needed ".   " becomes "" and .100 becomes ".1"
            if (value.IndexOf('.') != -1)
            {
                value = value.TrimEnd('0').TrimEnd('.');
            }

            return value;
        }

        private static Time makeTimeFromString(string target)
        {
            try
            {
               var timeSpan = TimeSpan.Parse(target);
                return new Time(timeSpan, timeSpan);
            }
            catch
            {
                return Time.Zero;
            }
        }
    }

    public class PBComparisonData : ComparisonData
    {
        public static PBComparisonData Default = new PBComparisonData(true, "");

        public PBComparisonData(bool enabled, string secondaryName)
            : base("", secondaryName, Time.Zero)
        {
            Enabled = enabled;
        }

        public PBComparisonData(PBComparisonData other)
            : base(other)
        {
            Enabled = other.Enabled;
        }

        public override string FormattedName => formatName();

        public bool Enabled { get; set; }

        private string formatName()
        {
            if (SecondaryName != "") return SecondaryName;
            return "Theory PB";
        }
    }
}
