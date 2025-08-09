namespace Budget
{
    partial class RefundTransacsForm
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
            this.refundTransacBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDataSet = new Budget.MainDataSet();
            this.btnSave = new WinformsLib.SaveButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.transCtrlSelectable = new Budget.TransacEditingGridCtrl();
            this.transCtrlInRefund = new Budget.TransacEditingGridCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.refundTransacBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // refundTransacBindingSource
            // 
            this.refundTransacBindingSource.DataMember = "RefundTransac";
            this.refundTransacBindingSource.DataSource = this.mainDataSet;
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // refundTransacTableAdapter
            // 
            // REDESIGN THIS FORM   this.refundTransacTableAdapter.ClearBeforeFill = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(1288, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.transCtrlSelectable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.transCtrlInRefund);
            this.splitContainer1.Size = new System.Drawing.Size(1354, 450);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 15;
            // 
            // transCtrlSelectable
            // 
            this.transCtrlSelectable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transCtrlSelectable.Location = new System.Drawing.Point(0, 0);
            this.transCtrlSelectable.Name = "transCtrlSelectable";
            this.transCtrlSelectable.Size = new System.Drawing.Size(1354, 228);
            this.transCtrlSelectable.TabIndex = 14;
            // 
            // transCtrlInRefund
            // 
            this.transCtrlInRefund.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transCtrlInRefund.Location = new System.Drawing.Point(0, 0);
            this.transCtrlInRefund.Name = "transCtrlInRefund";
            this.transCtrlInRefund.Size = new System.Drawing.Size(1354, 218);
            this.transCtrlInRefund.TabIndex = 0;
            // 
            // RefundTransacsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 503);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSave);
            this.Name = "RefundTransacsForm";
            this.Text = "Refund Transactions";
            this.Load += new System.EventHandler(this.RefundTransacsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.refundTransacBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource refundTransacBindingSource;
        private WinformsLib.SaveButton btnSave;
        private TransacEditingGridCtrl transCtrlSelectable;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TransacEditingGridCtrl transCtrlInRefund;
    }
}