namespace Budget
{
    partial class TransacListSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelAccountOwner = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboAccountOwner = new System.Windows.Forms.ComboBox();
            this.panelAccountType = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAccountType = new System.Windows.Forms.ComboBox();
            this.panelAdjustForRefunds = new System.Windows.Forms.Panel();
            this.chBoxRefunds = new System.Windows.Forms.CheckBox();
            this.panelDates = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboToMonth = new Budget.MonthComboBox();
            this.comboFromMonth = new Budget.MonthComboBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelAccountOwner.SuspendLayout();
            this.panelAccountType.SuspendLayout();
            this.panelAdjustForRefunds.SuspendLayout();
            this.panelDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panelAccountOwner);
            this.flowLayoutPanel1.Controls.Add(this.panelAccountType);
            this.flowLayoutPanel1.Controls.Add(this.panelAdjustForRefunds);
            this.flowLayoutPanel1.Controls.Add(this.panelDates);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(196, 121);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panelAccountOwner
            // 
            this.panelAccountOwner.Controls.Add(this.label1);
            this.panelAccountOwner.Controls.Add(this.comboAccountOwner);
            this.panelAccountOwner.Location = new System.Drawing.Point(3, 3);
            this.panelAccountOwner.Name = "panelAccountOwner";
            this.panelAccountOwner.Size = new System.Drawing.Size(189, 23);
            this.panelAccountOwner.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Accounts of:";
            // 
            // comboAccountOwner
            // 
            this.comboAccountOwner.FormattingEnabled = true;
            this.comboAccountOwner.Location = new System.Drawing.Point(75, 1);
            this.comboAccountOwner.Name = "comboAccountOwner";
            this.comboAccountOwner.Size = new System.Drawing.Size(114, 21);
            this.comboAccountOwner.TabIndex = 11;
            this.comboAccountOwner.SelectionChangeCommitted += new System.EventHandler(this.CtrlSelectionChanged);
            // 
            // panelAccountType
            // 
            this.panelAccountType.Controls.Add(this.label2);
            this.panelAccountType.Controls.Add(this.comboAccountType);
            this.panelAccountType.Location = new System.Drawing.Point(2, 31);
            this.panelAccountType.Margin = new System.Windows.Forms.Padding(2);
            this.panelAccountType.Name = "panelAccountType";
            this.panelAccountType.Size = new System.Drawing.Size(189, 23);
            this.panelAccountType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Account type:";
            // 
            // comboAccountType
            // 
            this.comboAccountType.FormattingEnabled = true;
            this.comboAccountType.Location = new System.Drawing.Point(75, 1);
            this.comboAccountType.Name = "comboAccountType";
            this.comboAccountType.Size = new System.Drawing.Size(114, 21);
            this.comboAccountType.TabIndex = 13;
            this.comboAccountType.SelectionChangeCommitted += new System.EventHandler(this.CtrlSelectionChanged);
            // 
            // panelAdjustForRefunds
            // 
            this.panelAdjustForRefunds.Controls.Add(this.chBoxRefunds);
            this.panelAdjustForRefunds.Location = new System.Drawing.Point(3, 59);
            this.panelAdjustForRefunds.Name = "panelAdjustForRefunds";
            this.panelAdjustForRefunds.Size = new System.Drawing.Size(189, 23);
            this.panelAdjustForRefunds.TabIndex = 5;
            // 
            // chBoxRefunds
            // 
            this.chBoxRefunds.AutoSize = true;
            this.chBoxRefunds.Checked = true;
            this.chBoxRefunds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxRefunds.Location = new System.Drawing.Point(75, 3);
            this.chBoxRefunds.Name = "chBoxRefunds";
            this.chBoxRefunds.Size = new System.Drawing.Size(113, 17);
            this.chBoxRefunds.TabIndex = 16;
            this.chBoxRefunds.Text = "Adjust for Refunds";
            this.chBoxRefunds.UseVisualStyleBackColor = true;
            this.chBoxRefunds.CheckedChanged += new System.EventHandler(this.CtrlSelectionChanged);
            // 
            // panelDates
            // 
            this.panelDates.Controls.Add(this.label5);
            this.panelDates.Controls.Add(this.comboToMonth);
            this.panelDates.Controls.Add(this.label4);
            this.panelDates.Controls.Add(this.comboFromMonth);
            this.panelDates.Location = new System.Drawing.Point(3, 88);
            this.panelDates.Name = "panelDates";
            this.panelDates.Size = new System.Drawing.Size(189, 23);
            this.panelDates.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "From";
            // 
            // comboToMonth
            // 
            this.comboToMonth.FormattingEnabled = true;
            this.comboToMonth.Location = new System.Drawing.Point(119, 1);
            this.comboToMonth.Name = "comboToMonth";
            this.comboToMonth.Size = new System.Drawing.Size(63, 21);
            this.comboToMonth.TabIndex = 17;
            this.comboToMonth.SelectionChangeCommitted += new System.EventHandler(this.CtrlSelectionChanged);
            // 
            // comboFromMonth
            // 
            this.comboFromMonth.FormattingEnabled = true;
            this.comboFromMonth.Location = new System.Drawing.Point(36, 1);
            this.comboFromMonth.Name = "comboFromMonth";
            this.comboFromMonth.Size = new System.Drawing.Size(63, 21);
            this.comboFromMonth.TabIndex = 15;
            this.comboFromMonth.SelectionChangeCommitted += new System.EventHandler(this.CtrlSelectionChanged);
            // 
            // TransacListSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "TransacListSelector";
            this.Size = new System.Drawing.Size(196, 121);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelAccountOwner.ResumeLayout(false);
            this.panelAccountOwner.PerformLayout();
            this.panelAccountType.ResumeLayout(false);
            this.panelAccountType.PerformLayout();
            this.panelAdjustForRefunds.ResumeLayout(false);
            this.panelAdjustForRefunds.PerformLayout();
            this.panelDates.ResumeLayout(false);
            this.panelDates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelAccountOwner;
        private System.Windows.Forms.Panel panelAccountType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboAccountOwner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAccountType;
        private System.Windows.Forms.Panel panelAdjustForRefunds;
        private System.Windows.Forms.Panel panelDates;
        private System.Windows.Forms.CheckBox chBoxRefunds;
        private System.Windows.Forms.Label label5;
        private MonthComboBox comboToMonth;
        private System.Windows.Forms.Label label4;
        private MonthComboBox comboFromMonth;
    }
}
