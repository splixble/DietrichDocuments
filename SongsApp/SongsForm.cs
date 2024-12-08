using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Songs
{
    public partial class SongsForm : Form
    {
        SqlDataAdapter _SongsAdap;

        int _ContextMenuRow = -1;
        int _ContextMenuColumn = -1;

        bool _Initialized = false;

        bool _DataModified = false;
        bool DataModified
        {
            get { return _DataModified; }
            set
            {
                _DataModified = value;
                btnSave.Enabled = _DataModified;
            }
        }

        public SongsForm()
        {
            InitializeComponent();
            _SongsAdap = new SqlDataAdapter("",  global::Songs.Properties.Settings.Default.LocalSpeepmasterSongbookConn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.flags' table. You can move, or remove it, as needed.
            this.flagsTableAdapter.Fill(this.dataSet1.flags);

            Redraw();
            _Initialized = true;
            DataModified = false;

            // show version
            this.Text = "Songs App - v. " + Application.ProductVersion;
        }

        void Redraw()
        {
            // Get positioned-on song, so we can go back to that after:
            int currentSongID = -1;
            if (grid1.CurrentRow != null)
                currentSongID = GetSongIDFromRowIndex(grid1.CurrentRow.Index);

            // TODO: This line of code loads data into the 'dataSet1.ViewArtistNameForListBox' table. You can move, or remove it, as needed.
            this.viewArtistNameForListBoxTableAdapter.Fill(this.dataSet1.ViewArtistNameForListBox);
            // TODO: This line of code loads data into the 'viewSongsDataSet.ViewSongsSingleField' table. You can move, or remove it, as needed.
            this.viewSongsSingleFieldTableAdapter.Fill(this.dataSet1.ViewSongsSingleField);

            // load songs table:
            dataSet1.songs.Clear();
            _SongsAdap.SelectCommand.CommandText =
                "SELECT TitlePrefix, Title, Code, SongKey, OriginalKey, Comment, PageNumber, Category, Song" +
                "bookOnly, ID, Artist, InTablet, SetlistAddable, DiffPDFName FROM songbook.songs " + tbWhereClause.Text;
            // NOTE: when I moved db from Azure back to local on 12/6/24, had to change "FROM songs" to "FROM songbook.songs" --
            // dont know why it worked before
            _SongsAdap.Fill(dataSet1.songs);
            // was: this.songsTableAdapter.Fill(this.dataSet1.songs);
            grid1.Sort(titleDataGridViewTextBoxColumn, ListSortDirection.Ascending);

            // Read in the SongFlags view:
            AzureDataSetTableAdapters.viewsongflagsTableAdapter flagAdap = 
                new Songs.AzureDataSetTableAdapters.viewsongflagsTableAdapter();
            AzureDataSet.viewsongflagsDataTable flagsTable = 
                new AzureDataSet.viewsongflagsDataTable();
            flagAdap.Fill(flagsTable);
            // now, fill in the Flags field:
            for (int rowIndex=0; rowIndex<grid1.RowCount; rowIndex++)
            {
                int songID = GetSongIDFromRowIndex(rowIndex);
                if (songID != -1)
                {
                    AzureDataSet.viewsongflagsRow flagsRow = flagsTable.FindBySong(songID);
                    if (flagsRow != null && flagsRow["Flags"] != DBNull.Value)
                    {
                        grid1[ColFlags.Index, rowIndex].Value = flagsRow.Flags;
                    }
                }
            }

            // position back on the last song:
            if (currentSongID != -1)
            {
                int rowIndex = GetRowIndexFromSongID(currentSongID);
                if (rowIndex != -1)
                    grid1.FirstDisplayedScrollingRowIndex = rowIndex;
            }

        }

        void ConstructQueryString()
        {
            //"SELECT TitlePrefix, Title, Code, SongKey, Comment, PageNumber, Category, SongbookOnly, ID, Artist ";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private void UpdateDB()
        {
            songsBindingSource.EndEdit();
            this.songsTableAdapter.Update(this.dataSet1.songs);
            DataModified = false;
            Redraw();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redraw();
        }

        private void artistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArtistsForm form = new ArtistsForm();
            form.Show();
        }

        private void flagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlagsForm form = new FlagsForm();
            form.Show();
        }

        private void grid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowDetailForm(e.RowIndex);
        }

        int GetRowIndexFromSongID(int songID)
        {
            // cant search by primary key... because this view may have dup IDs for cover songs
            foreach (DataGridViewRow gridRow in grid1.Rows)
            {
                object obRowItem = gridRow.DataBoundItem;
                if (obRowItem is DataRowView && ((DataRowView)obRowItem).Row is AzureDataSet.songsRow)
                {
                    AzureDataSet.songsRow songRow = (AzureDataSet.songsRow)((DataRowView)obRowItem).Row;
                    if (songRow.ID == songID)
                        return gridRow.Index;
                }
            }
            return -1; // if not found
        }

        int GetSongIDFromRowIndex(int rowIndex)
        {
            // Get the DB table row corresponding to this grid row:
            object obRowItem = grid1.Rows[rowIndex].DataBoundItem;
            if (obRowItem is DataRowView && ((DataRowView)obRowItem).Row is AzureDataSet.songsRow)
            {
                AzureDataSet.songsRow songRow =
                    (AzureDataSet.songsRow)((DataRowView)obRowItem).Row;
                return songRow.ID;
            }
            else
                return -1;
        }

        void ShowDetailForm(int rowIndex)
        {
            if (DataModified)
            {
                DialogResult saveDiaRes = MessageBox.Show("Save changes so far?",
                    "Changes must be saved before entering detail form", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);
                if (saveDiaRes == DialogResult.Cancel)
                    return;
                else if (saveDiaRes == DialogResult.Yes)
                    UpdateDB();
            }

            int songID = GetSongIDFromRowIndex(rowIndex);
            if (songID != -1)
            {
                string artistName = grid1[ArtistColumn.Index, rowIndex].FormattedValue as string;

                SongDetailForm detailForm = new SongDetailForm();
                DialogResult diaRes = detailForm.ShowDialog(songID, artistName);
                //``if diares = OK...??
            }
        }

        private void SongsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DataModified)
            {
                DialogResult diaRes = MessageBox.Show("Save changes before closing?",
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (diaRes == DialogResult.Cancel)
                    e.Cancel = true;
                else if (diaRes == DialogResult.Yes)
                    UpdateDB();
            }
        }

        private void songsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(dataSet1.songs);
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetailForm(_ContextMenuRow);
        }

        private void grid1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            _ContextMenuRow = e.RowIndex;
            _ContextMenuColumn = e.ColumnIndex;
        }

        private void tOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowTOC();
        }

        string GetSongsWhereClause()
        {
            List<string> clauses = new List<string>();

            // memorized? 
            if (cbMemorized.CheckState == CheckState.Checked)
                clauses.Add("Code = 'a'");
            else if (cbMemorized.CheckState == CheckState.Unchecked)
                clauses.Add("IFNULL(Code, 'NULL') <> 'a'");

            // flags?
            if (cbWithFlag.Checked && comboFlag.SelectedValue != DBNull.Value)
            {
                clauses.Add("ID in (SELECT Song FROM FlaggedSongs WHERE FlagID = " + comboFlag.SelectedValue.ToString() + ")");
            }

            string clauseString = "";
            foreach (string clause in clauses)
            {
                if (clauseString == "")
                    clauseString += " WHERE ";
                else
                    clauseString += " AND ";
                clauseString += "(" + clause + ")";
            }
            return clauseString;
        }

        private void cbMemorized_CheckStateChanged(object sender, EventArgs e)
        {
            if (_Initialized)
                SetWhereClause();
        }

        private void cbWithFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (_Initialized)
            {
                comboFlag.Enabled = cbWithFlag.Checked;
                SetWhereClause();
            }
        }

        private void comboFlag_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_Initialized)
                SetWhereClause();
        }

        private void tbWhereClause_Validated(object sender, EventArgs e)
        {
            Redraw();
        }

        void SetWhereClause()
        {
            tbWhereClause.Text = GetSongsWhereClause();
            Redraw();
        }

        private void grid1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column == ArtistColumn)
            {
                // special case -- gotta sort by artist 1st name and last name
                if (e.CellValue1 == null)
                {
                    if (e.CellValue2 == null)
                        e.SortResult = 0;
                    else
                        e.SortResult = -1;
                }
                else
                {
                    if (e.CellValue2 == null)
                        e.SortResult = 1;
                    else
                    {
                        AzureDataSet.artistsRow artistRow1 = dataSet1.artists.FindByArtistID((int)e.CellValue1);
                        AzureDataSet.artistsRow artistRow2 = dataSet1.artists.FindByArtistID((int)e.CellValue2);
                        string lastname1 = ComparableString(artistRow1.ArtistLastName);
                        string lastname2 = ComparableString(artistRow2.ArtistLastName);
                        e.SortResult = lastname1.CompareTo(lastname2);
                        if (e.SortResult == 0)
                        {
                            string firstname1 = ComparableString(artistRow1.ArtistFirstName);
                            string firstname2 = ComparableString(artistRow2.ArtistFirstName);
                            e.SortResult = lastname1.CompareTo(firstname2);
                        }
                    }
                }
                e.Handled = true;
            }
        }

        string ComparableString(object fieldValue)
        {
            if (fieldValue is string)
                return (String)fieldValue;
            else
                return "";
        }

        int ArtistSortOrderToggle = 0;
        private void grid1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == ArtistColumn.Index)
            {
                if (ArtistSortOrderToggle == 0)
                    ArtistSortOrderToggle = 1;
                else
                    ArtistSortOrderToggle = -ArtistSortOrderToggle;
                //```grid1.Sort(new ArtistColumnComparer(ArtistSortOrderToggle));
            }
            else
                ArtistSortOrderToggle = 0;
        }

        private void performancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformancesForm form = new PerformancesForm();
            form.Show();
        }

        private void performancesListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowPerformanceList(true);
        }

        private void comboSongFinder_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Set current row to the row containing this song:
            if (comboSongFinder.SelectedValue is int)
            {
                int selectedSongID = (int)comboSongFinder.SelectedValue;
                for (int rowIndex = 0; rowIndex < grid1.RowCount; rowIndex++)
                {
                    if (selectedSongID == GetSongIDFromRowIndex(rowIndex))
                    {
                        grid1.CurrentCell = grid1[titleDataGridViewTextBoxColumn.Index, rowIndex];
                        return;
                    }
                }
            }
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowList(tbWhereClause.Text);
        }

        private void listByArtistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowListByArtist(tbWhereClause.Text);
        }

        private void listByFlagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowListByEachFlag();
        }

        private void performanceTotalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowPerformanceTotalsList();
        }

        private void songsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bandGigsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListingForm form = new ListingForm();
            form.ShowPerformanceList(false);
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfProcessor proc = new PdfProcessor();            
            proc.ProcessPDFs();
        }

        private void createNoLyricsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PdfProcessor pdfProc = new PdfProcessor();
            pdfProc.CreateNoLyricFiles();
        }

        private void venuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VenuesForm form = new VenuesForm();
            form.Show();
        }

    }
}