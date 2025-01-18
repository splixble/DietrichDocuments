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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gridMain = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyTransactionTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGroupingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateAccountBalancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.investmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitConInner = new System.Windows.Forms.SplitContainer();
            this.splitContainerCharts = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tvGroupings = new System.Windows.Forms.TreeView();
            this.splitConOuter = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboAccountType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboAccountOwner = new System.Windows.Forms.ComboBox();
            this.viewBudgetGroupingsInOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.viewBudgetGroupingsInOrderTableAdapter = new Budget.MainDataSetTableAdapters.ViewBudgetGroupingsInOrderTableAdapter();
            this.comboToMonth = new Budget.MonthComboBox();
            this.comboFromMonth = new Budget.MonthComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConInner)).BeginInit();
            this.splitConInner.Panel1.SuspendLayout();
            this.splitConInner.Panel2.SuspendLayout();
            this.splitConInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCharts)).BeginInit();
            this.splitContainerCharts.Panel1.SuspendLayout();
            this.splitContainerCharts.Panel2.SuspendLayout();
            this.splitContainerCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitConOuter)).BeginInit();
            this.splitConOuter.Panel1.SuspendLayout();
            this.splitConOuter.Panel2.SuspendLayout();
            this.splitConOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetGroupingsInOrderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Budget.ReportMonthly.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(391, 459);
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
            this.gridMain.Size = new System.Drawing.Size(1175, 163);
            this.gridMain.TabIndex = 1;
            this.gridMain.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridMain_CellDoubleClick);
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
            this.loadSourceFileToolStripMenuItem,
            this.printPreviewToolStripMenuItem});
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
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyTransactionTypesToolStripMenuItem,
            this.editGroupingsToolStripMenuItem,
            this.calculateAccountBalancesToolStripMenuItem,
            this.investmentsToolStripMenuItem,
            this.accountsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // applyTransactionTypesToolStripMenuItem
            // 
            this.applyTransactionTypesToolStripMenuItem.Name = "applyTransactionTypesToolStripMenuItem";
            this.applyTransactionTypesToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.applyTransactionTypesToolStripMenuItem.Text = "Apply Grouping Patterns";
            this.applyTransactionTypesToolStripMenuItem.Click += new System.EventHandler(this.applyTransactionTypesToolStripMenuItem_Click);
            // 
            // editGroupingsToolStripMenuItem
            // 
            this.editGroupingsToolStripMenuItem.Name = "editGroupingsToolStripMenuItem";
            this.editGroupingsToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.editGroupingsToolStripMenuItem.Text = "Edit Groupings";
            this.editGroupingsToolStripMenuItem.Click += new System.EventHandler(this.editGroupingsToolStripMenuItem_Click);
            // 
            // calculateAccountBalancesToolStripMenuItem
            // 
            this.calculateAccountBalancesToolStripMenuItem.Name = "calculateAccountBalancesToolStripMenuItem";
            this.calculateAccountBalancesToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.calculateAccountBalancesToolStripMenuItem.Text = "Calculate Account Balances";
            this.calculateAccountBalancesToolStripMenuItem.Click += new System.EventHandler(this.calculateAccountBalancesToolStripMenuItem_Click);
            // 
            // investmentsToolStripMenuItem
            // 
            this.investmentsToolStripMenuItem.Name = "investmentsToolStripMenuItem";
            this.investmentsToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.investmentsToolStripMenuItem.Text = "Investments";
            this.investmentsToolStripMenuItem.Click += new System.EventHandler(this.investmentsToolStripMenuItem_Click);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.accountsToolStripMenuItem.Text = "Accounts";
            this.accountsToolStripMenuItem.Click += new System.EventHandler(this.accountsToolStripMenuItem_Click);
            // 
            // splitConInner
            // 
            this.splitConInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConInner.Location = new System.Drawing.Point(0, 0);
            this.splitConInner.Name = "splitConInner";
            this.splitConInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitConInner.Panel1
            // 
            this.splitConInner.Panel1.Controls.Add(this.gridMain);
            // 
            // splitConInner.Panel2
            // 
            this.splitConInner.Panel2.Controls.Add(this.splitContainerCharts);
            this.splitConInner.Size = new System.Drawing.Size(1175, 626);
            this.splitConInner.SplitterDistance = 163;
            this.splitConInner.TabIndex = 5;
            // 
            // splitContainerCharts
            // 
            this.splitContainerCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCharts.Location = new System.Drawing.Point(0, 0);
            this.splitContainerCharts.Name = "splitContainerCharts";
            // 
            // splitContainerCharts.Panel1
            // 
            this.splitContainerCharts.Panel1.Controls.Add(this.reportViewer1);
            this.splitContainerCharts.Panel1Collapsed = true;
            // 
            // splitContainerCharts.Panel2
            // 
            this.splitContainerCharts.Panel2.Controls.Add(this.label3);
            this.splitContainerCharts.Panel2.Controls.Add(this.chart1);
            this.splitContainerCharts.Size = new System.Drawing.Size(1175, 459);
            this.splitContainerCharts.SplitterDistance = 391;
            this.splitContainerCharts.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "All-New Chart Doodad, No Report Necessary";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(17, 50);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1145, 382);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tvGroupings
            // 
            this.tvGroupings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvGroupings.CheckBoxes = true;
            this.tvGroupings.Location = new System.Drawing.Point(0, 82);
            this.tvGroupings.Name = "tvGroupings";
            this.tvGroupings.Size = new System.Drawing.Size(192, 544);
            this.tvGroupings.TabIndex = 6;
            this.tvGroupings.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvGroupings_AfterCheck);
            // 
            // splitConOuter
            // 
            this.splitConOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitConOuter.Location = new System.Drawing.Point(12, 27);
            this.splitConOuter.Name = "splitConOuter";
            // 
            // splitConOuter.Panel1
            // 
            this.splitConOuter.Panel1.Controls.Add(this.label5);
            this.splitConOuter.Panel1.Controls.Add(this.comboToMonth);
            this.splitConOuter.Panel1.Controls.Add(this.label4);
            this.splitConOuter.Panel1.Controls.Add(this.comboFromMonth);
            this.splitConOuter.Panel1.Controls.Add(this.label2);
            this.splitConOuter.Panel1.Controls.Add(this.comboAccountType);
            this.splitConOuter.Panel1.Controls.Add(this.label1);
            this.splitConOuter.Panel1.Controls.Add(this.comboAccountOwner);
            this.splitConOuter.Panel1.Controls.Add(this.tvGroupings);
            // 
            // splitConOuter.Panel2
            // 
            this.splitConOuter.Panel2.Controls.Add(this.splitConInner);
            this.splitConOuter.Size = new System.Drawing.Size(1371, 626);
            this.splitConOuter.SplitterDistance = 192;
            this.splitConOuter.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "to";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Account type:";
            // 
            // comboAccountType
            // 
            this.comboAccountType.FormattingEnabled = true;
            this.comboAccountType.Location = new System.Drawing.Point(78, 27);
            this.comboAccountType.Name = "comboAccountType";
            this.comboAccountType.Size = new System.Drawing.Size(114, 21);
            this.comboAccountType.TabIndex = 9;
            this.comboAccountType.SelectionChangeCommitted += new System.EventHandler(this.comboAccountType_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Accounts of:";
            // 
            // comboAccountOwner
            // 
            this.comboAccountOwner.FormattingEnabled = true;
            this.comboAccountOwner.Location = new System.Drawing.Point(78, 0);
            this.comboAccountOwner.Name = "comboAccountOwner";
            this.comboAccountOwner.Size = new System.Drawing.Size(114, 21);
            this.comboAccountOwner.TabIndex = 7;
            this.comboAccountOwner.SelectionChangeCommitted += new System.EventHandler(this.comboAccountOwner_SelectionChangeCommitted);
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
            // comboToMonth
            // 
            this.comboToMonth.FormattingEnabled = true;
            this.comboToMonth.Location = new System.Drawing.Point(129, 55);
            this.comboToMonth.Name = "comboToMonth";
            this.comboToMonth.Size = new System.Drawing.Size(63, 21);
            this.comboToMonth.TabIndex = 13;
            this.comboToMonth.SelectionChangeCommitted += new System.EventHandler(this.comboToMonth_SelectionChangeCommitted);
            // 
            // comboFromMonth
            // 
            this.comboFromMonth.FormattingEnabled = true;
            this.comboFromMonth.Location = new System.Drawing.Point(46, 55);
            this.comboFromMonth.Name = "comboFromMonth";
            this.comboFromMonth.Size = new System.Drawing.Size(63, 21);
            this.comboFromMonth.TabIndex = 11;
            this.comboFromMonth.SelectionChangeCommitted += new System.EventHandler(this.comboFromMonth_SelectionChangeCommitted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1395, 665);
            this.Controls.Add(this.splitConOuter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitConInner.Panel1.ResumeLayout(false);
            this.splitConInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConInner)).EndInit();
            this.splitConInner.ResumeLayout(false);
            this.splitContainerCharts.Panel1.ResumeLayout(false);
            this.splitContainerCharts.Panel2.ResumeLayout(false);
            this.splitContainerCharts.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCharts)).EndInit();
            this.splitContainerCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.splitConOuter.Panel1.ResumeLayout(false);
            this.splitConOuter.Panel1.PerformLayout();
            this.splitConOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitConOuter)).EndInit();
            this.splitConOuter.ResumeLayout(false);
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
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource viewBudgetGroupingsInOrderBindingSource;
        private MainDataSetTableAdapters.ViewBudgetGroupingsInOrderTableAdapter viewBudgetGroupingsInOrderTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem editGroupingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSourceFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitConInner;
        private System.Windows.Forms.TreeView tvGroupings;
        private System.Windows.Forms.ToolStripMenuItem calculateAccountBalancesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitConOuter;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboAccountOwner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAccountType;
        private System.Windows.Forms.ToolStripMenuItem investmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label3;
        private MonthComboBox comboFromMonth;
        private System.Windows.Forms.Label label5;
        private MonthComboBox comboToMonth;
        private System.Windows.Forms.Label label4;
    }
}

