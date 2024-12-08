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
        // DIAG BUG!! UI payments (IUI), when loaded from src file, are not marked as Income with bit field, so are being Normalized as negative. Do we still use that bitt field??

        public SourceFileForm()
        {
            InitializeComponent();

            comboAccount.DataSource = Program.LookupTableSet.MainDataSet.BudgetAccount;
            comboAccount.DisplayMember = "AccountName";
            comboAccount.ValueMember = "AccountID";

            comboFileFormat.DataSource = Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;
            comboFileFormat.DisplayMember = "FormatCode";
            comboFileFormat.ValueMember = "FormatCode";

            sourceFileChecklistCtrl1.Initialize(this);
        }

        SourceFileProcessor _Processor = null;

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            PreImport_Budget();

            _Processor = new BudgetSourceFileProcessor(budgetCtrl.BudgetTable, comboAccount.SelectedValue as string, comboFileFormat.SelectedValue as string, chBoxManualEntry.Checked);
            _Processor.Process();

            tbFileText.Clear();

            lblFilePath.Text = "File: " + _Processor.SourceFilePath;
            lblFilePath.ForeColor = Color.Blue;
            
            PostImport_Budget();
        }

        public void ImportFileFromChecklist(string accountID, string accountFormat)
        {
            PreImport_Budget();

            _Processor = new BudgetSourceFileProcessor(budgetCtrl.BudgetTable, accountID, accountFormat, false);
            _Processor.ProcessFromChecklist();

            PostImport_Budget();
        }

        void CloseSourceFile()
        {
            _Processor = null;
            tbFileText.Clear(); 

            lblFilePath.Text = "File:";
            lblFilePath.ForeColor = SystemColors.ControlText;
        }

        void PreImport_Budget()
        {
            // By default, show only new (ID negative) records: 
            budgetCtrl.BindingSrc.Filter = "ID<0";
        }

        void PreImport_SharePrice()
        {
            // DIAG figure out how to filter:
            //sharePriceCtrl.ShareP
        }

        void PostImport_Budget()
        {
            // DIAG do this only if we're updating Budget control - like, separate Budget Pre/PostImport func from Share func
            // DIAG s/n have to cast
            BudgetSourceFileProcessor budgetProcessor = (BudgetSourceFileProcessor)_Processor;

            // DIAG write a PrimaryKeyValue member thing to generate a Filter stmt -- this'll work fine with int, but not share stuff 

            // update to include changed duplicate rows:
            string dupBudgetIDsList = "";
            foreach (PrimaryKeyValue dupKey in budgetProcessor.MatchingPrimaryKeys)
            {
                if (dupBudgetIDsList != "")
                    dupBudgetIDsList += ",";
                dupBudgetIDsList += dupKey.ToString();
            }

            budgetCtrl.BindingSrc.Filter = "ID<0";
            if (dupBudgetIDsList.Length > 0) 
                budgetCtrl.BindingSrc.Filter += " OR ID IN (" + dupBudgetIDsList + ")";
            budgetCtrl.Refresh();
            btnSaveBudgetItems.Enabled = true;
        }

        void PostImport_SharePrice()
        {
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
            PreImport_Budget();

            _Processor = new AmazonOrderFileProcessor(budgetCtrl.BudgetTable);
            _Processor.Process();

            PostImport_Budget();

        }

        private void amazonDigitalItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreImport_Budget();

            // DIAG these special format src file Processor classes s/spully a unique code to src file DB rec
            _Processor = new AmazonDigitalItemsProcessor(budgetCtrl.BudgetTable);
            _Processor.Process();

            PostImport_Budget();
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
            PreImport_Budget();

            _Processor.ProcessManualLines(tbFileText.Lines);

            PostImport_Budget();
        }

        private void yahooHoldingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreImport_SharePrice();

            _Processor = new SharePriceSourceFileProcessor(sharePriceCtrl.SharePriceTable, "YahooHoldings", false); // TODO s/get "YahooHoldings" from config
            _Processor.ProcessFromChecklist();

            PostImport_SharePrice();
        }
    }
}
