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
        }

        SourceFileProcessor _Processor = null;

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            PreImport();

            _Processor = new SourceFileProcessor(budgetCtrl.BudgetTable, comboAccount.SelectedValue as string);
            _Processor.Process();
            
            PostImport();
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

            budgetCtrl.BudgetBindingSource.Filter = "ID<0 OR ID IN (" + dupBudgetIDsList + ")";
            budgetCtrl.Refresh();
            btnSaveBudgetItems.Enabled = true;
        }

        private void SourceFileForm_Load(object sender, EventArgs e)
        {
            budgetCtrl.CreateNewSourceFileRow = true;
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            _Processor.SaveChanges();
            // not needed: budgetCtrl.BudgetBindingSource.Filter = "SourceFile=" + _NewestSourceFileRow.FileID;
            budgetCtrl.Grid.Refresh();
        }

        private void comboAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnSaveBudgetItems.Enabled = comboAccount.SelectedItem != null;
        }

        private void amazonOrderFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreImport();

            _Processor = new AmazonOrderFileProcessor(budgetCtrl.BudgetTable);
            _Processor.Process();

            PostImport();

        }
    }
}
