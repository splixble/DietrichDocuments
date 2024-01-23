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
    public partial class VenuesForm : Form
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

        public VenuesForm()
        {
            InitializeComponent();
        }

        private void VenuesForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'AzureDataSet.venues' table. You can move, or remove it, as needed.
            this.venuesTableAdapter1.Fill(this.dataSet1.venues);
            DataModified = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
            DialogResult = DialogResult.OK;
        }

        private void UpdateDB()
        {
            bindingSource.EndEdit();
            this.venuesTableAdapter1.Update(this.dataSet1.venues);
            dataGridView1.Refresh();
            DataModified = false;
        }

        private void VenuesForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void bindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(dataSet1.venues);
        }
    }
}
