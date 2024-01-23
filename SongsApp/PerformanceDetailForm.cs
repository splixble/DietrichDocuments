using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Songs
{
    public partial class PerformanceDetailForm : Form
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

        int _PerfID = -1;

        public PerformanceDetailForm()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(int perfID, string venueName)
        {
            _PerfID = perfID;
            lblVenue.Text = venueName;
            return ShowDialog();
        }

        private void PerformanceDetailForm_Load(object sender, EventArgs e)
        {
            Redraw();
            DataModified = false;
        }

        public void Redraw()
        {
            // TODO: This line of code loads data into the 'dataSet1.ViewSongsSingleField' table. You can move, or remove it, as needed.
            this.viewSongsSingleFieldTableAdapter.Fill(this.dataSet1.ViewSongsSingleField);
            // TODO: This line of code loads data into the 'AzureDataSet.performances' table. You can move, or remove it, as needed.
            this.performancesTableAdapter.FillByID(this.AzureDataSet.performances, _PerfID);
            // TODO: This line of code loads data into the 'AzureDataSet.songperformances' table. You can move, or remove it, as needed.
            this.songperformancesTableAdapter.FillByPerformance(this.AzureDataSet.songperformances, _PerfID);
        }

        private void UpdateDB()
        {
            songperformancesBindingSource.EndEdit();

            // Set Performance on any new records: 
            foreach (AzureDataSet.songperformancesRow row in AzureDataSet.songperformances)
            {
                if (row[this.AzureDataSet.songperformances.PerformanceColumn] == DBNull.Value)
                    row.Performance = _PerfID;
            }

            this.songperformancesTableAdapter.Update(AzureDataSet.songperformances);
            DataModified = false;
        }

        private void PerformanceDetailForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void songperformancesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(AzureDataSet.songperformances);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
            DialogResult = DialogResult.OK;
        }
    }
}
 