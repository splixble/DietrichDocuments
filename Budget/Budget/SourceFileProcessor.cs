﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budget.MainDataSet;
using System.Windows.Forms;
using Budget.MainDataSetTableAdapters;
using System.Diagnostics;

namespace Budget
{
    internal class SourceFileProcessor
    {
        public MainDataSet LookupMainDataSet => Program.LookupTableSet.MainDataSet;

        public BudgetDataTable BudgetTable => _BudgetTable;
        BudgetDataTable _BudgetTable;

        BudgetTableAdapter _BudgetAdapter = new BudgetTableAdapter();

        string _SelectedAccount;
        MainDataSet.BudgetAccountRow AccountRowSelected => Program.LookupTableSet.MainDataSet.BudgetAccount.FindByAccountID(_SelectedAccount as string);

        public virtual bool UpdateAccountFromSourceFile => true;
        public virtual bool AllowAddingNewBudgetRows => true;

        MainDataSet.BudgetSourceFileFormatDataTable SourceFileFormatTable => Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;

        public MainDataSet.BudgetSourceFileDataTable _SourceFileTable = new MainDataSet.BudgetSourceFileDataTable();
        public MainDataSet.BudgetSourceFileRow _NewestSourceFileRow = null;
        public MainDataSet.BudgetSourceFileItemsDataTable _SourceFileItemsTable = new MainDataSet.BudgetSourceFileItemsDataTable();
        public List<BudgetAndSourceItemRows> _ImportedBudgetItems = new List<BudgetAndSourceItemRows>();

        string _SourceFileName;

        public List<int> MatchingBudgetIDs => _MatchingBudgetIDs;
        List<int> _MatchingBudgetIDs = new List<int>();

        public SourceFileProcessor(BudgetDataTable budgetTable, string selectedAccount)
        {
            _BudgetTable = budgetTable;
            _SelectedAccount = selectedAccount;
        }

        // Extracts fields from a line of the text file, puts them in fieldsByColumnName.
        // Returns whether line was parseable.
        // Can be overridden for custom source files like Amazon order lists.
        protected virtual bool ExtractFields(string[] fileFields, Dictionary<string, object> fieldsByColumnName)
        {
            MainDataSet.BudgetSourceFileFormatRow formatRow = SourceFileFormatTable.FindByFormatCode(AccountRowSelected.SourceFileFormat);
            string[] formatFields = formatRow.FormatColumns.Split(',');

            bool lineParsable = true;
            for (int formatColIndex = 0; formatColIndex < formatFields.Length; formatColIndex++)
            {
                // DIAG can remove var lineParsable; just return false
                // 
                if (formatFields[formatColIndex] != "")
                {
                    if (formatColIndex >= fileFields.Length)
                    {
                        lineParsable = false;
                        break;
                    }

                    // which Budget column is it?
                    if (!_BudgetTable.Columns.Contains(formatFields[formatColIndex]))
                    {
                        lineParsable = false;
                        break;
                    }
                    DataColumn budgetColumn = _BudgetTable.Columns[formatFields[formatColIndex]];

                    // Parse the fields, depending on the column's data type:
                    if (budgetColumn.DataType == typeof(string))
                        fieldsByColumnName[budgetColumn.ColumnName] = fileFields[formatColIndex];
                    else if (budgetColumn.DataType == typeof(Decimal))
                    {
                        Decimal fileValue;
                        if (Decimal.TryParse(fileFields[formatColIndex], out fileValue))
                        {
                            // For Amount and Balance columns, handle the special case in which source file lists credits as negative and debits as positive:
                            if (formatRow.CreditsAreNegative)
                                fileValue = -fileValue;

                            fieldsByColumnName[budgetColumn.ColumnName] = fileValue;
                        }
                        else
                            lineParsable = false;
                    }
                    else if (budgetColumn.DataType == typeof(DateTime))
                    {
                        DateTime fileValue;
                        if (DateTime.TryParse(fileFields[formatColIndex], out fileValue))
                            fieldsByColumnName[budgetColumn.ColumnName] = fileValue;
                        else
                            lineParsable = false;
                    }
                    if (!lineParsable)
                        break;
                }
            }
            return lineParsable;
        }

        BudgetRow FindDuplicateRow(Dictionary<string, object> fieldsByColumnName)
        {
            {
                DataRowView[] dupRowViews = FindDuplicateRowViews(fieldsByColumnName);
                if (dupRowViews.Length == 0)
                    return null;
                else if (dupRowViews.Length == 1)
                    return dupRowViews[0].Row as BudgetRow;
                else
                {
                    string dupIDList = "";
                    foreach (DataRowView dupRowView in dupRowViews)
                    {
                        if (dupIDList != "")
                            dupIDList += ", ";
                        dupIDList += ((BudgetRow)dupRowView.Row).ID.ToString();
                    }
                    MessageBox.Show("Multiple duplicate Budget rows, with IDs " + dupIDList);
                    return null;
                }
            }

        }

        protected virtual DataRowView[] FindDuplicateRowViews(Dictionary<string, object> fieldsByColumnName)
        {
            // TODO maybe a custom made DataView per format, depending on what fields it captures...or just linear search...
            return _BudgetTable.DuplicatesView.FindRows(
                new object[] {
                    (DateTime)fieldsByColumnName["TrDate"],
                    (decimal)fieldsByColumnName["Amount"],
                    (string)fieldsByColumnName["Descrip"],
                    _SelectedAccount
                });
        }

        public void Process()
        {
            OpenFileDialog srcFileDlg = new OpenFileDialog();
            srcFileDlg.InitialDirectory = "D:\\Dietrich\\Business\\Budget App Input Files"; //get from Config file, which s/b read at beginning and globally available
            srcFileDlg.Title = "Select source file:";
            DialogResult diaRes = srcFileDlg.ShowDialog();

            if (diaRes == DialogResult.OK)
            {
                _SourceFileName = srcFileDlg.FileName;

                // make new source file row:
                _NewestSourceFileRow = _SourceFileTable.NewBudgetSourceFileRow();
                _NewestSourceFileRow.FilePath = _SourceFileName;
                _NewestSourceFileRow.Account = _SelectedAccount;
                _SourceFileTable.AddBudgetSourceFileRow(_NewestSourceFileRow);

                // Read in the full Budget table, to check for duplicates:
                _BudgetAdapter.Fill(_BudgetTable);

                // use Microsoft.VisualBasic.FileIO objects (TextFieldParser, TextFieldType) to load csv files:
                using (TextFieldParser parser = new TextFieldParser(_SourceFileName))
                {
                    _MatchingBudgetIDs.Clear();

                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // allow tab delim?
                    int lineNum = 0; // it's 1-relative
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fileFields = parser.ReadFields();
                        lineNum++;

                        Dictionary<string, object> fieldsByColumnName = new Dictionary<string, object>();

                        bool lineParsable = ExtractFields(fileFields, fieldsByColumnName);

                        if (lineParsable)
                        {
                            BudgetRow importedRow = null;

                            // Check for duplicate Budget rows:
                            BudgetRow dupRow = FindDuplicateRow(fieldsByColumnName);

                            if (dupRow != null)
                            {
                                // Duplicate row found
                                importedRow = dupRow;
                                // Add row ID:
                                _MatchingBudgetIDs.Add(dupRow.ID);
                            }
                            else
                            {
                                // no duplicate
                                if (AllowAddingNewBudgetRows)
                                {
                                    // add new row to table
                                    importedRow = _BudgetTable.NewBudgetRow();
                                    importedRow.IsIncome = false; // necessary initialization
                                    importedRow.Ignore = false; // necessary initialization
                                    importedRow.BalanceIsCalculated = false; // necessary initialization
                                }
                            }

                            // if no duplicate is found, and we're not allowed to add rows, then just move to the next line in src file:
                            if (importedRow == null)
                                continue;

                            // Copy fields in:
                            foreach (string columnName in fieldsByColumnName.Keys)
                            {
                                object newFieldValue = fieldsByColumnName[columnName];
                                if (newFieldValue != null)
                                    importedRow[columnName] = newFieldValue;
                            }
 
                            if (UpdateAccountFromSourceFile)
                                importedRow.Account = _SelectedAccount;

                            BudgetSourceFileItemsRow fileItemsRow = _SourceFileItemsTable.NewBudgetSourceFileItemsRow();
                            fileItemsRow.SourceFile = -1; // real value filled in on save 
                            fileItemsRow.BudgetItem = -1; // real value filled in on save 
                            fileItemsRow.SourceFileLine = lineNum; // 1-relative
                            _SourceFileItemsTable.AddBudgetSourceFileItemsRow(fileItemsRow);

                            _ImportedBudgetItems.Add(new BudgetAndSourceItemRows(importedRow, fileItemsRow));

                            // if new row, we need to add it to the DataTable:
                            if (dupRow == null)
                                _BudgetTable.AddBudgetRow(importedRow);
                        }
                    }
                }
            }
        }

        public void SaveChanges()
        {
            // DIAG use transactions (budgetTableAdapter.Transaction) in this
            MainDataSetTableAdapters.BudgetSourceFileTableAdapter sourceFileAdap = new BudgetSourceFileTableAdapter();
            _NewestSourceFileRow.ImportDateTime = DateTime.Now;
            sourceFileAdap.Update(_SourceFileTable);

            /* REMO
            foreach (MainDataSet.BudgetRow budgetRow in _Processor._ImportedBudgetRows)
            {
                budgetRow.SourceFile = _Processor._NewestSourceFileRow.FileID;
            }    
            */

            _BudgetAdapter.Update(_BudgetTable);

            foreach (BudgetAndSourceItemRows budgetObj in _ImportedBudgetItems)
            {
                // fill in new Items rows with updated, permanent ID field values:
                budgetObj._ItemsRow.BudgetItem = budgetObj._BudgetRow.ID;
                budgetObj._ItemsRow.SourceFile = _NewestSourceFileRow.FileID;
            }

            MainDataSetTableAdapters.BudgetSourceFileItemsTableAdapter fileItemsAdap = new BudgetSourceFileItemsTableAdapter();
            fileItemsAdap.Update(_SourceFileItemsTable);
        }

        // internally used classes
        public class BudgetAndSourceItemRows
        {
            public BudgetRow _BudgetRow;
            public BudgetSourceFileItemsRow _ItemsRow;

            public BudgetAndSourceItemRows(BudgetRow budgetRow, BudgetSourceFileItemsRow itemsRow)
            {
                _BudgetRow = budgetRow;
                _ItemsRow = itemsRow;
            }
        }
    }
}