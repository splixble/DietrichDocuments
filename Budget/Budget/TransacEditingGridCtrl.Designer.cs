﻿namespace Budget
{
    partial class TransacEditingGridCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardTransDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountNegatedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descrip2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripFromVendorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrTypeComboColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TrType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceIsCalculatedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CommentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acctTransferDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsIncomeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.transacBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.transacTableAdapter = new Budget.MainDataSetTableAdapters.TransacTableAdapter();
            this.label4 = new System.Windows.Forms.Label();
            this.comboTrType = new System.Windows.Forms.ComboBox();
            this.gBoxFilter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transacBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            this.gBoxFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.AllowUserToAddRows = false;
            this.grid1.AllowUserToDeleteRows = false;
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.AutoGenerateColumns = false;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.trDateDataGridViewTextBoxColumn,
            this.CardTransDateColumn,
            this.AmountColumn,
            this.AmountNegatedColumn,
            this.descripDataGridViewTextBoxColumn,
            this.Descrip2Column,
            this.DescripFromVendorColumn,
            this.trCodeDataGridViewTextBoxColumn,
            this.TrTypeComboColumn,
            this.TrType,
            this.accountDataGridViewTextBoxColumn,
            this.Balance,
            this.BalanceIsCalculatedColumn,
            this.CommentColumn,
            this.acctTransferDataGridViewCheckBoxColumn,
            this.IsIncomeColumn});
            this.grid1.DataSource = this.transacBindingSource;
            this.grid1.Location = new System.Drawing.Point(0, 35);
            this.grid1.Name = "grid1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid1.RowHeadersWidth = 50;
            this.grid1.Size = new System.Drawing.Size(1395, 425);
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
            dataGridViewCellStyle1.Format = "MM/dd/yy";
            this.trDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.trDateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.trDateDataGridViewTextBoxColumn.Name = "trDateDataGridViewTextBoxColumn";
            this.trDateDataGridViewTextBoxColumn.Width = 65;
            // 
            // CardTransDateColumn
            // 
            this.CardTransDateColumn.DataPropertyName = "CardTransDate";
            dataGridViewCellStyle2.Format = "MM/dd/yy";
            this.CardTransDateColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.CardTransDateColumn.HeaderText = "Card Tr Date";
            this.CardTransDateColumn.Name = "CardTransDateColumn";
            this.CardTransDateColumn.Width = 65;
            // 
            // AmountColumn
            // 
            this.AmountColumn.DataPropertyName = "Amount";
            this.AmountColumn.HeaderText = "Amount";
            this.AmountColumn.Name = "AmountColumn";
            this.AmountColumn.Width = 80;
            // 
            // AmountNegatedColumn
            // 
            this.AmountNegatedColumn.HeaderText = "Amount";
            this.AmountNegatedColumn.Name = "AmountNegatedColumn";
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
            // DescripFromVendorColumn
            // 
            this.DescripFromVendorColumn.DataPropertyName = "DescripFromVendor";
            this.DescripFromVendorColumn.HeaderText = "Descrip. from Vendor";
            this.DescripFromVendorColumn.Name = "DescripFromVendorColumn";
            // 
            // trCodeDataGridViewTextBoxColumn
            // 
            this.trCodeDataGridViewTextBoxColumn.DataPropertyName = "TrCode";
            this.trCodeDataGridViewTextBoxColumn.HeaderText = "TrCode";
            this.trCodeDataGridViewTextBoxColumn.Name = "trCodeDataGridViewTextBoxColumn";
            this.trCodeDataGridViewTextBoxColumn.Width = 200;
            // 
            // TrTypeComboColumn
            // 
            this.TrTypeComboColumn.DataPropertyName = "TrType";
            this.TrTypeComboColumn.HeaderText = "TrType";
            this.TrTypeComboColumn.Name = "TrTypeComboColumn";
            this.TrTypeComboColumn.Width = 130;
            // 
            // TrType
            // 
            this.TrType.DataPropertyName = "TrType";
            this.TrType.HeaderText = "TrType";
            this.TrType.Name = "TrType";
            this.TrType.ReadOnly = true;
            this.TrType.Width = 36;
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
            this.Balance.Width = 80;
            // 
            // BalanceIsCalculatedColumn
            // 
            this.BalanceIsCalculatedColumn.DataPropertyName = "BalanceIsCalculated";
            this.BalanceIsCalculatedColumn.HeaderText = "Balance Calc\'d?";
            this.BalanceIsCalculatedColumn.Name = "BalanceIsCalculatedColumn";
            this.BalanceIsCalculatedColumn.Width = 45;
            // 
            // CommentColumn
            // 
            this.CommentColumn.DataPropertyName = "Comment";
            this.CommentColumn.HeaderText = "Comment";
            this.CommentColumn.Name = "CommentColumn";
            this.CommentColumn.Width = 230;
            // 
            // acctTransferDataGridViewCheckBoxColumn
            // 
            this.acctTransferDataGridViewCheckBoxColumn.DataPropertyName = "AcctTransfer";
            this.acctTransferDataGridViewCheckBoxColumn.HeaderText = "Acct Xfer?";
            this.acctTransferDataGridViewCheckBoxColumn.Name = "acctTransferDataGridViewCheckBoxColumn";
            this.acctTransferDataGridViewCheckBoxColumn.Width = 40;
            // 
            // IsIncomeColumn
            // 
            this.IsIncomeColumn.HeaderText = "Income?";
            this.IsIncomeColumn.Name = "IsIncomeColumn";
            this.IsIncomeColumn.Width = 40;
            // 
            // transacBindingSource
            // 
            this.transacBindingSource.DataMember = "Transac";
            this.transacBindingSource.DataSource = this.mainDataSet;
            this.transacBindingSource.Sort = "";
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tbFilter
            // 
            this.tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilter.Location = new System.Drawing.Point(534, 4);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(848, 20);
            this.tbFilter.TabIndex = 3;
            this.tbFilter.Validated += new System.EventHandler(this.tbFilter_Validated);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(4, 463);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(22, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "- - -";
            this.lblStatus.Visible = false;
            // 
            // transacTableAdapter
            // 
            this.transacTableAdapter.ClearBeforeFill = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "TrType:";
            // 
            // comboTrType
            // 
            this.comboTrType.FormattingEnabled = true;
            this.comboTrType.Location = new System.Drawing.Point(102, 4);
            this.comboTrType.Name = "comboTrType";
            this.comboTrType.Size = new System.Drawing.Size(221, 21);
            this.comboTrType.TabIndex = 12;
            // 
            // gBoxFilter
            // 
            this.gBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBoxFilter.Controls.Add(this.label1);
            this.gBoxFilter.Controls.Add(this.comboTrType);
            this.gBoxFilter.Controls.Add(this.tbFilter);
            this.gBoxFilter.Controls.Add(this.label4);
            this.gBoxFilter.Location = new System.Drawing.Point(7, 4);
            this.gBoxFilter.Name = "gBoxFilter";
            this.gBoxFilter.Size = new System.Drawing.Size(1388, 29);
            this.gBoxFilter.TabIndex = 14;
            this.gBoxFilter.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Filters:";
            // 
            // TransacEditingGridCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gBoxFilter);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.grid1);
            this.Name = "TransacEditingGridCtrl";
            this.Size = new System.Drawing.Size(1395, 479);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transacBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.gBoxFilter.ResumeLayout(false);
            this.gBoxFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource transacBindingSource;
        private MainDataSet mainDataSet;
        private MainDataSetTableAdapters.TransacTableAdapter transacTableAdapter;
        private System.Windows.Forms.DataGridView grid1;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardTransDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountNegatedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descrip2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripFromVendorColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn TrTypeComboColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrType;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridViewCheckBoxColumn BalanceIsCalculatedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CommentColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn acctTransferDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsIncomeColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboTrType;
        private System.Windows.Forms.GroupBox gBoxFilter;
        private System.Windows.Forms.Label label1;
    }
}
