namespace Songs
{
    partial class SongsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongsForm));
            this.grid1 = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titlePrefixDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArtistColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.viewArtistNameForListBoxBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new Songs.AzureDataSet();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SongKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OriginalKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InTabletColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SetlistAddable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SongbookOnly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiffPDFName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFlags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.detailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.artistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.venuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNoLyricsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listByArtistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listByFlagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performancesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.performancesNewCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.performanceTotalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bandGigsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songsTableAdapter = new Songs.AzureDataSetTableAdapters.songsTableAdapter();
            this.viewArtistNameForListBoxTableAdapter = new Songs.AzureDataSetTableAdapters.ViewArtistNameForListBoxTableAdapter();
            this.cbMemorized = new System.Windows.Forms.CheckBox();
            this.comboFlag = new System.Windows.Forms.ComboBox();
            this.flagsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cbWithFlag = new System.Windows.Forms.CheckBox();
            this.flagsTableAdapter = new Songs.AzureDataSetTableAdapters.flagsTableAdapter();
            this.tbWhereClause = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboSongFinder = new System.Windows.Forms.ComboBox();
            this.viewSongsSingleFieldBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewSongsSingleFieldTableAdapter = new Songs.AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            this.writeDBTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewArtistNameForListBoxBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.songsBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flagsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsSingleFieldBindingSource)).BeginInit();
            this.SuspendLayout();
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
            this.titlePrefixDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.ArtistColumn,
            this.codeDataGridViewTextBoxColumn,
            this.SongKey,
            this.OriginalKey,
            this.pageNumberDataGridViewTextBoxColumn,
            this.InTabletColumn,
            this.SetlistAddable,
            this.commentDataGridViewTextBoxColumn,
            this.SongbookOnly,
            this.DiffPDFName,
            this.ColFlags,
            this.categoryDataGridViewTextBoxColumn});
            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
            this.grid1.DataSource = this.songsBindingSource;
            this.grid1.Location = new System.Drawing.Point(12, 79);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(1316, 249);
            this.grid1.TabIndex = 0;
            this.grid1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid1_CellContentDoubleClick);
            this.grid1.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.grid1_CellContextMenuStripNeeded);
            this.grid1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid1_ColumnHeaderMouseClick);
            this.grid1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.grid1_SortCompare);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 40;
            // 
            // titlePrefixDataGridViewTextBoxColumn
            // 
            this.titlePrefixDataGridViewTextBoxColumn.DataPropertyName = "TitlePrefix";
            this.titlePrefixDataGridViewTextBoxColumn.HeaderText = "Title Prefix";
            this.titlePrefixDataGridViewTextBoxColumn.Name = "titlePrefixDataGridViewTextBoxColumn";
            this.titlePrefixDataGridViewTextBoxColumn.Width = 40;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.Width = 240;
            // 
            // ArtistColumn
            // 
            this.ArtistColumn.DataPropertyName = "Artist";
            this.ArtistColumn.DataSource = this.viewArtistNameForListBoxBindingSource;
            this.ArtistColumn.DisplayMember = "Name";
            this.ArtistColumn.HeaderText = "Artist";
            this.ArtistColumn.Name = "ArtistColumn";
            this.ArtistColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ArtistColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.ArtistColumn.ValueMember = "Artist";
            this.ArtistColumn.Width = 180;
            // 
            // viewArtistNameForListBoxBindingSource
            // 
            this.viewArtistNameForListBoxBindingSource.DataMember = "ViewArtistNameForListBox";
            this.viewArtistNameForListBoxBindingSource.DataSource = this.dataSet1;
            this.viewArtistNameForListBoxBindingSource.Sort = "Name";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.Width = 40;
            // 
            // SongKey
            // 
            this.SongKey.DataPropertyName = "SongKey";
            this.SongKey.HeaderText = "Key";
            this.SongKey.Name = "SongKey";
            this.SongKey.Width = 40;
            // 
            // OriginalKey
            // 
            this.OriginalKey.DataPropertyName = "OriginalKey";
            this.OriginalKey.HeaderText = "Orig. Key";
            this.OriginalKey.Name = "OriginalKey";
            this.OriginalKey.Width = 40;
            // 
            // pageNumberDataGridViewTextBoxColumn
            // 
            this.pageNumberDataGridViewTextBoxColumn.DataPropertyName = "PageNumber";
            this.pageNumberDataGridViewTextBoxColumn.HeaderText = "Page #";
            this.pageNumberDataGridViewTextBoxColumn.Name = "pageNumberDataGridViewTextBoxColumn";
            this.pageNumberDataGridViewTextBoxColumn.Width = 40;
            // 
            // InTabletColumn
            // 
            this.InTabletColumn.DataPropertyName = "InTablet";
            this.InTabletColumn.FalseValue = "False";
            this.InTabletColumn.HeaderText = "In Tablet";
            this.InTabletColumn.Name = "InTabletColumn";
            this.InTabletColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.InTabletColumn.TrueValue = "True";
            this.InTabletColumn.Width = 40;
            // 
            // SetlistAddable
            // 
            this.SetlistAddable.DataPropertyName = "SetlistAddable";
            this.SetlistAddable.FalseValue = "False";
            this.SetlistAddable.HeaderText = "Setlist Add?";
            this.SetlistAddable.Name = "SetlistAddable";
            this.SetlistAddable.TrueValue = "True";
            this.SetlistAddable.Width = 40;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 180;
            // 
            // SongbookOnly
            // 
            this.SongbookOnly.DataPropertyName = "SongbookOnly";
            this.SongbookOnly.HeaderText = "Book Only";
            this.SongbookOnly.Name = "SongbookOnly";
            this.SongbookOnly.Width = 40;
            // 
            // DiffPDFName
            // 
            this.DiffPDFName.DataPropertyName = "DiffPDFName";
            this.DiffPDFName.HeaderText = "Diff. PDF Name";
            this.DiffPDFName.Name = "DiffPDFName";
            this.DiffPDFName.Width = 130;
            // 
            // ColFlags
            // 
            this.ColFlags.HeaderText = "Flags";
            this.ColFlags.Name = "ColFlags";
            this.ColFlags.ReadOnly = true;
            this.ColFlags.Width = 120;
            // 
            // categoryDataGridViewTextBoxColumn
            // 
            this.categoryDataGridViewTextBoxColumn.DataPropertyName = "Category";
            this.categoryDataGridViewTextBoxColumn.HeaderText = "Category";
            this.categoryDataGridViewTextBoxColumn.Name = "categoryDataGridViewTextBoxColumn";
            this.categoryDataGridViewTextBoxColumn.Width = 40;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 26);
            // 
            // detailToolStripMenuItem
            // 
            this.detailToolStripMenuItem.Name = "detailToolStripMenuItem";
            this.detailToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.detailToolStripMenuItem.Text = "&Detail";
            this.detailToolStripMenuItem.Click += new System.EventHandler(this.detailToolStripMenuItem_Click);
            // 
            // songsBindingSource
            // 
            this.songsBindingSource.DataMember = "songs";
            this.songsBindingSource.DataSource = this.dataSet1;
            this.songsBindingSource.CurrentChanged += new System.EventHandler(this.songsBindingSource_CurrentItemChanged);
            this.songsBindingSource.CurrentItemChanged += new System.EventHandler(this.songsBindingSource_CurrentItemChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Location = new System.Drawing.Point(1253, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.tablesToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.websiteToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1340, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // tablesToolStripMenuItem
            // 
            this.tablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.artistsToolStripMenuItem,
            this.flagsToolStripMenuItem,
            this.performancesToolStripMenuItem,
            this.venuesToolStripMenuItem});
            this.tablesToolStripMenuItem.Name = "tablesToolStripMenuItem";
            this.tablesToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.tablesToolStripMenuItem.Text = "&Tables";
            // 
            // artistsToolStripMenuItem
            // 
            this.artistsToolStripMenuItem.Name = "artistsToolStripMenuItem";
            this.artistsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.artistsToolStripMenuItem.Text = "&Artists";
            this.artistsToolStripMenuItem.Click += new System.EventHandler(this.artistsToolStripMenuItem_Click);
            // 
            // flagsToolStripMenuItem
            // 
            this.flagsToolStripMenuItem.Name = "flagsToolStripMenuItem";
            this.flagsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.flagsToolStripMenuItem.Text = "&Flags";
            this.flagsToolStripMenuItem.Click += new System.EventHandler(this.flagsToolStripMenuItem_Click);
            // 
            // performancesToolStripMenuItem
            // 
            this.performancesToolStripMenuItem.Name = "performancesToolStripMenuItem";
            this.performancesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.performancesToolStripMenuItem.Text = "&Performances";
            this.performancesToolStripMenuItem.Click += new System.EventHandler(this.performancesToolStripMenuItem_Click);
            // 
            // venuesToolStripMenuItem
            // 
            this.venuesToolStripMenuItem.Name = "venuesToolStripMenuItem";
            this.venuesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.venuesToolStripMenuItem.Text = "&Venues";
            this.venuesToolStripMenuItem.Click += new System.EventHandler(this.venuesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pDFToolStripMenuItem,
            this.createNoLyricsListToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "T&ools";
            // 
            // pDFToolStripMenuItem
            // 
            this.pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pDFToolStripMenuItem.Text = "PDF";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);
            // 
            // createNoLyricsListToolStripMenuItem
            // 
            this.createNoLyricsListToolStripMenuItem.Name = "createNoLyricsListToolStripMenuItem";
            this.createNoLyricsListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createNoLyricsListToolStripMenuItem.Text = "Create No Lyrics List";
            this.createNoLyricsListToolStripMenuItem.Click += new System.EventHandler(this.createNoLyricsListToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tOCToolStripMenuItem,
            this.listToolStripMenuItem,
            this.listByArtistToolStripMenuItem,
            this.listByFlagsToolStripMenuItem,
            this.performancesToolStripMenuItem1,
            this.performancesNewCommandToolStripMenuItem,
            this.performanceTotalsToolStripMenuItem,
            this.bandGigsToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.reportToolStripMenuItem.Text = "&Report";
            // 
            // tOCToolStripMenuItem
            // 
            this.tOCToolStripMenuItem.Name = "tOCToolStripMenuItem";
            this.tOCToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.tOCToolStripMenuItem.Text = "&TOC";
            this.tOCToolStripMenuItem.Click += new System.EventHandler(this.tOCToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.listToolStripMenuItem.Text = "&List";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // listByArtistToolStripMenuItem
            // 
            this.listByArtistToolStripMenuItem.Name = "listByArtistToolStripMenuItem";
            this.listByArtistToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.listByArtistToolStripMenuItem.Text = "List by &Artist";
            this.listByArtistToolStripMenuItem.Click += new System.EventHandler(this.listByArtistToolStripMenuItem_Click);
            // 
            // listByFlagsToolStripMenuItem
            // 
            this.listByFlagsToolStripMenuItem.Name = "listByFlagsToolStripMenuItem";
            this.listByFlagsToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.listByFlagsToolStripMenuItem.Text = "List by &Flags";
            this.listByFlagsToolStripMenuItem.Click += new System.EventHandler(this.listByFlagsToolStripMenuItem_Click);
            // 
            // performancesToolStripMenuItem1
            // 
            this.performancesToolStripMenuItem1.Name = "performancesToolStripMenuItem1";
            this.performancesToolStripMenuItem1.Size = new System.Drawing.Size(234, 22);
            this.performancesToolStripMenuItem1.Text = "&Performances OLD";
            this.performancesToolStripMenuItem1.Click += new System.EventHandler(this.performancesListingToolStripMenuItem_Click);
            // 
            // performancesNewCommandToolStripMenuItem
            // 
            this.performancesNewCommandToolStripMenuItem.Name = "performancesNewCommandToolStripMenuItem";
            this.performancesNewCommandToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.performancesNewCommandToolStripMenuItem.Text = "&Performances New Command";
            this.performancesNewCommandToolStripMenuItem.Click += new System.EventHandler(this.performancesNewCommandToolStripMenuItem_Click);
            // 
            // performanceTotalsToolStripMenuItem
            // 
            this.performanceTotalsToolStripMenuItem.Name = "performanceTotalsToolStripMenuItem";
            this.performanceTotalsToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.performanceTotalsToolStripMenuItem.Text = "Performance Tota&ls";
            this.performanceTotalsToolStripMenuItem.Click += new System.EventHandler(this.performanceTotalsToolStripMenuItem_Click);
            // 
            // bandGigsToolStripMenuItem
            // 
            this.bandGigsToolStripMenuItem.Name = "bandGigsToolStripMenuItem";
            this.bandGigsToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.bandGigsToolStripMenuItem.Text = "&Band Gigs";
            this.bandGigsToolStripMenuItem.Click += new System.EventHandler(this.bandGigsToolStripMenuItem_Click);
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.writeDBTablesToolStripMenuItem});
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.websiteToolStripMenuItem.Text = "&Website";
            // 
            // songsTableAdapter
            // 
            this.songsTableAdapter.ClearBeforeFill = true;
            // 
            // viewArtistNameForListBoxTableAdapter
            // 
            this.viewArtistNameForListBoxTableAdapter.ClearBeforeFill = true;
            // 
            // cbMemorized
            // 
            this.cbMemorized.AutoSize = true;
            this.cbMemorized.Checked = true;
            this.cbMemorized.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.cbMemorized.ForeColor = System.Drawing.Color.White;
            this.cbMemorized.Location = new System.Drawing.Point(12, 29);
            this.cbMemorized.Name = "cbMemorized";
            this.cbMemorized.Size = new System.Drawing.Size(77, 17);
            this.cbMemorized.TabIndex = 3;
            this.cbMemorized.Text = "Memorized";
            this.cbMemorized.ThreeState = true;
            this.cbMemorized.UseVisualStyleBackColor = true;
            this.cbMemorized.CheckStateChanged += new System.EventHandler(this.cbMemorized_CheckStateChanged);
            // 
            // comboFlag
            // 
            this.comboFlag.DataSource = this.flagsBindingSource;
            this.comboFlag.DisplayMember = "FlagName";
            this.comboFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFlag.Enabled = false;
            this.comboFlag.FormattingEnabled = true;
            this.comboFlag.Location = new System.Drawing.Point(178, 27);
            this.comboFlag.Name = "comboFlag";
            this.comboFlag.Size = new System.Drawing.Size(142, 21);
            this.comboFlag.TabIndex = 4;
            this.comboFlag.ValueMember = "FlagID";
            this.comboFlag.SelectionChangeCommitted += new System.EventHandler(this.comboFlag_SelectionChangeCommitted);
            // 
            // flagsBindingSource
            // 
            this.flagsBindingSource.DataMember = "flags";
            this.flagsBindingSource.DataSource = this.dataSet1;
            // 
            // cbWithFlag
            // 
            this.cbWithFlag.AutoSize = true;
            this.cbWithFlag.ForeColor = System.Drawing.Color.White;
            this.cbWithFlag.Location = new System.Drawing.Point(105, 29);
            this.cbWithFlag.Name = "cbWithFlag";
            this.cbWithFlag.Size = new System.Drawing.Size(71, 17);
            this.cbWithFlag.TabIndex = 6;
            this.cbWithFlag.Text = "With flag:";
            this.cbWithFlag.UseVisualStyleBackColor = true;
            this.cbWithFlag.CheckedChanged += new System.EventHandler(this.cbWithFlag_CheckedChanged);
            // 
            // flagsTableAdapter
            // 
            this.flagsTableAdapter.ClearBeforeFill = true;
            // 
            // tbWhereClause
            // 
            this.tbWhereClause.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWhereClause.Location = new System.Drawing.Point(338, 27);
            this.tbWhereClause.Name = "tbWhereClause";
            this.tbWhereClause.Size = new System.Drawing.Size(990, 20);
            this.tbWhereClause.TabIndex = 7;
            this.tbWhereClause.Validated += new System.EventHandler(this.tbWhereClause_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Find Song:";
            // 
            // comboSongFinder
            // 
            this.comboSongFinder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboSongFinder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboSongFinder.DataSource = this.viewSongsSingleFieldBindingSource;
            this.comboSongFinder.DisplayMember = "TitleAndArtist";
            this.comboSongFinder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSongFinder.FormattingEnabled = true;
            this.comboSongFinder.Location = new System.Drawing.Point(76, 52);
            this.comboSongFinder.Name = "comboSongFinder";
            this.comboSongFinder.Size = new System.Drawing.Size(475, 21);
            this.comboSongFinder.TabIndex = 9;
            this.comboSongFinder.ValueMember = "ID";
            this.comboSongFinder.SelectionChangeCommitted += new System.EventHandler(this.comboSongFinder_SelectionChangeCommitted);
            // 
            // viewSongsSingleFieldBindingSource
            // 
            this.viewSongsSingleFieldBindingSource.DataMember = "ViewSongsSingleField";
            this.viewSongsSingleFieldBindingSource.DataSource = this.dataSet1;
            // 
            // viewSongsSingleFieldTableAdapter
            // 
            this.viewSongsSingleFieldTableAdapter.ClearBeforeFill = true;
            // 
            // writeDBTablesToolStripMenuItem
            // 
            this.writeDBTablesToolStripMenuItem.Name = "writeDBTablesToolStripMenuItem";
            this.writeDBTablesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.writeDBTablesToolStripMenuItem.Text = "Write DB &Tables";
            this.writeDBTablesToolStripMenuItem.Click += new System.EventHandler(this.writeDBTablesToolStripMenuItem_Click);
            // 
            // SongsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1340, 369);
            this.Controls.Add(this.comboSongFinder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbWhereClause);
            this.Controls.Add(this.cbWithFlag);
            this.Controls.Add(this.comboFlag);
            this.Controls.Add(this.cbMemorized);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SongsForm";
            this.Text = "Songs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SongsForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewArtistNameForListBoxBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.songsBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flagsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSongsSingleFieldBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid1;
        private AzureDataSet dataSet1;
        private Songs.AzureDataSetTableAdapters.songsTableAdapter songsTableAdapter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem artistsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flagsToolStripMenuItem;
        private System.Windows.Forms.BindingSource viewArtistNameForListBoxBindingSource;
        private Songs.AzureDataSetTableAdapters.ViewArtistNameForListBoxTableAdapter viewArtistNameForListBoxTableAdapter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tOCToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbMemorized;
        private System.Windows.Forms.ComboBox comboFlag;
        private System.Windows.Forms.CheckBox cbWithFlag;
        private System.Windows.Forms.BindingSource flagsBindingSource;
        private Songs.AzureDataSetTableAdapters.flagsTableAdapter flagsTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.TextBox tbWhereClause;
        private System.Windows.Forms.ToolStripMenuItem performancesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem performancesToolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboSongFinder;
        private System.Windows.Forms.BindingSource viewSongsSingleFieldBindingSource;
        private Songs.AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter viewSongsSingleFieldTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem listByArtistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listByFlagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem performanceTotalsToolStripMenuItem;
        private System.Windows.Forms.BindingSource songsBindingSource;
        private System.Windows.Forms.ToolStripMenuItem bandGigsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNoLyricsListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem venuesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titlePrefixDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ArtistColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SongKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn OriginalKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn pageNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn InTabletColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SetlistAddable;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SongbookOnly;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiffPDFName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFlags;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem performancesNewCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeDBTablesToolStripMenuItem;
    }
}

