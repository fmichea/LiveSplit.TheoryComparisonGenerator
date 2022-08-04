using System.ComponentModel;

namespace LiveSplit.UI.Components
{
	partial class TheoryComparisonGeneratorSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.labelErrorFileOpen = new System.Windows.Forms.Label();
			this.tableLayoutPanelFileSelect = new System.Windows.Forms.TableLayoutPanel();
			this.labelTheoryTimeFile = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtTheoryTimesPath = new System.Windows.Forms.TextBox();
			this.btnUnload = new System.Windows.Forms.Button();
			this.groupBoxGeneralSettings = new System.Windows.Forms.GroupBox();
			this.labelDisplayName = new System.Windows.Forms.Label();
			this.txtTheoryPBAltName = new System.Windows.Forms.TextBox();
			this.checkboxAutomaticPBComp = new System.Windows.Forms.CheckBox();
			this.groupComparisons = new System.Windows.Forms.GroupBox();
			this.tableComparisons = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanelButtonBarComparisons = new System.Windows.Forms.TableLayoutPanel();
			this.btnAddComparison = new System.Windows.Forms.Button();
			this.btnShowAll = new System.Windows.Forms.Button();
			this.tableLayoutPanelMain.SuspendLayout();
			this.tableLayoutPanelFileSelect.SuspendLayout();
			this.groupBoxGeneralSettings.SuspendLayout();
			this.groupComparisons.SuspendLayout();
			this.tableComparisons.SuspendLayout();
			this.tableLayoutPanelButtonBarComparisons.SuspendLayout();
			this.SuspendLayout();
			//
			// tableLayoutPanelMain
			//
			this.tableLayoutPanelMain.AutoScroll = true;
			this.tableLayoutPanelMain.AutoSize = true;
			this.tableLayoutPanelMain.ColumnCount = 1;
			this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelMain.Controls.Add(this.labelErrorFileOpen, 0, 1);
			this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelFileSelect, 0, 0);
			this.tableLayoutPanelMain.Controls.Add(this.groupBoxGeneralSettings, 0, 2);
			this.tableLayoutPanelMain.Controls.Add(this.groupComparisons, 0, 3);
			this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
			this.tableLayoutPanelMain.RowCount = 4;
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 83F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
			this.tableLayoutPanelMain.Size = new System.Drawing.Size(459, 500);
			this.tableLayoutPanelMain.TabIndex = 0;
			//
			// labelErrorFileOpen
			//
			this.labelErrorFileOpen.ForeColor = System.Drawing.Color.Red;
			this.labelErrorFileOpen.Location = new System.Drawing.Point(3, 36);
			this.labelErrorFileOpen.Name = "labelErrorFileOpen";
			this.labelErrorFileOpen.Size = new System.Drawing.Size(453, 30);
			this.labelErrorFileOpen.TabIndex = 3;
			this.labelErrorFileOpen.Text = "ERROR: Another LiveSplit instance is currently editing this layout. Please close " + "both before modifying Theory Times in LiveSplit.";
			//
			// tableLayoutPanelFileSelect
			//
			this.tableLayoutPanelFileSelect.ColumnCount = 4;
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 275F));
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
			this.tableLayoutPanelFileSelect.Controls.Add(this.labelTheoryTimeFile, 0, 0);
			this.tableLayoutPanelFileSelect.Controls.Add(this.btnBrowse, 2, 0);
			this.tableLayoutPanelFileSelect.Controls.Add(this.txtTheoryTimesPath, 1, 0);
			this.tableLayoutPanelFileSelect.Controls.Add(this.btnUnload, 3, 0);
			this.tableLayoutPanelFileSelect.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanelFileSelect.Name = "tableLayoutPanelFileSelect";
			this.tableLayoutPanelFileSelect.RowCount = 1;
			this.tableLayoutPanelFileSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelFileSelect.Size = new System.Drawing.Size(453, 30);
			this.tableLayoutPanelFileSelect.TabIndex = 2;
			//
			// labelTheoryTimeFile
			//
			this.labelTheoryTimeFile.Location = new System.Drawing.Point(3, 0);
			this.labelTheoryTimeFile.Name = "labelTheoryTimeFile";
			this.labelTheoryTimeFile.Size = new System.Drawing.Size(52, 26);
			this.labelTheoryTimeFile.TabIndex = 1;
			this.labelTheoryTimeFile.Text = "Theory Time File";
			this.labelTheoryTimeFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			// btnBrowse
			//
			this.btnBrowse.Location = new System.Drawing.Point(336, 3);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(58, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			//
			// txtTheoryTimesPath
			//
			this.txtTheoryTimesPath.Enabled = false;
			this.txtTheoryTimesPath.Location = new System.Drawing.Point(61, 3);
			this.txtTheoryTimesPath.Name = "txtTheoryTimesPath";
			this.txtTheoryTimesPath.Size = new System.Drawing.Size(269, 20);
			this.txtTheoryTimesPath.TabIndex = 0;
			//
			// btnUnload
			//
			this.btnUnload.Location = new System.Drawing.Point(400, 3);
			this.btnUnload.Name = "btnUnload";
			this.btnUnload.Size = new System.Drawing.Size(50, 23);
			this.btnUnload.TabIndex = 3;
			this.btnUnload.Text = "Unload";
			this.btnUnload.UseVisualStyleBackColor = true;
			this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
			//
			// groupBoxGeneralSettings
			//
			this.groupBoxGeneralSettings.Controls.Add(this.labelDisplayName);
			this.groupBoxGeneralSettings.Controls.Add(this.txtTheoryPBAltName);
			this.groupBoxGeneralSettings.Controls.Add(this.checkboxAutomaticPBComp);
			this.groupBoxGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxGeneralSettings.Location = new System.Drawing.Point(3, 69);
			this.groupBoxGeneralSettings.Name = "groupBoxGeneralSettings";
			this.groupBoxGeneralSettings.Size = new System.Drawing.Size(453, 77);
			this.groupBoxGeneralSettings.TabIndex = 0;
			this.groupBoxGeneralSettings.TabStop = false;
			this.groupBoxGeneralSettings.Text = "General Settings";
			//
			// labelDisplayName
			//
			this.labelDisplayName.AutoSize = true;
			this.labelDisplayName.Location = new System.Drawing.Point(3, 52);
			this.labelDisplayName.Name = "labelDisplayName";
			this.labelDisplayName.Size = new System.Drawing.Size(75, 13);
			this.labelDisplayName.TabIndex = 2;
			this.labelDisplayName.Text = "Display Name:";
			//
			// txtTheoryPBAltName
			//
			this.txtTheoryPBAltName.Location = new System.Drawing.Point(84, 49);
			this.txtTheoryPBAltName.Name = "txtTheoryPBAltName";
			this.txtTheoryPBAltName.Size = new System.Drawing.Size(206, 20);
			this.txtTheoryPBAltName.TabIndex = 1;
			this.txtTheoryPBAltName.TextChanged += new System.EventHandler(this.txtTheoryPBAltName_TextChanged);
			//
			// checkboxAutomaticPBComp
			//
			this.checkboxAutomaticPBComp.Location = new System.Drawing.Point(6, 19);
			this.checkboxAutomaticPBComp.Name = "checkboxAutomaticPBComp";
			this.checkboxAutomaticPBComp.Size = new System.Drawing.Size(397, 24);
			this.checkboxAutomaticPBComp.TabIndex = 0;
			this.checkboxAutomaticPBComp.Text = "Automatically generate theory comparison for PB";
			this.checkboxAutomaticPBComp.UseVisualStyleBackColor = true;
			this.checkboxAutomaticPBComp.CheckedChanged += new System.EventHandler(this.checkboxAutomaticPBComp_CheckedChanged);
			//
			// groupComparisons
			//
			this.groupComparisons.AutoSize = true;
			this.groupComparisons.Controls.Add(this.tableComparisons);
			this.groupComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupComparisons.Location = new System.Drawing.Point(3, 152);
			this.groupComparisons.Name = "groupComparisons";
			this.groupComparisons.Size = new System.Drawing.Size(453, 345);
			this.groupComparisons.TabIndex = 1;
			this.groupComparisons.TabStop = false;
			this.groupComparisons.Text = "Theory Comparisons";
			//
			// tableComparisons
			//
			this.tableComparisons.AutoScroll = true;
			this.tableComparisons.AutoSize = true;
			this.tableComparisons.ColumnCount = 1;
			this.tableComparisons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.85235F));
			this.tableComparisons.Controls.Add(this.tableLayoutPanelButtonBarComparisons, 0, 0);
			this.tableComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableComparisons.Location = new System.Drawing.Point(3, 16);
			this.tableComparisons.Name = "tableComparisons";
			this.tableComparisons.RowCount = 2;
			this.tableComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.28527F));
			this.tableComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.71474F));
			this.tableComparisons.Size = new System.Drawing.Size(447, 326);
			this.tableComparisons.TabIndex = 0;
			//
			// tableLayoutPanelButtonBarComparisons
			//
			this.tableLayoutPanelButtonBarComparisons.ColumnCount = 3;
			this.tableLayoutPanelButtonBarComparisons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.14286F));
			this.tableLayoutPanelButtonBarComparisons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanelButtonBarComparisons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanelButtonBarComparisons.Controls.Add(this.btnAddComparison, 2, 0);
			this.tableLayoutPanelButtonBarComparisons.Controls.Add(this.btnShowAll, 1, 0);
			this.tableLayoutPanelButtonBarComparisons.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanelButtonBarComparisons.Name = "tableLayoutPanelButtonBarComparisons";
			this.tableLayoutPanelButtonBarComparisons.RowCount = 1;
			this.tableLayoutPanelButtonBarComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelButtonBarComparisons.Size = new System.Drawing.Size(441, 29);
			this.tableLayoutPanelButtonBarComparisons.TabIndex = 5;
			//
			// btnAddComparison
			//
			this.btnAddComparison.Location = new System.Drawing.Point(389, 3);
			this.btnAddComparison.Name = "btnAddComparison";
			this.btnAddComparison.Size = new System.Drawing.Size(49, 21);
			this.btnAddComparison.TabIndex = 3;
			this.btnAddComparison.Text = "Add Comparison";
			this.btnAddComparison.UseVisualStyleBackColor = true;
			this.btnAddComparison.Click += new System.EventHandler(this.btnAddComparison_Click);
			//
			// btnShowAll
			//
			this.btnShowAll.Location = new System.Drawing.Point(314, 3);
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(69, 21);
			this.btnShowAll.TabIndex = 4;
			this.btnShowAll.Text = "Show All";
			this.btnShowAll.UseVisualStyleBackColor = true;
			this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
			//
			// TheoryComparisonGeneratorSettings
			//
			this.AutoScroll = true;
			this.Controls.Add(this.tableLayoutPanelMain);
			this.Name = "TheoryComparisonGeneratorSettings";
			this.Size = new System.Drawing.Size(459, 500);
			this.Load += new System.EventHandler(this.TheoryComparisonGeneratorSettings_Load);
			this.tableLayoutPanelMain.ResumeLayout(false);
			this.tableLayoutPanelMain.PerformLayout();
			this.tableLayoutPanelFileSelect.ResumeLayout(false);
			this.tableLayoutPanelFileSelect.PerformLayout();
			this.groupBoxGeneralSettings.ResumeLayout(false);
			this.groupBoxGeneralSettings.PerformLayout();
			this.groupComparisons.ResumeLayout(false);
			this.groupComparisons.PerformLayout();
			this.tableComparisons.ResumeLayout(false);
			this.tableLayoutPanelButtonBarComparisons.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.Label labelErrorFileOpen;

		private System.Windows.Forms.Label label3;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtonBarComparisons;

		private System.Windows.Forms.Button btnUnload;

		private System.Windows.Forms.TextBox txtTheoryTimesPath;
		private System.Windows.Forms.Label labelTheoryTimeFile;
		private System.Windows.Forms.Button btnBrowse;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFileSelect;

		private System.Windows.Forms.CheckBox checkboxAutomaticPBComp;

		private System.Windows.Forms.GroupBox groupBoxGeneralSettings;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;

        #endregion

        private System.Windows.Forms.GroupBox groupComparisons;
        private System.Windows.Forms.TableLayoutPanel tableComparisons;
        private System.Windows.Forms.Button btnAddComparison;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Label labelDisplayName;
        private System.Windows.Forms.TextBox txtTheoryPBAltName;
    }
}
