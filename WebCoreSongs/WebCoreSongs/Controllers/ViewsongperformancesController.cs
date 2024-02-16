using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebCoreSongs.Models;

namespace WebCoreSongs.Controllers
{
    public class ViewsongperformancesController : Controller
    {
        private readonly SongsContext _context;

        public ViewsongperformancesController(SongsContext context)
        {
            _context = context;
        }

        // GET: Viewsongperformances
        public async Task<IActionResult> Index(string venueID)
        {
            // from from https://stackoverflow.com/questions/59601041/populate-dropdownlist-using-ef-core-from-another-model, 2nd suggestion:
            // var model = new ViewsongperformancesModel (ImageViewModel();) -- needed?
            ViewBag.VenuesList = new SelectList(_context.Venues.ToList(), "Id", "Name", venueID); // venueID is the selected value last set

            return View(await LoadSongPerformances(venueID).ToListAsync());
        }

        public async Task<IActionResult> GenerateList(string venueID)
        {
            return View(await LoadSongPerformances(venueID).ToListAsync());
        }

        public async Task<IActionResult> FilterPerfs(string venueID)
        {
            return View(await LoadSongPerformances("6").ToListAsync());
        }


        [Route("xxx/{venueID}")]
        public async Task<IActionResult> IndexByVenue(string venueID)
        {
            return View(await LoadSongPerformances("5").ToListAsync());
            // DIAG is this used at all? not unles xxx above is replaced with Viewsongperformances, and then we gen another error
        }

        IQueryable<Viewsongperformances> LoadSongPerformances(string venueID)
        {
            return _context.Viewsongperformances.FromSql(
                $"SELECT * FROM Songbook.Viewsongperformances WHERE Venue={venueID} ORDER BY PerfID, SongPerfID");
        }

        private bool ViewsongperformancesExists(int id)
        {
            return _context.Viewsongperformances.Any(e => e.PerformanceId == id);
        }
    }
}
