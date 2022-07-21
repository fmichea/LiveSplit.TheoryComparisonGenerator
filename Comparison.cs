using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.UI.Components
{
    public class Comparison
    {
        public static char StringDivider = '$';
        public static string ListDivider = "|";
        public bool Enabled { get; set; }
        public string SplitsName { get; set; }

        public string ComparisonName { get; set; }

        public Comparison(string split_name, string comparison_name, bool enabled)
        {
            SplitsName = split_name;
            ComparisonName = comparison_name;
            Enabled = enabled;
        }
        public Comparison(string comparison_string)
        {
            string[] fields = comparison_string.Split(StringDivider);
            if (fields.Length == 3)
            {
                SplitsName = fields[0];
                ComparisonName = fields[1];
                Enabled = bool.Parse(fields[2]);
            }
        }

        public override string ToString()
        {
            return $"{SplitsName}{StringDivider}{ComparisonName}{StringDivider}{Enabled.ToString()}";
        }
    }
}
