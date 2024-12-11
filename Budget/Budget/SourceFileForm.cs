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
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.FileIO;
using static Budget.MainDataSet;

namespace Budget
{
    public partial class SourceFileForm : Form
    {
        // DIAG BUG!! UI payments (IUI), when loaded from src file, are not marked as Income with bit field, so are being Normalized as negative. Do we still use that bitt field??

        public enum ImportedDataTypes { Bank, Investment, None};

        public ImportedDataTypes ImportedDataType
        {
            get
            {
                if (_Processor is BudgetSourceFileProcessor)
                    return ImportedDataTypes.Bank;
                else if (_Processor is SharePriceSourceFileProcessor)
                    return ImportedDataTypes.Investment;    
                else
                    return ImportedDataTypes.None;
            }
        }

        string SelectedAccount => comboAccount.SelectedValue as string;

        string SelectedFileFormat => comboFileFormat.SelectedValue as string;

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
            PreImport();

            _Processor = new BudgetSourceFileProcessor(budgetCtrl.BudgetTable, SelectedAccount, SelectedFileFormat, chBoxManualEntry.Checked);
            _Processor.Process();

            tbFileText.Clear();

            lblFilePath.Text = "File: " + _Processor.SourceFilePath;
            lblFilePath.ForeColor = Color.Blue;
            
            PostImport();
        }

        public void ImportFileFromChecklist(string accountID, string accountFormat)
        {
            CreateDefaultSourceFileProcessor(accountID, accountFormat);

            PreImport();

            _Processor.ProcessFromChecklist();

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
            UpdateBindingSourceFilter();
        }

        void PostImport()
        {
            // DIAG do this only if we're updating Budget control - like, separate Budget Pre/PostImport func from Share func
            // DIAG s/n have to cast
            //BudgetSourceFileProcessor budgetProcessor = (BudgetSourceFileProcessor)_Processor;

            // DIAG write a PrimaryKeyValue member thing to generate a Filter stmt -- this'll work fine with int, but not share stuff 

            /* REMOVE DIAG
            // update to include changed duplicate rows:
            string dupBudgetIDsList = "";
            foreach (PrimaryKeyValue dupKey in budgetProcessor.MatchingPrimaryKeys)
            {
                if (dupBudgetIDsList != "")
                    dupBudgetIDsList += ",";
                dupBudgetIDsList += dupKey.ToString();
            }

            */

            UpdateBindingSourceFilter();

            /* REMOVE DIAG
            budgetCtrl.BindingSrc.Filter = "ID<0";
            if (dupBudgetIDsList.Length > 0) 
                budgetCtrl.BindingSrc.Filter += " OR ID IN (" + dupBudgetIDsList + ")";
            */
            budgetCtrl.Refresh();
            sharePriceCtrl.Refresh();

            btnSaveBudgetItems.Enabled = true; // DIAG rename button
        }

        void UpdateBindingSourceFilter()
        {

            // DIAG hide, not disable, unused ctrls
            budgetCtrl.Visible = (ImportedDataType == ImportedDataTypes.Bank);
            sharePriceCtrl.Visible = (ImportedDataType == ImportedDataTypes.Investment);

            switch (ImportedDataType)
            {
                case ImportedDataTypes.Bank: // DIAG do these enums really buy us anything?
                    budgetCtrl.BindingSrc.Filter = _Processor.GetBindingSrcFilterText();
                    break;
                case ImportedDataTypes.Investment: // DIAG do these enums really buy us anything?
                    sharePriceCtrl.BindingSrc.Filter = _Processor.GetBindingSrcFilterText();
                    break;
            }
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

            btnSaveBudgetItems.Enabled = SelectedAccount != null;
            btnOpenSourceFile.Enabled = SelectedAccount != null;

            UpdateFileFormatSelection();
        }
        void UpdateFileFormatSelection()
        {
            // set file format combo to default one for account, if there is one:
            if (SelectedAccount == null)
                return;

            BudgetAccountRow accountRow = Program.LookupTableSet.MainDataSet.BudgetAccount.FindByAccountID(SelectedAccount);
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

        void CreateDefaultSourceFileProcessor(string accountID, string accountFormat)
        {
            BudgetAccountRow accountRow = Program.LookupTableSet.MainDataSet.BudgetAccount.FindByAccountID(accountID);
            if (accountRow.TrackedByShares)
                _Processor = new SharePriceSourceFileProcessor(sharePriceCtrl.SharePriceTable, accountRow.Fund, accountFormat, false);
            else
                _Processor = new BudgetSourceFileProcessor(budgetCtrl.BudgetTable, accountID, accountFormat, false);
        }

        private void btnImportManualText_Click(object sender, EventArgs e)
        {

            CreateDefaultSourceFileProcessor(SelectedAccount, SelectedFileFormat);
            PreImport();

            _Processor.ProcessManualLines(tbFileText.Lines);

            PostImport();
        }

        private void yahooHoldingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Processor = new SharePriceSourceFileProcessor(sharePriceCtrl.SharePriceTable, null, SourceFileFormats.YahooHoldings.ToString(), false);

            PreImport();

            _Processor.ProcessFromChecklist();

            PostImport();
        }
    }
}
