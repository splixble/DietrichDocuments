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
        string PicsPath
        {
            get => Properties.Settings.Default.PicsPath;
            set
            {
                if (value != Properties.Settings.Default.PicsPath)
                {
                    Properties.Settings.Default.PicsPath = value;
                    Properties.Settings.Default.Save();
                    UpdateInfoBar();
                }
            }
        }

        string RawFilesPath
        {
            get => Properties.Settings.Default.RawFilesPath;
            set
            {
                if (value != Properties.Settings.Default.RawFilesPath)
                {
                    Properties.Settings.Default.RawFilesPath = value;
                    Properties.Settings.Default.Save();
                    UpdateInfoBar();
                }
            }
        }

        string LastSelectionsCSVPath
        {
            get => Properties.Settings.Default.LastSelectionsCSVPath;
            set
            {
                if (value != Properties.Settings.Default.LastSelectionsCSVPath)
                {
                    Properties.Settings.Default.LastSelectionsCSVPath = value;
                    Properties.Settings.Default.Save();
                    UpdateInfoBar();
                }
            }
        }

        public FormMain()
        {
            InitializeComponent();

            DisplayPhotos();
        }


        void UpdateInfoBar()
        {
            lblInfoBar.Text = PicsPath + "; " + LastSelectionsCSVPath;
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
            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Directory of photos:";
            if (PicsPath != null && PicsPath != "")
                dirDlg.SelectedPath = PicsPath;

            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default if changed:
            if (dirDlg.SelectedPath != PicsPath)
                PicsPath = dirDlg.SelectedPath;
        }

        void DisplayPhotos()
        {
            listFiles.Clear();
            pictureBox1.Image = null;

            string[] files = Directory.GetFiles(PicsPath);

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
            string picPath = Path.Combine(PicsPath, picFile);
            pictureBox1.Image = Image.FromFile(picPath);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check for Ctrl + S
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveSelectionsInCSV(this, new EventArgs());
                return true; // Indicate that the key was handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void openSelectionsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSV files (*.csv)|*.csv";
            dlg.Title = "CSV selection file:";
            if (LastSelectionsCSVPath != null)
            {
                dlg.FileName = LastSelectionsCSVPath; // or Path.GetFileName(LastSelectionsCSVPath);
                                                          //dlg.InitialDirectory = Path.GetDirectoryName(LastSelectionsCSVPath);
            }
            else
                dlg.InitialDirectory = PicsPath;

            if (dlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default:
            LastSelectionsCSVPath = dlg.FileName;

            string picsNotFoundMsg = "";
            using (StreamReader csvFileReader = new StreamReader(LastSelectionsCSVPath))
            {
                string csvLine = csvFileReader.ReadLine();
                while (!csvFileReader.EndOfStream)
                {
                    // TODO clear checked later.
                    ListViewItem item = listFiles.FindItemWithText(csvLine);
                    if (item != null)
                        item.Checked = true;
                    else
                        picsNotFoundMsg += csvLine + ", "; // use comma list tool TODO

                    csvLine = csvFileReader.ReadLine(); // read next line
                }
            }

            if (picsNotFoundMsg.Length > 0)
                MessageBox.Show("Previously checked pics not found: " + picsNotFoundMsg);
        }

        private void SaveSelectionsInCSV(object sender, EventArgs e)
        {
            SaveSelectionsInCSV(LastSelectionsCSVPath == null || LastSelectionsCSVPath == "");
        }

        private void saveSelectionsToCSVAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectionsInCSV(true);
        }

        void SaveSelectionsInCSV(bool promptForCSVPath)
        {

            if (promptForCSVPath)
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Filter = "CSV files (*.csv)|*.csv"; ;
                saveDlg.Title = "CSV file to save to:";
                if (LastSelectionsCSVPath != null)
                {
                    saveDlg.FileName = LastSelectionsCSVPath; // or Path.GetFileName(LastSelectionsCSVPath);
                    //dlg.InitialDirectory = Path.GetDirectoryName(LastSelectionsCSVPath);
                }
                else
                    saveDlg.InitialDirectory = PicsPath;

                if (saveDlg.ShowDialog() == DialogResult.Cancel)
                    return;

                // set new dir default if changed:
                LastSelectionsCSVPath = saveDlg.FileName;
            }

            using (StreamWriter csvFileWriter = new StreamWriter(LastSelectionsCSVPath))
            {
                foreach (ListViewItem item in listFiles.Items)
                {
                    if (item.Checked)
                        csvFileWriter.WriteLine(item.Text);
                }
            }

            MessageBox.Show("Wrote selections to: " + LastSelectionsCSVPath);
        }

        void SelectRawFilesPath()
        {
            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Directory of RAW Files:";
            if (RawFilesPath != null && RawFilesPath != "")
                dirDlg.SelectedPath = RawFilesPath;

            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default if changed:
            if (dirDlg.SelectedPath != RawFilesPath)
                RawFilesPath = dirDlg.SelectedPath;
        }

        private void deleteUnmatchedRAWFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO make these global const's
            string picExtension = ".JPG";
            string rawExtension = ".NEF";

            SelectRawFilesPath();
            if (RawFilesPath == null)
            {
                MessageBox.Show("No RAW picFiles path specified");
                return;
            }

            // Make look-uppable list of pic files:
            Dictionary<string, object> picFilesDict = new Dictionary<string, object>();
            string[] picFiles = Directory.GetFiles(PicsPath);
            foreach (string file in picFiles)
            {
                if (Path.GetExtension(file) == picExtension)
                    picFilesDict.Add(Path.GetFileNameWithoutExtension(file), null);
            }

            // Go through each raw rawFile, see if there's a non-deleted pic file:
            List<string> unmatchedRawFiles = new List<string>();
            string[] rawFiles = Directory.GetFiles(RawFilesPath);
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

        private void copySelectedPicsToFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string picsPath = Properties.Settings.Default.PicsPath;
            string destPath = picsPath;

            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Copy pics to:";
            if (destPath != null && destPath != "")
                dirDlg.SelectedPath = destPath;

            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;
            else
                destPath = dirDlg.SelectedPath;

            // Now, copy pics to new dest:
            int numCopied = 0;
            foreach (ListViewItem item in listFiles.Items)
            {
                if (item.Checked)
                {
                    File.Copy(Path.Combine(picsPath, item.Text), Path.Combine(destPath, item.Text));
                    numCopied++;
                }
            }
            MessageBox.Show(numCopied.ToString() + " pics copied");
        }
    }
}
