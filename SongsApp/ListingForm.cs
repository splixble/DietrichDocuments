using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Songs
{
    public partial class ListingForm : Form
    {
        public TextBox TB { get { return tb; } }

        public ListingForm()
        {
            InitializeComponent();
        }

        public void ShowTOC()
        {
            AzureDataSet.ViewTOCArtistDataTable tblArtist = new AzureDataSet.ViewTOCArtistDataTable();
            AzureDataSet.ViewTOCTitleDataTable tblTitle = new AzureDataSet.ViewTOCTitleDataTable();
            AzureDataSetTableAdapters.ViewTOCArtistTableAdapter adapArtist =
                new Songs.AzureDataSetTableAdapters.ViewTOCArtistTableAdapter();
            AzureDataSetTableAdapters.ViewTOCTitleTableAdapter adapTitle =
                new Songs.AzureDataSetTableAdapters.ViewTOCTitleTableAdapter();
            adapArtist.Fill(tblArtist);
            adapTitle.Fill(tblTitle);

            // By title:
            foreach (AzureDataSet.ViewTOCTitleRow rowTitle in tblTitle)
            {
                tb.Text += rowTitle.TitleAndArtist + "\t" + rowTitle.PageNumber.ToString() + Environment.NewLine;
            }

            tb.Text += Environment.NewLine + Environment.NewLine; // blank lines

            // by artist:
            object obLastArtist = DBNull.Value;
            foreach (AzureDataSet.ViewTOCArtistRow rowArtist in tblArtist)
            {
                if (!(rowArtist[tblArtist.FullArtistNameColumn].Equals(obLastArtist)))
                {
                    string artistName = "";
                    if (rowArtist[tblArtist.FullArtistNameColumn] is string)
                        artistName = rowArtist.FullArtistName;
                    tb.Text += artistName + Environment.NewLine;
                    obLastArtist = rowArtist[tblArtist.FullArtistNameColumn];
                }
                tb.Text += "\t" + rowArtist.FullTitle + "\t" + rowArtist.PageNumber.ToString() + Environment.NewLine;
            }

            ShowDialog();
        }

        public void ShowPerformanceTotalsList()
        {
            AzureDataSet.viewsongperformancetotalsDataTable tblTotals =
                new AzureDataSet.viewsongperformancetotalsDataTable();
            AzureDataSetTableAdapters.viewsongperformancetotalsTableAdapter adap =
                new Songs.AzureDataSetTableAdapters.viewsongperformancetotalsTableAdapter();
            adap.Fill(tblTotals);

            foreach (AzureDataSet.viewsongperformancetotalsRow row in tblTotals)
            {
                tb.Text += row.RowNum.ToString() + "\t" + row.TitleAndArtist + "\t" + row.Total.ToString() + "\t" +
                    row.firstPerformed.ToShortDateString() + "\t" + row.lastPerformed.ToShortDateString() +
                    Environment.NewLine;
            }
            ShowDialog();
        }

        public void ShowListByEachFlag()
        {
            AzureDataSet.flagsDataTable flagsTable = new AzureDataSet.flagsDataTable();
            AzureDataSetTableAdapters.flagsTableAdapter adap = new Songs.AzureDataSetTableAdapters.flagsTableAdapter();
            adap.Fill(flagsTable);
            foreach (AzureDataSet.flagsRow flagsRow in flagsTable)
            {
                tb.Text += "`" + flagsRow.FlagName + Environment.NewLine;
                string songsWhereClause = "WHERE (ID in (SELECT Song FROM FlaggedSongs WHERE FlagID = " +
                    flagsRow.FlagID + "))";
                GenerateList(songsWhereClause);
            }

            ShowDialog();
        }

        public void ShowList(string songsWhereClause)
        {
            GenerateList(songsWhereClause);
            ShowDialog();
        }

        public void GenerateList(string songsWhereClause)
        {
            if (songsWhereClause != "")
                songsWhereClause += " AND Cover = 0";
            else
                songsWhereClause = "WHERE Cover = 0";

            AzureDataSet.ViewSongsSingleFieldDataTable tblSongs = new AzureDataSet.ViewSongsSingleFieldDataTable();
            string query = "SELECT * FROM ViewSongsSingleField " + songsWhereClause + " ORDER BY Title, TitlePrefix";
            SqlDataAdapter songsAdap = new SqlDataAdapter(query, 
                global::Songs.Properties.Settings.Default.AzureConnectionString);
            songsAdap.Fill(tblSongs);

            foreach (AzureDataSet.ViewSongsSingleFieldRow rowSong in tblSongs)
            {
                tb.Text += rowSong.SongFull + Environment.NewLine;
            }
        }

        public void ShowListByArtist(string songsWhereClause)
        {
            AzureDataSet.ViewSongsSingleFieldDataTable tblSongs = new AzureDataSet.ViewSongsSingleFieldDataTable();
            string query = "SELECT * FROM ViewSongsSingleField " + songsWhereClause + 
                " ORDER BY ArtistLastName, ArtistFirstName, Title, TitlePrefix";
            SqlDataAdapter songsAdap = new SqlDataAdapter(query,
                global::Songs.Properties.Settings.Default.AzureConnectionString);
            songsAdap.Fill(tblSongs);

            string lastArtist = "";
            foreach (AzureDataSet.ViewSongsSingleFieldRow rowSong in tblSongs)
            {
                string currentArtist = rowSong[tblSongs.FullArtistNameColumn] as string;
                if (currentArtist == null)
                    currentArtist = "";
                if (currentArtist != lastArtist)
                {
                    tb.Text += currentArtist + Environment.NewLine;
                    lastArtist = currentArtist;
                }
                tb.Text += "\t" + rowSong.TitleAndInfo + Environment.NewLine;
            }

            ShowDialog();
        }

        public void ShowPerformanceList(bool songsILead)
        {
            AzureDataSet.ViewSongsSingleFieldDataTable songTable = new AzureDataSet.ViewSongsSingleFieldDataTable();
            AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter songAdap =
                new Songs.AzureDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            songAdap.Fill(songTable);

            AzureDataSet.performancesDataTable perfsTable = new AzureDataSet.performancesDataTable();
            AzureDataSetTableAdapters.performancesTableAdapter perfAdap =
                new Songs.AzureDataSetTableAdapters.performancesTableAdapter();
            perfAdap.FillByDidILead(perfsTable, songsILead);

            AzureDataSet.venuesDataTable venuesTable = new AzureDataSet.venuesDataTable();
            AzureDataSetTableAdapters.venuesTableAdapter venuesAdap =
                new Songs.AzureDataSetTableAdapters.venuesTableAdapter();
            venuesAdap.Fill(venuesTable);

            AzureDataSet.songperformancesDataTable songPerfTable = new AzureDataSet.songperformancesDataTable();
            AzureDataSetTableAdapters.songperformancesTableAdapter songPerfAdap =
                new Songs.AzureDataSetTableAdapters.songperformancesTableAdapter();
            songPerfAdap.Fill(songPerfTable);

            foreach (AzureDataSet.performancesRow perfRow in perfsTable)
            {
                string gigLine = perfRow.PerformanceDate.ToLongDateString();
                AzureDataSet.venuesRow venueRow = venuesTable.FindByID(perfRow.Venue);
                if (venueRow != null)
                    gigLine += ", " + venueRow.Name;
                if (perfRow["Comment"] != DBNull.Value)
                    gigLine += ": " + perfRow.Comment;
                gigLine += Environment.NewLine;
                tb.Text += gigLine;

                DataView songPerfView = new  DataView(songPerfTable);
                songPerfView.Sort = "Performance"; 
                DataView songView = new  DataView(songTable);
                songView.Sort = "ID"; 

                DataRowView[] rowViews = songPerfView.FindRows(perfRow.ID);
                foreach (DataRowView rowView in rowViews)
                {
                    AzureDataSet.songperformancesRow songPerfRow = 
                        (AzureDataSet.songperformancesRow)rowView.Row;
                    int songTableIndex = songView.Find(songPerfRow.Song);
                    if (songTableIndex != -1)
                    {
                        AzureDataSet.ViewSongsSingleFieldRow songRow =
                            (AzureDataSet.ViewSongsSingleFieldRow)songView[songTableIndex].Row;
                        string songLine = "        " + songRow.TitleAndArtist;
                        if (songPerfRow["Comment"] != DBNull.Value)
                            songLine += " (" + songPerfRow.Comment + ")";
                        songLine += Environment.NewLine;
                        tb.Text += songLine;
                    }
                }
            }
            ShowDialog();
        }
    }
}