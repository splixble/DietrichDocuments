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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace PhotoTools
{
    public partial class FormMain : Form
    {
        string _PicsPath = Properties.Settings.Default.PicsPath;

        public FormMain()
        {
            InitializeComponent();

            DisplayPhotos();
        }

        private void openPhotoDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // In Design mode?
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
                return;

            SelectPicsPath();
            DisplayPhotos();
        }

        void SelectPicsPath()
        {
            string picsPath = Properties.Settings.Default.PicsPath;

            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Directory of photos:";
            if (picsPath != null && picsPath != "")
                dirDlg.SelectedPath = picsPath;

            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default if changed:
            if (dirDlg.SelectedPath != picsPath)
            {
                picsPath = dirDlg.SelectedPath;
                Properties.Settings.Default.PicsPath = dirDlg.SelectedPath;
                Properties.Settings.Default.Save();
            }

            _PicsPath = picsPath;
        }

        void DisplayPhotos()
        {
            listFiles.Clear();
            pictureBox1.Image = null;

            string[] files = Directory.GetFiles(_PicsPath);

            foreach (string file in files)
            {
               
                listFiles.Items.Add(Path.GetFileName(file));
            }

            if (files.Length > 0)
                ShowPic(files[0]);
        }

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPic(listFiles.FocusedItem.Text);
            /*
            if (listFiles.SelectedItems.Count > 0)
                ShowPic(listFiles.SelectedItems[0].Text);
            else
                ShowPic(null);
            */
        }

        void ShowPic(string picFile)
        {
            string picPath = Path.Combine(_PicsPath, picFile);
            pictureBox1.Image = Image.FromFile(picPath);
        }

        private void saveSelectionsInCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string csvFilePath = Path.Combine(_PicsPath, "List1.csv"); // TODO prompt for name
            using (StreamWriter csvFileWriter = new StreamWriter(csvFilePath))
            {
                foreach (ListViewItem item in listFiles.Items)
                {
                    if (item.Checked)
                        csvFileWriter.WriteLine(item.Text);
                }
            }

            MessageBox.Show("Wrote file: " + csvFilePath);
        }
    }
}
