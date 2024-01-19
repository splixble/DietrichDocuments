namespace Songs
{
    partial class FlagsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlagsForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.flagIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new Songs.MySqlDataSet();
            this.flagsTableAdapter = new Songs.MySqlDataSetTableAdapters.flagsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(454, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grid1
            // 
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.AutoGenerateColumns = false;
            this.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flagIDDataGridViewTextBoxColumn,
            this.flagNameDataGridViewTextBoxColumn,
            this.flagDescriptionDataGridViewTextBoxColumn});
            this.grid1.DataSource = this.flagsBindingSource;
            this.grid1.Location = new System.Drawing.Point(12, 12);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(517, 367);
            this.grid1.TabIndex = 3;
            // 
            // flagIDDataGridViewTextBoxColumn
            // 
            this.flagIDDataGridViewTextBoxColumn.DataPropertyName = "FlagID";
            this.flagIDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.flagIDDataGridViewTextBoxColumn.Name = "flagIDDataGridViewTextBoxColumn";
            this.flagIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.flagIDDataGridViewTextBoxColumn.Width = 40;
            // 
            // flagNameDataGridViewTextBoxColumn
            // 
            this.flagNameDataGridViewTextBoxColumn.DataPropertyName = "FlagName";
            this.flagNameDataGridViewTextBoxColumn.HeaderText = "Flag Name";
            this.flagNameDataGridViewTextBoxColumn.Name = "flagNameDataGridViewTextBoxColumn";
            this.flagNameDataGridViewTextBoxColumn.Width = 140;
            // 
            // flagDescriptionDataGridViewTextBoxColumn
            // 
            this.flagDescriptionDataGridViewTextBoxColumn.DataPropertyName = "FlagDescription";
            this.flagDescriptionDataGridViewTextBoxColumn.HeaderText = "Flag Description";
            this.flagDescriptionDataGridViewTextBoxColumn.Name = "flagDescriptionDataGridViewTextBoxColumn";
            this.flagDescriptionDataGridViewTextBoxColumn.Width = 250;
            // 
            // flagsBindingSource
            // 
            this.flagsBindingSource.DataMember = "flags";
            this.flagsBindingSource.DataSource = this.dataSet1;
            this.flagsBindingSource.CurrentItemChanged += new System.EventHandler(this.flagsBindingSource_CurrentItemChanged);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // flagsTableAdapter
            // 
            this.flagsTableAdapter.ClearBeforeFill = true;
            // 
            // FlagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.ClientSize = new System.Drawing.Size(541, 420);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grid1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FlagsForm";
            this.Text = "Flags";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FlagsForm_FormClosing);
            this.Load += new System.EventHandler(this.FlagsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView grid1;
        private MySqlDataSet dataSet1;
        private System.Windows.Forms.BindingSource flagsBindingSource;
        private Songs.MySqlDataSetTableAdapters.flagsTableAdapter flagsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagDescriptionDataGridViewTextBoxColumn;
    }
}