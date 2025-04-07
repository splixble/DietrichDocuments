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
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnSaveBudgetItems = new WinformsLib.SaveButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.customFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amazonOrderFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amazonDigitalItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yahooHoldingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboFileFormat = new System.Windows.Forms.ComboBox();
            this.chBoxManualEntry = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnImportManualText = new System.Windows.Forms.Button();
            this.tbFileText = new System.Windows.Forms.TextBox();
            this.sourceFileChecklistCtrl1 = new Budget.SourceFileChecklistCtrl();
            this.sharePriceCtrl = new Budget.SharePriceEditingGridCtrl();
            this.transacCtrl = new Budget.BudgetEditingGridCtrl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenSourceFile
            // 
            this.btnOpenSourceFile.Enabled = false;
            this.btnOpenSourceFile.Location = new System.Drawing.Point(675, 3);
            this.btnOpenSourceFile.Name = "btnOpenSourceFile";
            this.btnOpenSourceFile.Size = new System.Drawing.Size(107, 23);
            this.btnOpenSourceFile.TabIndex = 0;
            this.btnOpenSourceFile.Text = "Open Source File";
            this.btnOpenSourceFile.UseVisualStyleBackColor = true;
            this.btnOpenSourceFile.Click += new System.EventHandler(this.btnOpenSourceFile_Click);
            // 
            // comboAccount
            // 
            this.comboAccount.DisplayMember = "AccountID";
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Location = new System.Drawing.Point(193, 4);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(206, 21);
            this.comboAccount.TabIndex = 4;
            this.comboAccount.ValueMember = "AccountID";
            this.comboAccount.SelectionChangeCommitted += new System.EventHandler(this.comboAccount_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Account:";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(131, 493);
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
            this.btnSaveBudgetItems.Location = new System.Drawing.Point(12, 493);
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
            this.menuStrip1.Size = new System.Drawing.Size(1247, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // customFilesToolStripMenuItem
            // 
            this.customFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amazonOrderFileToolStripMenuItem,
            this.amazonDigitalItemsToolStripMenuItem,
            this.yahooHoldingsToolStripMenuItem});
            this.customFilesToolStripMenuItem.Name = "customFilesToolStripMenuItem";
            this.customFilesToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.customFilesToolStripMenuItem.Text = "Custom Files";
            // 
            // amazonOrderFileToolStripMenuItem
            // 
            this.amazonOrderFileToolStripMenuItem.Name = "amazonOrderFileToolStripMenuItem";
            this.amazonOrderFileToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.amazonOrderFileToolStripMenuItem.Text = "Amazon Retail Orders";
            this.amazonOrderFileToolStripMenuItem.Click += new System.EventHandler(this.amazonOrderFileToolStripMenuItem_Click);
            // 
            // amazonDigitalItemsToolStripMenuItem
            // 
            this.amazonDigitalItemsToolStripMenuItem.Name = "amazonDigitalItemsToolStripMenuItem";
            this.amazonDigitalItemsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.amazonDigitalItemsToolStripMenuItem.Text = "Amazon Digital Items";
            this.amazonDigitalItemsToolStripMenuItem.Click += new System.EventHandler(this.amazonDigitalItemsToolStripMenuItem_Click);
            // 
            // yahooHoldingsToolStripMenuItem
            // 
            this.yahooHoldingsToolStripMenuItem.Name = "yahooHoldingsToolStripMenuItem";
            this.yahooHoldingsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.yahooHoldingsToolStripMenuItem.Text = "Yahoo Holdings";
            this.yahooHoldingsToolStripMenuItem.Click += new System.EventHandler(this.yahooHoldingsToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sharePriceCtrl);
            this.splitContainer1.Panel2.Controls.Add(this.transacCtrl);
            this.splitContainer1.Size = new System.Drawing.Size(1223, 460);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1223, 258);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sourceFileChecklistCtrl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1215, 232);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Checklist";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboFileFormat);
            this.tabPage2.Controls.Add(this.chBoxManualEntry);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.lblFilePath);
            this.tabPage2.Controls.Add(this.comboAccount);
            this.tabPage2.Controls.Add(this.btnImportManualText);
            this.tabPage2.Controls.Add(this.btnOpenSourceFile);
            this.tabPage2.Controls.Add(this.tbFileText);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1215, 232);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Historic";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboFileFormat
            // 
            this.comboFileFormat.DisplayMember = "AccountID";
            this.comboFileFormat.FormattingEnabled = true;
            this.comboFileFormat.Location = new System.Drawing.Point(483, 4);
            this.comboFileFormat.Name = "comboFileFormat";
            this.comboFileFormat.Size = new System.Drawing.Size(171, 21);
            this.comboFileFormat.TabIndex = 10;
            this.comboFileFormat.ValueMember = "AccountID";
            // 
            // chBoxManualEntry
            // 
            this.chBoxManualEntry.AutoSize = true;
            this.chBoxManualEntry.Location = new System.Drawing.Point(6, 6);
            this.chBoxManualEntry.Name = "chBoxManualEntry";
            this.chBoxManualEntry.Size = new System.Drawing.Size(112, 17);
            this.chBoxManualEntry.TabIndex = 7;
            this.chBoxManualEntry.Text = "Manual Text Entry";
            this.chBoxManualEntry.UseVisualStyleBackColor = true;
            this.chBoxManualEntry.CheckedChanged += new System.EventHandler(this.chBoxManualEntry_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(416, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "File Format:";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(788, 9);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(26, 13);
            this.lblFilePath.TabIndex = 9;
            this.lblFilePath.Text = "File:";
            // 
            // btnImportManualText
            // 
            this.btnImportManualText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportManualText.Location = new System.Drawing.Point(6, 206);
            this.btnImportManualText.Name = "btnImportManualText";
            this.btnImportManualText.Size = new System.Drawing.Size(107, 23);
            this.btnImportManualText.TabIndex = 8;
            this.btnImportManualText.Text = "Import Manual Text";
            this.btnImportManualText.UseVisualStyleBackColor = true;
            this.btnImportManualText.Click += new System.EventHandler(this.btnImportManualText_Click);
            // 
            // tbFileText
            // 
            this.tbFileText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileText.Location = new System.Drawing.Point(6, 31);
            this.tbFileText.Multiline = true;
            this.tbFileText.Name = "tbFileText";
            this.tbFileText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbFileText.Size = new System.Drawing.Size(1203, 169);
            this.tbFileText.TabIndex = 6;
            // 
            // sourceFileChecklistCtrl1
            // 
            this.sourceFileChecklistCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceFileChecklistCtrl1.Location = new System.Drawing.Point(3, 3);
            this.sourceFileChecklistCtrl1.Name = "sourceFileChecklistCtrl1";
            this.sourceFileChecklistCtrl1.Size = new System.Drawing.Size(1209, 226);
            this.sourceFileChecklistCtrl1.TabIndex = 0;
            // 
            // sharePriceCtrl
            // 
            this.sharePriceCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sharePriceCtrl.Location = new System.Drawing.Point(0, 0);
            this.sharePriceCtrl.Name = "sharePriceCtrl";
            this.sharePriceCtrl.Size = new System.Drawing.Size(1223, 198);
            this.sharePriceCtrl.TabIndex = 4;
            // 
            // budgetCtrl
            // 
            this.transacCtrl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.transacCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transacCtrl.Location = new System.Drawing.Point(0, 0);
            this.transacCtrl.Name = "budgetCtrl";
            this.transacCtrl.Size = new System.Drawing.Size(1223, 198);
            this.transacCtrl.TabIndex = 3;
            // 
            // SourceFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 528);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSaveBudgetItems);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SourceFileForm";
            this.Text = "Import Source File";
            this.Load += new System.EventHandler(this.SourceFileForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSourceFile;
        private BudgetEditingGridCtrl transacCtrl;
        private System.Windows.Forms.ComboBox comboAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private WinformsLib.SaveButton btnSaveBudgetItems;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem customFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amazonOrderFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amazonDigitalItemsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbFileText;
        private System.Windows.Forms.CheckBox chBoxManualEntry;
        private System.Windows.Forms.Button btnImportManualText;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.ComboBox comboFileFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SourceFileChecklistCtrl sourceFileChecklistCtrl1;
        private System.Windows.Forms.ToolStripMenuItem yahooHoldingsToolStripMenuItem;
        private SharePriceEditingGridCtrl sharePriceCtrl;
    }
}