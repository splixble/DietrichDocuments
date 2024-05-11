namespace Budget
{
    partial class TrTypeForm
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ignoreDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.budgetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.budgetTableAdapter = new Budget.MainDataSetTableAdapters.BudgetTableAdapter();
            this.btnApplyTypes = new System.Windows.Forms.Button();
            this.chBoxShowUntypedOnly = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.trDateDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn,
            this.descripDataGridViewTextBoxColumn,
            this.trTypeDataGridViewTextBoxColumn,
            this.accountDataGridViewTextBoxColumn,
            this.trCodeDataGridViewTextBoxColumn,
            this.ignoreDataGridViewCheckBoxColumn});
            this.grid.DataSource = this.budgetBindingSource;
            this.grid.Location = new System.Drawing.Point(12, 43);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(1099, 529);
            this.grid.TabIndex = 0;
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
            this.descripDataGridViewTextBoxColumn.DataPropertyName = "Descrip";
            this.descripDataGridViewTextBoxColumn.HeaderText = "Descrip";
            this.descripDataGridViewTextBoxColumn.Name = "descripDataGridViewTextBoxColumn";
            this.descripDataGridViewTextBoxColumn.Width = 250;
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
            // trCodeDataGridViewTextBoxColumn
            // 
            this.trCodeDataGridViewTextBoxColumn.DataPropertyName = "TrCode";
            this.trCodeDataGridViewTextBoxColumn.HeaderText = "TrCode";
            this.trCodeDataGridViewTextBoxColumn.Name = "trCodeDataGridViewTextBoxColumn";
            this.trCodeDataGridViewTextBoxColumn.Width = 200;
            // 
            // ignoreDataGridViewCheckBoxColumn
            // 
            this.ignoreDataGridViewCheckBoxColumn.DataPropertyName = "Ignore";
            this.ignoreDataGridViewCheckBoxColumn.HeaderText = "Ignore";
            this.ignoreDataGridViewCheckBoxColumn.Name = "ignoreDataGridViewCheckBoxColumn";
            this.ignoreDataGridViewCheckBoxColumn.Width = 40;
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
            // btnApplyTypes
            // 
            this.btnApplyTypes.Location = new System.Drawing.Point(175, 12);
            this.btnApplyTypes.Name = "btnApplyTypes";
            this.btnApplyTypes.Size = new System.Drawing.Size(75, 23);
            this.btnApplyTypes.TabIndex = 1;
            this.btnApplyTypes.Text = "Apply Types";
            this.btnApplyTypes.UseVisualStyleBackColor = true;
            this.btnApplyTypes.Click += new System.EventHandler(this.btnApplyTypes_Click);
            // 
            // chBoxShowUntypedOnly
            // 
            this.chBoxShowUntypedOnly.AutoSize = true;
            this.chBoxShowUntypedOnly.Checked = true;
            this.chBoxShowUntypedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxShowUntypedOnly.Location = new System.Drawing.Point(12, 16);
            this.chBoxShowUntypedOnly.Name = "chBoxShowUntypedOnly";
            this.chBoxShowUntypedOnly.Size = new System.Drawing.Size(118, 17);
            this.chBoxShowUntypedOnly.TabIndex = 2;
            this.chBoxShowUntypedOnly.Text = "Untyped Items Only";
            this.chBoxShowUntypedOnly.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(256, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TrTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 584);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chBoxShowUntypedOnly);
            this.Controls.Add(this.btnApplyTypes);
            this.Controls.Add(this.grid);
            this.Name = "TrTypeForm";
            this.Text = "TrTypeForm";
            this.Load += new System.EventHandler(this.TrTypeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource budgetBindingSource;
        private MainDataSetTableAdapters.BudgetTableAdapter budgetTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ignoreDataGridViewCheckBoxColumn;
        private System.Windows.Forms.Button btnApplyTypes;
        private System.Windows.Forms.CheckBox chBoxShowUntypedOnly;
        private System.Windows.Forms.Button btnSave;
    }
}