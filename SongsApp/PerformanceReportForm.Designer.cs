namespace Songs
{
    partial class PerformanceReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformanceReportForm));
            this.tb = new System.Windows.Forms.TextBox();
            this.cboxReverseDate = new System.Windows.Forms.CheckBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlainText = new System.Windows.Forms.RadioButton();
            this.rbHTMLVenueColumns = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb
            // 
            this.tb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb.Location = new System.Drawing.Point(12, 112);
            this.tb.Multiline = true;
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(932, 417);
            this.tb.TabIndex = 1;
            // 
            // cboxReverseDate
            // 
            this.cboxReverseDate.AutoSize = true;
            this.cboxReverseDate.Checked = true;
            this.cboxReverseDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxReverseDate.Location = new System.Drawing.Point(12, 38);
            this.cboxReverseDate.Name = "cboxReverseDate";
            this.cboxReverseDate.Size = new System.Drawing.Size(117, 17);
            this.cboxReverseDate.TabIndex = 2;
            this.cboxReverseDate.Text = "Reverse date order";
            this.cboxReverseDate.UseVisualStyleBackColor = true;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(12, 61);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(117, 23);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbHTMLVenueColumns);
            this.groupBox1.Controls.Add(this.rbPlainText);
            this.groupBox1.Location = new System.Drawing.Point(156, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 71);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Format";
            // 
            // rbPlainText
            // 
            this.rbPlainText.AutoSize = true;
            this.rbPlainText.Location = new System.Drawing.Point(6, 19);
            this.rbPlainText.Name = "rbPlainText";
            this.rbPlainText.Size = new System.Drawing.Size(72, 17);
            this.rbPlainText.TabIndex = 0;
            this.rbPlainText.Text = "Plain Text";
            this.rbPlainText.UseVisualStyleBackColor = true;
            // 
            // rbHTMLVenueColumns
            // 
            this.rbHTMLVenueColumns.AutoSize = true;
            this.rbHTMLVenueColumns.Checked = true;
            this.rbHTMLVenueColumns.Location = new System.Drawing.Point(6, 42);
            this.rbHTMLVenueColumns.Name = "rbHTMLVenueColumns";
            this.rbHTMLVenueColumns.Size = new System.Drawing.Size(132, 17);
            this.rbHTMLVenueColumns.TabIndex = 1;
            this.rbHTMLVenueColumns.TabStop = true;
            this.rbHTMLVenueColumns.Text = "HTML Venue Columns";
            this.rbHTMLVenueColumns.UseVisualStyleBackColor = true;
            // 
            // PerformanceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(956, 541);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.cboxReverseDate);
            this.Controls.Add(this.tb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PerformanceReportForm";
            this.Text = "Performance Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb;
        private System.Windows.Forms.CheckBox cboxReverseDate;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbHTMLVenueColumns;
        private System.Windows.Forms.RadioButton rbPlainText;
    }
}