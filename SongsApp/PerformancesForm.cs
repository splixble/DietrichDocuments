using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Songs
{
    public partial class PerformancesForm : Form
    {
        OdbcDataAdapter _PerformancesAdap;

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

        public PerformancesForm()
        {
            InitializeComponent();
            _PerformancesAdap = new OdbcDataAdapter("", global::Songs.Properties.Settings.Default.MainConnectionString);
        }

        private void PerformancesForm_Load(object sender, EventArgs e)
        {
            Redraw();
            _Initialized = true;
            DataModified = false;
        }

        void Redraw()
        {
            // TODO: This line of code loads data into the 'performanceDataSet.venues' table. You can move, or remove it, as needed.
            this.venuesTableAdapter.Fill(this.performanceDataSet.venues);
            // TODO: This line of code loads data into the 'performanceDataSet.performances' table. You can move, or remove it, as needed.
            this.performancesTableAdapter.Fill(this.performanceDataSet.performances);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private void UpdateDB()
        {
            performancesBindingSource.EndEdit();
            this.performancesTableAdapter.Update(this.performanceDataSet.performances);
            DataModified = false;
            Redraw();
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDetailForm(_ContextMenuRow);
        }

        int GetPerformanceIDFromRowIndex(int rowIndex)
        {
            // Get the DB table row corresponding to this grid row:
            object obRowItem = grid1.Rows[rowIndex].DataBoundItem;
            if (obRowItem is DataRowView && ((DataRowView)obRowItem).Row is PerformanceDataSet.performancesRow)
            {
                PerformanceDataSet.performancesRow perfRow =
                    (PerformanceDataSet.performancesRow)((DataRowView)obRowItem).Row;
                return perfRow.ID;
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

            int songID = GetPerformanceIDFromRowIndex(rowIndex);
            if (songID != -1)
            {
                string venueName = grid1[VenueColumn.Index, rowIndex].FormattedValue as string;
                PerformanceDetailForm detailForm = new PerformanceDetailForm();
                DialogResult diaRes = detailForm.ShowDialog(songID, venueName);
                //``if diares = OK...??
            }
        }

        private void PerformancesForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void performancesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(performanceDataSet.performances);
        }

        private void grid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowDetailForm(e.RowIndex);
        }

        private void grid1_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            _ContextMenuRow = e.RowIndex;
            _ContextMenuColumn = e.ColumnIndex;
        }
    }
}