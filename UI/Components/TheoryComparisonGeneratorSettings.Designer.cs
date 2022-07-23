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
            this.checkboxAutomaticPBComp = new System.Windows.Forms.CheckBox();
            this.groupComparisons = new System.Windows.Forms.GroupBox();
            this.tableComparisons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddComparison = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxGeneralSettings.SuspendLayout();
            this.groupComparisons.SuspendLayout();
            this.tableComparisons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxGeneralSettings, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupComparisons, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(459, 500);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxGeneralSettings
            // 
            this.groupBoxGeneralSettings.Controls.Add(this.checkboxAutomaticPBComp);
            this.groupBoxGeneralSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGeneralSettings.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGeneralSettings.Name = "groupBoxGeneralSettings";
            this.groupBoxGeneralSettings.Size = new System.Drawing.Size(453, 94);
            this.groupBoxGeneralSettings.TabIndex = 0;
            this.groupBoxGeneralSettings.TabStop = false;
            this.groupBoxGeneralSettings.Text = "General Settings";
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
            this.groupComparisons.Location = new System.Drawing.Point(3, 103);
            this.groupComparisons.Name = "groupComparisons";
            this.groupComparisons.Size = new System.Drawing.Size(453, 394);
            this.groupComparisons.TabIndex = 1;
            this.groupComparisons.TabStop = false;
            this.groupComparisons.Text = "Theory Comparisons";
            // 
            // tableComparisons
            // 
            this.tableComparisons.AutoScroll = true;
            this.tableComparisons.AutoSize = true;
            this.tableComparisons.ColumnCount = 2;
            this.tableComparisons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.85235F));
            this.tableComparisons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.14765F));
            this.tableComparisons.Controls.Add(this.btnAddComparison, 1, 0);
            this.tableComparisons.Controls.Add(this.btnShowAll, 0, 0);
            this.tableComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableComparisons.Location = new System.Drawing.Point(3, 16);
            this.tableComparisons.Name = "tableComparisons";
            this.tableComparisons.RowCount = 2;
            this.tableComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableComparisons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableComparisons.Size = new System.Drawing.Size(447, 375);
            this.tableComparisons.TabIndex = 0;
            // 
            // btnAddComparison
            // 
            this.btnAddComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddComparison.Location = new System.Drawing.Point(369, 3);
            this.btnAddComparison.Name = "btnAddComparison";
            this.btnAddComparison.Size = new System.Drawing.Size(75, 23);
            this.btnAddComparison.TabIndex = 3;
            this.btnAddComparison.Text = "Add Comparison";
            this.btnAddComparison.UseVisualStyleBackColor = true;
            this.btnAddComparison.Click += new System.EventHandler(this.btnAddComparison_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(269, 3);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(75, 23);
            this.btnShowAll.TabIndex = 4;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
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
            this.groupComparisons.ResumeLayout(false);
            this.groupComparisons.PerformLayout();
            this.tableComparisons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private System.Windows.Forms.CheckBox checkboxAutomaticPBComp;

		private System.Windows.Forms.GroupBox groupBoxGeneralSettings;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        #endregion

        private System.Windows.Forms.GroupBox groupComparisons;
        private System.Windows.Forms.TableLayoutPanel tableComparisons;
        private System.Windows.Forms.Button btnAddComparison;
        private System.Windows.Forms.Button btnShowAll;
    }
}
