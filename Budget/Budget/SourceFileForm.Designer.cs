namespace Budget
{
    partial class SourceFileForm
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
            this.btnOpenSourceFile = new System.Windows.Forms.Button();
            this.comboSrcFileFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainDataSet = new Budget.MainDataSet();
            this.budgetSourceFileFormatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.budgetSourceFileFormatTableAdapter = new Budget.MainDataSetTableAdapters.BudgetSourceFileFormatTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetSourceFileFormatBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenSourceFile
            // 
            this.btnOpenSourceFile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenSourceFile.Name = "btnOpenSourceFile";
            this.btnOpenSourceFile.Size = new System.Drawing.Size(107, 23);
            this.btnOpenSourceFile.TabIndex = 0;
            this.btnOpenSourceFile.Text = "Open Source File";
            this.btnOpenSourceFile.UseVisualStyleBackColor = true;
            this.btnOpenSourceFile.Click += new System.EventHandler(this.btnOpenSourceFile_Click);
            // 
            // comboSrcFileFormat
            // 
            this.comboSrcFileFormat.DataSource = this.budgetSourceFileFormatBindingSource;
            this.comboSrcFileFormat.DisplayMember = "FormatCode";
            this.comboSrcFileFormat.FormattingEnabled = true;
            this.comboSrcFileFormat.Location = new System.Drawing.Point(183, 14);
            this.comboSrcFileFormat.Name = "comboSrcFileFormat";
            this.comboSrcFileFormat.Size = new System.Drawing.Size(257, 21);
            this.comboSrcFileFormat.TabIndex = 1;
            this.comboSrcFileFormat.ValueMember = "FormatCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Format:";
            // 
            // mainDataSet
            // 
            this.mainDataSet.DataSetName = "MainDataSet";
            this.mainDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // budgetSourceFileFormatBindingSource
            // 
            this.budgetSourceFileFormatBindingSource.DataMember = "BudgetSourceFileFormat";
            this.budgetSourceFileFormatBindingSource.DataSource = this.mainDataSet;
            // 
            // budgetSourceFileFormatTableAdapter
            // 
            this.budgetSourceFileFormatTableAdapter.ClearBeforeFill = true;
            // 
            // SourceFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboSrcFileFormat);
            this.Controls.Add(this.btnOpenSourceFile);
            this.Name = "SourceFileForm";
            this.Text = "SourceFileForm";
            this.Load += new System.EventHandler(this.SourceFileForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.budgetSourceFileFormatBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSourceFile;
        private System.Windows.Forms.ComboBox comboSrcFileFormat;
        private System.Windows.Forms.Label label1;
        private MainDataSet mainDataSet;
        private System.Windows.Forms.BindingSource budgetSourceFileFormatBindingSource;
        private MainDataSetTableAdapters.BudgetSourceFileFormatTableAdapter budgetSourceFileFormatTableAdapter;
    }
}