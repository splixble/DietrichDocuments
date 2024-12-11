namespace Budget
{
    partial class InvestmentBalancesForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveShareAmounts = new System.Windows.Forms.Button();
            this.gridShareAmount = new System.Windows.Forms.DataGridView();
            this.sQDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numSharesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shareQuantityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.comboAccountByShares = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gridShareQuantity = new System.Windows.Forms.DataGridView();
            this.sPDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pricePerShareDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sharePriceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSaveSharePrices = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.shareQuantityTableAdapter = new Budget.MainDataSetTableAdapters.ShareQuantityTableAdapter();
            this.sharePriceTableAdapter = new Budget.MainDataSetTableAdapters.SharePriceTableAdapter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpByShares = new System.Windows.Forms.TabPage();
            this.tpByDollars = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.comboAccountByDollars = new System.Windows.Forms.ComboBox();
            this.btnSaveBudgetItems = new System.Windows.Forms.Button();
            this.gridBudgetItems = new System.Windows.Forms.DataGridView();
            this.budgetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.budgetTableAdapter = new Budget.MainDataSetTableAdapters.BudgetTableAdapter();
            this.trDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridShareAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareQuantityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridShareQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharePriceBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tpByShares.SuspendLayout();
            this.tpByDollars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBudgetItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveShareAmounts);
            this.splitContainer1.Panel1.Controls.Add(this.gridShareAmount);
            this.splitContainer1.Panel1.Controls.Add(this.comboAccountByShares);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridShareQuantity);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveSharePrices);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(829, 502);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Account";
            // 
            // btnSaveShareAmounts
            // 
            this.btnSaveShareAmounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveShareAmounts.Location = new System.Drawing.Point(741, 196);
            this.btnSaveShareAmounts.Name = "btnSaveShareAmounts";
            this.btnSaveShareAmounts.Size = new System.Drawing.Size(75, 23);
            this.btnSaveShareAmounts.TabIndex = 7;
            this.btnSaveShareAmounts.Text = "Save";
            this.btnSaveShareAmounts.UseVisualStyleBackColor = true;
            this.btnSaveShareAmounts.Click += new System.EventHandler(this.btnSaveShareAmounts_Click);
            // 
            // gridShareAmount
            // 
            this.gridShareAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridShareAmount.AutoGenerateColumns = false;
            this.gridShareAmount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShareAmount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sQDateDataGridViewTextBoxColumn,
            this.numSharesDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.gridShareAmount.DataSource = this.shareQuantityBindingSource;
            this.gridShareAmount.Location = new System.Drawing.Point(13, 47);
            this.gridShareAmount.Name = "gridShareAmount";
            this.gridShareAmount.Size = new System.Drawing.Size(803, 143);
            this.gridShareAmount.TabIndex = 4;
            // 
            // sQDateDataGridViewTextBoxColumn
            // 
            this.sQDateDataGridViewTextBoxColumn.DataPropertyName = "SQDate";
            this.sQDateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.sQDateDataGridViewTextBoxColumn.Name = "sQDateDataGridViewTextBoxColumn";
            // 
            // numSharesDataGridViewTextBoxColumn
            // 
            this.numSharesDataGridViewTextBoxColumn.DataPropertyName = "NumShares";
            this.numSharesDataGridViewTextBoxColumn.HeaderText = "NumShares";
            this.numSharesDataGridViewTextBoxColumn.Name = "numSharesDataGridViewTextBoxColumn";
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            // 
            // shareQuantityBindingSource
            // 
            this.shareQuantityBindingSource.DataMember = "ShareQuantity";
            this.shareQuantityBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboAccountByShares
            // 
            this.comboAccountByShares.FormattingEnabled = true;
            this.comboAccountByShares.Location = new System.Drawing.Point(66, 3);
            this.comboAccountByShares.Name = "comboAccountByShares";
            this.comboAccountByShares.Size = new System.Drawing.Size(243, 23);
            this.comboAccountByShares.TabIndex = 1;
            this.comboAccountByShares.SelectionChangeCommitted += new System.EventHandler(this.comboAccountByShares_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Share Amounts";
            // 
            // gridShareQuantity
            // 
            this.gridShareQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridShareQuantity.AutoGenerateColumns = false;
            this.gridShareQuantity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridShareQuantity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sPDateDataGridViewTextBoxColumn,
            this.pricePerShareDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn1});
            this.gridShareQuantity.DataSource = this.sharePriceBindingSource;
            this.gridShareQuantity.Location = new System.Drawing.Point(13, 18);
            this.gridShareQuantity.Name = "gridShareQuantity";
            this.gridShareQuantity.Size = new System.Drawing.Size(803, 225);
            this.gridShareQuantity.TabIndex = 7;
            // 
            // sPDateDataGridViewTextBoxColumn
            // 
            this.sPDateDataGridViewTextBoxColumn.DataPropertyName = "SPDate";
            this.sPDateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.sPDateDataGridViewTextBoxColumn.Name = "sPDateDataGridViewTextBoxColumn";
            // 
            // pricePerShareDataGridViewTextBoxColumn
            // 
            this.pricePerShareDataGridViewTextBoxColumn.DataPropertyName = "PricePerShare";
            this.pricePerShareDataGridViewTextBoxColumn.HeaderText = "Price Per Share";
            this.pricePerShareDataGridViewTextBoxColumn.Name = "pricePerShareDataGridViewTextBoxColumn";
            // 
            // commentDataGridViewTextBoxColumn1
            // 
            this.commentDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commentDataGridViewTextBoxColumn1.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn1.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn1.Name = "commentDataGridViewTextBoxColumn1";
            // 
            // sharePriceBindingSource
            // 
            this.sharePriceBindingSource.DataMember = "SharePrice";
            this.sharePriceBindingSource.DataSource = this.mainDataSet;
            // 
            // btnSaveSharePrices
            // 
            this.btnSaveSharePrices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSharePrices.Location = new System.Drawing.Point(741, 249);
            this.btnSaveSharePrices.Name = "btnSaveSharePrices";
            this.btnSaveSharePrices.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSharePrices.TabIndex = 6;
            this.btnSaveSharePrices.Text = "Save";
            this.btnSaveSharePrices.UseVisualStyleBackColor = true;
            this.btnSaveSharePrices.Click += new System.EventHandler(this.btnSaveSharePrices_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Share Prices";
            // 
            // shareQuantityTableAdapter
            // 
            this.shareQuantityTableAdapter.ClearBeforeFill = true;
            // 
            // sharePriceTableAdapter
            // 
            this.sharePriceTableAdapter.ClearBeforeFill = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpByShares);
            this.tabControl1.Controls.Add(this.tpByDollars);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(843, 534);
            this.tabControl1.TabIndex = 8;
            // 
            // tpByShares
            // 
            this.tpByShares.Controls.Add(this.splitContainer1);
            this.tpByShares.Location = new System.Drawing.Point(4, 22);
            this.tpByShares.Name = "tpByShares";
            this.tpByShares.Padding = new System.Windows.Forms.Padding(3);
            this.tpByShares.Size = new System.Drawing.Size(835, 508);
            this.tpByShares.TabIndex = 0;
            this.tpByShares.Text = "By Shares";
            this.tpByShares.UseVisualStyleBackColor = true;
            // 
            // tpByDollars
            // 
            this.tpByDollars.Controls.Add(this.gridBudgetItems);
            this.tpByDollars.Controls.Add(this.btnSaveBudgetItems);
            this.tpByDollars.Controls.Add(this.label4);
            this.tpByDollars.Controls.Add(this.comboAccountByDollars);
            this.tpByDollars.Location = new System.Drawing.Point(4, 22);
            this.tpByDollars.Name = "tpByDollars";
            this.tpByDollars.Padding = new System.Windows.Forms.Padding(3);
            this.tpByDollars.Size = new System.Drawing.Size(835, 508);
            this.tpByDollars.TabIndex = 1;
            this.tpByDollars.Text = "By Dollars";
            this.tpByDollars.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Account";
            // 
            // comboAccountByDollars
            // 
            this.comboAccountByDollars.FormattingEnabled = true;
            this.comboAccountByDollars.Location = new System.Drawing.Point(66, 8);
            this.comboAccountByDollars.Name = "comboAccountByDollars";
            this.comboAccountByDollars.Size = new System.Drawing.Size(243, 21);
            this.comboAccountByDollars.TabIndex = 3;
            this.comboAccountByDollars.SelectionChangeCommitted += new System.EventHandler(this.comboAccountByDollars_SelectionChangeCommitted);
            // 
            // btnSaveBudgetItems
            // 
            this.btnSaveBudgetItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveBudgetItems.Location = new System.Drawing.Point(752, 477);
            this.btnSaveBudgetItems.Name = "btnSaveBudgetItems";
            this.btnSaveBudgetItems.Size = new System.Drawing.Size(75, 23);
            this.btnSaveBudgetItems.TabIndex = 7;
            this.btnSaveBudgetItems.Text = "Save";
            this.btnSaveBudgetItems.UseVisualStyleBackColor = true;
            this.btnSaveBudgetItems.Click += new System.EventHandler(this.btnSaveBudgetItems_Click);
            // 
            // gridBudgetItems
            // 
            this.gridBudgetItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridBudgetItems.AutoGenerateColumns = false;
            this.gridBudgetItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBudgetItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trDateDataGridViewTextBoxColumn,
            this.balanceDataGridViewTextBoxColumn,
            this.Comment});
            this.gridBudgetItems.DataSource = this.budgetBindingSource;
            this.gridBudgetItems.Location = new System.Drawing.Point(8, 35);
            this.gridBudgetItems.Name = "gridBudgetItems";
            this.gridBudgetItems.Size = new System.Drawing.Size(819, 436);
            this.gridBudgetItems.TabIndex = 8;
            // 
            // budgetBindingSource
            // 
            this.budgetBindingSource.DataMember = "Budget";
            this.budgetBindingSource.DataSource = this.mainDataSet;
            // 
            // budgetTableAdapter
            // 
            this.budgetTableAdapter.ClearBeforeFill = true;
            // 
            // trDateDataGridViewTextBoxColumn
            // 
            this.trDateDataGridViewTextBoxColumn.DataPropertyName = "TrDate";
            this.trDateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.trDateDataGridViewTextBoxColumn.Name = "trDateDataGridViewTextBoxColumn";
            // 
            // balanceDataGridViewTextBoxColumn
            // 
            this.balanceDataGridViewTextBoxColumn.DataPropertyName = "Balance";
            this.balanceDataGridViewTextBoxColumn.HeaderText = "Balance";
            this.balanceDataGridViewTextBoxColumn.Name = "balanceDataGridViewTextBoxColumn";
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            // 
            // InvestmentBalancesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 534);
            this.Controls.Add(this.tabControl1);
            this.Name = "InvestmentBalancesForm";
            this.Text = "Investment Balances";
            this.Load += new System.EventHandler(this.ShareAmountAndPriceForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridShareAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareQuantityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridShareQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharePriceBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tpByShares.ResumeLayout(false);
            this.tpByDollars.ResumeLayout(false);
            this.tpByDollars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBudgetItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboAccountByShares;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gridShareAmount;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource shareQuantityBindingSource;
        private MainDataSetTableAdapters.ShareQuantityTableAdapter shareQuantityTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn sQDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numSharesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sharePriceBindingSource;
        private MainDataSetTableAdapters.SharePriceTableAdapter sharePriceTableAdapter;
        private System.Windows.Forms.Button btnSaveSharePrices;
        private System.Windows.Forms.DataGridView gridShareQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn sPDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pricePerShareDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button btnSaveShareAmounts;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpByShares;
        private System.Windows.Forms.TabPage tpByDollars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboAccountByDollars;
        private System.Windows.Forms.DataGridView gridBudgetItems;
        private System.Windows.Forms.Button btnSaveBudgetItems;
        private System.Windows.Forms.BindingSource budgetBindingSource;
        private MainDataSetTableAdapters.BudgetTableAdapter budgetTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn trDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn balanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
    }
}