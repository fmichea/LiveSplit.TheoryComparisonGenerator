using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            SplitsName = Path.GetFileNameWithoutExtension(CurrentState?.Run.FilePath);
            StartingSize = Size;
            StartingTableLayoutSize = tableComparisons.Size + tableLayoutPanel2.Size;
            ComparisonsList = new List<ComparisonSettings>();
            ShowAll = false;
            TheoryPBData = PBComparisonData.Default;
            TheoryTimesFilePath = null;
        }

        public Size StartingSize { get; set; }
        public Size StartingTableLayoutSize { get; set; }
        public LiveSplitState CurrentState { get; set; }

        public PBComparisonData TheoryPBData { get; set; }

        public string TheoryTimesFilePath
        {
            get => _theoryTimesFilePath;
            protected set
            {
                txtTheoryTimesPath.Text = _theoryTimesFilePath = value;
                toggleViewFileLoaded(value);
            }
        }

        private string _theoryTimesFilePath { get; set; }

        public FileSystemWatcher TheoryTimesFileWatcher { get; set; }

        public string SplitsName { get; set; }

        public IList<ComparisonSettings> ComparisonsList { get; set; }

        private bool ShowAll { get; set; }

        public event EventHandler OnChange;
        public event EventHandler<PBComparisonSettingsChangeEventArgs> OnChangePBComparison;
        public event EventHandler<ComparisonSettingsChangeEventArgs> OnChangeComparison;

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            var theoryTimesFilePath = SettingsHelper.ParseString(element["TheoryTimesFilePath"]);
            if (_verifyTheoryTimeFilePathValid(theoryTimesFilePath, true))
                _updateTheoryTimesFilePath(theoryTimesFilePath);
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
            return SettingsHelper.CreateSetting(document, parent, "TheoryTimesFilePath", TheoryTimesFilePath);
        }

        private bool _verifyTheoryTimeFilePathValid(string theoryTimeFilePath, bool expectPresent)
        {
            if (theoryTimeFilePath == null)
                return false;

            if (!File.Exists(theoryTimeFilePath))
                return !expectPresent;

            var theoryTimeFile = File.ReadAllText(theoryTimeFilePath);

            if (theoryTimeFile.Length == 0)
                return true;

            var document = new XmlDocument();
            document.LoadXml(theoryTimeFile);

            var elements = document.GetElementsByTagName("TheoryTimesConfig");
            return elements.Count == 1;
        }

        private int _createTheoryTimeFileSettings(XmlDocument document, XmlElement parent)
        {
            var hashCode = SettingsHelper.CreateSetting(document, parent, "AutoTheoryPB", TheoryPBData.Enabled);
            hashCode ^= SettingsHelper.CreateSetting(document, parent, "AutoTheoryDisplayName",
                TheoryPBData.SecondaryName);

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

        private void column_MovedDown(object sender, EventArgs e)
        {
            var column = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(column);
            ComparisonsList.Remove(column);
            ComparisonsList.Insert(index + 1, column);
            ResetComparisons();
            column.SelectControl();
            _writeTheoryTimesFilePath();
            OnChange?.Invoke(this, null);
        }

        private void column_MovedUp(object sender, EventArgs e)
        {
            var column = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(column);
            ComparisonsList.Remove(column);
            ComparisonsList.Insert(index - 1, column);
            ResetComparisons();
            column.SelectControl();
            _writeTheoryTimesFilePath();
            OnChange?.Invoke(this, null);
        }

        private void column_ColumnRemoved(object sender, EventArgs e)
        {
            var comparison = (ComparisonSettings)sender;
            var index = ComparisonsList.IndexOf(comparison);
            ComparisonsList.Remove(comparison);
            ResetComparisons();
            if (ComparisonsList.Count > 0)
                ComparisonsList.Last().SelectControl();
            _writeTheoryTimesFilePath();
            OnChange?.Invoke(this, null);
        }

        private void ResetComparisons()
        {
            ClearLayout();
            var index = 1;
            foreach (var comparison in ComparisonsList)
                if (comparison.Data.SplitsName == SplitsName || ShowAll)
                {
                    UpdateLayoutForColumn();
                    AddColumnToLayout(comparison, index);
                    comparison.UpdateEnabledButtons();
                    index++;
                }
        }

        private void ClearLayout()
        {
            tableComparisons.RowCount = 1;
            tableComparisons.RowStyles.Clear();
            tableComparisons.RowStyles.Add(new RowStyle(SizeType.Absolute, 34f));
            tableComparisons.Size = StartingTableLayoutSize;
            foreach (var control in tableComparisons.Controls.OfType<ComparisonSettings>().ToList())
                tableComparisons.Controls.Remove(control);
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
            _writeTheoryTimesFilePath();
            OnChange?.Invoke(this, null);
        }

        private void TheoryComparisonGeneratorSettings_Load(object sender, EventArgs e)
        {
            SplitsName = Path.GetFileNameWithoutExtension(CurrentState?.Run.FilePath);
            _updateTheoryPBViewFields();
            ResetComparisons();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAll = !ShowAll;
            if (ShowAll)
                btnShowAll.Text = "Hide other splits comparisons";
            else
                btnShowAll.Text = "Show All";
            ResetComparisons();
        }

        private void checkboxAutomaticPBComp_CheckedChanged(object sender, EventArgs e)
        {
            var prevData = TheoryPBData;
            TheoryPBData = new PBComparisonData(TheoryPBData) { Enabled = checkboxAutomaticPBComp.Checked };
            _writeTheoryTimesFilePath();
            OnChangePBComparison?.Invoke(this, new PBComparisonSettingsChangeEventArgs(prevData, TheoryPBData));
        }

        private void txtTheoryPBAltName_TextChanged(object sender, EventArgs e)
        {
            var prevData = TheoryPBData;
            TheoryPBData = new PBComparisonData(TheoryPBData) { SecondaryName = txtTheoryPBAltName.Text };
            _writeTheoryTimesFilePath();
            OnChangePBComparison?.Invoke(this, new PBComparisonSettingsChangeEventArgs(prevData, TheoryPBData));
        }

        private void comparisonSettings_OnChange(object sender, ComparisonSettingsChangeEventArgs e)
        {
            _writeTheoryTimesFilePath();
            OnChangeComparison?.Invoke(this, e);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Live Split Theory Time (*.lstt)|*.lstt|All Files (*.*)|*.*",
                CheckFileExists = false
            };

            dialog.FileOk += delegate(object s, CancelEventArgs ev)
            {
                if (!_verifyTheoryTimeFilePathValid(dialog.FileName, false))
                {
                    MessageBox.Show(
                        "File does not contain valid theory times, it must either be an empty file or an already valid configuration.",
                        "Invalid File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation
                    );
                    ev.Cancel = true;
                }
            };

            if (File.Exists(TheoryTimesFilePath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(TheoryTimesFilePath);
                dialog.FileName = Path.GetFileName(TheoryTimesFilePath);
            }

            // TODO: add safety to not overwrite random files. Limit to files empty or already .lstt contents?

            if (dialog.ShowDialog() == DialogResult.OK) _updateTheoryTimesFilePath(dialog.FileName);
        }

        private void _writeTheoryTimesFilePath()
        {
            if (TheoryTimesFilePath == null)
                return;

            var contentsStream = new MemoryStream();

            var document = new XmlDocument();

            XmlNode docNode = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(docNode);

            var parent = document.CreateElement("TheoryTimesConfig");
            document.AppendChild(parent);

            _createTheoryTimeFileSettings(document, parent);

            document.Save(contentsStream);

            _writeTheoryTimesFileIfChanged(contentsStream.ToString());
        }

        private void _writeTheoryTimesFileIfChanged(string contents)
        {
            var currentContents = File.ReadAllText(TheoryTimesFilePath);
            if (currentContents != contents)
                return;

            File.WriteAllText(TheoryTimesFilePath, contents);
        }

        private void _updateTheoryTimesFilePath(string filePath)
        {
            if (TheoryTimesFileWatcher != null)
            {
                TheoryTimesFileWatcher.Changed -= _theoryTimesFile_OnChange;
                TheoryTimesFileWatcher.Renamed -= _theoryTimesFile_OnRenamed;
            }

            TheoryTimesFilePath = filePath;

            _loadTheoryTimesFilePath(filePath);

            _updateTheoryPBViewFields();
            ResetComparisons();

            TheoryTimesFileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath));
            TheoryTimesFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

            TheoryTimesFileWatcher.Changed += _theoryTimesFile_OnChange;
            TheoryTimesFileWatcher.Renamed += _theoryTimesFile_OnRenamed;

            OnChange?.Invoke(this, null);
        }

        private bool _loadTheoryTimesFilePath(string filePath)
        {
            TheoryPBData = PBComparisonData.Default;
            ComparisonsList.Clear();

            if (!File.Exists(filePath))
                return false;

            var fileContents = File.ReadAllText(filePath);

            if (fileContents.Length == 0)
                return true;

            var document = new XmlDocument();
            document.LoadXml(fileContents);

            var elements = document.GetElementsByTagName("TheoryTimesConfig");
            if (elements.Count != 1)
                return false;

            var element = elements[0];

            TheoryPBData = new PBComparisonData(TheoryPBData)
            {
                Enabled = SettingsHelper.ParseBool(element["AutoTheoryPB"]),
                SecondaryName = SettingsHelper.ParseString(element["AutoTheoryDisplayName"])
            };

            var comparisonsElement = element["Comparisons"];
            if (comparisonsElement == null)
                return true;

            foreach (var child in comparisonsElement.ChildNodes)
            {
                var comparisonData = ComparisonData.FromXml((XmlNode)child);

                var comparisonControl = new ComparisonSettings(CurrentState, comparisonData.SplitsName, ComparisonsList)
                    { Data = comparisonData };
                comparisonControl.OnChange += comparisonSettings_OnChange;
                ComparisonsList.Add(comparisonControl);
            }

            return true;
        }

        private void _updateTheoryPBViewFields()
        {
            checkboxAutomaticPBComp.Checked = TheoryPBData.Enabled;
            txtTheoryPBAltName.Text = TheoryPBData.SecondaryName;
        }

        private void _theoryTimesFile_OnChange(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath != TheoryTimesFilePath)
                return;

            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    break;

                case WatcherChangeTypes.Renamed:
                    break;
            }
        }

        private void _theoryTimesFile_OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("Renamed:");
            Console.WriteLine($"    Old: {e.OldFullPath}");
            Console.WriteLine($"    New: {e.FullPath}");
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            TheoryTimesFilePath = null;
            TheoryPBData = PBComparisonData.Default;
            ComparisonsList.Clear();

            _updateTheoryPBViewFields();
            ResetComparisons();
        }

        private void toggleViewFileLoaded(string value)
        {
            var hasFileLoaded = value != null;

            btnUnload.Enabled = hasFileLoaded;
            txtTheoryPBAltName.Enabled = hasFileLoaded;
            checkboxAutomaticPBComp.Enabled = hasFileLoaded;
            btnAddComparison.Enabled = hasFileLoaded;
            btnShowAll.Enabled = hasFileLoaded;
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
