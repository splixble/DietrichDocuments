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
    public class SongperformancesController : Controller
    {
        private readonly SongsContext _context;

        public SongperformancesController(SongsContext context)
        {
            _context = context;
        }

        // GET: Songperformances
        public async Task<IActionResult> Index()
        {
            return View(await _context.Songperformances.ToListAsync());
        }

        // GET: Songperformances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songperformances = await _context.Songperformances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songperformances == null)
            {
                return NotFound();
            }

            return View(songperformances);
        }

        // GET: Songperformances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songperformances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Performance,Song,Comment")] Songperformances songperformances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songperformances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(songperformances);
        }

        // GET: Songperformances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songperformances = await _context.Songperformances.FindAsync(id);
            if (songperformances == null)
            {
                return NotFound();
            }
            return View(songperformances);
        }

        // POST: Songperformances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Performance,Song,Comment")] Songperformances songperformances)
        {
            if (id != songperformances.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songperformances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongperformancesExists(songperformances.Id))
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
            return View(songperformances);
        }

        // GET: Songperformances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songperformances = await _context.Songperformances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songperformances == null)
            {
                return NotFound();
            }

            return View(songperformances);
        }

        // POST: Songperformances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songperformances = await _context.Songperformances.FindAsync(id);
            if (songperformances != null)
            {
                _context.Songperformances.Remove(songperformances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongperformancesExists(int id)
        {
            return _context.Songperformances.Any(e => e.Id == id);
        }
    }
}
