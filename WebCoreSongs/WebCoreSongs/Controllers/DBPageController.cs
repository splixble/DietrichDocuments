using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebCoreSongs.Models;

namespace WebCoreSongs.Controllers
{
    public class DBPageController : Controller
    {
        private readonly SongsContext _context;

        public DBPageController(SongsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SongAndPerfInfo(string songID)
        {
            Viewsongssinglefield songRow = await _context.Viewsongssinglefield.FirstOrDefaultAsync(m => m.Id == int.Parse(songID));

            List<Viewsongperformances> songPerfs = await _context.Viewsongperformances.FromSql(
                $"SELECT * FROM Songbook.Viewsongperformances WHERE Song={songID} ORDER BY PerformanceDate DESC, SongPerfID").ToListAsync();

            SongAndPerfInfoViewModel model = new SongAndPerfInfoViewModel(songRow, songPerfs);
            return View(model);
        }

        public async Task<IActionResult> PerformanceList()
        {
            List<Performances> perfs = await _context.Performances.FromSql(
                $"SELECT * FROM Songbook.Performances ORDER BY PerformanceDate DESC").ToListAsync();

            Dictionary<int, Venues> venuesLookup = await _context.Venues.ToDictionaryAsync(ven => ven.Id);

            PerformancesViewModel model = new PerformancesViewModel(perfs, venuesLookup);
            return View(model);
        }

        // Calling sequence is different than Default (defined in Program), so need to add Route:
        [Route("DBPage/PerformanceEdit/{perfID}/{canEditSongPerf}/{songPerfID?}")] 
        public async Task<IActionResult> PerformanceEdit(int? perfID, bool canEditSongPerf, int? songPerfID)
        {
            if (perfID == null)
            {
                return NotFound();
            }

            Performances? performance = await _context.Performances.FindAsync(perfID); // changed from scaffolding to strongly typr the var
            if (performance == null)
            {
                return NotFound();
            }

            List<Venues> venuesList= await _context.Venues.ToListAsync(); // create a venues list for the drop down list
            List<Songperformances> songPerfs = await _context.Songperformances.FromSql(
                $"SELECT * FROM Songbook.Songperformances WHERE Performance={perfID} ORDER BY ID").ToListAsync();

            List<Viewsongssinglefield> songsList = await _context.Viewsongssinglefield.FromSql( // create a songs list for the drop down list
                $"SELECT * FROM Songbook.Viewsongssinglefield ORDER BY TitleAndArtist").ToListAsync();

            var model = new PerformanceEditViewModel(performance, canEditSongPerf, songPerfID, venuesList, songPerfs, songsList);
            return View(model);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // DIAG our Model's members are null... why???
        // DIAG And it requires a paramless ctor in model -- but they're never filled in
        // for now, we could just have model create its own venues list maybe...?
        // DOES NOT have to have the same name as the form that called it. Just needs to match the form asp-action on the page.
        public async Task<IActionResult> PerformanceEditSubmit(int id, [Bind("Id,PerformanceDate,Venue,Comment,Series,PerformanceType,DidIlead")] PerformanceEditViewModel model)
        {
            if (id != model.Id)
            {
                // model._PerformanceRow.Id is 0! why? Why's it not passing? Cuz the direct 
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.ToPerformancesRow());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformancesExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PerformanceEdit), "DBPage", new { @id = id }); // really screwy way for MS to do this
            }
            return View(model);
        }

        private bool PerformancesExists(int id)
        {
            return _context.Performances.Any(e => e.Id == id);
        }
    }
}
