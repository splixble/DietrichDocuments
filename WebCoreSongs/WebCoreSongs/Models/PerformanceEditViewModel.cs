namespace WebCoreSongs.Models
{
    public class PerformanceEditViewModel
    {
        public List<Venues> _VenuesList;

        public List<Songperformances> _SongPerfList;

        public List<Viewsongssinglefield> _SongsList;

        public int? _SelectedSongPerfID = null;
        public bool _CanEditSongPerformance = false;

        public Dictionary<int, Viewsongssinglefield> _SongRowsByID;

        // From Performances table:
        public int Id { get; set; }
        public DateOnly PerformanceDate { get; set; }
        public int Venue { get; set; }
        public string Comment { get; set; } = ""; // initialized so ModelState wont be invalid
        public int? Series { get; set; }
        public string PerformanceType { get; set; } = ""; // initialized so ModelState wont be invalid
        public bool DidIlead { get; set; }

        // From SongPerformances table:
        public int SongPerf_Id { get; set; }
        public int SongPerf_Song { get; set; }
        public string? SongPerf_Comment { get; set; } // initialized so ModelState wont be invalid -- DIAG but it makes me unmable to save a null value! does it need to be "nullable" string?
        // DIAG defining as string? makes it able to take a null value! set this to the other nullable fields as well?
        public PerformanceEditViewModel()
        {
            // must have a param-less ctor? How does it fill it in on Save then?
            _VenuesList = new List<Venues>();
            _SongPerfList = new List<Songperformances>();
        }

        public PerformanceEditViewModel(Performances performanceRow, bool canEditSongPerf, int? songPerfID, 
            List<Venues> venuesList, List<Songperformances> songPerfList, List<Viewsongssinglefield> songsList)
        {
            FromPerformancesRow(performanceRow);

            _SongsList = songsList;
            _CanEditSongPerformance = canEditSongPerf;
            _SelectedSongPerfID = songPerfID;
            _VenuesList = venuesList;
            _SongPerfList = songPerfList;
            _SongsList = songsList;

            if (songPerfID != null)
                FromSongPerformancesRow(_SongPerfList.Find(songPerf => songPerf.Id == (int)songPerfID));

            _SongRowsByID = new Dictionary<int, Viewsongssinglefield>();
            foreach (Viewsongssinglefield songRow in _SongsList)
                _SongRowsByID[songRow.Id] = songRow;
        }

        public void FromSongPerformancesRow(Songperformances songPerfRow)
        {
            // Copy fields over from Performances row: 
            SongPerf_Id = songPerfRow.Id;
            Id = songPerfRow.Performance;
            SongPerf_Song = songPerfRow.Song;
            SongPerf_Comment = songPerfRow.Comment;
        }

        public Songperformances ToSongPerformancesRow()
        {
            // Copy fields over to Performances row: 
            // (Tried getting the Songperformances object from _SongPerfList; that didn't solve the Comment field Validate problem)
            Songperformances songPerfRow = new Songperformances();

            songPerfRow.Id = SongPerf_Id;
            songPerfRow.Performance = Id;
            songPerfRow.Song = SongPerf_Song;
            songPerfRow.Comment = SongPerf_Comment;

            return songPerfRow;
        }
        public void FromPerformancesRow(Performances perfRow)
        {
            // Copy fields over from Performances row: 
            Id = perfRow.Id;
            PerformanceDate = perfRow.PerformanceDate;
            Venue = perfRow.Venue;
            Comment = perfRow.Comment;
            Series = perfRow.Series;
            PerformanceType = perfRow.PerformanceType;
            DidIlead = perfRow.DidIlead;
        }

        public Performances ToPerformancesRow()
        {
            // Copy fields over to Performances row: 
            Performances perfRow = new Performances();
            perfRow.Id = Id;
            perfRow.PerformanceDate = PerformanceDate;
            perfRow.Venue = Venue;
            perfRow.Comment = Comment;
            perfRow.Series = Series;
            perfRow.PerformanceType = PerformanceType;
            perfRow.DidIlead = DidIlead;

            return perfRow;
        }
    }
}
