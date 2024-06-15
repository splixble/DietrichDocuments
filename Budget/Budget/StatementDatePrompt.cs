using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Budget
{
    public partial class StatementDatePrompt : Form
    {
        public DateTime StatementDate => dtpStatementDate.Value;

        public StatementDatePrompt()
        {
            InitializeComponent();
        }

        public void InitializeWithFilename(string sourceFileName)
        {
            // can part of the filename be parsed into a date?
            string fileNameBody = Path.GetFileNameWithoutExtension(sourceFileName);
            // this is BofA filename format
            Match match = Regex.Match(fileNameBody, @"(\d\d\d\d-\d\d-\d\d)");
            if (match.Success)
            {
                DateTime dateTimeInFilename;
                if (DateTime.TryParse(match.Groups[1].Value, out dateTimeInFilename))
                    dtpStatementDate.Value = dateTimeInFilename;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtpStatementDate.Value == DateTime.MinValue)
            {
                // DIAG does this even do anything?
                MessageBox.Show("Must enter a Statement Date field");
                DialogResult = DialogResult.Cancel;
            }
            DialogResult = DialogResult.OK;

        }
    }
}
