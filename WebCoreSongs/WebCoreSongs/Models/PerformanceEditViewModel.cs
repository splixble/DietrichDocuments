namespace WebCoreSongs.Models
{
    public class PerformanceEditViewModel
    {
        // TODO will need to add SongPerformances list to this
        public List<Venues> _VenuesList;

        public List<Viewsongperformances> _ViewSongPerformanceRows;


        // From Performances table:
        public int Id { get; set; }
        public DateOnly PerformanceDate { get; set; }
        public int Venue { get; set; }
        public string Comment { get; set; }
        public int? Series { get; set; }
        public string PerformanceType { get; set; }
        public bool DidIlead { get; set; }

        public PerformanceEditViewModel()
        {
            // must have a param-less ctor? How does it fill it in on Save then?
            _VenuesList = new List<Venues>();
            _ViewSongPerformanceRows = new List<Viewsongperformances>();
        }

        public PerformanceEditViewModel(Performances performanceRow,  List<Venues> venuesList, List<Viewsongperformances> viewSongPerformanceRows)
        {
            FromPerformancesRow(performanceRow);
            _VenuesList = venuesList;
            _ViewSongPerformanceRows = viewSongPerformanceRows;
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
