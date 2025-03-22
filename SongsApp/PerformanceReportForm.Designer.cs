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
            this.rbHTMLbyDate = new System.Windows.Forms.RadioButton();
            this.rbHTMLbyVenue = new System.Windows.Forms.RadioButton();
            this.rbPlainText = new System.Windows.Forms.RadioButton();
            this.dpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.cboxReverseDate.Location = new System.Drawing.Point(12, 89);
            this.cboxReverseDate.Name = "cboxReverseDate";
            this.cboxReverseDate.Size = new System.Drawing.Size(117, 17);
            this.cboxReverseDate.TabIndex = 2;
            this.cboxReverseDate.Text = "Reverse date order";
            this.cboxReverseDate.UseVisualStyleBackColor = true;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(370, 83);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(117, 23);
            this.btnGenerateReport.TabIndex = 3;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbHTMLbyDate);
            this.groupBox1.Controls.Add(this.rbHTMLbyVenue);
            this.groupBox1.Controls.Add(this.rbPlainText);
            this.groupBox1.Location = new System.Drawing.Point(201, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 93);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Format";
            // 
            // rbHTMLbyDate
            // 
            this.rbHTMLbyDate.AutoSize = true;
            this.rbHTMLbyDate.Location = new System.Drawing.Point(6, 65);
            this.rbHTMLbyDate.Name = "rbHTMLbyDate";
            this.rbHTMLbyDate.Size = new System.Drawing.Size(95, 17);
            this.rbHTMLbyDate.TabIndex = 2;
            this.rbHTMLbyDate.TabStop = true;
            this.rbHTMLbyDate.Text = "HTML by Date";
            this.rbHTMLbyDate.UseVisualStyleBackColor = true;
            // 
            // rbHTMLbyVenue
            // 
            this.rbHTMLbyVenue.AutoSize = true;
            this.rbHTMLbyVenue.Checked = true;
            this.rbHTMLbyVenue.Location = new System.Drawing.Point(6, 42);
            this.rbHTMLbyVenue.Name = "rbHTMLbyVenue";
            this.rbHTMLbyVenue.Size = new System.Drawing.Size(103, 17);
            this.rbHTMLbyVenue.TabIndex = 1;
            this.rbHTMLbyVenue.TabStop = true;
            this.rbHTMLbyVenue.Text = "HTML by Venue";
            this.rbHTMLbyVenue.UseVisualStyleBackColor = true;
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
            // dpFromDate
            // 
            this.dpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpFromDate.Location = new System.Drawing.Point(48, 19);
            this.dpFromDate.Name = "dpFromDate";
            this.dpFromDate.ShowCheckBox = true;
            this.dpFromDate.Size = new System.Drawing.Size(116, 20);
            this.dpFromDate.TabIndex = 5;
            // 
            // dpToDate
            // 
            this.dpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpToDate.Location = new System.Drawing.Point(47, 45);
            this.dpToDate.Name = "dpToDate";
            this.dpToDate.ShowCheckBox = true;
            this.dpToDate.Size = new System.Drawing.Size(117, 20);
            this.dpToDate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "To:";
            // 
            // PerformanceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(956, 541);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dpToDate);
            this.Controls.Add(this.dpFromDate);
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
        private System.Windows.Forms.RadioButton rbHTMLbyVenue;
        private System.Windows.Forms.RadioButton rbPlainText;
        private System.Windows.Forms.RadioButton rbHTMLbyDate;
        private System.Windows.Forms.DateTimePicker dpFromDate;
        private System.Windows.Forms.DateTimePicker dpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}