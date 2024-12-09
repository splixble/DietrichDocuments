namespace Budget
{
    partial class SharePriceEditingGridCtrl
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
            this.components = new System.ComponentModel.Container();
            this.grid = new System.Windows.Forms.DataGridView();
            this.fundDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sPDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pricePerShareDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sharePriceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.sharePriceTableAdapter = new Budget.MainDataSetTableAdapters.SharePriceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharePriceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fundDataGridViewTextBoxColumn,
            this.sPDateDataGridViewTextBoxColumn,
            this.pricePerShareDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.grid.DataSource = this.sharePriceBindingSource;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(484, 300);
            this.grid.TabIndex = 0;
            this.grid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grid_RowsAdded);
            // 
            // fundDataGridViewTextBoxColumn
            // 
            this.fundDataGridViewTextBoxColumn.DataPropertyName = "Fund";
            this.fundDataGridViewTextBoxColumn.HeaderText = "Fund";
            this.fundDataGridViewTextBoxColumn.Name = "fundDataGridViewTextBoxColumn";
            this.fundDataGridViewTextBoxColumn.Width = 40;
            // 
            // sPDateDataGridViewTextBoxColumn
            // 
            this.sPDateDataGridViewTextBoxColumn.DataPropertyName = "SPDate";
            this.sPDateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.sPDateDataGridViewTextBoxColumn.Name = "sPDateDataGridViewTextBoxColumn";
            this.sPDateDataGridViewTextBoxColumn.Width = 60;
            // 
            // pricePerShareDataGridViewTextBoxColumn
            // 
            this.pricePerShareDataGridViewTextBoxColumn.DataPropertyName = "PricePerShare";
            this.pricePerShareDataGridViewTextBoxColumn.HeaderText = "Price";
            this.pricePerShareDataGridViewTextBoxColumn.Name = "pricePerShareDataGridViewTextBoxColumn";
            this.pricePerShareDataGridViewTextBoxColumn.Width = 60;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            // 
            // sharePriceBindingSource
            // 
            this.sharePriceBindingSource.DataMember = "SharePrice";
            this.sharePriceBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sharePriceTableAdapter
            // 
            this.sharePriceTableAdapter.ClearBeforeFill = true;
            // 
            // SharePriceEditingGridCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid);
            this.Name = "SharePriceEditingGridCtrl";
            this.Size = new System.Drawing.Size(484, 300);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharePriceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.BindingSource sharePriceBindingSource;
        private MainDataSet mainDataSet;
        private MainDataSetTableAdapters.SharePriceTableAdapter sharePriceTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn fundDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sPDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pricePerShareDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}
