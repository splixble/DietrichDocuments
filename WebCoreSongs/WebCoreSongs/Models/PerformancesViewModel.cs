namespace WebCoreSongs.Models
{
    public class PerformancesViewModel
    {
        public List<Performances> _PerformanceRows;
        public Dictionary<int, Venues> _VenuesLookup;

        public PerformancesViewModel(List<Performances> performanceRows, Dictionary<int, Venues> venuesLookup)
        {
            _PerformanceRows = performanceRows;
            _VenuesLookup = venuesLookup;
        }
    }
}
