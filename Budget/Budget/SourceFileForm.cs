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
using Microsoft.VisualBasic.FileIO;

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

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog srcFileDlg = new OpenFileDialog();
            srcFileDlg.InitialDirectory = "D:\\Dietrich\\Business\\Budget App Input Files"; //get from Config file, which s/b read at beginning and globally available
            srcFileDlg.Title = "Select source file:";
            DialogResult diaRes = srcFileDlg.ShowDialog();
            
            if (diaRes == DialogResult.OK)
            {
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
                using (TextFieldParser parser = new TextFieldParser(srcFileDlg.FileName))
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
                                        newBudgetRow[budgetColumn] = fileValue;
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
        }
    }
}
