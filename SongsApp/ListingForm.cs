using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

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
            DataSet1.ViewTOCArtistDataTable tblArtist = new DataSet1.ViewTOCArtistDataTable();
            DataSet1.ViewTOCTitleDataTable tblTitle = new DataSet1.ViewTOCTitleDataTable();
            DataSet1TableAdapters.ViewTOCArtistTableAdapter adapArtist =
                new Songs.DataSet1TableAdapters.ViewTOCArtistTableAdapter();
            DataSet1TableAdapters.ViewTOCTitleTableAdapter adapTitle =
                new Songs.DataSet1TableAdapters.ViewTOCTitleTableAdapter();
            adapArtist.Fill(tblArtist);
            adapTitle.Fill(tblTitle);

            foreach (DataSet1.ViewTOCTitleRow rowTitle in tblTitle)
            {
                tb.Text += rowTitle.TitleAndArtist + "\t" + rowTitle.PageNumber.ToString() + Environment.NewLine;
            }

            tb.Text += Environment.NewLine + Environment.NewLine; // blank lines

            foreach (DataSet1.ViewTOCArtistRow rowArtist in tblArtist)
            {
                tb.Text += rowArtist.ArtistAndTitle + "\t" + rowArtist.PageNumber.ToString() + Environment.NewLine;
            }

            ShowDialog();
        }

        public void ShowListByEachFlag()
        {
            DataSet1.flagsDataTable flagsTable = new DataSet1.flagsDataTable();
            DataSet1TableAdapters.flagsTableAdapter adap = new Songs.DataSet1TableAdapters.flagsTableAdapter();
            adap.Fill(flagsTable);
            foreach (DataSet1.flagsRow flagsRow in flagsTable)
            {
                tb.Text += Environment.NewLine + "`" + flagsRow.FlagName + "`" + Environment.NewLine + Environment.NewLine;
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

            ViewSongsDataSet.ViewSongsSingleFieldDataTable tblSongs = new ViewSongsDataSet.ViewSongsSingleFieldDataTable();
            string query = "SELECT SongFull, SongFullArtistFirst, ArtistFirstName, ArtistLastName, Title, TitlePrefix, ID, Code " + 
                "FROM ViewSongsSingleField " + songsWhereClause + " ORDER BY Title, TitlePrefix";
            OdbcDataAdapter songsAdap = new OdbcDataAdapter(query, 
                global::Songs.Properties.Settings.Default.MainConnectionString);
            songsAdap.Fill(tblSongs);

            foreach (ViewSongsDataSet.ViewSongsSingleFieldRow rowSong in tblSongs)
            {
                tb.Text += rowSong.SongFull + Environment.NewLine;
            }
        }

        public void ShowListByArtist(string songsWhereClause)
        {
            ViewSongsDataSet.ViewSongsSingleFieldDataTable tblSongs = new ViewSongsDataSet.ViewSongsSingleFieldDataTable();
            string query = "SELECT SongFull, SongFullArtistFirst, ArtistFirstName, ArtistLastName, "+
                "Title, TitlePrefix, ID, Code, TitleAndInfo, FullArtistName " +
                "FROM ViewSongsSingleField " + songsWhereClause + 
                " ORDER BY ArtistLastName, ArtistFirstName, Title, TitlePrefix";
            OdbcDataAdapter songsAdap = new OdbcDataAdapter(query,
                global::Songs.Properties.Settings.Default.MainConnectionString);
            songsAdap.Fill(tblSongs);

            string lastArtist = "";
            foreach (ViewSongsDataSet.ViewSongsSingleFieldRow rowSong in tblSongs)
            {
                string currentArtist = rowSong[tblSongs.FullArtistNameColumn] as string;
                if (currentArtist == null)
                    currentArtist = "";
                if (currentArtist != lastArtist)
                {
                    tb.Text += currentArtist;
                    lastArtist = currentArtist;
                }
                tb.Text += "\t" + rowSong.TitleAndInfo + Environment.NewLine;
            }

            ShowDialog();
        }

        public void ShowPerformanceList()
        {
            ViewSongsDataSet.ViewSongsSingleFieldDataTable songTable = new ViewSongsDataSet.ViewSongsSingleFieldDataTable();
            ViewSongsDataSetTableAdapters.ViewSongsSingleFieldTableAdapter songAdap =
                new Songs.ViewSongsDataSetTableAdapters.ViewSongsSingleFieldTableAdapter();
            songAdap.Fill(songTable);

            PerformanceDataSet.performancesDataTable perfsTable = new PerformanceDataSet.performancesDataTable();
            PerformanceDataSetTableAdapters.performancesTableAdapter perfAdap =
                new Songs.PerformanceDataSetTableAdapters.performancesTableAdapter();
            perfAdap.Fill(perfsTable);

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
                        ViewSongsDataSet.ViewSongsSingleFieldRow songRow =
                            (ViewSongsDataSet.ViewSongsSingleFieldRow)songView[songTableIndex].Row;
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