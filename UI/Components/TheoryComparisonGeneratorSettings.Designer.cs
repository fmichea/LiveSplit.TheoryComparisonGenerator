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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBoxGeneralSettings = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTheoryPBAltName = new System.Windows.Forms.TextBox();
			this.checkboxAutomaticPBComp = new System.Windows.Forms.CheckBox();
			this.groupComparisons = new System.Windows.Forms.GroupBox();
			this.tableComparisons = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnAddComparison = new System.Windows.Forms.Button();
			this.btnShowAll = new System.Windows.Forms.Button();
			this.tableLayoutPanelFileSelect = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtTheoryTimesPath = new System.Windows.Forms.TextBox();
			this.btnUnload = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBoxGeneralSettings.SuspendLayout();
			this.groupComparisons.SuspendLayout();
			this.tableComparisons.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanelFileSelect.SuspendLayout();
			this.SuspendLayout();
			//
			// tableLayoutPanel1
			//
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.groupBoxGeneralSettings, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupComparisons, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelFileSelect, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(459, 500);
			this.tableLayoutPanel1.TabIndex = 0;
			//
			// groupBoxGeneralSettings
			//
			this.groupBoxGeneralSettings.Controls.Add(this.label1);
			this.groupBoxGeneralSettings.Controls.Add(this.txtTheoryPBAltName);
			this.groupBoxGeneralSettings.Controls.Add(this.checkboxAutomaticPBComp);
			this.groupBoxGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxGeneralSettings.Location = new System.Drawing.Point(3, 39);
			this.groupBoxGeneralSettings.Name = "groupBoxGeneralSettings";
			this.groupBoxGeneralSettings.Size = new System.Drawing.Size(453, 94);
			this.groupBoxGeneralSettings.TabIndex = 0;
			this.groupBoxGeneralSettings.TabStop = false;
			this.groupBoxGeneralSettings.Text = "General Settings";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Display Name:";
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
			this.groupComparisons.Location = new System.Drawing.Point(3, 139);
			this.groupComparisons.Name = "groupComparisons";
			this.groupComparisons.Size = new System.Drawing.Size(453, 358);
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
			this.tableComparisons.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableComparisons.Location = new System.Drawing.Point(3, 16);
			this.tableComparisons.Name = "tableComparisons";
			this.tableComparisons.RowCount = 2;
			this.tableComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.32448F));
			this.tableComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.67551F));
			this.tableComparisons.Size = new System.Drawing.Size(447, 339);
			this.tableComparisons.TabIndex = 0;
			//
			// tableLayoutPanel2
			//
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.14286F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanel2.Controls.Add(this.btnAddComparison, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnShowAll, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(441, 28);
			this.tableLayoutPanel2.TabIndex = 5;
			//
			// btnAddComparison
			//
			this.btnAddComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddComparison.Location = new System.Drawing.Point(388, 3);
			this.btnAddComparison.Name = "btnAddComparison";
			this.btnAddComparison.Size = new System.Drawing.Size(50, 22);
			this.btnAddComparison.TabIndex = 3;
			this.btnAddComparison.Text = "Add Comparison";
			this.btnAddComparison.UseVisualStyleBackColor = true;
			this.btnAddComparison.Click += new System.EventHandler(this.btnAddComparison_Click);
			//
			// btnShowAll
			//
			this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnShowAll.Location = new System.Drawing.Point(320, 3);
			this.btnShowAll.Name = "btnShowAll";
			this.btnShowAll.Size = new System.Drawing.Size(62, 22);
			this.btnShowAll.TabIndex = 4;
			this.btnShowAll.Text = "Show All";
			this.btnShowAll.UseVisualStyleBackColor = true;
			this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
			//
			// tableLayoutPanelFileSelect
			//
			this.tableLayoutPanelFileSelect.ColumnCount = 4;
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 275F));
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
			this.tableLayoutPanelFileSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
			this.tableLayoutPanelFileSelect.Controls.Add(this.label2, 0, 0);
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
			// label2
			//
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "Theory Time File";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			// TheoryComparisonGeneratorSettings
			//
			this.AutoScroll = true;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TheoryComparisonGeneratorSettings";
			this.Size = new System.Drawing.Size(459, 500);
			this.Load += new System.EventHandler(this.TheoryComparisonGeneratorSettings_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupBoxGeneralSettings.ResumeLayout(false);
			this.groupBoxGeneralSettings.PerformLayout();
			this.groupComparisons.ResumeLayout(false);
			this.groupComparisons.PerformLayout();
			this.tableComparisons.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanelFileSelect.ResumeLayout(false);
			this.tableLayoutPanelFileSelect.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

		private System.Windows.Forms.Button btnUnload;

		private System.Windows.Forms.TextBox txtTheoryTimesPath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowse;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFileSelect;

		private System.Windows.Forms.CheckBox checkboxAutomaticPBComp;

		private System.Windows.Forms.GroupBox groupBoxGeneralSettings;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        #endregion

        private System.Windows.Forms.GroupBox groupComparisons;
        private System.Windows.Forms.TableLayoutPanel tableComparisons;
        private System.Windows.Forms.Button btnAddComparison;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTheoryPBAltName;
    }
}
