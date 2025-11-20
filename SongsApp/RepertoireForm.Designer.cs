namespace Songs
{
    partial class RepertoireForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepertoireForm));
            this.comboBoxBand = new System.Windows.Forms.ComboBox();
            this.bandsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.azureDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.azureDataSet = new Songs.AzureDataSet();
            this.label1 = new System.Windows.Forms.Label();
            this.SongGrid = new System.Windows.Forms.DataGridView();
            this.ColumnSong = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.viewSongsSingleFieldBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.performanceNotesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repertoireBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewSongsSingleFieldTableAdapter = new Songs.AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            this.repertoireTableAdapter = new Songs.AzureDataSetTableAdapters.RepertoireTableAdapter();
            this.bandsTableAdapter = new Songs.AzureDataSetTableAdapters.bandsTableAdapter();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bandsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.azureDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.azureDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SongGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsSingleFieldBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repertoireBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxBand
            // 
            this.comboBoxBand.DataSource = this.bandsBindingSource;
            this.comboBoxBand.DisplayMember = "BandName";
            this.comboBoxBand.FormattingEnabled = true;
            this.comboBoxBand.Location = new System.Drawing.Point(52, 12);
            this.comboBoxBand.Name = "comboBoxBand";
            this.comboBoxBand.Size = new System.Drawing.Size(195, 21);
            this.comboBoxBand.TabIndex = 0;
            this.comboBoxBand.ValueMember = "BandID";
            this.comboBoxBand.SelectionChangeCommitted += new System.EventHandler(this.comboBoxBand_SelectionChangeCommitted);
            // 
            // bandsBindingSource
            // 
            this.bandsBindingSource.DataMember = "bands";
            this.bandsBindingSource.DataSource = this.azureDataSetBindingSource;
            // 
            // azureDataSetBindingSource
            // 
            this.azureDataSetBindingSource.DataSource = this.azureDataSet;
            this.azureDataSetBindingSource.Position = 0;
            // 
            // azureDataSet
            // 
            this.azureDataSet.DataSetName = "AzureDataSet";
            this.azureDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Band:";
            // 
            // SongGrid
            // 
            this.SongGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SongGrid.AutoGenerateColumns = false;
            this.SongGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SongGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSong,
            this.performanceNotesDataGridViewTextBoxColumn});
            this.SongGrid.DataSource = this.repertoireBindingSource;
            this.SongGrid.Location = new System.Drawing.Point(14, 39);
            this.SongGrid.Name = "SongGrid";
            this.SongGrid.Size = new System.Drawing.Size(1428, 370);
            this.SongGrid.TabIndex = 2;
            // 
            // ColumnSong
            // 
            this.ColumnSong.DataPropertyName = "Song";
            this.ColumnSong.DataSource = this.viewSongsSingleFieldBindingSource;
            this.ColumnSong.DisplayMember = "TitleAndArtist";
            this.ColumnSong.HeaderText = "Song";
            this.ColumnSong.Name = "ColumnSong";
            this.ColumnSong.ValueMember = "ID";
            this.ColumnSong.Width = 300;
            // 
            // viewSongsSingleFieldBindingSource
            // 
            this.viewSongsSingleFieldBindingSource.DataMember = "ViewSongsSingleField";
            this.viewSongsSingleFieldBindingSource.DataSource = this.azureDataSetBindingSource;
            // 
            // performanceNotesDataGridViewTextBoxColumn
            // 
            this.performanceNotesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.performanceNotesDataGridViewTextBoxColumn.DataPropertyName = "PerformanceNotes";
            this.performanceNotesDataGridViewTextBoxColumn.HeaderText = "Performance Notes";
            this.performanceNotesDataGridViewTextBoxColumn.Name = "performanceNotesDataGridViewTextBoxColumn";
            // 
            // repertoireBindingSource
            // 
            this.repertoireBindingSource.DataMember = "Repertoire";
            this.repertoireBindingSource.DataSource = this.azureDataSetBindingSource;
            this.repertoireBindingSource.CurrentItemChanged += new System.EventHandler(this.repertoireBindingSource_CurrentItemChanged);
            // 
            // viewSongsSingleFieldTableAdapter
            // 
            this.viewSongsSingleFieldTableAdapter.ClearBeforeFill = true;
            // 
            // repertoireTableAdapter
            // 
            this.repertoireTableAdapter.ClearBeforeFill = true;
            // 
            // bandsTableAdapter
            // 
            this.bandsTableAdapter.ClearBeforeFill = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(1367, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // RepertoireForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(1454, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.SongGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxBand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RepertoireForm";
            this.Text = "Band Repertoire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RepertoireForm_FormClosing);
            this.Load += new System.EventHandler(this.RepertoireForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bandsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.azureDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.azureDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SongGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsSingleFieldBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repertoireBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxBand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView SongGrid;
        private System.Windows.Forms.BindingSource azureDataSetBindingSource;
        private AzureDataSet azureDataSet;
        private System.Windows.Forms.BindingSource viewSongsSingleFieldBindingSource;
        private AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter viewSongsSingleFieldTableAdapter;
        private System.Windows.Forms.BindingSource repertoireBindingSource;
        private AzureDataSetTableAdapters.RepertoireTableAdapter repertoireTableAdapter;
        private System.Windows.Forms.BindingSource bandsBindingSource;
        private AzureDataSetTableAdapters.bandsTableAdapter bandsTableAdapter;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnSong;
        private System.Windows.Forms.DataGridViewTextBoxColumn performanceNotesDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnSave;
    }
}