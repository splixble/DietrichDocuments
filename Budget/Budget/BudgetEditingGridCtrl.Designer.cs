namespace Budget
{
    partial class BudgetEditingGridCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.budgetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.budgetTableAdapter = new Budget.MainDataSetTableAdapters.BudgetTableAdapter();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descrip2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ignoreDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // budgetBindingSource
            // 
            this.budgetBindingSource.DataMember = "Budget";
            this.budgetBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // budgetTableAdapter
            // 
            this.budgetTableAdapter.ClearBeforeFill = true;
            // 
            // grid1
            // 
            this.grid1.AllowUserToAddRows = false;
            this.grid1.AllowUserToDeleteRows = false;
            this.grid1.AutoGenerateColumns = false;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.trDateDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn,
            this.descripDataGridViewTextBoxColumn,
            this.Descrip2Column,
            this.trCodeDataGridViewTextBoxColumn,
            this.trTypeDataGridViewTextBoxColumn,
            this.accountDataGridViewTextBoxColumn,
            this.Balance,
            this.CommentColumn,
            this.ignoreDataGridViewCheckBoxColumn});
            this.grid1.DataSource = this.budgetBindingSource;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid1.RowHeadersWidth = 50;
            this.grid1.Size = new System.Drawing.Size(1395, 479);
            this.grid1.TabIndex = 1;
            this.grid1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gridBudgetItems_RowsAdded);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 40;
            // 
            // trDateDataGridViewTextBoxColumn
            // 
            this.trDateDataGridViewTextBoxColumn.DataPropertyName = "TrDate";
            this.trDateDataGridViewTextBoxColumn.HeaderText = "TrDate";
            this.trDateDataGridViewTextBoxColumn.Name = "trDateDataGridViewTextBoxColumn";
            this.trDateDataGridViewTextBoxColumn.Width = 80;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            this.amountDataGridViewTextBoxColumn.HeaderText = "Amount";
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.Width = 90;
            // 
            // descripDataGridViewTextBoxColumn
            // 
            this.descripDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descripDataGridViewTextBoxColumn.DataPropertyName = "Descrip";
            this.descripDataGridViewTextBoxColumn.HeaderText = "Descrip";
            this.descripDataGridViewTextBoxColumn.Name = "descripDataGridViewTextBoxColumn";
            // 
            // Descrip2Column
            // 
            this.Descrip2Column.DataPropertyName = "Descrip2";
            this.Descrip2Column.HeaderText = "Descrip2";
            this.Descrip2Column.Name = "Descrip2Column";
            // 
            // trCodeDataGridViewTextBoxColumn
            // 
            this.trCodeDataGridViewTextBoxColumn.DataPropertyName = "TrCode";
            this.trCodeDataGridViewTextBoxColumn.HeaderText = "TrCode";
            this.trCodeDataGridViewTextBoxColumn.Name = "trCodeDataGridViewTextBoxColumn";
            this.trCodeDataGridViewTextBoxColumn.Width = 200;
            // 
            // trTypeDataGridViewTextBoxColumn
            // 
            this.trTypeDataGridViewTextBoxColumn.DataPropertyName = "TrType";
            this.trTypeDataGridViewTextBoxColumn.HeaderText = "TrType";
            this.trTypeDataGridViewTextBoxColumn.Name = "trTypeDataGridViewTextBoxColumn";
            this.trTypeDataGridViewTextBoxColumn.Width = 50;
            // 
            // accountDataGridViewTextBoxColumn
            // 
            this.accountDataGridViewTextBoxColumn.DataPropertyName = "Account";
            this.accountDataGridViewTextBoxColumn.HeaderText = "Account";
            this.accountDataGridViewTextBoxColumn.Name = "accountDataGridViewTextBoxColumn";
            this.accountDataGridViewTextBoxColumn.Width = 40;
            // 
            // Balance
            // 
            this.Balance.DataPropertyName = "Balance";
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            // 
            // CommentColumn
            // 
            this.CommentColumn.DataPropertyName = "Comment";
            this.CommentColumn.HeaderText = "Comment";
            this.CommentColumn.Name = "CommentColumn";
            this.CommentColumn.Width = 230;
            // 
            // ignoreDataGridViewCheckBoxColumn
            // 
            this.ignoreDataGridViewCheckBoxColumn.DataPropertyName = "Ignore";
            this.ignoreDataGridViewCheckBoxColumn.HeaderText = "Ignore";
            this.ignoreDataGridViewCheckBoxColumn.Name = "ignoreDataGridViewCheckBoxColumn";
            this.ignoreDataGridViewCheckBoxColumn.Width = 40;
            // 
            // BudgetEditingGridCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grid1);
            this.Name = "BudgetEditingGridCtrl";
            this.Size = new System.Drawing.Size(1395, 479);
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource budgetBindingSource;
        private MainDataSet mainDataSet;
        private MainDataSetTableAdapters.BudgetTableAdapter budgetTableAdapter;
        private System.Windows.Forms.DataGridView grid1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descrip2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn trCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ignoreDataGridViewCheckBoxColumn;
    }
}
