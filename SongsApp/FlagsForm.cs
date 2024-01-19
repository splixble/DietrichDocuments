using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Songs
{
    public partial class FlagsForm : Form
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

        public FlagsForm()
        {
            InitializeComponent();
        }

        private void FlagsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.flags' table. You can move, or remove it, as needed.
            this.flagsTableAdapter.Fill(this.dataSet1.flags);
            DataModified = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDB();
            DialogResult = DialogResult.OK;
        }

        private void UpdateDB()
        {
            flagsBindingSource.EndEdit();
            this.flagsTableAdapter.Update(this.dataSet1.flags);
            DataModified = false;
        }

        private void FlagsForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void flagsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(dataSet1.flags);
        }
    }
}