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
            _PerformancesAdap = new OdbcDataAdapter("", global::Songs.Properties.Settings.Default.AzureConnectionString);
        }

        private void PerformancesForm_Load(object sender, EventArgs e)
        {
            Redraw();
            DataModified = false;

            this.AzureDataSet.performances.TableNewRow += Performances_TableNewRow;
        }

        private void Performances_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row[AzureDataSet.performances.DidILeadColumn] = true;
        }

        void Redraw()
        {
            // TODO: This line of code loads data into the 'AzureDataSet.venues' table. You can move, or remove it, as needed.
            this.venuesTableAdapter.Fill(this.AzureDataSet.venues);
            // TODO: This line of code loads data into the 'AzureDataSet.performances' table. You can move, or remove it, as needed.
            this.performancesTableAdapter.Fill(this.AzureDataSet.performances);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private void UpdateDB()
        {
            performancesBindingSource.EndEdit();
            this.performancesTableAdapter.Update(this.AzureDataSet.performances);
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
            if (obRowItem is DataRowView && ((DataRowView)obRowItem).Row is AzureDataSet.performancesRow)
            {
                AzureDataSet.performancesRow perfRow =
                    (AzureDataSet.performancesRow)((DataRowView)obRowItem).Row;
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
            DataModified = Utils.DataTableIsModified(AzureDataSet.performances);
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

        private void performancesBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}