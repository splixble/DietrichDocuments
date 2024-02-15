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
            ViewBag.VenuesList = _context.Venues.ToList();

            // does it work to replace?
            // return View(await _context.Viewsongperformances.FromSql($"SELECT * FROM Songbook.Viewsongperformances WHERE Venue={venueID}").ToListAsync());
            return View(await LoadSongPerformances(venueID).ToListAsync());
        }

        [Route("venue/{venueID}")]
        public async Task<IActionResult> IndexByVenue(string venueID)
        {
            return View(await LoadSongPerformances(venueID).ToListAsync());

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
