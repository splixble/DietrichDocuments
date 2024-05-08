namespace WebCoreSongs.Models
{
    public class SongAndPerfInfoViewModel
    {
        public Viewsongssinglefield _SongRow;
        public List<Viewsongperformances> _SongPerfRows;

        public SongAndPerfInfoViewModel(Viewsongssinglefield songRow, List<Viewsongperformances> songPerfRows)
        {
            _SongRow = songRow;
            _SongPerfRows = songPerfRows;
        }
    }
}
