namespace Budget
{
    partial class BalanceCalculationForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.transacCtrl = new Budget.TransacEditingGridCtrl();
            this.btnCalcBalances = new System.Windows.Forms.Button();
            this.btnSaveBudgetItems = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Account:";
            // 
            // comboAccount
            // 
            this.comboAccount.DisplayMember = "AccountID";
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Location = new System.Drawing.Point(68, 12);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(254, 21);
            this.comboAccount.TabIndex = 7;
            this.comboAccount.ValueMember = "AccountID";
            this.comboAccount.SelectedIndexChanged += new System.EventHandler(this.comboAccount_SelectedIndexChanged);
            // 
            // budgetCtrl
            // 
            this.transacCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transacCtrl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.transacCtrl.Location = new System.Drawing.Point(12, 40);
            this.transacCtrl.Name = "budgetCtrl";
            this.transacCtrl.Size = new System.Drawing.Size(776, 369);
            this.transacCtrl.TabIndex = 6;
            // 
            // btnCalcBalances
            // 
            this.btnCalcBalances.Location = new System.Drawing.Point(328, 12);
            this.btnCalcBalances.Name = "btnCalcBalances";
            this.btnCalcBalances.Size = new System.Drawing.Size(118, 23);
            this.btnCalcBalances.TabIndex = 9;
            this.btnCalcBalances.Text = "Calculate Balances";
            this.btnCalcBalances.UseVisualStyleBackColor = true;
            this.btnCalcBalances.Click += new System.EventHandler(this.btnCalcBalances_Click);
            // 
            // btnSaveBudgetItems
            // 
            this.btnSaveBudgetItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveBudgetItems.Location = new System.Drawing.Point(12, 415);
            this.btnSaveBudgetItems.Name = "btnSaveBudgetItems";
            this.btnSaveBudgetItems.Size = new System.Drawing.Size(113, 23);
            this.btnSaveBudgetItems.TabIndex = 10;
            this.btnSaveBudgetItems.Text = "Save Changes";
            this.btnSaveBudgetItems.UseVisualStyleBackColor = true;
            this.btnSaveBudgetItems.Click += new System.EventHandler(this.btnSaveBudgetItems_Click);
            // 
            // BalanceCalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSaveBudgetItems);
            this.Controls.Add(this.btnCalcBalances);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboAccount);
            this.Controls.Add(this.transacCtrl);
            this.Name = "BalanceCalculationForm";
            this.Text = "BalanceCalculationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboAccount;
        private TransacEditingGridCtrl transacCtrl;
        private System.Windows.Forms.Button btnCalcBalances;
        private System.Windows.Forms.Button btnSaveBudgetItems;
    }
}