using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient; // NOTE: Must be Microsoft.Data.SqlClient, NOT System.Data.SqlClient
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Security.Cryptography.Xml;
using PrintLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Budget.Constants;

namespace Budget
{
    public partial class Form1 : Form
    {
        // use this?
        public MainDataSet MainData { get { return _MainData; } }
        MainDataSet _MainData = new MainDataSet();

        MainDataSet.GroupingsInOrderDataTable _GroupingsInOrderTbl = new MainDataSet.GroupingsInOrderDataTable();

        const int groupingsGridCheckboxColumn = 0;

        public string AccountOwner => comboAccountOwner.SelectedValue as string;
        string _PrevAccountOwner = "";

        public char AccountType => ((string)comboAccountType.SelectedValue)[0];
        char _PrevAccountType = ' ';

        TreeNode _BalanceTotalNode = null;
        TreeNode _IncomeNode = null;
        TreeNode _ExpensesNode = null;

        public Form1()
        {
            InitializeComponent();
        }

        void AddToGroupingListIfChecked(TreeNode node, ref string groupingsList)
        {
            if (node.Checked)
            {
                if (groupingsList != "")
                    groupingsList += ",";
                groupingsList += "'" + node.Text.Replace("'", "''") + "'"; // 'escape out' any single quotes in node text, since they're enclosed in single quotes in the SQL
            }

            // recursively call this for child nodes:
            foreach (TreeNode childNode in node.Nodes)
                AddToGroupingListIfChecked(childNode, ref groupingsList);
        }

        void BuildGroupingsTree()
        {
            // Remove all nodes, of they exist:
            tvGroupings.Nodes.Clear();

            // Clear and requery the table: 
            _GroupingsInOrderTbl.Clear();
            string groupingsInOrderSelectStr = "SELECT DISTINCT TOP (100) PERCENT Grouping, "
                + "CASE [grouping] WHEN 'Income' THEN 1 WHEN 'Expenses' THEN 2 ELSE 3 END AS OrderNum, "
                + "GroupingType, dbo.BudgetTypeGroupings.ParentGroupingLabel "
                + "FROM ViewBudgetWithMonthly left join BudgetTypeGroupings on ViewBudgetWithMonthly.Grouping = BudgetTypeGroupings.GroupingLabel "
                + "WHERE (Grouping IS NOT NULL) AND AccountOwner = '" + AccountOwner + "'";
            if (AccountType != Constants.AccountType.BothValue)
                groupingsInOrderSelectStr += "AND AccountType = '" + AccountType + "'";
            groupingsInOrderSelectStr += "ORDER BY OrderNum, Grouping";

            using (SqlConnection reportDataConn = new SqlConnection(Properties.Settings.Default.BudgetConnectionString))
            {
                // reportDataConn.Open();
                SqlCommand groupingsInOrderCmd = new SqlCommand();
                // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                groupingsInOrderCmd.Connection = Program.DbConnection;
                groupingsInOrderCmd.CommandText = groupingsInOrderSelectStr;

                SqlDataAdapter groupingsInOrderAdap = new SqlDataAdapter(groupingsInOrderCmd);
                groupingsInOrderAdap.Fill(_GroupingsInOrderTbl);
            }

            // Populate Groupings tree, and save certain nodes for future reference:
            // Add parent nodes:
            foreach (MainDataSet.GroupingsInOrderRow groupingRow in _GroupingsInOrderTbl)
            {
                if (groupingRow.IsParentGroupingLabelNull() && groupingRow.GroupingType != Constants.GroupingType.BalanceOfAccount)
                {
                    TreeNode node = tvGroupings.Nodes.Add(groupingRow.Grouping, groupingRow.Grouping);

                    if (groupingRow.GroupingType == Constants.GroupingType.BalanceTotal)
                        _BalanceTotalNode = node;
                    else if (groupingRow.Grouping == Constants.GroupingName.Income)
                        _IncomeNode = node;
                    else if (groupingRow.Grouping == Constants.GroupingName.Expense)
                        _ExpensesNode = node;
                }
            }

            // Now, add child nodes:
            foreach (MainDataSet.GroupingsInOrderRow groupingRow in _GroupingsInOrderTbl)
            {
                TreeNode parentNode = null;
                if (!groupingRow.IsParentGroupingLabelNull())
                {
                    TreeNode[] parentNodeArray = tvGroupings.Nodes.Find(groupingRow.ParentGroupingLabel, false); // search only top level
                    if (parentNodeArray.Length > 0) // should never be 0, or >1
                        parentNode = parentNodeArray[0];
                }
                else if (groupingRow.GroupingType == Constants.GroupingType.BalanceOfAccount)
                    parentNode = _BalanceTotalNode;

                if (parentNode != null)
                    parentNode.Nodes.Add(groupingRow.Grouping);
            }

            // Check initial default groupings:
            switch (AccountType)
            {
                case Constants.AccountType.Bank:
                    _ExpensesNode.Checked = true;
                    _IncomeNode.Checked = true;
                    break;
                case Constants.AccountType.Investment:
                    _BalanceTotalNode.Checked = true;
                    break;
            }
        }

        void RefreshDisplay()
        {
            // If account owner or type changed, reset initial check state of grouping nodes: 
            bool accountOwnerOrTypeChanged = false;
            if (AccountOwner != _PrevAccountOwner)
            {
                _PrevAccountOwner = AccountOwner;
                accountOwnerOrTypeChanged = true;
            }
            if (AccountType != _PrevAccountType)
            {
                _PrevAccountType = AccountType;
                accountOwnerOrTypeChanged = true;
            }

            if (accountOwnerOrTypeChanged)
                BuildGroupingsTree();

            // Get groupings to display, from checked 
            string groupingsList = "";

            foreach (TreeNode node in tvGroupings.Nodes)
            {
                AddToGroupingListIfChecked(node, ref groupingsList);
            }
            
            MainData.ViewBudgetMonthlyReport.Clear();
            if (groupingsList != "") // if no groupings checked, just leave it cleared
            {
                string selectStr = "SELECT * FROM ViewBudgetMonthlyReport WHERE Grouping IN (" + groupingsList + ")" 
                    + " AND AccountOwner = '" + AccountOwner + "'";
                if (AccountType != Constants.AccountType.BothValue) 
                    selectStr += " AND AccountType = '" + AccountType + "'";

                using (SqlConnection reportDataConn = new SqlConnection(Properties.Settings.Default.BudgetConnectionString))
                {
                    // reportDataConn.Open();
                    SqlCommand reportDataCmd = new SqlCommand();
                    // this dont compile: CommandBehavior fillCommandBehavior = FillCommandBehavior;
                    reportDataCmd.Connection = Program.DbConnection;
                    reportDataCmd.CommandText = selectStr;

                    SqlDataAdapter reportDataAdap = new SqlDataAdapter(reportDataCmd);
                    reportDataAdap.Fill(MainData.ViewBudgetMonthlyReport);
                }
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", MainData.ViewBudgetMonthlyReport as DataTable);

            PopulateMainGrid();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }

        void PopulateMainGrid()
        {
            gridMain.Rows.Clear();
            gridMain.Columns.Clear();

            gridMain.RowHeadersWidth = 120;


            SortedList<DateTime, object> cols = new SortedList<DateTime, object>();
            SortedList<string, object> rows = new SortedList<string, object>();
            foreach (MainDataSet.ViewBudgetMonthlyReportRow tblRow in MainData.ViewBudgetMonthlyReport)
            {
                if (!tblRow.IsGroupingNull())
                    rows[tblRow.GroupingWithParent] = tblRow.Grouping;

                cols[tblRow.TrMonth] = null;
            }

            Dictionary<DateTime, int> colIndices = new Dictionary<DateTime, int>();
            int colIndex = 0;
            foreach (DateTime trMonth in cols.Keys)
            {
                colIndices[trMonth] = colIndex;
                gridMain.Columns.Add("col" + colIndex.ToString(), trMonth.ToString("MMM yy"));
                DataGridViewColumn col = gridMain.Columns[colIndex];
                col.Width = 70;
                col.ValueType = typeof(Decimal);
                col.Tag = trMonth;
                col.DefaultCellStyle.Format = "$0,0.00";
                colIndex++;
            }

            Dictionary<string, int> rowIndices = new Dictionary<string, int>();
            int rowIndex = 0;
            foreach (string groupingWithParent in rows.Keys)
            {
                rowIndices[groupingWithParent] = rowIndex;
                //dataGridView1.Columns.Add("row" + rowIndex.ToString(), trMonth.ToString("MMM yy"));
                rowIndex++;
            }

            gridMain.RowCount = rowIndex;
            for (int ri = 0; ri < gridMain.RowCount; ri++)
            {
                DataGridViewRow row = gridMain.Rows[ri]; 
                row.HeaderCell.Value = rows.Values[ri];
                row.Tag = rows.Keys[ri];
            }

            foreach (MainDataSet.ViewBudgetMonthlyReportRow tblRow in MainData.ViewBudgetMonthlyReport)
            {
                if (!tblRow.IsGroupingNull())
                {
                    gridMain[colIndices[tblRow.TrMonth], rowIndices[tblRow.GroupingWithParent]].Value = tblRow.AmountNormalized;
                }

                if (tblRow.IsGroupingParentNull())
                    gridMain.Rows[rowIndices[tblRow.GroupingWithParent]].DefaultCellStyle.BackColor = Color.PaleTurquoise;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Program.LookupTableSet.LoadWithRetryOption())
                return;

            comboAccountOwner.DataSource = Program.LookupTableSet.MainDataSet.AccountOwner;
            comboAccountOwner.ValueMember = "OwnerID";
            comboAccountOwner.DisplayMember = "OwnerDescription";
            comboAccountOwner.SelectedValue = "D";// initialize it

            comboAccountType.DataSource = Program.LookupTableSet.MainDataSet.ViewAccountTypesWithAllOption;
            comboAccountType.ValueMember = "TypeCode";
            comboAccountType.DisplayMember = "TypeDescription";
            comboAccountType.SelectedValue = "B"; // initialize it to Bank            

            // Make gridMain printable by taking a Printable tag onto it:
            gridMain.Tag = new PrintableGridTag(gridMain);

            RefreshDisplay();
        }

        private void gridMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DateTime cellMonth = (DateTime)gridMain.Columns[e.ColumnIndex].Tag;
            string cellGrouping = (string)gridMain.Rows[e.RowIndex].Tag;
            MonthGroupingForm monthGroupingForm = new MonthGroupingForm();
            monthGroupingForm.Initialize(cellMonth, cellGrouping, AccountOwner, AccountType);
            monthGroupingForm.ShowDialog();
        }

        private void applyTransactionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupingAssignmentForm trTypeForm = new GroupingAssignmentForm();
            trTypeForm.ShowDialog();
        }

        private void editGroupingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupingForm form = new GroupingForm();
            form.ShowDialog();
        }

        private void loadSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SourceFileForm form = new SourceFileForm();
            form.ShowDialog();
        }

        private void tvGroupings_AfterCheck(object sender, TreeViewEventArgs e)
        {
            RefreshDisplay();
        }

        private void calculateAccountBalancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceCalculationForm balanceCalculationForm = new BalanceCalculationForm();
            balanceCalculationForm.ShowDialog();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DNPrintDocument printDoc = new DNPrintDocument();
            printDoc.PrintPreview(splitConInner.Panel1); // contains gridMain. Or can I just pass gridMain in?
        }

        private void comboAccountOwner_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void comboAccountType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        private void investmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvestmentBalancesForm form = new InvestmentBalancesForm();
            form.ShowDialog();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsForm form = new AccountsForm();
            form.ShowDialog();
        }
    }
}
