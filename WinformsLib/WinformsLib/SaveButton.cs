using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsLib
{
    // Prompts user to save changed data on closure of form. Enabled only if data has changed.
    public class SaveButton : Button
    {
        bool _DataModified = false; // DIAG or take value directly from DataTable?
        bool DataModified
        {
            get { return _DataModified; }
            set
            {
                _DataModified = value;
                Enabled = _DataModified;
            }
        }

        Form _Form;
        BindingSource _BindingSource;
        DataTable _Table;

        public void Initialize(BindingSource bindingSource, DataTable table)
        {
            _BindingSource = bindingSource;
            _Table = table;

            if (_BindingSource != null ) 
                _BindingSource.CurrentItemChanged += _BindingSource_CurrentItemChanged;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            // DIAG not called! Well, this SaveButton doesn't work the way it should with grids anyway....
            base.OnControlAdded(e);
            _Form = this.FindForm();
            _Form.FormClosing += _Form_FormClosing;
        }

        private void _BindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            DataModified = Utils.DataTableIsModified(_Table);
        }

        private void _Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataModified = Utils.DataTableIsModified(_Table);
            if (DataModified)
            {
                DialogResult diaRes = MessageBox.Show("Save changes before closing?",
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (diaRes == DialogResult.Cancel)
                    e.Cancel = true;
                else if (diaRes == DialogResult.Yes)
                    OnClick(new EventArgs());
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            DataModified = false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            DataModified = false;
        }
    }

}
