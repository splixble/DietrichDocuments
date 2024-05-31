namespace Budget
{
    partial class MonthGroupingForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.mainDataSet = new Budget.MainDataSet();
            this.viewBudgetWithMonthlyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewBudgetWithMonthlyTableAdapter = new Budget.MainDataSetTableAdapters.ViewBudgetWithMonthlyTableAdapter();
            this.trDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountNormalizedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetWithMonthlyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trDateDataGridViewTextBoxColumn,
            this.descripDataGridViewTextBoxColumn,
            this.TrCode,
            this.amountNormalizedDataGridViewTextBoxColumn,
            this.trTypeDataGridViewTextBoxColumn,
            this.accountDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.viewBudgetWithMonthlyBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(931, 603);
            this.dataGridView1.TabIndex = 0;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // viewBudgetWithMonthlyBindingSource
            // 
            this.viewBudgetWithMonthlyBindingSource.DataMember = "ViewBudgetWithMonthly";
            this.viewBudgetWithMonthlyBindingSource.DataSource = this.mainDataSet;
            // 
            // viewBudgetWithMonthlyTableAdapter
            // 
            this.viewBudgetWithMonthlyTableAdapter.ClearBeforeFill = true;
            // 
            // trDateDataGridViewTextBoxColumn
            // 
            this.trDateDataGridViewTextBoxColumn.DataPropertyName = "TrDate";
            this.trDateDataGridViewTextBoxColumn.HeaderText = "TrDate";
            this.trDateDataGridViewTextBoxColumn.Name = "trDateDataGridViewTextBoxColumn";
            this.trDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripDataGridViewTextBoxColumn
            // 
            this.descripDataGridViewTextBoxColumn.DataPropertyName = "Descrip";
            this.descripDataGridViewTextBoxColumn.HeaderText = "Descrip";
            this.descripDataGridViewTextBoxColumn.Name = "descripDataGridViewTextBoxColumn";
            this.descripDataGridViewTextBoxColumn.ReadOnly = true;
            this.descripDataGridViewTextBoxColumn.Width = 300;
            // 
            // TrCode
            // 
            this.TrCode.DataPropertyName = "TrCode";
            this.TrCode.HeaderText = "TrCode";
            this.TrCode.Name = "TrCode";
            this.TrCode.ReadOnly = true;
            // 
            // amountNormalizedDataGridViewTextBoxColumn
            // 
            this.amountNormalizedDataGridViewTextBoxColumn.DataPropertyName = "AmountNormalized";
            this.amountNormalizedDataGridViewTextBoxColumn.HeaderText = "AmountNormalized";
            this.amountNormalizedDataGridViewTextBoxColumn.Name = "amountNormalizedDataGridViewTextBoxColumn";
            this.amountNormalizedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // trTypeDataGridViewTextBoxColumn
            // 
            this.trTypeDataGridViewTextBoxColumn.DataPropertyName = "TrType";
            this.trTypeDataGridViewTextBoxColumn.HeaderText = "TrType";
            this.trTypeDataGridViewTextBoxColumn.Name = "trTypeDataGridViewTextBoxColumn";
            this.trTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // accountDataGridViewTextBoxColumn
            // 
            this.accountDataGridViewTextBoxColumn.DataPropertyName = "Account";
            this.accountDataGridViewTextBoxColumn.HeaderText = "Account";
            this.accountDataGridViewTextBoxColumn.Name = "accountDataGridViewTextBoxColumn";
            this.accountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MonthGroupingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 603);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MonthGroupingForm";
            this.Text = "MonthGroupingForm";
            this.Load += new System.EventHandler(this.MonthGroupingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBudgetWithMonthlyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource viewBudgetWithMonthlyBindingSource;
        private MainDataSetTableAdapters.ViewBudgetWithMonthlyTableAdapter viewBudgetWithMonthlyTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn trDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountNormalizedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountDataGridViewTextBoxColumn;
    }
}