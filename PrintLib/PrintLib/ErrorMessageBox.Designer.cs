namespace PrintLib
{
    partial class ErrorMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageBox));
            this.btnLeft = new System.Windows.Forms.Button();
            this.lblHeadingEnterDescription = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.tbDetailsToEmail = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblHeadingToNotify = new System.Windows.Forms.Label();
            this.tbTechnicalInfo = new System.Windows.Forms.TextBox();
            this.lblHeadingTechInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeft.BackColor = System.Drawing.SystemColors.Control;
            this.btnLeft.Location = new System.Drawing.Point(422, 470);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(97, 23);
            this.btnLeft.TabIndex = 2;
            this.btnLeft.Text = "Notify";
            this.btnLeft.UseVisualStyleBackColor = false;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // lblHeadingEnterDescription
            // 
            this.lblHeadingEnterDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeadingEnterDescription.Location = new System.Drawing.Point(12, 351);
            this.lblHeadingEnterDescription.MinimumSize = new System.Drawing.Size(570, 32);
            this.lblHeadingEnterDescription.Name = "lblHeadingEnterDescription";
            this.lblHeadingEnterDescription.Size = new System.Drawing.Size(610, 32);
            this.lblHeadingEnterDescription.TabIndex = 6;
            this.lblHeadingEnterDescription.Text = resources.GetString("lblHeadingEnterDescription.Text");
            // 
            // lblHeading
            // 
            this.lblHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeading.Location = new System.Drawing.Point(12, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(610, 48);
            this.lblHeading.TabIndex = 4;
            this.lblHeading.Text = "label1";
            // 
            // tbDetailsToEmail
            // 
            this.tbDetailsToEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDetailsToEmail.Location = new System.Drawing.Point(12, 386);
            this.tbDetailsToEmail.Multiline = true;
            this.tbDetailsToEmail.Name = "tbDetailsToEmail";
            this.tbDetailsToEmail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDetailsToEmail.Size = new System.Drawing.Size(610, 78);
            this.tbDetailsToEmail.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(525, 470);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblHeadingToNotify
            // 
            this.lblHeadingToNotify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeadingToNotify.AutoSize = true;
            this.lblHeadingToNotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadingToNotify.Location = new System.Drawing.Point(12, 334);
            this.lblHeadingToNotify.Name = "lblHeadingToNotify";
            this.lblHeadingToNotify.Size = new System.Drawing.Size(255, 13);
            this.lblHeadingToNotify.TabIndex = 9;
            this.lblHeadingToNotify.Text = "To notify Software Engineering of this error:";
            // 
            // tbTechnicalInfo
            // 
            this.tbTechnicalInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTechnicalInfo.Location = new System.Drawing.Point(12, 84);
            this.tbTechnicalInfo.Multiline = true;
            this.tbTechnicalInfo.Name = "tbTechnicalInfo";
            this.tbTechnicalInfo.ReadOnly = true;
            this.tbTechnicalInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTechnicalInfo.Size = new System.Drawing.Size(610, 237);
            this.tbTechnicalInfo.TabIndex = 10;
            // 
            // lblHeadingTechInfo
            // 
            this.lblHeadingTechInfo.AutoSize = true;
            this.lblHeadingTechInfo.Location = new System.Drawing.Point(12, 67);
            this.lblHeadingTechInfo.Name = "lblHeadingTechInfo";
            this.lblHeadingTechInfo.Size = new System.Drawing.Size(111, 13);
            this.lblHeadingTechInfo.TabIndex = 11;
            this.lblHeadingTechInfo.Text = "Technical information:";
            // 
            // ErrorMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(634, 505);
            this.Controls.Add(this.lblHeadingTechInfo);
            this.Controls.Add(this.tbTechnicalInfo);
            this.Controls.Add(this.lblHeadingToNotify);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbDetailsToEmail);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.lblHeadingEnterDescription);
            this.Controls.Add(this.lblHeading);
            this.MinimizeBox = false;
            this.Name = "ErrorMessageBox";
            this.Text = "Nexelis App Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Label lblHeadingEnterDescription;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TextBox tbDetailsToEmail;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblHeadingToNotify;
        private System.Windows.Forms.TextBox tbTechnicalInfo;
        private System.Windows.Forms.Label lblHeadingTechInfo;
    }
}