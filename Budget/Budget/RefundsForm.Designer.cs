﻿namespace Budget
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
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.mainDataSet = new Budget.MainDataSet();
            this.refundBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.refundTableAdapter = new Budget.MainDataSetTableAdapters.RefundTableAdapter();
            this.refundIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new WinformsLib.SaveButton();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.refundBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.AutoGenerateColumns = false;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.refundIDDataGridViewTextBoxColumn,
            this.notesDataGridViewTextBoxColumn});
            this.grid1.DataSource = this.refundBindingSource;
            this.grid1.Location = new System.Drawing.Point(12, 12);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(776, 397);
            this.grid1.TabIndex = 0;
            this.grid1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid1_CellDoubleClick);
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // refundBindingSource
            // 
            this.refundBindingSource.DataMember = "Refund";
            this.refundBindingSource.DataSource = this.mainDataSet;
            // 
            // refundTableAdapter
            // 
            this.refundTableAdapter.ClearBeforeFill = true;
            // 
            // refundIDDataGridViewTextBoxColumn
            // 
            this.refundIDDataGridViewTextBoxColumn.DataPropertyName = "RefundID";
            this.refundIDDataGridViewTextBoxColumn.HeaderText = "Refund ID";
            this.refundIDDataGridViewTextBoxColumn.Name = "refundIDDataGridViewTextBoxColumn";
            this.refundIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.refundIDDataGridViewTextBoxColumn.Width = 50;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Notes";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(710, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // RefundsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grid1);
            this.Name = "RefundsForm";
            this.Text = "RefundsForm";
            this.Load += new System.EventHandler(this.RefundsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.refundBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid1;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource refundBindingSource;
        private MainDataSetTableAdapters.RefundTableAdapter refundTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn refundIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private WinformsLib.SaveButton btnSave;
    }
}