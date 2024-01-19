namespace Songs
{
    partial class SongDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongDetailForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblSong = new System.Windows.Forms.Label();
            this.songsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new Songs.DataSet1();
            this.label3 = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.songsTableAdapter = new Songs.DataSet1TableAdapters.songsTableAdapter();
            this.btnSave = new System.Windows.Forms.Button();
            this.gridFlags = new System.Windows.Forms.DataGridView();
            this.FlagID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.flagsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.songDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flaggedSongIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flaggedsongsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flaggedsongsTableAdapter = new Songs.DataSet1TableAdapters.flaggedsongsTableAdapter();
            this.flagsTableAdapter = new Songs.DataSet1TableAdapters.flagsTableAdapter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridAltArtists = new System.Windows.Forms.DataGridView();
            this.ArtistIDColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.viewArtistNameForListBoxBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.alternateArtistsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.alternateArtistsDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.alternateArtistsDataSet = new Songs.AlternateArtistsDataSet();
            this.alternateArtistsTableAdapter = new Songs.AlternateArtistsDataSetTableAdapters.AlternateArtistsTableAdapter();
            this.viewArtistNameForListBoxTableAdapter = new Songs.DataSet1TableAdapters.ViewArtistNameForListBoxTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.songsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFlags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flaggedsongsBindingSource)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAltArtists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewArtistNameForListBoxBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alternateArtistsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alternateArtistsDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alternateArtistsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Song:";
            // 
            // lblSong
            // 
            this.lblSong.AutoSize = true;
            this.lblSong.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.songsBindingSource, "Title", true));
            this.lblSong.Location = new System.Drawing.Point(53, 9);
            this.lblSong.Name = "lblSong";
            this.lblSong.Size = new System.Drawing.Size(35, 13);
            this.lblSong.TabIndex = 1;
            this.lblSong.Text = "label2";
            // 
            // songsBindingSource
            // 
            this.songsBindingSource.DataMember = "songs";
            this.songsBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Artist:";
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(53, 31);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(35, 13);
            this.lblArtist.TabIndex = 3;
            this.lblArtist.Text = "label4";
            // 
            // songsTableAdapter
            // 
            this.songsTableAdapter.ClearBeforeFill = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(403, 368);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gridFlags
            // 
            this.gridFlags.AutoGenerateColumns = false;
            this.gridFlags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFlags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FlagID,
            this.songDataGridViewTextBoxColumn,
            this.flaggedSongIDDataGridViewTextBoxColumn,
            this.flagIDDataGridViewTextBoxColumn});
            this.gridFlags.DataSource = this.flaggedsongsBindingSource;
            this.gridFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFlags.Location = new System.Drawing.Point(0, 0);
            this.gridFlags.Name = "gridFlags";
            this.gridFlags.Size = new System.Drawing.Size(466, 190);
            this.gridFlags.TabIndex = 4;
            // 
            // FlagID
            // 
            this.FlagID.DataPropertyName = "FlagID";
            this.FlagID.DataSource = this.flagsBindingSource;
            this.FlagID.DisplayMember = "FlagName";
            this.FlagID.HeaderText = "Flag";
            this.FlagID.Name = "FlagID";
            this.FlagID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FlagID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FlagID.ValueMember = "FlagID";
            this.FlagID.Width = 160;
            // 
            // flagsBindingSource
            // 
            this.flagsBindingSource.DataMember = "flags";
            this.flagsBindingSource.DataSource = this.dataSet1;
            // 
            // songDataGridViewTextBoxColumn
            // 
            this.songDataGridViewTextBoxColumn.DataPropertyName = "Song";
            this.songDataGridViewTextBoxColumn.HeaderText = "Song";
            this.songDataGridViewTextBoxColumn.Name = "songDataGridViewTextBoxColumn";
            // 
            // flaggedSongIDDataGridViewTextBoxColumn
            // 
            this.flaggedSongIDDataGridViewTextBoxColumn.DataPropertyName = "FlaggedSongID";
            this.flaggedSongIDDataGridViewTextBoxColumn.HeaderText = "FlaggedSongID";
            this.flaggedSongIDDataGridViewTextBoxColumn.Name = "flaggedSongIDDataGridViewTextBoxColumn";
            this.flaggedSongIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // flagIDDataGridViewTextBoxColumn
            // 
            this.flagIDDataGridViewTextBoxColumn.DataPropertyName = "FlagID";
            this.flagIDDataGridViewTextBoxColumn.HeaderText = "FlagID";
            this.flagIDDataGridViewTextBoxColumn.Name = "flagIDDataGridViewTextBoxColumn";
            // 
            // flaggedsongsBindingSource
            // 
            this.flaggedsongsBindingSource.DataMember = "flaggedsongs";
            this.flaggedsongsBindingSource.DataSource = this.dataSet1;
            this.flaggedsongsBindingSource.CurrentItemChanged += new System.EventHandler(this.flaggedsongsBindingSource_CurrentItemChanged);
            // 
            // flaggedsongsTableAdapter
            // 
            this.flaggedsongsTableAdapter.ClearBeforeFill = true;
            // 
            // flagsTableAdapter
            // 
            this.flagsTableAdapter.ClearBeforeFill = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 58);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridAltArtists);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridFlags);
            this.splitContainer1.Size = new System.Drawing.Size(466, 304);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 6;
            // 
            // gridAltArtists
            // 
            this.gridAltArtists.AutoGenerateColumns = false;
            this.gridAltArtists.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAltArtists.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArtistIDColumn});
            this.gridAltArtists.DataSource = this.alternateArtistsBindingSource;
            this.gridAltArtists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAltArtists.Location = new System.Drawing.Point(0, 0);
            this.gridAltArtists.Name = "gridAltArtists";
            this.gridAltArtists.Size = new System.Drawing.Size(466, 110);
            this.gridAltArtists.TabIndex = 0;
            // 
            // ArtistIDColumn
            // 
            this.ArtistIDColumn.DataPropertyName = "ArtistID";
            this.ArtistIDColumn.DataSource = this.viewArtistNameForListBoxBindingSource;
            this.ArtistIDColumn.DisplayMember = "Name";
            this.ArtistIDColumn.HeaderText = "Alternate Artist";
            this.ArtistIDColumn.Name = "ArtistIDColumn";
            this.ArtistIDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ArtistIDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ArtistIDColumn.ValueMember = "Artist";
            this.ArtistIDColumn.Width = 300;
            // 
            // viewArtistNameForListBoxBindingSource
            // 
            this.viewArtistNameForListBoxBindingSource.DataMember = "ViewArtistNameForListBox";
            this.viewArtistNameForListBoxBindingSource.DataSource = this.dataSet1;
            this.viewArtistNameForListBoxBindingSource.Sort = "Name";
            // 
            // alternateArtistsBindingSource
            // 
            this.alternateArtistsBindingSource.DataMember = "AlternateArtists";
            this.alternateArtistsBindingSource.DataSource = this.alternateArtistsDataSetBindingSource;
            this.alternateArtistsBindingSource.CurrentItemChanged += new System.EventHandler(this.alternateArtistsBindingSource_CurrentItemChanged);
            // 
            // alternateArtistsDataSetBindingSource
            // 
            this.alternateArtistsDataSetBindingSource.DataSource = this.alternateArtistsDataSet;
            this.alternateArtistsDataSetBindingSource.Position = 0;
            // 
            // alternateArtistsDataSet
            // 
            this.alternateArtistsDataSet.DataSetName = "AlternateArtistsDataSet";
            this.alternateArtistsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // alternateArtistsTableAdapter
            // 
            this.alternateArtistsTableAdapter.ClearBeforeFill = true;
            // 
            // viewArtistNameForListBoxTableAdapter
            // 
            this.viewArtistNameForListBoxTableAdapter.ClearBeforeFill = true;
            // 
            // SongDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(490, 403);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSong);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SongDetailForm";
            this.Text = "Song Detail Form";
            this.Load += new System.EventHandler(this.SongDetailForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SongDetailForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.songsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFlags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flagsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flaggedsongsBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAltArtists)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewArtistNameForListBoxBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alternateArtistsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alternateArtistsDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alternateArtistsDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblArtist;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource songsBindingSource;
        private Songs.DataSet1TableAdapters.songsTableAdapter songsTableAdapter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView gridFlags;
        private System.Windows.Forms.BindingSource flaggedsongsBindingSource;
        private Songs.DataSet1TableAdapters.flaggedsongsTableAdapter flaggedsongsTableAdapter;
        private System.Windows.Forms.BindingSource flagsBindingSource;
        private Songs.DataSet1TableAdapters.flagsTableAdapter flagsTableAdapter;
        private System.Windows.Forms.DataGridViewComboBoxColumn FlagID;
        private System.Windows.Forms.DataGridViewTextBoxColumn songDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flaggedSongIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView gridAltArtists;
        private System.Windows.Forms.BindingSource alternateArtistsDataSetBindingSource;
        private AlternateArtistsDataSet alternateArtistsDataSet;
        private System.Windows.Forms.BindingSource alternateArtistsBindingSource;
        private Songs.AlternateArtistsDataSetTableAdapters.AlternateArtistsTableAdapter alternateArtistsTableAdapter;
        private System.Windows.Forms.BindingSource viewArtistNameForListBoxBindingSource;
        private Songs.DataSet1TableAdapters.ViewArtistNameForListBoxTableAdapter viewArtistNameForListBoxTableAdapter;
        private System.Windows.Forms.DataGridViewComboBoxColumn ArtistIDColumn;
    }
}