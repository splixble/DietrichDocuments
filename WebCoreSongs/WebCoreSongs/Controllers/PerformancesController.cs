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
    public class PerformancesController : Controller
    {
        private readonly SongsContext _context;

        public PerformancesController(SongsContext context)
        {
            _context = context;
        }

        // GET: Performances
        public async Task<IActionResult> Index()
        {
            // DIAG s/b in Model
            ViewBag.VenuesLookup = await _context.Venues.ToDictionaryAsync(ven => ven.Id);

            return View(await _context.Performances.ToListAsync());
        }

        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performances = await _context.Performances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performances == null)
            {
                return NotFound();
            }

            return View(performances);
        }

        // GET: Performances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerformanceDate,Venue,Comment,Series,PerformanceType,DidIlead")] Performances performances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performances);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performances);
        }

        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performances = await _context.Performances.FindAsync(id);
            if (performances == null)
            {
                return NotFound();
            }
            return View(performances);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PerformanceDate,Venue,Comment,Series,PerformanceType,DidIlead")] Performances performances)
        {
            if (id != performances.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformancesExists(performances.Id))
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
            return View(performances);
        }

        // GET: Performances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performances = await _context.Performances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (performances == null)
            {
                return NotFound();
            }

            return View(performances);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performances = await _context.Performances.FindAsync(id);
            if (performances != null)
            {
                _context.Performances.Remove(performances);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformancesExists(int id)
        {
            return _context.Performances.Any(e => e.Id == id);
        }
    }
}
