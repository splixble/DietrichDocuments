using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;
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

        [Route("DBPage/CreatePerformance")]
        public async Task<IActionResult> CreatePerformance()
        {
            return await PerformanceEdit(null, true, null);
        }

        // Calling sequence is different than Default (defined in Program), so need to add Route:
        [Route("DBPage/PerformanceEdit/{perfID}/{canEditSongPerf}/{songPerfID?}")]
        public async Task<IActionResult> PerformanceEdit(int? perfID, bool canEditSongPerf, int? songPerfID)
        {
            // TODO As of 3/18/24, perfID of -1 means create new one. Not good solution... but..
            // TODO also, these paarmeters s/b separated by ? on url, to be proper, o?
            if (perfID == null)
            {
                return NotFound();
            }

            Performances? performance; // DIAG anything else need be init'd?
            if (perfID == -1) // DIAG s/b bool param
            {
                performance = new Performances();
            }
            else
            {
                performance = await _context.Performances.FindAsync(perfID); // changed from scaffolding to strongly typr the var
                if (performance == null)
                {
                    return NotFound();
                }
            }

            List<Venues> venuesList= await _context.Venues.ToListAsync(); // create a venues list for the drop down list
            List<Songperformances> songPerfs = await _context.Songperformances.FromSql(
                $"SELECT * FROM Songbook.Songperformances WHERE Performance={perfID} ORDER BY ID").ToListAsync();

            List<Viewsongssinglefield> songsList = await _context.Viewsongssinglefield.FromSql( // create a songs list for the drop down list
                $"SELECT * FROM Songbook.Viewsongssinglefield ORDER BY TitleAndArtist").ToListAsync();

            // Have we been directed to add a new song performance? If so, add blank rec:
            if (songPerfID == 0) // DIAG try 0 instead of -1 to indicate null value. It works! Warn that this is screwy design tho! Also, apply this "0" idea to new Performances as well!
            {
                Songperformances newSongPerf = new Songperformances();
                newSongPerf.Id = 0; // DIAG try 0 instead of -1 to indicate null value
                newSongPerf.Performance = (int)perfID;
                songPerfs.Add(newSongPerf);
            }

            var model = new PerformanceEditViewModel(performance, canEditSongPerf, songPerfID, venuesList, songPerfs, songsList);
            return View(model);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // DIAG And it requires a paramless ctor in model -- but they're never filled in
        // for now, we could just have model create its own venues list maybe...?
        // DOES NOT have to have the same name as the form that called it. Just needs to match the form asp-action on the page.
        public async Task<IActionResult> PerformanceEditSave(int id, [Bind("Id,PerformanceDate,Venue,Comment,Series,PerformanceType,DidIlead")] PerformanceEditViewModel model)
        {
            if (id != model.Id)
            {
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
                return Redirect("PerformanceEdit/"+model.Id+"/false");
                // was: return RedirectToAction(...); but i had peoblems with parameters
            }
            return View(model);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Saves SongPerformance fields on line items
        // First param name must match name of property in model
        public async Task<IActionResult> PerformanceEditSaveSong(int songPerf_Id, [Bind("SongPerf_Id, SongPerf_Song, SongPerf_Comment, Id")] PerformanceEditViewModel model)
        {
            if (songPerf_Id != model.SongPerf_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.ToSongPerformancesRow());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // DIAG how do I change this?
                    if (!PerformancesExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("PerformanceEdit/" + model.Id + "/false");
                // was: return RedirectToAction(...); but i had problems with parameters
            }
            return View(model);
        }

        private bool PerformancesExists(int id)
        {
            return _context.Performances.Any(e => e.Id == id);
        }
    }
}
