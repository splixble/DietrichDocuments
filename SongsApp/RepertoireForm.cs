using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Songs
{
    public partial class RepertoireForm : Form
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

        public RepertoireForm()
        {
            InitializeComponent();

            azureDataSet.Repertoire.TableNewRow += Repertoire_TableNewRow;
        }

        private void Repertoire_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            ((AzureDataSet.RepertoireRow)e.Row).Band = (int)comboBoxBand.SelectedValue;
        }

        private void RepertoireForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'azureDataSet.bands' table. You can move, or remove it, as needed.
            this.bandsTableAdapter.Fill(this.azureDataSet.bands);
            // TODO: This line of code loads data into the 'azureDataSet.ViewSongsSingleField' table. You can move, or remove it, as needed.
            this.viewSongsSingleFieldTableAdapter.Fill(this.azureDataSet.ViewSongsSingleField);

            if (comboBoxBand.SelectedValue is int)
                LoadRepertoireRecsOfBand();

            DataModified = false;
        }
        //             if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) // if not in Designer mode

        private void comboBoxBand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadRepertoireRecsOfBand();
        }

        void LoadRepertoireRecsOfBand()
        {
            if (comboBoxBand.SelectedValue is int)
                this.repertoireTableAdapter.FillByBandInSongOrder(this.azureDataSet.Repertoire, (int)comboBoxBand.SelectedValue);
            else
                this.azureDataSet.Repertoire.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
            // DialogResult = DialogResult.OK;
        }

        private void UpdateDB()
        {
            repertoireBindingSource.EndEdit();
            this.repertoireTableAdapter.Update(this.azureDataSet.Repertoire);
            DataModified = false;
        }

        private void RepertoireForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void repertoireBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(azureDataSet.Repertoire);
        }
    }
}
