namespace WebCoreSongs.Models
{
    public class SetlistEditable
    {
        public List<Viewsongperformances> SongPerfRows => _SongPerfRows;
        List<Viewsongperformances> _SongPerfRows;

        public SetlistEditable(List<Viewsongperformances> songPerfRows)
        {
            _SongPerfRows = songPerfRows;
        }
    }
}
