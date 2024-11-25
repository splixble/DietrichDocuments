namespace Budget
{
    partial class AccountsForm
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
            this.budgetAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.budgetAccountTableAdapter = new Budget.MainDataSetTableAdapters.BudgetAccountTableAdapter();
            this.btnSave = new System.Windows.Forms.Button();
            this.accountIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountOwnerColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.AccountTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.currentlyTrackedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.trackedBySharesDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sourceFileLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultFormatAutoEntryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultFormatManualEntryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetAccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accountIDDataGridViewTextBoxColumn,
            this.accountNameDataGridViewTextBoxColumn,
            this.AccountOwnerColumn,
            this.AccountTypeColumn,
            this.currentlyTrackedDataGridViewCheckBoxColumn,
            this.trackedBySharesDataGridViewCheckBoxColumn,
            this.sourceFileLocationDataGridViewTextBoxColumn,
            this.defaultFormatAutoEntryDataGridViewTextBoxColumn,
            this.defaultFormatManualEntryDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.budgetAccountBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1306, 541);
            this.dataGridView1.TabIndex = 0;
            // 
            // budgetAccountBindingSource
            // 
            this.budgetAccountBindingSource.DataMember = "BudgetAccount";
            this.budgetAccountBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // budgetAccountTableAdapter
            // 
            this.budgetAccountTableAdapter.ClearBeforeFill = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1243, 559);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // accountIDDataGridViewTextBoxColumn
            // 
            this.accountIDDataGridViewTextBoxColumn.DataPropertyName = "AccountID";
            this.accountIDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.accountIDDataGridViewTextBoxColumn.Name = "accountIDDataGridViewTextBoxColumn";
            this.accountIDDataGridViewTextBoxColumn.Width = 50;
            // 
            // accountNameDataGridViewTextBoxColumn
            // 
            this.accountNameDataGridViewTextBoxColumn.DataPropertyName = "AccountName";
            this.accountNameDataGridViewTextBoxColumn.HeaderText = "Account Name";
            this.accountNameDataGridViewTextBoxColumn.Name = "accountNameDataGridViewTextBoxColumn";
            this.accountNameDataGridViewTextBoxColumn.Width = 180;
            // 
            // AccountOwnerColumn
            // 
            this.AccountOwnerColumn.DataPropertyName = "AccountOwner";
            this.AccountOwnerColumn.FillWeight = 50F;
            this.AccountOwnerColumn.HeaderText = "Account Owner";
            this.AccountOwnerColumn.Name = "AccountOwnerColumn";
            this.AccountOwnerColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AccountOwnerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AccountOwnerColumn.Width = 140;
            // 
            // AccountTypeColumn
            // 
            this.AccountTypeColumn.DataPropertyName = "AccountType";
            this.AccountTypeColumn.HeaderText = "Account Type";
            this.AccountTypeColumn.Name = "AccountTypeColumn";
            this.AccountTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AccountTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AccountTypeColumn.Width = 80;
            // 
            // currentlyTrackedDataGridViewCheckBoxColumn
            // 
            this.currentlyTrackedDataGridViewCheckBoxColumn.DataPropertyName = "CurrentlyTracked";
            this.currentlyTrackedDataGridViewCheckBoxColumn.HeaderText = "Currently Tracked?";
            this.currentlyTrackedDataGridViewCheckBoxColumn.Name = "currentlyTrackedDataGridViewCheckBoxColumn";
            this.currentlyTrackedDataGridViewCheckBoxColumn.Width = 50;
            // 
            // trackedBySharesDataGridViewCheckBoxColumn
            // 
            this.trackedBySharesDataGridViewCheckBoxColumn.DataPropertyName = "TrackedByShares";
            this.trackedBySharesDataGridViewCheckBoxColumn.HeaderText = "By Shares?";
            this.trackedBySharesDataGridViewCheckBoxColumn.Name = "trackedBySharesDataGridViewCheckBoxColumn";
            this.trackedBySharesDataGridViewCheckBoxColumn.Width = 50;
            // 
            // sourceFileLocationDataGridViewTextBoxColumn
            // 
            this.sourceFileLocationDataGridViewTextBoxColumn.DataPropertyName = "SourceFileLocation";
            this.sourceFileLocationDataGridViewTextBoxColumn.HeaderText = "Source File Location";
            this.sourceFileLocationDataGridViewTextBoxColumn.Name = "sourceFileLocationDataGridViewTextBoxColumn";
            this.sourceFileLocationDataGridViewTextBoxColumn.Width = 160;
            // 
            // defaultFormatAutoEntryDataGridViewTextBoxColumn
            // 
            this.defaultFormatAutoEntryDataGridViewTextBoxColumn.DataPropertyName = "DefaultFormatAutoEntry";
            this.defaultFormatAutoEntryDataGridViewTextBoxColumn.HeaderText = "Default Format Auto-entry";
            this.defaultFormatAutoEntryDataGridViewTextBoxColumn.Name = "defaultFormatAutoEntryDataGridViewTextBoxColumn";
            this.defaultFormatAutoEntryDataGridViewTextBoxColumn.Width = 160;
            // 
            // defaultFormatManualEntryDataGridViewTextBoxColumn
            // 
            this.defaultFormatManualEntryDataGridViewTextBoxColumn.DataPropertyName = "DefaultFormatManualEntry";
            this.defaultFormatManualEntryDataGridViewTextBoxColumn.HeaderText = "Default Format Manual Entry";
            this.defaultFormatManualEntryDataGridViewTextBoxColumn.Name = "defaultFormatManualEntryDataGridViewTextBoxColumn";
            this.defaultFormatManualEntryDataGridViewTextBoxColumn.Width = 160;
            // 
            // AccountsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1330, 584);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AccountsForm";
            this.Text = "AccountsForm";
            this.Load += new System.EventHandler(this.AccountsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetAccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource budgetAccountBindingSource;
        private MainDataSetTableAdapters.BudgetAccountTableAdapter budgetAccountTableAdapter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn AccountOwnerColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn AccountTypeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn currentlyTrackedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn trackedBySharesDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceFileLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultFormatAutoEntryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultFormatManualEntryDataGridViewTextBoxColumn;
    }
}