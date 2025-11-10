using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Songs
{
    public partial class SongDetailForm : Form
    {
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

        int _SongID = -1;

        public SongDetailForm()
        {
            InitializeComponent();

            Utils.AllowNullFields(this.dataSet.AlternateArtists); // for newly added rows; FK will be filled in on save
            Utils.AllowNullFields(dataSet1.SongsFlagged); // for newly added rows; FK will be filled in on save
        }

        public DialogResult ShowDialog(int songID, string artistName)
        {
            _SongID = songID;
            lblArtist.Text = artistName;
            return ShowDialog();
        }

        private void SongDetailForm_Load(object sender, EventArgs e)
        {
            Redraw();
            DataModified = false;
        }

        public void Redraw()
        {
            // TODO: This line of code loads data into the 'dataSet1.songs' table. You can move, or remove it, as needed.
            this.songsTableAdapter.FillByID(this.dataSet1.songs, _SongID);
            // TODO: This line of code loads data into the 'dataSet1.SongsFlagged' table. You can move, or remove it, as needed.
            this.SongsFlaggedTableAdapter.FillBySong(this.dataSet1.SongsFlagged, _SongID);
            // TODO: This line of code loads data into the 'dataSet1.flags' table. You can move, or remove it, as needed.
            this.flagsTableAdapter.Fill(this.dataSet1.flags);
            // TODO: This line of code loads data into the 'dataSet1.ViewArtistNameForListBox' table. You can move, or remove it, as needed.
            this.viewArtistNameForListBoxTableAdapter.Fill(this.dataSet1.ViewArtistNameForListBox);
            // TODO: This line of code loads data into the 'alternateArtistsDataSet.AlternateArtists' table. You can move, or remove it, as needed.
            this.alternateArtistsTableAdapter.FillBySong(this.dataSet.AlternateArtists, _SongID);
        }

        private void UpdateDB()
        {
            SongsFlaggedBindingSource.EndEdit();
            alternateArtistsBindingSource.EndEdit();
            
            // Set SongID on any new Flag records: 
            foreach (AzureDataSet.SongsFlaggedRow row in dataSet1.SongsFlagged)
            {
                if (row[dataSet1.SongsFlagged.SongIDColumn] == DBNull.Value)
                    row.SongID = _SongID;
            }

            // Set SongID on any new Alt Artist records: 
            foreach (AzureDataSet.AlternateArtistsRow row in dataSet.AlternateArtists)
            {
                if (row[dataSet.AlternateArtists.SongIDColumn] == DBNull.Value)
                    row.SongID = _SongID;
            }

            this.SongsFlaggedTableAdapter.Update(dataSet1.SongsFlagged);
            this.alternateArtistsTableAdapter.Update(dataSet.AlternateArtists);
            DataModified = false;
        }

        private void SongDetailForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void SongsFlaggedBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            CheckIfDataModified();
        }

        void CheckIfDataModified()
        {
            DataModified = Utils.DataTableIsModified(dataSet1.SongsFlagged) || 
                Utils.DataTableIsModified(dataSet.AlternateArtists);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
            DialogResult = DialogResult.OK;
        }

        private void alternateArtistsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            CheckIfDataModified();
        }
    }
}