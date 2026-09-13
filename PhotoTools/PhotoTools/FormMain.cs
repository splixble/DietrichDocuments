using Microsoft.VisualBasic.FileIO;
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
        string _RawFilesPath = Properties.Settings.Default.RawFilesPath;

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

            MessageBox.Show("Wrote rawFile: " + csvFilePath);
        }

        void SelectRawFilesPath()
        {
            string picsPath = Properties.Settings.Default.RawFilesPath;

            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Directory of RAW Files:";
            if (picsPath != null && picsPath != "")
                dirDlg.SelectedPath = picsPath;

            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default if changed:
            if (dirDlg.SelectedPath != picsPath)
            {
                picsPath = dirDlg.SelectedPath;
                Properties.Settings.Default.RawFilesPath = dirDlg.SelectedPath;
                Properties.Settings.Default.Save();
            }

            _RawFilesPath = picsPath;
        }

        private void deleteUnmatchedRAWFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO make these global const's
            string picExtension = ".JPG";
            string rawExtension = ".NEF";

            SelectRawFilesPath();
            if (_RawFilesPath == null)
            {
                MessageBox.Show("No RAW picFiles path specified");
                return;
            }

            // Make look-uppable list of pic files:
            Dictionary<string, object> picFilesDict = new Dictionary<string, object>();
            string[] picFiles = Directory.GetFiles(_PicsPath);
            foreach (string file in picFiles)
            {
                if (Path.GetExtension(file) == picExtension)
                    picFilesDict.Add(Path.GetFileNameWithoutExtension(file), null);
            }

            // Go through each raw rawFile, see if there's a non-deleted pic file:
            List<string> unmatchedRawFiles = new List<string>();
            string[] rawFiles = Directory.GetFiles(_RawFilesPath);
            foreach (string rawFile in rawFiles)
            {
                
                if (Path.GetExtension(rawFile) == rawExtension  // Is it a RAW file?
                    && !(picFilesDict.ContainsKey(Path.GetFileNameWithoutExtension(rawFile))))
                    unmatchedRawFiles.Add(rawFile);
            }

            if (unmatchedRawFiles.Count == 0)
                MessageBox.Show("No unmatched RAW files found.");
            else
            {
                string promptText = "Found " + unmatchedRawFiles.Count.ToString() + " unmatched RAW files:" + Environment.NewLine;
                foreach (string rawFilePath in unmatchedRawFiles)
                    promptText += Path.GetFileName(rawFilePath) + " ";
                promptText += Environment.NewLine + "Delete (recycle) them all?";
                DialogResult res = MessageBox.Show(promptText, "Unmatched RAW Files Found", MessageBoxButtons.OKCancel);

                if (res == DialogResult.OK)
                {
                    foreach (string rawFilePath in unmatchedRawFiles)
                        RecycleFile(rawFilePath);

                    MessageBox.Show("Recycled " + unmatchedRawFiles.Count.ToString() + " files.");
                }
            }
        }

        void RecycleFile(string filePath) // TODO put in libr
        {
            // TODO put in try/catch
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }
    }
}
