using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Songs.AzureDataSet;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // initialize date pickers:
            dpFromDate.Value = DateTime.Today.AddMonths(-3);
            dpToDate.Value = DateTime.Today;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            ShowPerformanceList(true);
        }

        public void ShowPerformanceList(bool songsILead)
        {
            tb.Clear();

            AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter songAdap =
                new Songs.AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            songAdap.Fill(_SongTable);

            AzureDataSetTableAdapters.performancesTableAdapter perfAdap =
                new Songs.AzureDataSetTableAdapters.performancesTableAdapter();
            perfAdap.FillByDate(_PerfsTable, dpFromDate.Value, dpToDate.Value, songsILead); 
            // DIAG what if the date pickers are unchecked?

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


            if (rbHTMLbyVenue.Checked)
                tb.Text = GenerateHTMLText(perfView, true);
            else if (rbHTMLbyDate.Checked)
                tb.Text = GenerateHTMLText(perfView, false);
            else
            {
                foreach (DataRowView perfRowView in perfView)
                {
                    AzureDataSet.performancesRow perfRow = perfRowView.Row as AzureDataSet.performancesRow;

                    tb.Text += perfRow.PerformanceDate.ToLongDateString();
                    AzureDataSet.venuesRow venueRow = _VenuesTable.FindByID(perfRow.Venue);
                    tb.Text += ", " + venueRow.Name;
                    if (!perfRow.IsCommentNull())
                        tb.Text += ": " + perfRow.Comment;
                    tb.Text += NL;

                    foreach (string songLine in PerformanceSongList(perfRow))
                        tb.Text += "        " + songLine;
                }
            }
        }

        string GenerateHTMLText(DataView perfView, bool byVenue)
        {
            string html = "";
            html += "<html>" + NL;
            html += "<body style=\"font-family: sans-serif\">" + NL;
            html += "Updated " + DateTime.Now.ToString("f")+ "<p>" + NL;

            if (byVenue)
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
                // (table not used) html += "<table>" + NL;
                // (table not used) html += "<tr style=\"vertical-align:top\">" + NL;

                foreach (string venue in perfListsByVenue.Keys)
                {
                    // (table not used) html += "<td>" + NL;
                    html += "<h2>" + ToHTML(venue) + "</h2>" + NL;
                    foreach (AzureDataSet.performancesRow perfRow in perfListsByVenue[venue])
                        html += HTMLPerformanceText(perfRow, true);
                    // (table not used) html += "</td>" + NL;
                }

                // (table not used) html += "</tr>" + NL;
                // (table not used) html += "</table>" + NL;
            }
            else
            {
                // by date
                foreach (DataRowView perfRowView in perfView)
                {
                    AzureDataSet.performancesRow perfRow = perfRowView.Row as AzureDataSet.performancesRow;
                    html += HTMLPerformanceText(perfRow, false);
                }
            }

            html += "</body>" + NL;
            html += "</html>" + NL;
            return  html;
        }

        string HTMLPerformanceText(AzureDataSet.performancesRow perfRow, bool byVenue)
        {
            string html = "";
            // Date and info about performance: 
            if (byVenue)
                html += "<b>" + perfRow.PerformanceDate.ToString("MMMM dd, yyyy") + "</b>"; // no day shown if By Venue; it's generally the same day
            else
            {
                html += "<b>" + perfRow.PerformanceDate.ToLongDateString();
                AzureDataSet.venuesRow venueRow = _VenuesTable.FindByID(perfRow.Venue);
                html += ", " + venueRow.Name + "</b>";
            }

            if (!perfRow.IsCommentNull())
                html += " " + ToHTML(perfRow.Comment);
            html += NL;

            // List of songs played:
            html += "<ul style = \"list-style: none; margin-top:0;\">" + NL;
            foreach (string songLine in PerformanceSongList(perfRow))
                html += "<li>" + songLine + "</li>" + NL;
            html += "</ul>" + NL;
            return html;
        }

        string ToHTML(string text)
        {
            return text; // impl this DIAG
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
                    list.Add(songLine);
                }
            }
            return list;
        }
    }
}
