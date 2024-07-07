using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Budget.MainDataSetTableAdapters;
using Microsoft.VisualBasic.FileIO;
using static Budget.MainDataSet;

namespace Budget
{
    public partial class SourceFileForm : Form
    {
        public SourceFileForm()
        {
            InitializeComponent();

            comboAccount.DataSource = Program.LookupTableSet.MainDataSet.BudgetAccount;
            comboAccount.DisplayMember = "AccountName";
            comboAccount.ValueMember = "AccountID";

            comboFileFormat.DataSource = Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;
            comboFileFormat.DisplayMember = "FormatCode";
            comboFileFormat.ValueMember = "FormatCode";
        }

        SourceFileProcessor _Processor = null;

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            PreImport();

            if (chBoxManualEntry.Checked)
                _Processor = new ManualEnteredSourceTextProcessor(budgetCtrl.BudgetTable, comboAccount.SelectedValue as string, comboFileFormat.SelectedValue as string);
            else
                _Processor = new SourceFileProcessor(budgetCtrl.BudgetTable, comboAccount.SelectedValue as string, comboFileFormat.SelectedValue as string);
            _Processor.Process();

            tbFileText.Clear();

            lblFilePath.Text = "File: " + _Processor.SourceFileName;
            lblFilePath.ForeColor = Color.Blue;
            
            PostImport();
        }

        void CloseSourceFile()
        {
            _Processor = null;
            tbFileText.Clear(); 

            lblFilePath.Text = "File:";
            lblFilePath.ForeColor = SystemColors.ControlText;
        }

        void PreImport()
        {
            // By default, show only new (ID negative) records: 
            budgetCtrl.BudgetBindingSource.Filter = "ID<0";
        }

        void PostImport()
        {
            // update to include changed duplicate rows:
            string dupBudgetIDsList = "";
            foreach (int dupID in _Processor.MatchingBudgetIDs)
            {
                if (dupBudgetIDsList != "")
                    dupBudgetIDsList += ",";
                dupBudgetIDsList += dupID;
            }

            budgetCtrl.BudgetBindingSource.Filter = "ID<0";
            if (dupBudgetIDsList.Length > 0) 
                budgetCtrl.BudgetBindingSource.Filter += " OR ID IN (" + dupBudgetIDsList + ")";
            budgetCtrl.Refresh();
            btnSaveBudgetItems.Enabled = true;
        }

        private void SourceFileForm_Load(object sender, EventArgs e)
        {
            budgetCtrl.CreateNewSourceFileRow = true;

            UpdateManualEntryBasedControls();
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            _Processor.SaveChanges();
            // not needed: budgetCtrl.BudgetBindingSource.Filter = "SourceFile=" + _NewestSourceFileRow.FileID;
            budgetCtrl.Grid.Refresh();
        }

        private void comboAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CloseSourceFile();

            btnSaveBudgetItems.Enabled = comboAccount.SelectedItem != null;
            btnOpenSourceFile.Enabled = comboAccount.SelectedItem != null;

            UpdateFileFormatSelection();
        }
        void UpdateFileFormatSelection()
        {
            // set file format combo to default one for account, if there is one:
            if (comboAccount.SelectedValue == null)
                return;

            BudgetAccountRow accountRow = Program.LookupTableSet.MainDataSet.BudgetAccount.FindByAccountID((string)comboAccount.SelectedValue);
            if (accountRow == null) 
                return;

            string fileFormatCode = null;
            if (chBoxManualEntry.Checked)
            {
                if (!accountRow.IsDefaultFormatManualEntryNull())
                    fileFormatCode = accountRow.DefaultFormatManualEntry;
            }
            else
            {
                if (!accountRow.IsDefaultFormatAutoEntryNull())
                    fileFormatCode = accountRow.DefaultFormatAutoEntry;
            }
            if (fileFormatCode != null && comboFileFormat.SelectedValue as string != fileFormatCode)
                comboFileFormat.SelectedValue = fileFormatCode;

        }

        private void amazonOrderFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreImport();

            _Processor = new AmazonOrderFileProcessor(budgetCtrl.BudgetTable);
            _Processor.Process();

            PostImport();

        }

        private void amazonDigitalItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreImport();

            // DIAG these special format src file Processor classes s/spully a unique code to src file DB rec
            _Processor = new AmazonDigitalItemsProcessor(budgetCtrl.BudgetTable);
            _Processor.Process();

            PostImport();
        }

        void UpdateManualEntryBasedControls()
        {
            tbFileText.Enabled = chBoxManualEntry.Checked;
            // REMOVED btnOpenSourceFile.Enabled = !chBoxManualEntry.Checked;
            btnImportManualText.Enabled = chBoxManualEntry.Checked;

            UpdateFileFormatSelection();
        }

        private void chBoxManualEntry_CheckedChanged(object sender, EventArgs e)
        {
            UpdateManualEntryBasedControls();
        }

        private void btnImportManualText_Click(object sender, EventArgs e)
        {
            PreImport();

            ((ManualEnteredSourceTextProcessor)_Processor).ProcessManualLines(tbFileText.Lines);

            PostImport();
        }
    }
}
