using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget
{
    public partial class ManuallyEnteredSourceFileSavePrompt : Form
    {
        public string FileName => tbFileName.Text;

        public DateTime StatementDate => dtpStatementDate.Value;

        public ManuallyEnteredSourceFileSavePrompt()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Check if necessary fields are filled in: 
            string errorList = "";
            if (tbFileName.Text == "")
                errorList = "File Name";
            if (dtpStatementDate.Value == DateTime.MinValue)
            {
                if (errorList != "")
                    errorList += ", ";
                errorList += "Statement Date";
            }
            if (errorList != "")
            {
                MessageBox.Show("Must enter field(s) " + errorList);
                DialogResult = DialogResult.Cancel;
            }
            DialogResult = DialogResult.OK;
        }

        private void tbFileName_Validated(object sender, EventArgs e)
        {
            // can part of the filename be parsed into a date?
            // this is BofA filename format
            string pattern = @"^(.* )?(-?(\d|,)+\.\d\d)$";
            Match match = Regex.Match(FileName, @"(\d\d\d\d-\d\d-\d\d)");
            if (match.Success)
            {
                DateTime dateTimeInFilename;
                if (DateTime.TryParse(match.Groups[1].Value, out dateTimeInFilename))
                    dtpStatementDate.Value = dateTimeInFilename;
            }
        }
    }
}
