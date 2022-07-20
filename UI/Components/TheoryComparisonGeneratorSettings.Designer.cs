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
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBoxGeneralSettings.SuspendLayout();
			this.SuspendLayout();
			//
			// tableLayoutPanel1
			//
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBoxGeneralSettings, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
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
			// TrueBalancedComparisonSettings
			//
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "theoryComparisonGeneratorSettings";
			this.Size = new System.Drawing.Size(540, 276);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBoxGeneralSettings.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.CheckBox checkboxAutomaticPBComp;

		private System.Windows.Forms.GroupBox groupBoxGeneralSettings;

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		#endregion
	}
}
