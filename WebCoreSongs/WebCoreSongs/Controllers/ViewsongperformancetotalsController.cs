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
    public class ViewsongperformancetotalsController : Controller
    {
        private readonly SongsContext _context;

        public ViewsongperformancetotalsController(SongsContext context)
        {
            _context = context;
        }

        // GET: Viewsongperformancetotals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Viewsongperformancetotals.ToListAsync());
        }

        // GET: Viewsongperformancetotals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformancetotals = await _context.Viewsongperformancetotals
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (viewsongperformancetotals == null)
            {
                return NotFound();
            }

            return View(viewsongperformancetotals);
        }

        // GET: Viewsongperformancetotals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Viewsongperformancetotals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Total,SongId,TitleAndArtist,FirstPerformed,LastPerformed")] Viewsongperformancetotals viewsongperformancetotals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewsongperformancetotals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewsongperformancetotals);
        }

        // GET: Viewsongperformancetotals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformancetotals = await _context.Viewsongperformancetotals.FindAsync(id);
            if (viewsongperformancetotals == null)
            {
                return NotFound();
            }
            return View(viewsongperformancetotals);
        }

        // POST: Viewsongperformancetotals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Total,SongId,TitleAndArtist,FirstPerformed,LastPerformed")] Viewsongperformancetotals viewsongperformancetotals)
        {
            if (id != viewsongperformancetotals.SongId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewsongperformancetotals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewsongperformancetotalsExists(viewsongperformancetotals.SongId))
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
            return View(viewsongperformancetotals);
        }

        // GET: Viewsongperformancetotals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongperformancetotals = await _context.Viewsongperformancetotals
                .FirstOrDefaultAsync(m => m.SongId == id);
            if (viewsongperformancetotals == null)
            {
                return NotFound();
            }

            return View(viewsongperformancetotals);
        }

        // POST: Viewsongperformancetotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewsongperformancetotals = await _context.Viewsongperformancetotals.FindAsync(id);
            if (viewsongperformancetotals != null)
            {
                _context.Viewsongperformancetotals.Remove(viewsongperformancetotals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewsongperformancetotalsExists(int id)
        {
            return _context.Viewsongperformancetotals.Any(e => e.SongId == id);
        }
    }
}
