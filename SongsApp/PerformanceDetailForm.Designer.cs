namespace Songs
{
    partial class PerformanceDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformanceDetailForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.songDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.viewSongsSingleFieldBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewSongsDataSet1 = new Songs.ViewSongsDataSet();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.songperformancesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.performanceDataSet = new Songs.PerformanceDataSet();
            this.lblDate = new System.Windows.Forms.Label();
            this.performancesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblVenue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.songperformancesTableAdapter = new Songs.PerformanceDataSetTableAdapters.songperformancesTableAdapter();
            this.performancesTableAdapter = new Songs.PerformanceDataSetTableAdapters.performancesTableAdapter();
            this.viewSongsSingleFieldTableAdapter = new Songs.ViewSongsDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsSingleFieldBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.songperformancesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performancesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(633, 294);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
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
            this.iDDataGridViewTextBoxColumn,
            this.songDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.grid1.DataSource = this.songperformancesBindingSource;
            this.grid1.Location = new System.Drawing.Point(12, 61);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(696, 227);
            this.grid1.TabIndex = 8;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 30;
            // 
            // songDataGridViewTextBoxColumn
            // 
            this.songDataGridViewTextBoxColumn.DataPropertyName = "Song";
            this.songDataGridViewTextBoxColumn.DataSource = this.viewSongsSingleFieldBindingSource;
            this.songDataGridViewTextBoxColumn.DisplayMember = "SongFull";
            this.songDataGridViewTextBoxColumn.FillWeight = 300F;
            this.songDataGridViewTextBoxColumn.HeaderText = "Song";
            this.songDataGridViewTextBoxColumn.Name = "songDataGridViewTextBoxColumn";
            this.songDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.songDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.songDataGridViewTextBoxColumn.ValueMember = "ID";
            this.songDataGridViewTextBoxColumn.Width = 400;
            // 
            // viewSongsSingleFieldBindingSource
            // 
            this.viewSongsSingleFieldBindingSource.DataMember = "ViewSongsSingleField";
            this.viewSongsSingleFieldBindingSource.DataSource = this.viewSongsDataSet1;
            // 
            // viewSongsDataSet1
            // 
            this.viewSongsDataSet1.DataSetName = "ViewSongsDataSet";
            this.viewSongsDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 200;
            // 
            // songperformancesBindingSource
            // 
            this.songperformancesBindingSource.DataMember = "songperformances";
            this.songperformancesBindingSource.DataSource = this.performanceDataSet;
            this.songperformancesBindingSource.CurrentItemChanged += new System.EventHandler(this.songperformancesBindingSource_CurrentItemChanged);
            // 
            // performanceDataSet
            // 
            this.performanceDataSet.DataSetName = "PerformanceDataSet";
            this.performanceDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.performancesBindingSource, "PerformanceDate", true));
            this.lblDate.Location = new System.Drawing.Point(62, 31);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 13);
            this.lblDate.TabIndex = 13;
            this.lblDate.Text = "label4";
            // 
            // performancesBindingSource
            // 
            this.performancesBindingSource.DataMember = "performances";
            this.performancesBindingSource.DataSource = this.performanceDataSet;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Date:";
            // 
            // lblVenue
            // 
            this.lblVenue.AutoSize = true;
            this.lblVenue.Location = new System.Drawing.Point(62, 9);
            this.lblVenue.Name = "lblVenue";
            this.lblVenue.Size = new System.Drawing.Size(35, 13);
            this.lblVenue.TabIndex = 11;
            this.lblVenue.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Venue:";
            // 
            // songperformancesTableAdapter
            // 
            this.songperformancesTableAdapter.ClearBeforeFill = true;
            // 
            // performancesTableAdapter
            // 
            this.performancesTableAdapter.ClearBeforeFill = true;
            // 
            // viewSongsSingleFieldTableAdapter
            // 
            this.viewSongsSingleFieldTableAdapter.ClearBeforeFill = true;
            // 
            // PerformanceDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(720, 329);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVenue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grid1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PerformanceDetailForm";
            this.Text = "Performance Detail Form";
            this.Load += new System.EventHandler(this.PerformanceDetailForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PerformanceDetailForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsSingleFieldBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.songperformancesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performancesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView grid1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblVenue;
        private System.Windows.Forms.Label label1;
        private PerformanceDataSet performanceDataSet;
        private System.Windows.Forms.BindingSource songperformancesBindingSource;
        private Songs.PerformanceDataSetTableAdapters.songperformancesTableAdapter songperformancesTableAdapter;
        private System.Windows.Forms.BindingSource performancesBindingSource;
        private Songs.PerformanceDataSetTableAdapters.performancesTableAdapter performancesTableAdapter;
        private System.Windows.Forms.BindingSource viewSongsSingleFieldBindingSource;
        private Songs.ViewSongsDataSetTableAdapters.ViewSongsSingleFieldTableAdapter viewSongsSingleFieldTableAdapter;
        private ViewSongsDataSet viewSongsDataSet1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn songDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}