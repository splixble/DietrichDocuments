namespace WebCoreSongs.Models
{
    public class SongAndPerformancesInfo
    {
        public Viewsongssinglefield _SongRow;
        public List<Viewsongperformances> _SongPerfRows;

        public SongAndPerformancesInfo(Viewsongssinglefield songRow, List<Viewsongperformances> songPerfRows)
        {
            _SongRow = songRow;
            _SongPerfRows = songPerfRows;
        }
    }
}
