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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.customFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amazonOrderFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenSourceFile
            // 
            this.btnOpenSourceFile.Location = new System.Drawing.Point(12, 27);
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
            this.budgetCtrl.Location = new System.Drawing.Point(12, 56);
            this.budgetCtrl.Name = "budgetCtrl";
            this.budgetCtrl.Size = new System.Drawing.Size(776, 353);
            this.budgetCtrl.TabIndex = 3;
            // 
            // comboAccount
            // 
            this.comboAccount.DisplayMember = "AccountID";
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Location = new System.Drawing.Point(196, 29);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(254, 21);
            this.comboAccount.TabIndex = 4;
            this.comboAccount.ValueMember = "AccountID";
            this.comboAccount.SelectionChangeCommitted += new System.EventHandler(this.comboAccount_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 32);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customFilesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // customFilesToolStripMenuItem
            // 
            this.customFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amazonOrderFileToolStripMenuItem});
            this.customFilesToolStripMenuItem.Name = "customFilesToolStripMenuItem";
            this.customFilesToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.customFilesToolStripMenuItem.Text = "Custom Files";
            // 
            // amazonOrderFileToolStripMenuItem
            // 
            this.amazonOrderFileToolStripMenuItem.Name = "amazonOrderFileToolStripMenuItem";
            this.amazonOrderFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.amazonOrderFileToolStripMenuItem.Text = "Amazon Order File";
            this.amazonOrderFileToolStripMenuItem.Click += new System.EventHandler(this.amazonOrderFileToolStripMenuItem_Click);
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
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SourceFileForm";
            this.Text = "Import Source File";
            this.Load += new System.EventHandler(this.SourceFileForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem customFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amazonOrderFileToolStripMenuItem;
    }
}