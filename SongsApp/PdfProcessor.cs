using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace Songs
{
    class PdfProcessor
    {
        public void ProcessPDFs()
        {
            const string customProperty = "/Custom";
            const string artistsProperty = "/Artists";

            // FolderBrowserDialog 

            AzureDataSet.viewsongsforsetlistsDataTable songTable = new AzureDataSet.viewsongsforsetlistsDataTable();
            AzureDataSetTableAdapters.viewsongsforsetlistsTableAdapter songAdap = new AzureDataSetTableAdapters.viewsongsforsetlistsTableAdapter();
            songAdap.FillByInTablet(songTable, true);

            /* 
            tb.Text += "title;pages;custom" + Environment.NewLine; ;
            foreach (AzureDataSet.viewsongsforsetlistsRow songRow in songTable)
                tb.Text += songRow.FullTitle + ";1;" + songRow.SetlistCaption + Environment.NewLine;
            */

            string pdfsWithoutRec = "";
            string recsWithoutPDFs = "";
            string matches = "";

            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Location of PDF lyric files:";
            dirDlg.SelectedPath = Properties.Settings.Default.LyricPDFsDirectory;
            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default if changed:
            if (dirDlg.SelectedPath != Properties.Settings.Default.LyricPDFsDirectory)
            {
                Properties.Settings.Default.LyricPDFsDirectory = dirDlg.SelectedPath;
                Properties.Settings.Default.Save();
            }

            string pdfDir = dirDlg.SelectedPath;
            DirectoryInfo dir = new DirectoryInfo(pdfDir);

            Dictionary<string, AzureDataSet.viewsongsforsetlistsRow> pdfsInDir = 
                new Dictionary<string, AzureDataSet.viewsongsforsetlistsRow>(StringComparer.CurrentCultureIgnoreCase); 
            // - key=pdf filename w/o dir; value = DB row if found for it
            foreach (FileInfo fileInfo in dir.GetFiles("*.pdf"))
                pdfsInDir.Add(fileInfo.Name, null);

            foreach(AzureDataSet.viewsongsforsetlistsRow songRow in songTable )
            {
                string dbPDFName;
                if (!songRow.IsDiffPDFNameNull() && songRow.DiffPDFName != "")
                    dbPDFName = songRow.DiffPDFName + ".pdf";
                else
                    dbPDFName = songRow.FullTitle + ".pdf";

                if (pdfsInDir.ContainsKey(dbPDFName))
                    pdfsInDir[dbPDFName] = songRow;
                else
                    recsWithoutPDFs += (recsWithoutPDFs == "" ? "" : ", ") + dbPDFName;
            }

            // DIAG try catch?
            // go thru files in DB and in dir:
            foreach (string pdfFileName in pdfsInDir.Keys)
            {
                if (pdfsInDir[pdfFileName] == null)
                    pdfsWithoutRec += (pdfsWithoutRec == "" ? "" : ", ") + pdfFileName;
                else
                {
                    string inputfile = pdfDir + "\\" + pdfFileName;
                    string outputfile = pdfDir + "\\Altered\\" + pdfFileName; // because norton firewall wont let me change in place...DIAG wtat to do?
                    string custValue = pdfsInDir[pdfFileName].SetlistCaption;
                    string artistsValue = pdfsInDir[pdfFileName].ArtistList; 
                    PdfDocument document = PdfReader.Open(inputfile);

                    bool setCustomProperty;
                    if (document.Info.Elements.ContainsKey(customProperty))
                    {
                        // set only if value changed
                        setCustomProperty = !(document.Info.Elements[customProperty] is PdfString)
                            || ((PdfString)document.Info.Elements[customProperty]).Value != custValue;
                    }
                    else
                        setCustomProperty = true;

                    bool setArtistProperty;
                    if (document.Info.Elements.ContainsKey(artistsProperty))
                    {
                        // set only if value changed
                        setArtistProperty = !(document.Info.Elements[artistsProperty] is PdfString)
                            || ((PdfString)document.Info.Elements[artistsProperty]).Value != artistsValue;
                    }
                    else
                        setArtistProperty = true;

                    if (setCustomProperty || setArtistProperty)
                    {
                        if (setCustomProperty)
                            document.Info.Elements[customProperty] = new PdfString(custValue);
                        if (setArtistProperty)
                            document.Info.Elements[artistsProperty] = new PdfString(artistsValue);
                        document.Save(outputfile);
                        matches += (matches == "" ? "" : ", ") + pdfFileName;
                    }
                }
            }
            // Test with cmd: \Dietrich\Apps\Exiftool\exiftool "D:\Dietrich\Music\LYRICS\TabletPDFs\Altered\A Song For You.pdf"

            MessageBox.Show("Wrote to files " + matches + "." + Environment.NewLine + Environment.NewLine +
                "No PDFs found for " + recsWithoutPDFs + "." + Environment.NewLine + Environment.NewLine +
                "No DB record found for " + pdfsWithoutRec);
        }

        public void CreateNoLyricFiles()
        {
            AzureDataSet.viewsongsforsetlistsDataTable songTable = new AzureDataSet.viewsongsforsetlistsDataTable();
            AzureDataSetTableAdapters.viewsongsforsetlistsTableAdapter songAdap = new AzureDataSetTableAdapters.viewsongsforsetlistsTableAdapter();
            songAdap.FillByInTablet(songTable, false);

            PdfDocument document = new PdfDocument();
            XFont fontHeading = new XFont("Times New Roman", 22);
            XFont fontSubheading = new XFont("Times New Roman", 18);

            // let user pick directory, if different from last usage:
            FolderBrowserDialog dirDlg = new FolderBrowserDialog();
            dirDlg.Description = "Location of lists of lyricless songs:";
            dirDlg.SelectedPath = Properties.Settings.Default.MobilesheetsNoLyricsDirectory;
            if (dirDlg.ShowDialog() == DialogResult.Cancel)
                return;

            // set new dir default if changed:
            if (dirDlg.SelectedPath != Properties.Settings.Default.MobilesheetsNoLyricsDirectory)
            {
                Properties.Settings.Default.LyricPDFsDirectory = dirDlg.SelectedPath;
                Properties.Settings.Default.Save();
            }

            string dir = dirDlg.SelectedPath;
            string pdfFilePath = dir + "\\NoLyrics" + DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss") + ".pdf";
            string csvFilePath = dir + "\\NoLyrics" + DateTime.Now.ToString("yyyy-MM-dd hh.mm.ss") + ".csv";

            using (StreamWriter csvFileWriter = new StreamWriter(csvFilePath))
            {
                // write csv heading:
                csvFileWriter.WriteLine("title;pages;custom;artists"); // NOTE: field name "artist" does not work; has to be "artists"

                int pageNum = 1;
                foreach (AzureDataSet.viewsongsforsetlistsRow songRow in songTable)
                {
                    csvFileWriter.WriteLine(songRow.FullTitle + ";" + pageNum.ToString() + ";" + songRow.SetlistCaption + ";" + songRow.ArtistList);

                    PdfPage page = document.AddPage();
                    page.Size = PdfSharp.PageSize.Letter;
                    XUnit top = new XUnit(0, XGraphicsUnit.Inch);
                    XUnit left = new XUnit(0, XGraphicsUnit.Inch);
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XRect textRect = new XRect(0, new XUnit(1, XGraphicsUnit.Inch), page.Width, new XUnit(1, XGraphicsUnit.Inch));

                    gfx.DrawString(songRow.FullTitle, fontHeading, XBrushes.Black, textRect, XStringFormats.TopCenter);
                    textRect.Y += new XUnit(.5, XGraphicsUnit.Inch);
                    gfx.DrawString(songRow.SetlistCaption, fontSubheading, XBrushes.Black, textRect, XStringFormats.TopCenter);

                    pageNum++;
                }
            }

            document.Save(pdfFilePath);

            MessageBox.Show("Wrote files:" + Environment.NewLine + csvFilePath + Environment.NewLine + pdfFilePath);
        }
    }
}
