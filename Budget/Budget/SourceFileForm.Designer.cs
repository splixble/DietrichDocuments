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
            this.budgetCtrl = new Budget.BudgetEditingGridCtrl();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSaveBudgetItems = new System.Windows.Forms.Button();
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
            // budgetCtrl
            // 
            this.budgetCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.budgetCtrl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.budgetCtrl.Location = new System.Drawing.Point(12, 41);
            this.budgetCtrl.Name = "budgetCtrl";
            this.budgetCtrl.Size = new System.Drawing.Size(776, 368);
            this.budgetCtrl.TabIndex = 3;
            // 
            // comboAccount
            // 
            this.comboAccount.DisplayMember = "AccountID";
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Location = new System.Drawing.Point(196, 14);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(254, 21);
            this.comboAccount.TabIndex = 4;
            this.comboAccount.ValueMember = "AccountID";
            this.comboAccount.SelectionChangeCommitted += new System.EventHandler(this.comboAccount_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Account:";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(131, 415);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Cancel Changes";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnSaveBudgetItems
            // 
            this.btnSaveBudgetItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveBudgetItems.Enabled = false;
            this.btnSaveBudgetItems.Location = new System.Drawing.Point(12, 415);
            this.btnSaveBudgetItems.Name = "btnSaveBudgetItems";
            this.btnSaveBudgetItems.Size = new System.Drawing.Size(113, 23);
            this.btnSaveBudgetItems.TabIndex = 11;
            this.btnSaveBudgetItems.Text = "Save Changes";
            this.btnSaveBudgetItems.UseVisualStyleBackColor = true;
            this.btnSaveBudgetItems.Click += new System.EventHandler(this.btnSaveBudgetItems_Click);
            // 
            // SourceFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSaveBudgetItems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboAccount);
            this.Controls.Add(this.budgetCtrl);
            this.Controls.Add(this.btnOpenSourceFile);
            this.Name = "SourceFileForm";
            this.Text = "Import Source File";
            this.Load += new System.EventHandler(this.SourceFileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSourceFile;
        private BudgetEditingGridCtrl budgetCtrl;
        private System.Windows.Forms.ComboBox comboAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnSaveBudgetItems;
    }
}