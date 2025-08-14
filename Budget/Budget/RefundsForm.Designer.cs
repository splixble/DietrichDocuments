namespace Budget
{
    partial class RefundsForm
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
            this.gridRefunds = new System.Windows.Forms.DataGridView();
            this.refundBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.refundTableAdapter = new Budget.MainDataSetTableAdapters.RefundTableAdapter();
            this.btnSaveTransactions = new WinformsLib.SaveButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSaveRefundSets = new WinformsLib.SaveButton();
            this.transacCtrl = new Budget.TransacEditingGridCtrl();
            this.refundIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefundName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColApplyToSelected = new System.Windows.Forms.DataGridViewButtonColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridRefunds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.refundBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridRefunds
            // 
            this.gridRefunds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRefunds.AutoGenerateColumns = false;
            this.gridRefunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRefunds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.refundIDDataGridViewTextBoxColumn,
            this.RefundName,
            this.ColApplyToSelected,
            this.notesDataGridViewTextBoxColumn});
            this.gridRefunds.DataSource = this.refundBindingSource;
            this.gridRefunds.Location = new System.Drawing.Point(0, 0);
            this.gridRefunds.Name = "gridRefunds";
            this.gridRefunds.Size = new System.Drawing.Size(1451, 147);
            this.gridRefunds.TabIndex = 0;
            this.gridRefunds.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid1_CellClick);
            // 
            // refundBindingSource
            // 
            this.refundBindingSource.DataMember = "Refund";
            this.refundBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // refundTableAdapter
            // 
            this.refundTableAdapter.ClearBeforeFill = true;
            // 
            // btnSaveTransactions
            // 
            this.btnSaveTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveTransactions.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnSaveTransactions.Enabled = false;
            this.btnSaveTransactions.Location = new System.Drawing.Point(1222, 367);
            this.btnSaveTransactions.Name = "btnSaveTransactions";
            this.btnSaveTransactions.Size = new System.Drawing.Size(114, 23);
            this.btnSaveTransactions.TabIndex = 12;
            this.btnSaveTransactions.Text = "Save Transactions";
            this.btnSaveTransactions.UseVisualStyleBackColor = false;
            this.btnSaveTransactions.Click += new System.EventHandler(this.btnSaveTransactions_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveRefundSets);
            this.splitContainer1.Panel1.Controls.Add(this.gridRefunds);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveTransactions);
            this.splitContainer1.Panel2.Controls.Add(this.transacCtrl);
            this.splitContainer1.Size = new System.Drawing.Size(1451, 572);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 14;
            // 
            // btnSaveRefundSets
            // 
            this.btnSaveRefundSets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRefundSets.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnSaveRefundSets.Enabled = false;
            this.btnSaveRefundSets.Location = new System.Drawing.Point(1337, 153);
            this.btnSaveRefundSets.Name = "btnSaveRefundSets";
            this.btnSaveRefundSets.Size = new System.Drawing.Size(114, 23);
            this.btnSaveRefundSets.TabIndex = 13;
            this.btnSaveRefundSets.Text = "Save Refund Sets";
            this.btnSaveRefundSets.UseVisualStyleBackColor = false;
            this.btnSaveRefundSets.Click += new System.EventHandler(this.saveRefundSets_Click);
            // 
            // transacCtrl
            // 
            this.transacCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transacCtrl.Location = new System.Drawing.Point(0, 0);
            this.transacCtrl.Name = "transacCtrl";
            this.transacCtrl.Size = new System.Drawing.Size(1451, 390);
            this.transacCtrl.TabIndex = 13;
            // 
            // refundIDDataGridViewTextBoxColumn
            // 
            this.refundIDDataGridViewTextBoxColumn.DataPropertyName = "RefundID";
            this.refundIDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.refundIDDataGridViewTextBoxColumn.Name = "refundIDDataGridViewTextBoxColumn";
            this.refundIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.refundIDDataGridViewTextBoxColumn.Width = 50;
            // 
            // RefundName
            // 
            this.RefundName.DataPropertyName = "RefundName";
            this.RefundName.HeaderText = "Refund Name";
            this.RefundName.Name = "RefundName";
            this.RefundName.Width = 150;
            // 
            // ColApplyToSelected
            // 
            this.ColApplyToSelected.HeaderText = "Apply to Selected Transacs";
            this.ColApplyToSelected.Name = "ColApplyToSelected";
            this.ColApplyToSelected.Text = "Apply to Selected Transacs";
            this.ColApplyToSelected.UseColumnTextForButtonValue = true;
            this.ColApplyToSelected.Width = 160;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Notes";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            // 
            // RefundsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 625);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RefundsForm";
            this.Text = "RefundsForm";
            this.Load += new System.EventHandler(this.RefundsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRefunds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.refundBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridRefunds;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource refundBindingSource;
        private MainDataSetTableAdapters.RefundTableAdapter refundTableAdapter;
        private WinformsLib.SaveButton btnSaveTransactions;
        private TransacEditingGridCtrl transacCtrl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private WinformsLib.SaveButton btnSaveRefundSets;
        private System.Windows.Forms.DataGridViewTextBoxColumn refundIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefundName;
        private System.Windows.Forms.DataGridViewButtonColumn ColApplyToSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
    }
}