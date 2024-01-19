using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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
            MySqlDataSet.ViewTOCArtistDataTable tblArtist = new MySqlDataSet.ViewTOCArtistDataTable();
            MySqlDataSet.ViewTOCTitleDataTable tblTitle = new MySqlDataSet.ViewTOCTitleDataTable();
            MySqlDataSetTableAdapters.ViewTOCArtistTableAdapter adapArtist =
                new Songs.MySqlDataSetTableAdapters.ViewTOCArtistTableAdapter();
            MySqlDataSetTableAdapters.ViewTOCTitleTableAdapter adapTitle =
                new Songs.MySqlDataSetTableAdapters.ViewTOCTitleTableAdapter();
            adapArtist.Fill(tblArtist);
            adapTitle.Fill(tblTitle);

            // By title:
            foreach (MySqlDataSet.ViewTOCTitleRow rowTitle in tblTitle)
            {
                tb.Text += rowTitle.TitleAndArtist + "\t" + rowTitle.PageNumber.ToString() + Environment.NewLine;
            }

            tb.Text += Environment.NewLine + Environment.NewLine; // blank lines

            // by artist:
            object obLastArtist = DBNull.Value;
            foreach (MySqlDataSet.ViewTOCArtistRow rowArtist in tblArtist)
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
            ViewSongPerformanceTotalsDataSet.viewsongperformancetotalsDataTable tblTotals =
                new ViewSongPerformanceTotalsDataSet.viewsongperformancetotalsDataTable();
            ViewSongPerformanceTotalsDataSetTableAdapters.viewsongperformancetotalsTableAdapter adap =
                new Songs.ViewSongPerformanceTotalsDataSetTableAdapters.viewsongperformancetotalsTableAdapter();
            adap.Fill(tblTotals);

            foreach (ViewSongPerformanceTotalsDataSet.viewsongperformancetotalsRow row in tblTotals)
            {
                tb.Text += row.TitleAndArtist + "\t" + row.Total.ToString() + "\t" +
                    row.firstPerformed.ToShortDateString() + "\t" + row.lastPerformed.ToShortDateString() +
                    Environment.NewLine;
            }
            ShowDialog();
        }

        public void ShowListByEachFlag()
        {
            MySqlDataSet.flagsDataTable flagsTable = new MySqlDataSet.flagsDataTable();
            MySqlDataSetTableAdapters.flagsTableAdapter adap = new Songs.MySqlDataSetTableAdapters.flagsTableAdapter();
            adap.Fill(flagsTable);
            foreach (MySqlDataSet.flagsRow flagsRow in flagsTable)
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

            ViewSongsDataSet.viewsongssinglefieldDataTable tblSongs = new ViewSongsDataSet.viewsongssinglefieldDataTable();
            string query = "SELECT * FROM ViewSongsSingleField " + songsWhereClause + " ORDER BY Title, TitlePrefix";
            MySqlDataAdapter songsAdap = new MySqlDataAdapter(query, 
                global::Songs.Properties.Settings.Default.songbookConnectionString);
            songsAdap.Fill(tblSongs);

            foreach (ViewSongsDataSet.viewsongssinglefieldRow rowSong in tblSongs)
            {
                tb.Text += rowSong.SongFull + Environment.NewLine;
            }
        }

        public void ShowListByArtist(string songsWhereClause)
        {
            ViewSongsDataSet.viewsongssinglefieldDataTable tblSongs = new ViewSongsDataSet.viewsongssinglefieldDataTable();
            string query = "SELECT * FROM ViewSongsSingleField " + songsWhereClause + 
                " ORDER BY ArtistLastName, ArtistFirstName, Title, TitlePrefix";
            MySqlDataAdapter songsAdap = new MySqlDataAdapter(query,
                global::Songs.Properties.Settings.Default.songbookConnectionString);
            songsAdap.Fill(tblSongs);

            string lastArtist = "";
            foreach (ViewSongsDataSet.viewsongssinglefieldRow rowSong in tblSongs)
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
            ViewSongsDataSet.viewsongssinglefieldDataTable songTable = new ViewSongsDataSet.viewsongssinglefieldDataTable();
            ViewSongsDataSetTableAdapters.viewsongssinglefieldTableAdapter songAdap =
                new Songs.ViewSongsDataSetTableAdapters.viewsongssinglefieldTableAdapter();
            songAdap.Fill(songTable);

            PerformanceDataSet.performancesDataTable perfsTable = new PerformanceDataSet.performancesDataTable();
            PerformanceDataSetTableAdapters.performancesTableAdapter perfAdap =
                new Songs.PerformanceDataSetTableAdapters.performancesTableAdapter();
            perfAdap.FillByDidILead(perfsTable, songsILead ? 1 : 0);

            PerformanceDataSet.venuesDataTable venuesTable = new PerformanceDataSet.venuesDataTable();
            PerformanceDataSetTableAdapters.venuesTableAdapter venuesAdap =
                new Songs.PerformanceDataSetTableAdapters.venuesTableAdapter();
            venuesAdap.Fill(venuesTable);

            PerformanceDataSet.songperformancesDataTable songPerfTable = new PerformanceDataSet.songperformancesDataTable();
            PerformanceDataSetTableAdapters.songperformancesTableAdapter songPerfAdap =
                new Songs.PerformanceDataSetTableAdapters.songperformancesTableAdapter();
            songPerfAdap.Fill(songPerfTable);

            foreach (PerformanceDataSet.performancesRow perfRow in perfsTable)
            {
                string gigLine = perfRow.PerformanceDate.ToLongDateString();
                PerformanceDataSet.venuesRow venueRow = venuesTable.FindByID(perfRow.Venue);
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
                    PerformanceDataSet.songperformancesRow songPerfRow = 
                        (PerformanceDataSet.songperformancesRow)rowView.Row;
                    int songTableIndex = songView.Find(songPerfRow.Song);
                    if (songTableIndex != -1)
                    {
                        ViewSongsDataSet.viewsongssinglefieldRow songRow =
                            (ViewSongsDataSet.viewsongssinglefieldRow)songView[songTableIndex].Row;
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