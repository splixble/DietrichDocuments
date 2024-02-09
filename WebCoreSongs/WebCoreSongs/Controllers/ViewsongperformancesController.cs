using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index()
        {
            // DIAG tried:  ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");

            /*
            // NOTE: ViewBag only transfers data from controller to view, not visa-versa. ViewBag values will be null if redirection occurs.
            ViewBag.VenuesList = GetVenuesList();

            // DIAG try this?
            // ViewBag.ManagerId = new SelectList(managers, "Id", "Name", employee.ManagerId);
            // From https://stackoverflow.com/questions/59601041/populate-dropdownlist-using-ef-core-from-another-model:
            List<Venues> venues = _context.Venues.ToList(); //get your albums from _context
            var model = new Viewsongperformances();
            model.Venues = new SelectList(venues, "Id", "Name");
            */

            // from from https://stackoverflow.com/questions/59601041/populate-dropdownlist-using-ef-core-from-another-model, 2nd suggestion:
            // var model = new ViewsongperformancesModel (ImageViewModel();) -- needed?
            ViewBag.VenuesList = _context.Venues.ToList();

            return View(await _context.Viewsongperformances.FromSql($"SELECT * FROM Songbook.Viewsongperformances WHERE Venue=51").ToListAsync());
            // what's the equivalent link predicate for this sql?
            // venue = 51 s/b param from dropdown
            // ORIG: return View(await _context.Viewsongperformances.ToListAsync());
        }

        public List<Venues> GetVenuesList()
        {
            return null; // DIAG try this if a continue... _context.Venues; // is this not null? DIAG not needed??
        }

        // GET: Viewsongperformances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformances = await _context.Viewsongperformances
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (viewsongperformances == null)
            {
                return NotFound();
            }

            return View(viewsongperformances);
        }

        // GET: Viewsongperformances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Viewsongperformances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformanceId,Song,Comment,TitleAndArtist,PerformanceDate,DidIlead,PerformanceYear,PerformanceQtr,PerformanceMonth,VenueName,Venue")] Viewsongperformances viewsongperformances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewsongperformances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewsongperformances);
        }

        // GET: Viewsongperformances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformances = await _context.Viewsongperformances.FindAsync(id);
            if (viewsongperformances == null)
            {
                return NotFound();
            }
            return View(viewsongperformances);
        }

        // POST: Viewsongperformances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformanceId,Song,Comment,TitleAndArtist,PerformanceDate,DidIlead,PerformanceYear,PerformanceQtr,PerformanceMonth,VenueName,Venue")] Viewsongperformances viewsongperformances)
        {
            if (id != viewsongperformances.PerformanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewsongperformances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewsongperformancesExists(viewsongperformances.PerformanceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewsongperformances);
        }

        // GET: Viewsongperformances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformances = await _context.Viewsongperformances
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (viewsongperformances == null)
            {
                return NotFound();
            }

            return View(viewsongperformances);
        }

        // POST: Viewsongperformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewsongperformances = await _context.Viewsongperformances.FindAsync(id);
            if (viewsongperformances != null)
            {
                _context.Viewsongperformances.Remove(viewsongperformances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewsongperformancesExists(int id)
        {
            return _context.Viewsongperformances.Any(e => e.PerformanceId == id);
        }
    }
}
