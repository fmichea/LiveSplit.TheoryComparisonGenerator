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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAddTheoryTime = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxGeneralSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxGeneralSettings, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 257);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBoxGeneralSettings
            // 
            this.groupBoxGeneralSettings.Controls.Add(this.checkboxAutomaticPBComp);
            this.groupBoxGeneralSettings.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGeneralSettings.Name = "groupBoxGeneralSettings";
            this.groupBoxGeneralSettings.Size = new System.Drawing.Size(516, 122);
            this.groupBoxGeneralSettings.TabIndex = 0;
            this.groupBoxGeneralSettings.TabStop = false;
            this.groupBoxGeneralSettings.Text = "General Settings";
            // 
            // checkboxAutomaticPBComp
            // 
            this.checkboxAutomaticPBComp.Location = new System.Drawing.Point(6, 19);
            this.checkboxAutomaticPBComp.Name = "checkboxAutomaticPBComp";
            this.checkboxAutomaticPBComp.Size = new System.Drawing.Size(510, 24);
            this.checkboxAutomaticPBComp.TabIndex = 0;
            this.checkboxAutomaticPBComp.Text = "Automatically generate theory comparison for PB";
            this.checkboxAutomaticPBComp.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.63566F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.36434F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAddTheoryTime, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 131);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(516, 123);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // buttonAddTheoryTime
            // 
            this.buttonAddTheoryTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddTheoryTime.Location = new System.Drawing.Point(8, 50);
            this.buttonAddTheoryTime.Name = "buttonAddTheoryTime";
            this.buttonAddTheoryTime.Size = new System.Drawing.Size(75, 23);
            this.buttonAddTheoryTime.TabIndex = 2;
            this.buttonAddTheoryTime.Text = "Add Time";
            this.buttonAddTheoryTime.UseVisualStyleBackColor = true;
            this.buttonAddTheoryTime.Click += new System.EventHandler(this.buttonAddTheoryTime_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.checkedListBox1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(94, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.80342F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.19658F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(419, 117);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 25);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(413, 89);
            this.checkedListBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(413, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Saved times for Loaded Split File:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // TheoryComparisonGeneratorSettings
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TheoryComparisonGeneratorSettings";
            this.Size = new System.Drawing.Size(540, 276);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxGeneralSettings.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

		}

		private System.Windows.Forms.CheckBox checkboxAutomaticPBComp;

		private System.Windows.Forms.GroupBox groupBoxGeneralSettings;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAddTheoryTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
    }
}
