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

            comboSrcFileFormat.DataSource = Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;
            comboSrcFileFormat.DisplayMember = "FormatCode";
            comboSrcFileFormat.ValueMember = "FormatCode";
        }

        public MainDataSet LookupMainDataSet => Program.LookupTableSet.MainDataSet;

        MainDataSet.BudgetSourceFileFormatDataTable SourceFileFormatTable => Program.LookupTableSet.MainDataSet.BudgetSourceFileFormat;

        AlteredTableData _AlteredTableData = null;

        string _SourceFileName = "";

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog srcFileDlg = new OpenFileDialog();
            srcFileDlg.InitialDirectory = "D:\\Dietrich\\Business\\Budget App Input Files"; //get from Config file, which s/b read at beginning and globally available
            srcFileDlg.Title = "Select source file:";
            DialogResult diaRes = srcFileDlg.ShowDialog();
            
            if (diaRes == DialogResult.OK)
            {
                _SourceFileName = srcFileDlg.FileName;

                // DIAG so, just one src file at a time, read in and save, then go to next src file? And do we need a Cancel button?
                _AlteredTableData = new AlteredTableData();

                // make new source file row:
                _AlteredTableData._NewestSourceFileRow = _AlteredTableData._SourceFileTable.NewBudgetSourceFileRow();
                _AlteredTableData._NewestSourceFileRow.FilePath = _SourceFileName;
                _AlteredTableData._NewestSourceFileRow.Account = comboAccount.SelectedValue as string;
                _AlteredTableData._SourceFileTable.AddBudgetSourceFileRow(_AlteredTableData._NewestSourceFileRow);

                MainDataSet.BudgetSourceFileFormatRow formatRow = SourceFileFormatTable.FindByFormatCode(comboSrcFileFormat.SelectedValue as string);
                string[] formatFields = formatRow.FormatColumns.Split(',');

                // By default, show only new (ID negative) records: 
                budgetCtrl.BudgetBindingSource.Filter = "ID<0";

                // Read in the full Budget table, to check for duplicates:
                budgetCtrl.BudgetAdapter.Fill(budgetCtrl.BudgetTable);

                /* Old file reading code:
                using (StreamReader srcFileStream = new StreamReader(srcFileDlg.FileName))
                {
                    // Go through each line of text in the file:
                    string fileLine;
                    do
                    {
                        fileLine = srcFileStream.ReadLine();
                        if (fileLine == null)
                            break;

                        string[] fileFields = fileLine.Split(','); // DIAG delimiter dependent
                        ...
                    }
                    while (fileLine != null);
                 * */

                // use Microsoft.VisualBasic.FileIO objects (TextFieldParser, TextFieldType) to load csv files:
                using (TextFieldParser parser = new TextFieldParser(_SourceFileName))
                {
                    List<int> dupBudgetIDs = new List<int>();   

                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // allow tab delim?
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fileFields = parser.ReadFields();

                        bool lineParsable = true;

                        Dictionary<string, object> fieldsByColumnName = new Dictionary<string, object>();

                        for (int formatColIndex = 0; formatColIndex < formatFields.Length; formatColIndex++) 
                        {
                            if (formatFields[formatColIndex] != "")
                            {
                                if (formatColIndex >= fileFields.Length)
                                {
                                    lineParsable = false;
                                    break;
                                }

                                // which Budget column is it?
                                if (!budgetCtrl.BudgetTable.Columns.Contains(formatFields[formatColIndex]))
                                {
                                    lineParsable = false;
                                    break;
                                }
                                DataColumn budgetColumn = budgetCtrl.BudgetTable.Columns[formatFields[formatColIndex]];

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

                        if (lineParsable)
                        {
                            // DIAG make sure Account is selected

                            BudgetRow importedRow;

                            // Check for duplicate Budget rows:
                            BudgetRow dupRow = budgetCtrl.BudgetTable.FindDuplicate(
                                (DateTime)fieldsByColumnName["TrDate"],
                                (decimal)fieldsByColumnName["Amount"],
                                (string)fieldsByColumnName["Descrip"],
                                comboAccount.SelectedValue as string);

                            if (dupRow != null)
                            {
                                // Duplicate row found
                                importedRow = dupRow;
                                // Add row ID:
                                dupBudgetIDs.Add(dupRow.ID);
                            }
                            else
                            {
                                // no duplicate; add new row to table
                                importedRow = budgetCtrl.BudgetTable.NewBudgetRow();
                                importedRow.IsIncome = false; // necessary initialization
                                importedRow.Ignore = false; // necessary initialization
                            }

                            // Copy fields in:
                            foreach (string columnName in fieldsByColumnName.Keys)
                            {
                                object newFieldValue = fieldsByColumnName[columnName];
                                if (newFieldValue != null)
                                    importedRow[columnName] = newFieldValue;
                            }

                            importedRow.Account = comboAccount.SelectedValue as string;
                            _AlteredTableData._ImportedBudgetRows.Add(importedRow);

                            // if new row, we need to add it to the DataTable:
                            if (dupRow == null)
                                budgetCtrl.BudgetTable.AddBudgetRow(importedRow);
                        }
                    }

                    // update to include changed duplicate rows:
                    string dupBudgetIDsList = "";
                    foreach (int dupID in dupBudgetIDs)
                    {
                        if (dupBudgetIDsList != "")
                            dupBudgetIDsList += ",";
                        dupBudgetIDsList += dupID;
                    }
                    budgetCtrl.BudgetBindingSource.Filter = "ID<0 OR ID IN (" + dupBudgetIDsList + ")";
                }
            }
            budgetCtrl.Refresh();
        }

        private void SourceFileForm_Load(object sender, EventArgs e)
        {
            budgetCtrl.CreateNewSourceFileRow = true;
        }

        private void btnSaveBudgetItems_Click(object sender, EventArgs e)
        {
            // DIAG should set the new source file's (temp) ID when it's first added (substituted on save) -- so we can import >1 file before saving
            // DIAG use transactions (budgetTableAdapter.Transaction) in this
            MainDataSetTableAdapters.BudgetSourceFileTableAdapter sourceFileAdap = new BudgetSourceFileTableAdapter();
            sourceFileAdap.Update(_AlteredTableData._SourceFileTable);

            foreach (MainDataSet.BudgetRow budgetRow in _AlteredTableData._ImportedBudgetRows)
            {
                budgetRow.SourceFile = _AlteredTableData._NewestSourceFileRow.FileID;
            }    
            budgetCtrl.BudgetAdapter.Update(budgetCtrl.BudgetTable);
            budgetCtrl.BudgetBindingSource.Filter = "SourceFile=" + _AlteredTableData._NewestSourceFileRow.FileID; 
            budgetCtrl.Grid.Refresh();
        }

        class AlteredTableData
        {
            public MainDataSet.BudgetSourceFileDataTable _SourceFileTable = new MainDataSet.BudgetSourceFileDataTable();
            public MainDataSet.BudgetSourceFileRow _NewestSourceFileRow = null;
            public List<MainDataSet.BudgetRow> _ImportedBudgetRows = new List<MainDataSet.BudgetRow>();
        }

    }
}
