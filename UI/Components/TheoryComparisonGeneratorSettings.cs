using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.TheoryComparisonGenerator.Comparisons;
// FIXME:
// - Lock file might not release clean every time. Think this has to do with unloading splits file, doesn't close the lock. Looks for wrong path.
// - Text box not reset when saying no on save even tho it should load back from file. LoadedFileContents is the same as File contents
// - Cancel on main window when asking to save does not close window.

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
            TheoryPBData = PBComparisonData.Default;
            TheoryTimesFilePath = null;

            // All events related to changes to internal data cause callbackt to be called to save to file.
            OnChange += _theoryTimesConfiguration_OnChange;
            OnChangeComparison += _theoryTimesConfiguration_OnChange;
            OnChangePBComparison += _theoryTimesConfiguration_OnChange;
        }

        public bool HasChanged { get; set; }

        public Size StartingSize { get; set; }
        public Size StartingTableLayoutSize { get; set; }

        public LiveSplitState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                SplitsName = Path.GetFileNameWithoutExtension(value?.Run.FilePath);
            }
        }
        private LiveSplitState _currentState { get; set; }
        public string SplitsName { get; set; }

        public PBComparisonData TheoryPBData
        {
            get { return _theoryPBData; }
            set
            {
                _theoryPBData = value; // Important to do this first so event callbacks do not loop.
                _updateTheoryPBDataUI(value);
            }
        }
        public PBComparisonData _theoryPBData { get; set; }

        public string TheoryTimesFilePath
        {
            get => _theoryTimesFilePath;
            protected set
            {
                _theoryTimesFilePath = value; // Important to do this first so event callbacks do not loop.
                _updateTheoryTimeFilePathUI(value);
            }
        }
        private string _theoryTimesFilePath { get; set; }

        public FileSystemWatcher TheoryTimesFileWatcher { get; set; }

        public IList<ComparisonSettings> ComparisonsList { get; set; }

        private bool ShowAll { get; set; }

        private string LoadedFileContents { get; set; }

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

            try
            {
                var document = new XmlDocument();
                document.LoadXml(theoryTimeFile);

                var elements = document.GetElementsByTagName("TheoryTimesConfig");
                return elements.Count == 1;
            }
            catch
            {
                return false;
            }
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

        void AddColumnToLayout(ComparisonSettings comparison, int index)
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

        private delegate void ResetComparisonsDelegate();

        private void ResetComparisons()
        {
            if (InvokeRequired)
            {
                ResetComparisonsDelegate d = new ResetComparisonsDelegate(ResetComparisons);
                Invoke(d, new object[] { });
            }
            else
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
        }

        private delegate void ClearLayoutDelegate();

        private void ClearLayout()
        {
            if (InvokeRequired)
            {
                ClearLayoutDelegate d = new ClearLayoutDelegate(ClearLayout);
                Invoke(d, new object[] { });
            }
            else
            {
                tableComparisons.RowCount = 1;
                tableComparisons.RowStyles.Clear();
                tableComparisons.RowStyles.Add(new RowStyle(SizeType.Absolute, 34f));
                tableComparisons.Size = StartingTableLayoutSize;
                foreach (var control in tableComparisons.Controls.OfType<ComparisonSettings>().ToList())
                    tableComparisons.Controls.Remove(control);
                Size = StartingSize;
            }
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

        private FileStream fileLock { get; set; }

        private void TheoryComparisonGeneratorSettings_Load(object sender, EventArgs e)
        {
            Parent.CausesValidation = true;
            ParentForm.Enter += _theoryComparisonGeneratorSettings_Enter;
            Parent.Leave += _theoryComparisonGeneratorSettings_Leave;
            ParentForm.FormClosing += _LayoutSettingsClosed;
            //_theoryComparisonGeneratorSettings_Enter(sender, e);
        }

        private void _theoryComparisonGeneratorSettings_Enter(object sender, EventArgs e)
        {
            HasChanged = false;
            _removeTheoryTimesFileWatcher();
            _lockFileAndUpdateUI();
        }

        private void _showErrorMessageFileAlreadyOpen()
        {
            labelErrorFileOpen.Visible = true;
            groupComparisons.Visible = false;
            groupBoxGeneralSettings.Visible = false;
        }

        private void _showTheoryTimesFileSettings()
        {
            labelErrorFileOpen.Visible = false;
            groupComparisons.Visible = true;
            groupBoxGeneralSettings.Visible = true;
        }

        private string TheoryTimesFilePathLock
        {
            get { return TheoryTimesFilePath + ".lock"; }
        }

        private bool _lockFileAndUpdateUI()
        {
            try
            {
                if (TheoryTimesFilePath == null)
                    return false;

                fileLock = new FileStream(TheoryTimesFilePathLock, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

                _showTheoryTimesFileSettings();
                ResetComparisons();

                return true;
            }
            catch (IOException e)
            {
                _showErrorMessageFileAlreadyOpen();
                return false;
            }
        }

        private void _unlockFile()
        {
            if (fileLock != null)
            {
                fileLock.Close();
                fileLock = null;

                File.Delete(TheoryTimesFilePathLock);
            }
        }
        private void _LayoutSettingsClosed(object sender, EventArgs e)
        {
            var shouldSave = false;

                if (HasChanged)
                {
                    var result = MessageBox.Show(
                        "Would you like to save changes made to the theory times file?",
                        "Save Changes",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    shouldSave = result == DialogResult.Yes;
                }

                if (fileLock != null)
                {
                    if (shouldSave)
                        _writeTheoryTimesFilePath();
                    else
                        _loadTheoryTimesFilePath(TheoryTimesFilePath);
                }
                else if (shouldSave && !string.IsNullOrEmpty(TheoryTimesFilePath) && !File.Exists(TheoryTimesFilePath))
                {
                    _writeTheoryTimesFilePath();
                }
            
            _addTheoryTimesFileWatcher();
            _unlockFile();
            LoadedFileContents = null;

        }
        private void _theoryComparisonGeneratorSettings_Leave(object sender, EventArgs e)
        {
            _unlockFile();
            LoadedFileContents = null;
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
            var newChecked = checkboxAutomaticPBComp.Checked;
            if (newChecked == TheoryPBData.Enabled)
                return;

            var prevData = TheoryPBData;
            TheoryPBData = new PBComparisonData(TheoryPBData) { Enabled = newChecked };
            OnChangePBComparison?.Invoke(this, new PBComparisonSettingsChangeEventArgs(prevData, TheoryPBData));
        }

        private void txtTheoryPBAltName_TextChanged(object sender, EventArgs e)
        {
            var newText = txtTheoryPBAltName.Text;
            if (newText == TheoryPBData.SecondaryName)
                return;

            var prevData = TheoryPBData;
            TheoryPBData = new PBComparisonData(TheoryPBData) { SecondaryName = newText };
            OnChangePBComparison?.Invoke(this, new PBComparisonSettingsChangeEventArgs(prevData, TheoryPBData));
        }

        private void comparisonSettings_OnChange(object sender, ComparisonSettingsChangeEventArgs e)
        {
            OnChangeComparison?.Invoke(this, e);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Live Split Theory Time (*.lstt)|*.lstt|All Files (*.*)|*.*",
                CheckFileExists = false
            };

            dialog.FileOk += delegate (object s, CancelEventArgs ev)
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

            if (dialog.ShowDialog() == DialogResult.OK) _updateTheoryTimesFilePath(dialog.FileName);
        }

        private void _writeTheoryTimesFilePath()
        {
            if (TheoryTimesFilePath == null)
                return;

            var document = new XmlDocument();

            XmlNode docNode = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(docNode);

            var parent = document.CreateElement("TheoryTimesConfig");
            document.AppendChild(parent);

            _createTheoryTimeFileSettings(document, parent);

            using (var contentsStream = new MemoryStream())
            {
                document.Save(contentsStream);

                contentsStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(contentsStream))
                {
                    _writeTheoryTimesFileIfChanged(reader.ReadToEnd());
                }
            }
        }

        private void _writeTheoryTimesFileIfChanged(string contents)
        {
            try
            {
                var currentContents = File.ReadAllText(TheoryTimesFilePath);
                if (currentContents == contents)
                    return;
            }
            catch (IOException e)
            { }

            LoadedFileContents = contents;
            File.WriteAllText(TheoryTimesFilePath, contents);
        }

        private void _updateTheoryTimesFilePath(string filePath)
        {
            bool wasLocked = fileLock != null;
            bool hadWatch = _removeTheoryTimesFileWatcher();

            if (wasLocked) _unlockFile();
            TheoryTimesFilePath = filePath;
            if (wasLocked) _lockFileAndUpdateUI();

            _loadTheoryTimesFilePath(filePath);

            if (hadWatch) _addTheoryTimesFileWatcher();
        }

        private bool _removeTheoryTimesFileWatcher()
        {
            if (TheoryTimesFileWatcher == null) return false;

            TheoryTimesFileWatcher.EnableRaisingEvents = false;
            TheoryTimesFileWatcher.Changed -= _theoryTimesFile_OnChange;
            TheoryTimesFileWatcher.Renamed -= _theoryTimesFile_OnRenamed;
            TheoryTimesFileWatcher = null;

            return true;
        }

        private void _addTheoryTimesFileWatcher()
        {
            var directory = Path.GetDirectoryName(TheoryTimesFilePath);
            if (directory != null)
            {
                TheoryTimesFileWatcher = new FileSystemWatcher(directory);
                TheoryTimesFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

                TheoryTimesFileWatcher.Changed += _theoryTimesFile_OnChange;
                TheoryTimesFileWatcher.Renamed += _theoryTimesFile_OnRenamed;

                TheoryTimesFileWatcher.EnableRaisingEvents = true;
            }
        }

        private bool _loadTheoryTimesFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            string fileContents = "";
            int maxRetries = 5;

            for (int idx = 0; idx < maxRetries; idx++)
            {
                try
                {
                    fileContents = File.ReadAllText(filePath);
                }
                catch (IOException e)
                {
                    if (idx == maxRetries - 1)
                        throw;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(20));
            }

            if (fileContents == LoadedFileContents)
                return false;

            LoadedFileContents = fileContents;

            if (fileContents.Length == 0)
                return true;

            TheoryPBData = PBComparisonData.Default;
            ComparisonsList.Clear();

            try
            {
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

                    var comparisonControl =
                        new ComparisonSettings(CurrentState, comparisonData.SplitsName, ComparisonsList)
                        { Data = comparisonData };
                    comparisonControl.OnChange += comparisonSettings_OnChange;
                    ComparisonsList.Add(comparisonControl);
                }

                OnChange?.Invoke(this, null);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                ResetComparisons();
                _showTheoryTimesFileSettings();
            }
        }

        private void _theoryTimesFile_OnChange(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath != TheoryTimesFilePath)
                return;

            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            _loadTheoryTimesFilePath(TheoryTimesFilePath);
        }

        private void _theoryTimesFile_OnRenamed(object sender, RenamedEventArgs e)
        {
            if (e.OldFullPath != TheoryTimesFilePath)
                return;

            _updateTheoryTimesFilePath(e.FullPath);
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            _unlockFile();
            LoadedFileContents = null;
            TheoryTimesFilePath = null;
            TheoryPBData = PBComparisonData.Default;
            ComparisonsList.Clear();

            ResetComparisons();

            OnChange?.Invoke(this, null);
        }

        private delegate void _updateTheoryTimeFileUIDelegate(string value);

        private void _updateTheoryTimeFilePathUI(string value)
        {
            if (InvokeRequired)
            {
                _updateTheoryTimeFileUIDelegate d = new _updateTheoryTimeFileUIDelegate(_updateTheoryTimeFilePathUI);
                Invoke(d, new object[] { value });
            }
            else
            {
                var hasFileLoaded = value != null;

                txtTheoryTimesPath.Text = value;
                btnUnload.Enabled = hasFileLoaded;
                txtTheoryPBAltName.Enabled = hasFileLoaded;
                checkboxAutomaticPBComp.Enabled = hasFileLoaded;
                btnAddComparison.Enabled = hasFileLoaded;
                btnShowAll.Enabled = hasFileLoaded;
            }
        }

        private delegate void _updateTheoryPBDataUIDelegate(PBComparisonData value);

        private void _updateTheoryPBDataUI(PBComparisonData value)
        {
            if (InvokeRequired)
            {
                _updateTheoryPBDataUIDelegate d = new _updateTheoryPBDataUIDelegate(_updateTheoryPBDataUI);
                Invoke(d, new object[] { value });
            }
            else
            {
                checkboxAutomaticPBComp.Checked = value.Enabled;
                txtTheoryPBAltName.Text = value.SecondaryName;
            }
        }

        private void _theoryTimesConfiguration_OnChange(object sender, EventArgs args)
        {
            HasChanged = true;
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
