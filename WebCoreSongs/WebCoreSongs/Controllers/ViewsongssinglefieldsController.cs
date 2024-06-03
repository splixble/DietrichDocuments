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
    public class ViewsongssinglefieldsController : Controller
    {
        private readonly SongsContext _context;

        public ViewsongssinglefieldsController(SongsContext context)
        {
            _context = context;
        }

        // GET: Viewsongssinglefields
        public async Task<IActionResult> Index()
        {
            return View(await _context.Viewsongssinglefield.ToListAsync());
        }

        // GET: Viewsongssinglefields/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongssinglefield = await _context.Viewsongssinglefield
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewsongssinglefield == null)
            {
                return NotFound();
            }

            return View(viewsongssinglefield);
        }

        // GET: Viewsongssinglefields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Viewsongssinglefields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongFull,TitleAndInfo,TitleAndArtist,FullTitle,SongFullArtistFirst,SongInfo,ArtistFirstName,ArtistLastName,FullArtistName,Title,TitlePrefix,Id,Code,PageNumber,InTablet,Cover")] Viewsongssinglefield viewsongssinglefield)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewsongssinglefield);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewsongssinglefield);
        }

        // GET: Viewsongssinglefields/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongssinglefield = await _context.Viewsongssinglefield.FindAsync(id);
            if (viewsongssinglefield == null)
            {
                return NotFound();
            }
            return View(viewsongssinglefield);
        }

        // POST: Viewsongssinglefields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongFull,TitleAndInfo,TitleAndArtist,FullTitle,SongFullArtistFirst,SongInfo,ArtistFirstName,ArtistLastName,FullArtistName,Title,TitlePrefix,Id,Code,PageNumber,InTablet,Cover")] Viewsongssinglefield viewsongssinglefield)
        {
            if (id != viewsongssinglefield.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewsongssinglefield);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViewsongssinglefieldExists(viewsongssinglefield.Id))
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
            return View(viewsongssinglefield);
        }

        // GET: Viewsongssinglefields/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewsongssinglefield = await _context.Viewsongssinglefield
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewsongssinglefield == null)
            {
                return NotFound();
            }

            return View(viewsongssinglefield);
        }

        // POST: Viewsongssinglefields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viewsongssinglefield = await _context.Viewsongssinglefield.FindAsync(id);
            if (viewsongssinglefield != null)
            {
                _context.Viewsongssinglefield.Remove(viewsongssinglefield);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViewsongssinglefieldExists(int id)
        {
            return _context.Viewsongssinglefield.Any(e => e.Id == id);
        }
    }
}
