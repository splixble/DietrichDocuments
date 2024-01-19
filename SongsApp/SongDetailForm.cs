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

            Utils.AllowNullFields(this.alternateArtistsDataSet.AlternateArtists);
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
            // TODO: This line of code loads data into the 'dataSet1.flaggedsongs' table. You can move, or remove it, as needed.
            this.flaggedsongsTableAdapter.FillBySong(this.dataSet1.flaggedsongs, _SongID);
            // TODO: This line of code loads data into the 'dataSet1.flags' table. You can move, or remove it, as needed.
            this.flagsTableAdapter.Fill(this.dataSet1.flags);
            // TODO: This line of code loads data into the 'dataSet1.ViewArtistNameForListBox' table. You can move, or remove it, as needed.
            this.viewArtistNameForListBoxTableAdapter.Fill(this.dataSet1.ViewArtistNameForListBox);
            // TODO: This line of code loads data into the 'alternateArtistsDataSet.AlternateArtists' table. You can move, or remove it, as needed.
            this.alternateArtistsTableAdapter.FillBySong(this.alternateArtistsDataSet.AlternateArtists, _SongID);
        }

        private void UpdateDB()
        {
            flaggedsongsBindingSource.EndEdit();
            alternateArtistsBindingSource.EndEdit();
            
            // Set SongID on any new Flag records: 
            foreach (DataSet1.flaggedsongsRow row in dataSet1.flaggedsongs)
            {
                if (row[dataSet1.flaggedsongs.SongColumn] == DBNull.Value)
                    row.Song = _SongID;
            }

            // Set SongID on any new Alt Artist records: 
            foreach (AlternateArtistsDataSet.AlternateArtistsRow row in alternateArtistsDataSet.AlternateArtists)
            {
                if (row[alternateArtistsDataSet.AlternateArtists.SongIDColumn] == DBNull.Value)
                    row.SongID = _SongID;
            }

            this.flaggedsongsTableAdapter.Update(dataSet1.flaggedsongs);
            this.alternateArtistsTableAdapter.Update(alternateArtistsDataSet.AlternateArtists);
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

        private void flaggedsongsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            CheckIfDataModified();
        }

        void CheckIfDataModified()
        {
            DataModified = Utils.DataTableIsModified(dataSet1.flaggedsongs) || 
                Utils.DataTableIsModified(alternateArtistsDataSet.AlternateArtists);
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