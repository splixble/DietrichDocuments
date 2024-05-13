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
    public partial class SourceFileForm : Form
    {
        public SourceFileForm()
        {
            InitializeComponent();
        }

        MainDataSet.BudgetSourceFileFormatDataTable SourceFileFormatTable => mainDataSet.BudgetSourceFileFormat;

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog srcFileDlg = new OpenFileDialog();
            srcFileDlg.InitialDirectory = "D:\\Dietrich\\Business\\Budget App Input Files"; //get from Config file, which s/b read at beginning and globally available
            srcFileDlg.Title = "Select source file:";
            DialogResult diaRes = srcFileDlg.ShowDialog();
            // DIAG they gotta select a format
            if (diaRes == DialogResult.OK)
            {
                using (StreamReader srcFileStream = new StreamReader(srcFileDlg.FileName))
                {
                    MainDataSet.BudgetSourceFileFormatRow formatRow = SourceFileFormatTable.FindByFormatCode(comboSrcFileFormat.SelectedValue as string);
                    string[] formatFields = formatRow.FormatColumns.Split(',');

                    // DIAG Use my csv file object from last project!
                    // Go through each line of text in the file:
                    string fileLine;
                    do
                    {
                        fileLine = srcFileStream.ReadLine();
                        if (fileLine == null)
                            break;

                        string[] fileFields = fileLine.Split(','); // DIAG delimiter dependent
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
                            newBudgetRow.Account = "DIAG";// DIAG gotta prompt for
                            newBudgetRow.Ignore = false; // necessary initialization
                            budgetCtrl.BudgetTable.AddBudgetRow(newBudgetRow);
                        }
                    }
                    while (fileLine != null);
                }
            }
            budgetCtrl.Refresh();
        }

        private void SourceFileForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.BudgetSourceFileFormat' table. You can move, or remove it, as needed.
            this.budgetSourceFileFormatTableAdapter.Fill(this.mainDataSet.BudgetSourceFileFormat);

        }
    }
}
