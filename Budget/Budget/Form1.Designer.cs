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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyTransactionTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridGroupings = new System.Windows.Forms.DataGridView();
            this.viewBudgetGroupingsInOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.viewBudgetGroupingsInOrderTableAdapter = new Budget.MainDataSetTableAdapters.ViewBudgetGroupingsInOrderTableAdapter();
            this.btnRefreshGroupingChange = new System.Windows.Forms.Button();
            this.ColumnChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetGroupingsInOrderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Budget.ReportMonthly.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(182, 163);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1213, 502);
            this.reportViewer1.TabIndex = 0;
            // 
            // gridMain
            // 
            this.gridMain.AllowUserToAddRows = false;
            this.gridMain.AllowUserToDeleteRows = false;
            this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMain.Location = new System.Drawing.Point(182, 27);
            this.gridMain.Name = "gridMain";
            this.gridMain.ReadOnly = true;
            this.gridMain.Size = new System.Drawing.Size(1213, 130);
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
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyTransactionTypesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // applyTransactionTypesToolStripMenuItem
            // 
            this.applyTransactionTypesToolStripMenuItem.Name = "applyTransactionTypesToolStripMenuItem";
            this.applyTransactionTypesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.applyTransactionTypesToolStripMenuItem.Text = "Apply Transaction Types";
            this.applyTransactionTypesToolStripMenuItem.Click += new System.EventHandler(this.applyTransactionTypesToolStripMenuItem_Click);
            // 
            // gridGroupings
            // 
            this.gridGroupings.AllowUserToAddRows = false;
            this.gridGroupings.AllowUserToDeleteRows = false;
            this.gridGroupings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridGroupings.AutoGenerateColumns = false;
            this.gridGroupings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGroupings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnChecked,
            this.groupingDataGridViewTextBoxColumn});
            this.gridGroupings.DataSource = this.viewBudgetGroupingsInOrderBindingSource;
            this.gridGroupings.Location = new System.Drawing.Point(12, 56);
            this.gridGroupings.Name = "gridGroupings";
            this.gridGroupings.RowHeadersVisible = false;
            this.gridGroupings.Size = new System.Drawing.Size(164, 609);
            this.gridGroupings.TabIndex = 3;
            this.gridGroupings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridGroupings_CellContentClick);
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
            // btnRefreshGroupingChange
            // 
            this.btnRefreshGroupingChange.Enabled = false;
            this.btnRefreshGroupingChange.Location = new System.Drawing.Point(12, 27);
            this.btnRefreshGroupingChange.Name = "btnRefreshGroupingChange";
            this.btnRefreshGroupingChange.Size = new System.Drawing.Size(164, 23);
            this.btnRefreshGroupingChange.TabIndex = 4;
            this.btnRefreshGroupingChange.Text = "Refresh After Grouping Change";
            this.btnRefreshGroupingChange.UseVisualStyleBackColor = true;
            this.btnRefreshGroupingChange.Click += new System.EventHandler(this.btnRefreshGroupingChange_Click);
            // 
            // ColumnChecked
            // 
            this.ColumnChecked.HeaderText = "";
            this.ColumnChecked.Name = "ColumnChecked";
            this.ColumnChecked.Width = 35;
            // 
            // groupingDataGridViewTextBoxColumn
            // 
            this.groupingDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.groupingDataGridViewTextBoxColumn.DataPropertyName = "Grouping";
            this.groupingDataGridViewTextBoxColumn.HeaderText = "Grouping";
            this.groupingDataGridViewTextBoxColumn.Name = "groupingDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 665);
            this.Controls.Add(this.btnRefreshGroupingChange);
            this.Controls.Add(this.gridGroupings);
            this.Controls.Add(this.gridMain);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetGroupingsInOrderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
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
        private System.Windows.Forms.DataGridView gridGroupings;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource viewBudgetGroupingsInOrderBindingSource;
        private MainDataSetTableAdapters.ViewBudgetGroupingsInOrderTableAdapter viewBudgetGroupingsInOrderTableAdapter;
        private System.Windows.Forms.Button btnRefreshGroupingChange;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupingDataGridViewTextBoxColumn;
    }
}

