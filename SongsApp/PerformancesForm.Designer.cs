namespace Songs
{
    partial class PerformancesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformancesForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.venuesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AzureDataSet = new Songs.AzureDataSet();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.detailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performancesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.performancesTableAdapter = new Songs.AzureDataSetTableAdapters.performancesTableAdapter();
            this.venuesTableAdapter = new Songs.AzureDataSetTableAdapters.venuesTableAdapter();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.performanceDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VenueColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seriesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DidILead = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.venuesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AzureDataSet)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performancesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(801, 342);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grid1
            // 
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.AutoGenerateColumns = false;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.performanceDateDataGridViewTextBoxColumn,
            this.VenueColumn,
            this.commentDataGridViewTextBoxColumn,
            this.seriesDataGridViewTextBoxColumn,
            this.DidILead});
            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
            this.grid1.DataSource = this.performancesBindingSource;
            this.grid1.Location = new System.Drawing.Point(12, 12);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(864, 324);
            this.grid1.TabIndex = 2;
            this.grid1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid1_CellContentDoubleClick);
            this.grid1.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.grid1_CellContextMenuStripNeeded);
            // 
            // venuesBindingSource
            // 
            this.venuesBindingSource.DataMember = "venues";
            this.venuesBindingSource.DataSource = this.AzureDataSet;
            // 
            // AzureDataSet
            // 
            this.AzureDataSet.DataSetName = "AzureDataSet";
            this.AzureDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // detailToolStripMenuItem
            // 
            this.detailToolStripMenuItem.Name = "detailToolStripMenuItem";
            this.detailToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.detailToolStripMenuItem.Text = "&Detail";
            this.detailToolStripMenuItem.Click += new System.EventHandler(this.detailToolStripMenuItem_Click);
            // 
            // performancesBindingSource
            // 
            this.performancesBindingSource.DataMember = "performances";
            this.performancesBindingSource.DataSource = this.AzureDataSet;
            this.performancesBindingSource.CurrentChanged += new System.EventHandler(this.performancesBindingSource_CurrentChanged);
            this.performancesBindingSource.CurrentItemChanged += new System.EventHandler(this.performancesBindingSource_CurrentItemChanged);
            // 
            // performancesTableAdapter
            // 
            this.performancesTableAdapter.ClearBeforeFill = true;
            // 
            // venuesTableAdapter
            // 
            this.venuesTableAdapter.ClearBeforeFill = true;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 40;
            // 
            // performanceDateDataGridViewTextBoxColumn
            // 
            this.performanceDateDataGridViewTextBoxColumn.DataPropertyName = "PerformanceDate";
            this.performanceDateDataGridViewTextBoxColumn.HeaderText = "Performance Date";
            this.performanceDateDataGridViewTextBoxColumn.Name = "performanceDateDataGridViewTextBoxColumn";
            // 
            // VenueColumn
            // 
            this.VenueColumn.DataPropertyName = "Venue";
            this.VenueColumn.DataSource = this.venuesBindingSource;
            this.VenueColumn.DisplayMember = "Name";
            this.VenueColumn.HeaderText = "Venue";
            this.VenueColumn.Name = "VenueColumn";
            this.VenueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VenueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.VenueColumn.ValueMember = "ID";
            this.VenueColumn.Width = 220;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 240;
            // 
            // seriesDataGridViewTextBoxColumn
            // 
            this.seriesDataGridViewTextBoxColumn.DataPropertyName = "Series";
            this.seriesDataGridViewTextBoxColumn.HeaderText = "Series";
            this.seriesDataGridViewTextBoxColumn.Name = "seriesDataGridViewTextBoxColumn";
            // 
            // DidILead
            // 
            this.DidILead.DataPropertyName = "DidILead";
            this.DidILead.FalseValue = "0";
            this.DidILead.HeaderText = "Did I lead?";
            this.DidILead.Name = "DidILead";
            this.DidILead.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DidILead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DidILead.TrueValue = "1";
            this.DidILead.Width = 40;
            // 
            // PerformancesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(888, 377);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grid1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PerformancesForm";
            this.Text = "Performances Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PerformancesForm_FormClosing);
            this.Load += new System.EventHandler(this.PerformancesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.venuesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AzureDataSet)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.performancesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView grid1;
        private AzureDataSet AzureDataSet;
        private System.Windows.Forms.BindingSource performancesBindingSource;
        private Songs.AzureDataSetTableAdapters.performancesTableAdapter performancesTableAdapter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detailToolStripMenuItem;
        private System.Windows.Forms.BindingSource venuesBindingSource;
        private Songs.AzureDataSetTableAdapters.venuesTableAdapter venuesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn performanceDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn VenueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn seriesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DidILead;
    }
}