using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCoreSongs.Models.DB;

namespace WebCoreSongs.Controllers
{
    public class ViewsongperformancesController : Controller
    {
        private readonly SongbookContext _context;

        public ViewsongperformancesController(SongbookContext context)
        {
            _context = context;
        }

        // GET: Viewsongperformances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Viewsongperformance.ToListAsync());
        }

        // GET: Viewsongperformances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformance = await _context.Viewsongperformance
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (viewsongperformance == null)
            {
                return NotFound();
            }

            return View(viewsongperformance);
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
        public async Task<IActionResult> Create([Bind("PerformanceId,Song,Comment,TitleAndArtist,PerformanceDate,DidIlead,PerformanceYear,PerformanceQtr,PerformanceMonth,VenueName,Venue")] Viewsongperformance viewsongperformance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewsongperformance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewsongperformance);
        }

        // GET: Viewsongperformances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformance = await _context.Viewsongperformance.FindAsync(id);
            if (viewsongperformance == null)
            {
                return NotFound();
            }
            return View(viewsongperformance);
        }

        // POST: Viewsongperformances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformanceId,Song,Comment,TitleAndArtist,PerformanceDate,DidIlead,PerformanceYear,PerformanceQtr,PerformanceMonth,VenueName,Venue")] Viewsongperformance viewsongperformance)
        {
            if (id != viewsongperformance.PerformanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewsongperformance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewsongperformanceExists(viewsongperformance.PerformanceId))
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
            return View(viewsongperformance);
        }

        // GET: Viewsongperformances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformance = await _context.Viewsongperformance
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (viewsongperformance == null)
            {
                return NotFound();
            }

            return View(viewsongperformance);
        }

        // POST: Viewsongperformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewsongperformance = await _context.Viewsongperformance.FindAsync(id);
            if (viewsongperformance != null)
            {
                _context.Viewsongperformance.Remove(viewsongperformance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewsongperformanceExists(int id)
        {
            return _context.Viewsongperformance.Any(e => e.PerformanceId == id);
        }
    }
}
