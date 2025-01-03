﻿using System;
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
        MainDataSet.ViewSourceFileWithAccountsDataTable _SourceFileTable = new MainDataSet.ViewSourceFileWithAccountsDataTable();
        MainDataSetTableAdapters.ViewSourceFileWithAccountsTableAdapter _SourceFileAdapter = new MainDataSetTableAdapters.ViewSourceFileWithAccountsTableAdapter();

        MainDataSet.ViewLatestActivityPerAccountDataTable _LatestActivityTable = new MainDataSet.ViewLatestActivityPerAccountDataTable();
        MainDataSetTableAdapters.ViewLatestActivityPerAccountTableAdapter _LatestActivityAdapter = new MainDataSetTableAdapters.ViewLatestActivityPerAccountTableAdapter();

        // columns:
        DataGridViewTextBoxColumn _AccountColumn;
        DataGridViewButtonColumn _LoadFileButtonColumn;
        DataGridViewTextBoxColumn _LastTransDateColumn;
        DataGridViewTextBoxColumn _LoadedFilesColumn;

        public SourceFileChecklistCtrl()
        {
            InitializeComponent();

            if (Program.LookupTableSet != null) // if not in Designer mode
            {
                budgetAccountBindingSource.DataSource = Program.LookupTableSet.MainDataSet;
                budgetAccountBindingSource.DataMember = "Account";
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

            _LastTransDateColumn = new DataGridViewTextBoxColumn();
            _LastTransDateColumn.Name = "LastTransDateColumn";
            _LastTransDateColumn.Width = 70;
            _LastTransDateColumn.HeaderText = "Last Trans. Date";
            _LastTransDateColumn.ValueType = typeof(DateTime);
            _LastTransDateColumn.ReadOnly = true;

            _LoadedFilesColumn = new DataGridViewTextBoxColumn();
            _LoadedFilesColumn.Name = "LoadedFilesColumn";
            _LoadedFilesColumn.Width = 300;
            _LoadedFilesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _LoadedFilesColumn.HeaderText = "File(s) Loaded";
            _LoadedFilesColumn.ReadOnly = true;

            grid.Columns.AddRange(new DataGridViewColumn[] { _AccountColumn, _LoadFileButtonColumn, _LastTransDateColumn, _LoadedFilesColumn });

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

            _LatestActivityAdapter.Connection = Program.DbConnection;
            _LatestActivityAdapter.Fill(_LatestActivityTable);

            Dictionary<string, string> sourceFilesByAccount = new Dictionary<string, string>();
            foreach (MainDataSet.ViewSourceFileWithAccountsRow sourceFileRow in _SourceFileTable)
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
                MainDataSet.AccountRow accountRow = GetBudgetAccountRowFromGridRow(gridRow.Index);

                MainDataSet.ViewLatestActivityPerAccountRow latestActivityRow = _LatestActivityTable.FindByAccount(accountRow.AccountID);
                if (latestActivityRow != null)
                    gridRow.Cells[_LastTransDateColumn.Index].Value = latestActivityRow.TrDate;

                if (sourceFilesByAccount.ContainsKey(accountRow.AccountID))
                {
                    gridRow.Cells[_LoadedFilesColumn.Index].Value = sourceFilesByAccount[accountRow.AccountID];
                }
            }
        }

        MainDataSet.AccountRow GetBudgetAccountRowFromGridRow(int gridRowIndex)
        {
            DataRowView dataRowView = budgetAccountBindingSource[gridRowIndex] as DataRowView;
            return (MainDataSet.AccountRow)dataRowView.Row;
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == _LoadFileButtonColumn.Index)
            {
                if (_Form == null)
                    throw (new Exception("The SourceFileForm needs to call SourceFileChecklistControl.Initialize()."));

                MainDataSet.AccountRow accountRow = GetBudgetAccountRowFromGridRow(e.RowIndex);
                _Form.ImportFileFromChecklist(accountRow.AccountID, accountRow.DefaultFormatAutoEntry);

                // TODO refresh the grid after the import goes thru
            }
        }
    }
}
