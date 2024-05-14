namespace Budget
{
    partial class SourceFileForm
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
            this.btnOpenSourceFile = new System.Windows.Forms.Button();
            this.comboSrcFileFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.budgetCtrl = new Budget.BudgetEditingGridCtrl();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenSourceFile
            // 
            this.btnOpenSourceFile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenSourceFile.Name = "btnOpenSourceFile";
            this.btnOpenSourceFile.Size = new System.Drawing.Size(107, 23);
            this.btnOpenSourceFile.TabIndex = 0;
            this.btnOpenSourceFile.Text = "Open Source File";
            this.btnOpenSourceFile.UseVisualStyleBackColor = true;
            this.btnOpenSourceFile.Click += new System.EventHandler(this.btnOpenSourceFile_Click);
            // 
            // comboSrcFileFormat
            // 
            this.comboSrcFileFormat.FormattingEnabled = true;
            this.comboSrcFileFormat.Location = new System.Drawing.Point(183, 14);
            this.comboSrcFileFormat.Name = "comboSrcFileFormat";
            this.comboSrcFileFormat.Size = new System.Drawing.Size(257, 21);
            this.comboSrcFileFormat.TabIndex = 1;
            this.comboSrcFileFormat.ValueMember = "FormatCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Format:";
            // 
            // budgetCtrl
            // 
            this.budgetCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.budgetCtrl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.budgetCtrl.Location = new System.Drawing.Point(12, 41);
            this.budgetCtrl.Name = "budgetCtrl";
            this.budgetCtrl.Size = new System.Drawing.Size(776, 397);
            this.budgetCtrl.TabIndex = 3;
            // 
            // comboAccount
            // 
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Location = new System.Drawing.Point(514, 14);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(254, 21);
            this.comboAccount.TabIndex = 4;
            this.comboAccount.ValueMember = "AccountID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(458, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Account:";
            // 
            // SourceFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboAccount);
            this.Controls.Add(this.budgetCtrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboSrcFileFormat);
            this.Controls.Add(this.btnOpenSourceFile);
            this.Name = "SourceFileForm";
            this.Text = "SourceFileForm";
            this.Load += new System.EventHandler(this.SourceFileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSourceFile;
        private System.Windows.Forms.ComboBox comboSrcFileFormat;
        private System.Windows.Forms.Label label1;
        private BudgetEditingGridCtrl budgetCtrl;
        private System.Windows.Forms.ComboBox comboAccount;
        private System.Windows.Forms.Label label2;
    }
}