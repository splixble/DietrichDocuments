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

namespace Budget
{
    public partial class SourceFileChecklistCtrl : UserControl
    {
        MainDataSet.BudgetSourceFileDataTable _SourceFileTable = new MainDataSet.BudgetSourceFileDataTable();
        MainDataSetTableAdapters.BudgetSourceFileTableAdapter _SourceFileAdapter = new MainDataSetTableAdapters.BudgetSourceFileTableAdapter();

        // columns:
        DataGridViewTextBoxColumn _AccountColumn;
        DataGridViewButtonColumn _LoadFileButtonColumn;
        DataGridViewTextBoxColumn _LoadedFilesColumn;

        public SourceFileChecklistCtrl()
        {
            InitializeComponent();

            if (Program.LookupTableSet != null) // if not in Designer mode
            {
                budgetAccountBindingSource.DataSource = Program.LookupTableSet.MainDataSet;
                budgetAccountBindingSource.DataMember = "BudgetAccount";
                budgetAccountBindingSource.Position = 0;
            }

            // define grid columns:
            _AccountColumn = new DataGridViewTextBoxColumn();
            _AccountColumn.DataPropertyName = "AccountName";
            _AccountColumn.HeaderText = "Account";
            _AccountColumn.Name = "AccountName";
            _AccountColumn.Width = 240;
            _AccountColumn.ReadOnly = true;

            _LoadFileButtonColumn = new DataGridViewButtonColumn();
            _LoadFileButtonColumn.HeaderText = "Load File";
            _LoadFileButtonColumn.Text = "Load File";
            _LoadFileButtonColumn.UseColumnTextForButtonValue = true;
            _LoadFileButtonColumn.Name = "LoadFileButtonColumn";
            _LoadFileButtonColumn.Width = 80;

            _LoadedFilesColumn = new DataGridViewTextBoxColumn();
            _LoadedFilesColumn.Name = "LoadedFilesColumn";
            _LoadedFilesColumn.Width = 300;
            _LoadedFilesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _LoadedFilesColumn.HeaderText = "File(s) Loaded";
            _LoadedFilesColumn.ReadOnly = true;

            grid.Columns.AddRange(new DataGridViewColumn[] { _AccountColumn, _LoadFileButtonColumn, _LoadedFilesColumn });

            budgetAccountBindingSource.BindingComplete += BudgetAccountBindingSource_BindingComplete;
            grid.RowsAdded += Grid_RowsAdded;
            grid.DataBindingComplete += Grid_DataBindingComplete;
        }

        SourceFileForm _Form = null;

        public void Initialize(SourceFileForm form)
        {
            _Form = form;
        }

        private void Grid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset)
                RefreshGridData();
        }

        private void Grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void BudgetAccountBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
        }

        void RefreshGridData()
        {
            _SourceFileAdapter.Connection = Program.DbConnection;
            _SourceFileAdapter.FillThisMonth(_SourceFileTable);

            Dictionary<string, string> sourceFilesByAccount = new Dictionary<string, string>();
            foreach (MainDataSet.BudgetSourceFileRow sourceFileRow in _SourceFileTable)
            {
                if (!sourceFileRow.IsAccountNull())
                {
                    string sourceFileInfo = Path.GetFileName(sourceFileRow.FilePath) + sourceFileRow.ImportDateTime.ToString(" (MM/dd)");
                    if (sourceFilesByAccount.ContainsKey(sourceFileRow.Account))
                        sourceFilesByAccount[sourceFileRow.Account] += ", " + sourceFileInfo;
                    else
                        sourceFilesByAccount[sourceFileRow.Account] = sourceFileInfo;
                }
            }

            foreach (DataGridViewRow gridRow in grid.Rows) 
            {
                MainDataSet.BudgetAccountRow accountRow = GetBudgetAccountRowFromGridRow(gridRow.Index);
                if (sourceFilesByAccount.ContainsKey(accountRow.AccountID))
                {
                    gridRow.Cells[_LoadedFilesColumn.Index].Value = sourceFilesByAccount[accountRow.AccountID];
                }
            }
        }

        MainDataSet.BudgetAccountRow GetBudgetAccountRowFromGridRow(int gridRowIndex)
        {
            DataRowView dataRowView = budgetAccountBindingSource[gridRowIndex] as DataRowView;
            return (MainDataSet.BudgetAccountRow)dataRowView.Row;
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == _LoadFileButtonColumn.Index)
            {
                if (_Form == null)
                    throw (new Exception("The SourceFileForm needs to call SourceFileChecklistControl.Initialize()."));

                MainDataSet.BudgetAccountRow accountRow = GetBudgetAccountRowFromGridRow(e.RowIndex);
                _Form.ImportFileFromChecklist(accountRow.AccountID, accountRow.DefaultFormatAutoEntry);

                // TODO refresh the grid after the import goes thru
            }
        }
    }
}
