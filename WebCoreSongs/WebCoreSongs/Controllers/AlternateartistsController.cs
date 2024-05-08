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
    public class AlternateartistsController : Controller
    {
        private readonly SongsContext _context;

        public AlternateartistsController(SongsContext context)
        {
            _context = context;
        }

        // GET: Alternateartists
        public async Task<IActionResult> Index()
        {
            var songsContext = _context.Alternateartists.Include(a => a.Artist);
            return View(await songsContext.ToListAsync());
        }

        // GET: Alternateartists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alternateartists = await _context.Alternateartists
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alternateartists == null)
            {
                return NotFound();
            }

            return View(alternateartists);
        }

        // GET: Alternateartists/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId");
            return View();
        }

        // POST: Alternateartists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,ArtistId")] Alternateartists alternateartists)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alternateartists);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", alternateartists.ArtistId);
            return View(alternateartists);
        }

        // GET: Alternateartists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alternateartists = await _context.Alternateartists.FindAsync(id);
            if (alternateartists == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", alternateartists.ArtistId);
            return View(alternateartists);
        }

        // POST: Alternateartists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,ArtistId")] Alternateartists alternateartists)
        {
            if (id != alternateartists.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alternateartists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlternateartistsExists(alternateartists.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", alternateartists.ArtistId);
            return View(alternateartists);
        }

        // GET: Alternateartists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alternateartists = await _context.Alternateartists
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alternateartists == null)
            {
                return NotFound();
            }

            return View(alternateartists);
        }

        // POST: Alternateartists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alternateartists = await _context.Alternateartists.FindAsync(id);
            if (alternateartists != null)
            {
                _context.Alternateartists.Remove(alternateartists);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlternateartistsExists(int id)
        {
            return _context.Alternateartists.Any(e => e.Id == id);
        }
    }
}
