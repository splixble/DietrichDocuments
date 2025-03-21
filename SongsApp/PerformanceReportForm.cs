using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Songs
{
    public partial class PerformanceReportForm : Form
    {
        string NL => Environment.NewLine;

        AzureDataSet.ViewSongsSingleFieldDataTable _SongTable = new AzureDataSet.ViewSongsSingleFieldDataTable();
        AzureDataSet.performancesDataTable _PerfsTable = new AzureDataSet.performancesDataTable();
        AzureDataSet.venuesDataTable _VenuesTable = new AzureDataSet.venuesDataTable();
        AzureDataSet.songperformancesDataTable _SongPerfTable = new AzureDataSet.songperformancesDataTable();

        public PerformanceReportForm()
        {
            InitializeComponent();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            ShowPerformanceList(true);
        }

        public void ShowPerformanceList(bool songsILead)
        {
            /* DIAG rework the HTML by venue to use bookmarks:
             * <h2 id="C4">Chapter 4</h2>
            // DIAG -- indent, with 
<ul style="list-style: none;">
  < li > test </ li >
</ ul >
             * */
            // DIAG remove the function by this name in ListingForm 

            tb.Clear();

            AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter songAdap =
                new Songs.AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            songAdap.Fill(_SongTable);

            AzureDataSetTableAdapters.performancesTableAdapter perfAdap =
                new Songs.AzureDataSetTableAdapters.performancesTableAdapter();
            perfAdap.FillByDate(_PerfsTable, DateTime.Today.AddMonths(-3), DateTime.Today, songsILead); // DIAG configure by ctrls on form

            AzureDataSetTableAdapters.venuesTableAdapter venuesAdap =
                new Songs.AzureDataSetTableAdapters.venuesTableAdapter();
            venuesAdap.Fill(_VenuesTable);

            AzureDataSetTableAdapters.songperformancesTableAdapter songPerfAdap =
                new Songs.AzureDataSetTableAdapters.songperformancesTableAdapter();
            songPerfAdap.Fill(_SongPerfTable);

            DataView perfView = new DataView(_PerfsTable);
            if (cboxReverseDate.Checked)
                perfView.Sort = "PerformanceDate DESC";
            else
                perfView.Sort = "PerformanceDate ASC";


            if (rbHTMLVenueColumns.Checked)
                tb.Text = GenerateHTMLVenueColumnsText(perfView);
            else
            {
                foreach (DataRowView perfRowView in perfView)
                {
                    AzureDataSet.performancesRow perfRow = perfRowView.Row as AzureDataSet.performancesRow;
                    tb.Text += PerformanceText(perfRow, true) + NL;
                    foreach (string songLine in PerformanceSongList(perfRow))
                        tb.Text += "        " + songLine;
                }
            }
        }

        string GenerateHTMLVenueColumnsText(DataView perfView)
        {
            SortedList<string, List<AzureDataSet.performancesRow>> perfListsByVenue = new SortedList<string, List<AzureDataSet.performancesRow>>();

            // Arrange performances by venue:
            foreach (DataRowView perfRowView in perfView)
            {
                AzureDataSet.performancesRow perfRow = perfRowView.Row as AzureDataSet.performancesRow;
                AzureDataSet.venuesRow venueRow = _VenuesTable.FindByID(perfRow.Venue);
                if (!perfListsByVenue.ContainsKey(venueRow.Name))
                    perfListsByVenue.Add(venueRow.Name, new List<AzureDataSet.performancesRow>());
                perfListsByVenue[venueRow.Name].Add(perfRow);
            }

            // Put performances in an HTML table:
            // DIAG convert to HTML as necessary
            string html = "";
            html += "<html>" + NL;
            html += "<body>" + NL;
            html += "should show date <p>" + NL;
            html += "<table>" + NL;
            html += "<tr style=\"vertical-align:top\">" + NL;

            foreach (string venue in perfListsByVenue.Keys)
            {
                html += "<td>" + NL;
                html += ToHTML(venue) + "<p>";
                foreach (AzureDataSet.performancesRow perfRow in perfListsByVenue[venue])
                {
                    // Date and info about performance: 
                    html += ToHTML(PerformanceText(perfRow, false)) + "<br>" + NL;

                    // Songs played:
                    foreach (string songLine in PerformanceSongList(perfRow))
                        html += " &nbsp; &nbsp; " + songLine + "<br>" + NL;
                }
                html += "</td>" + NL;
            }

            html += "</tr>" + NL;
            html += "</table>" + NL;
            html += "</body>" + NL;

            html += "</html>" + NL;
            return  html;
        }

        string ToHTML(string text)
        {
            return text; // impl this DIAG
        }

        string PerformanceText(AzureDataSet.performancesRow perfRow, bool includeVenue)
        {
            string gigLine;
            gigLine = perfRow.PerformanceDate.ToLongDateString();
            if (includeVenue)
            {
                AzureDataSet.venuesRow venueRow = _VenuesTable.FindByID(perfRow.Venue);
                if (venueRow != null)
                    gigLine += ", " + venueRow.Name;
            }
            if (!perfRow.IsCommentNull())
                gigLine += ": " + perfRow.Comment;

            return gigLine; 
        }

        List<string> PerformanceSongList(AzureDataSet.performancesRow perfRow)
        {
            List<string> list = new List<string>();           

            DataView songPerfView = new DataView(_SongPerfTable);
            songPerfView.Sort = "Performance";
            DataView songView = new DataView(_SongTable);
            songView.Sort = "ID";

            DataRowView[] songPerfRowViews = songPerfView.FindRows(perfRow.ID);
            foreach (DataRowView songPerfRowView in songPerfRowViews)
            {
                AzureDataSet.songperformancesRow songPerfRow =
                    (AzureDataSet.songperformancesRow)songPerfRowView.Row;
                int songTableIndex = songView.Find(songPerfRow.Song);
                if (songTableIndex != -1)
                {
                    AzureDataSet.ViewSongsSingleFieldRow songRow =
                        (AzureDataSet.ViewSongsSingleFieldRow)songView[songTableIndex].Row;
                    string songLine = songRow.TitleAndArtist;
                    if (songPerfRow["Comment"] != DBNull.Value)
                        songLine += " (" + songPerfRow.Comment + ")";
                    songLine += NL;
                    list.Add(songLine);
                }
            }
            return list;
        }
    }
}
