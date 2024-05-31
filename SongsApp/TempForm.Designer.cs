namespace Songs
{
    partial class TempForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.azureDataSet = new Songs.AzureDataSet();
            this.songperformancesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.songperformancesTableAdapter = new Songs.AzureDataSetTableAdapters.songperformancesTableAdapter();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.performanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.songDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.azureDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.songperformancesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.performanceDataGridViewTextBoxColumn,
            this.songDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.songperformancesBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(31, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(692, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // azureDataSet
            // 
            this.azureDataSet.DataSetName = "AzureDataSet";
            this.azureDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // songperformancesBindingSource
            // 
            this.songperformancesBindingSource.DataMember = "songperformances";
            this.songperformancesBindingSource.DataSource = this.azureDataSet;
            // 
            // songperformancesTableAdapter
            // 
            this.songperformancesTableAdapter.ClearBeforeFill = true;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // performanceDataGridViewTextBoxColumn
            // 
            this.performanceDataGridViewTextBoxColumn.DataPropertyName = "Performance";
            this.performanceDataGridViewTextBoxColumn.HeaderText = "Performance";
            this.performanceDataGridViewTextBoxColumn.Name = "performanceDataGridViewTextBoxColumn";
            // 
            // songDataGridViewTextBoxColumn
            // 
            this.songDataGridViewTextBoxColumn.DataPropertyName = "Song";
            this.songDataGridViewTextBoxColumn.HeaderText = "Song";
            this.songDataGridViewTextBoxColumn.Name = "songDataGridViewTextBoxColumn";
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            // 
            // TempForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TempForm";
            this.Text = "TempForm";
            this.Load += new System.EventHandler(this.TempForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.azureDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.songperformancesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private AzureDataSet azureDataSet;
        private System.Windows.Forms.BindingSource songperformancesBindingSource;
        private AzureDataSetTableAdapters.songperformancesTableAdapter songperformancesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn performanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn songDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}