namespace Budget
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gridMain = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyTransactionTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGroupingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewBudgetGroupingsInOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.viewBudgetGroupingsInOrderTableAdapter = new Budget.MainDataSetTableAdapters.ViewBudgetGroupingsInOrderTableAdapter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvGroupings = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetGroupingsInOrderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Budget.ReportMonthly.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1194, 459);
            this.reportViewer1.TabIndex = 0;
            // 
            // gridMain
            // 
            this.gridMain.AllowUserToAddRows = false;
            this.gridMain.AllowUserToDeleteRows = false;
            this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMain.Location = new System.Drawing.Point(0, 0);
            this.gridMain.Name = "gridMain";
            this.gridMain.ReadOnly = true;
            this.gridMain.Size = new System.Drawing.Size(1194, 163);
            this.gridMain.TabIndex = 1;
            this.gridMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1395, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSourceFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSourceFileToolStripMenuItem
            // 
            this.loadSourceFileToolStripMenuItem.Name = "loadSourceFileToolStripMenuItem";
            this.loadSourceFileToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.loadSourceFileToolStripMenuItem.Text = "Load Source File";
            this.loadSourceFileToolStripMenuItem.Click += new System.EventHandler(this.loadSourceFileToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyTransactionTypesToolStripMenuItem,
            this.editGroupingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // applyTransactionTypesToolStripMenuItem
            // 
            this.applyTransactionTypesToolStripMenuItem.Name = "applyTransactionTypesToolStripMenuItem";
            this.applyTransactionTypesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.applyTransactionTypesToolStripMenuItem.Text = "Apply Grouping Patterns";
            this.applyTransactionTypesToolStripMenuItem.Click += new System.EventHandler(this.applyTransactionTypesToolStripMenuItem_Click);
            // 
            // editGroupingsToolStripMenuItem
            // 
            this.editGroupingsToolStripMenuItem.Name = "editGroupingsToolStripMenuItem";
            this.editGroupingsToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.editGroupingsToolStripMenuItem.Text = "Edit Groupings";
            this.editGroupingsToolStripMenuItem.Click += new System.EventHandler(this.editGroupingsToolStripMenuItem_Click);
            // 
            // viewBudgetGroupingsInOrderBindingSource
            // 
            this.viewBudgetGroupingsInOrderBindingSource.DataMember = "ViewBudgetGroupingsInOrder";
            this.viewBudgetGroupingsInOrderBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // viewBudgetGroupingsInOrderTableAdapter
            // 
            this.viewBudgetGroupingsInOrderTableAdapter.ClearBeforeFill = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(183, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(1194, 626);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 5;
            // 
            // tvGroupings
            // 
            this.tvGroupings.CheckBoxes = true;
            this.tvGroupings.Location = new System.Drawing.Point(12, 27);
            this.tvGroupings.Name = "tvGroupings";
            this.tvGroupings.Size = new System.Drawing.Size(165, 626);
            this.tvGroupings.TabIndex = 6;
            this.tvGroupings.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvGroupings_AfterCheck);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 665);
            this.Controls.Add(this.tvGroupings);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetGroupingsInOrderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DataGridView gridMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyTransactionTypesToolStripMenuItem;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource viewBudgetGroupingsInOrderBindingSource;
        private MainDataSetTableAdapters.ViewBudgetGroupingsInOrderTableAdapter viewBudgetGroupingsInOrderTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem editGroupingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSourceFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvGroupings;
    }
}

