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

        private void btnOpenSourceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog srcFileDlg = new OpenFileDialog();
            srcFileDlg.InitialDirectory = "D:\\Dietrich\\Business\\Budget App Input Files"; //get from Confile file, which s/b read at beginning and globally available
            srcFileDlg.Title = "Select source file:";
            DialogResult diaRes = srcFileDlg.ShowDialog();
            if (diaRes == DialogResult.OK)
            {
                using (StreamReader srcFileStream = new StreamReader(srcFileDlg.FileName));
            }
                    

        }

        private void SourceFileForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mainDataSet.BudgetSourceFileFormat' table. You can move, or remove it, as needed.
            this.budgetSourceFileFormatTableAdapter.Fill(this.mainDataSet.BudgetSourceFileFormat);

        }
    }
}
