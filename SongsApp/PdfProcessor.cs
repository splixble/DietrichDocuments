using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Songs
{
    class PdfProcessor
    {
        public void ProcessPDFs()
        {
            const string customProperty = "/Custom";
            const string artistsProperty = "/Artists";
            const string collectionProperty = "/Collection";

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
                    string collectionsValue = pdfsInDir[pdfFileName].CollectionList;
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

                    bool setCollectionProperty;
                    if (document.Info.Elements.ContainsKey(collectionProperty))
                    {
                        // set only if value changed
                        setCollectionProperty = !(document.Info.Elements[collectionProperty] is PdfString)
                            || ((PdfString)document.Info.Elements[collectionProperty]).Value != collectionsValue;
                    }
                    else
                        setCollectionProperty = true;

                    if (setCustomProperty || setArtistProperty || setCollectionProperty)
                    {
                        if (setCustomProperty)
                            document.Info.Elements[customProperty] = new PdfString(custValue);
                        if (setArtistProperty)
                            document.Info.Elements[artistsProperty] = new PdfString(artistsValue);
                        if (setCollectionProperty)
                            document.Info.Elements[collectionProperty] = new PdfString(collectionsValue);
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

            PdfDocument document = new PdfDocument();

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
                csvFileWriter.WriteLine("title;pages;custom;artists;collection"); // NOTE: field name "artist" does not work; has to be "artists"
                int pageNum = 1;

                songAdap.FillByInTablet(songTable, false);
                WriteSongsToCSVAndPDFFiles(songTable, csvFileWriter, document, ref pageNum);
                
                songAdap.FillWithBandRepertoire(songTable);
                WriteSongsToCSVAndPDFFiles(songTable, csvFileWriter, document, ref pageNum);
            }

            document.Save(pdfFilePath);

            MessageBox.Show("Wrote files:" + Environment.NewLine + csvFilePath + Environment.NewLine + pdfFilePath);
        }

        void WriteSongsToCSVAndPDFFiles(AzureDataSet.viewsongsforsetlistsDataTable songTable, StreamWriter csvFileWriter, PdfDocument document, ref int pageNum)
        {
            var options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            XFont fontHeading = new XFont("Segoe UI Symbol", 30, XFontStyle.Bold, options);
            XFont fontSubheading = new XFont("Segoe UI Symbol", 22, XFontStyle.Bold, options);
            // NOTE: Segoe UI Symbol is the first font I found that renders the flat symbol (♭)

            foreach (AzureDataSet.viewsongsforsetlistsRow songRow in songTable)
            {
                csvFileWriter.WriteLine(songRow.RepertoirePrefix + songRow.FullTitle + ";" + pageNum.ToString() + ";" + songRow.SetlistCaption + ";" + songRow.ArtistList
                    + ";" + songRow.CollectionList);

                XUnit inch = new XUnit(1, XGraphicsUnit.Inch);
                XUnit halfInch = new XUnit(0.5, XGraphicsUnit.Inch);

                XUnit titleTop = new XUnit(0.5, XGraphicsUnit.Inch);
                XUnit captionTop = new XUnit(1, XGraphicsUnit.Inch);
                XUnit infoTop = new XUnit(1.5, XGraphicsUnit.Inch);

                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.Letter;
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Calculate the height of the SetlistInfo (the one variable dimension on the page), then reset the page size so that it takes up
                // no more vertical space than necessary:
                double infoTextHeight = GetTextHeight(gfx, fontSubheading, songRow.SetlistInfo, page.Width - inch);
                page.Height = infoTop + infoTextHeight + halfInch;

                XRect textRect = new XRect(halfInch, titleTop, page.Width - inch, inch);
                gfx.DrawString(songRow.FullTitle, fontHeading, XBrushes.Black, textRect, XStringFormats.TopLeft);

                textRect.Y = captionTop;
                gfx.DrawString(songRow.SetlistCaption, fontSubheading, XBrushes.Black, textRect, XStringFormats.TopLeft);

                textRect.Y = infoTop;
                textRect.Height = infoTextHeight;
                XTextFormatter txtFmt = new XTextFormatter(gfx);
                txtFmt.DrawString(songRow.SetlistInfo, fontSubheading, XBrushes.Black, textRect, XStringFormats.TopLeft);
                // gfx.DrawString(songRow.SetlistInfo, fontSubheading, XBrushes.Black, textRect, XStringFormats.TopCenter);

                pageNum++;
            }
        }

        // this is adapted from https://stackoverflow.com/questions/21947827/measuring-text-height-within-a-rectangle-pdfsharp : 
        private double GetTextHeight(XGraphics gfx, XFont font, string text, double rectWidth)
        {
            var fontHeight = font.GetHeight();
            var absoluteTextHeight = gfx.MeasureString(text, font).Height;
            var absoluteTextWidth = gfx.MeasureString(text, font).Width;

            if (absoluteTextWidth > rectWidth)
            {
                var linesToAdd = (int)Math.Ceiling(absoluteTextWidth / 290) - 1;
                return absoluteTextHeight + linesToAdd * (fontHeight);
            }
            return absoluteTextHeight;
        }

    }
}
