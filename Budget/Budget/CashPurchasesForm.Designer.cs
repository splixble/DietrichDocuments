namespace Budget
{
    partial class CashPurchasesForm
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
            this.btnSaveSharePrices = new System.Windows.Forms.Button();
            this.transacCtrl = new Budget.TransacEditingGridCtrl();
            this.SuspendLayout();
            // 
            // btnSaveSharePrices
            // 
            this.btnSaveSharePrices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSharePrices.Location = new System.Drawing.Point(1142, 415);
            this.btnSaveSharePrices.Name = "btnSaveSharePrices";
            this.btnSaveSharePrices.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSharePrices.TabIndex = 7;
            this.btnSaveSharePrices.Text = "Save";
            this.btnSaveSharePrices.UseVisualStyleBackColor = true;
            this.btnSaveSharePrices.Click += new System.EventHandler(this.btnSaveSharePrices_Click);
            // 
            // transacCtrl
            // 
            this.transacCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transacCtrl.Location = new System.Drawing.Point(12, 12);
            this.transacCtrl.Name = "transacCtrl";
            this.transacCtrl.Size = new System.Drawing.Size(1205, 416);
            this.transacCtrl.TabIndex = 0;
            // 
            // CashPurchasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 450);
            this.Controls.Add(this.btnSaveSharePrices);
            this.Controls.Add(this.transacCtrl);
            this.Name = "CashPurchasesForm";
            this.Text = "Cash Purchases";
            this.ResumeLayout(false);

        }

        #endregion

        private TransacEditingGridCtrl transacCtrl;
        private System.Windows.Forms.Button btnSaveSharePrices;
    }
}