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
                MainDataSet.BudgetSourceFileFormatRow formatRow = SourceFileFormatTable.FindByFormatCode(comboSrcFileFormat.SelectedValue as string);
                string[] formatFields = formatRow.FormatColumns.Split(',');

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
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(","); // allow tab delim?
                    while (!parser.EndOfData)
                    {
                        //Processing row
                        string[] fileFields = parser.ReadFields();

                        bool lineParsable = true;

                        MainDataSet.BudgetRow newBudgetRow = budgetCtrl.BudgetTable.NewBudgetRow();
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
                                    newBudgetRow[budgetColumn] = fileFields[formatColIndex];
                                else if (budgetColumn.DataType == typeof(Decimal))
                                {
                                    Decimal fileValue;
                                    if (Decimal.TryParse(fileFields[formatColIndex], out fileValue))
                                    {
                                        // For Amount and Balance columns, handle the special case in which source file lists credits as negative and debits as positive:
                                        if (formatRow.CreditsAreNegative)
                                            fileValue = -fileValue;

                                        newBudgetRow[budgetColumn] = fileValue;
                                    }
                                    else
                                        lineParsable = false;
                                }
                                else if (budgetColumn.DataType == typeof(DateTime))
                                {
                                    DateTime fileValue;
                                    if (DateTime.TryParse(fileFields[formatColIndex], out fileValue))
                                        newBudgetRow[budgetColumn] = fileValue;
                                    else
                                        lineParsable = false;
                                }
                                if (!lineParsable)
                                    break;
                            }
                        }

                        if (lineParsable)
                        {
                            newBudgetRow.Account = comboAccount.SelectedValue as string;// DIAG make sure it's selected
                            newBudgetRow.Ignore = false; // necessary initialization
                            budgetCtrl.BudgetTable.AddBudgetRow(newBudgetRow);
                        }
                    }
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
            MainDataSet.BudgetSourceFileDataTable sourceFileTable = new MainDataSet.BudgetSourceFileDataTable();
            MainDataSetTableAdapters.BudgetSourceFileTableAdapter sourceFileAdap = new BudgetSourceFileTableAdapter();

            // make new source file row:
            MainDataSet.BudgetSourceFileRow newSourceFileRow = sourceFileTable.NewBudgetSourceFileRow();
            newSourceFileRow.FilePath = _SourceFileName;
            newSourceFileRow.Account = comboAccount.SelectedValue as string;
            sourceFileTable.AddBudgetSourceFileRow(newSourceFileRow);
            sourceFileAdap.Update(sourceFileTable);

            foreach (MainDataSet.BudgetRow budgetRow in budgetCtrl.BudgetTable)
            {
                // DIAG and check for duplicate budget recs!
                if (budgetRow.RowState == DataRowState.Added)
                    budgetRow.SourceFile = newSourceFileRow.FileID;
            }    
            budgetCtrl.BudgetAdapter.Update(budgetCtrl.BudgetTable);
            budgetCtrl.Grid.Refresh();
        }
    }
}
