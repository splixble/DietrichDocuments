﻿using System;
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
            // REMOVE THIS; not used any more
            // from from https://stackoverflow.com/questions/59601041/populate-dropdownlist-using-ef-core-from-another-model, 2nd suggestion:
            ViewBag.VenuesList = new SelectList(_context.Venues.ToList(), "Id", "Name", venueID); // venueID is the selected value last set, if it's been set

            return View(await LoadSongPerformances(venueID).ToListAsync());
        }

        public async Task<IActionResult> Setlists(string venueID)
        {
            // from from https://stackoverflow.com/questions/59601041/populate-dropdownlist-using-ef-core-from-another-model, 2nd suggestion:
            ViewBag.VenuesList =  new SelectList(_context.Venues.ToList() // all rows in Venues table
                .Append(Venues._SelectRow) // add "Select" pseudo-row
                .OrderBy(x => x.Name), "Id", "Name", venueID); // venueID is the selected value last set, if it's been set

            return View(await LoadSongPerformances(venueID).ToListAsync());
        }

        public async Task<IActionResult> SetlistEditable(string venueID) // DIAG make this usable gradually
        {
            // from from https://stackoverflow.com/questions/59601041/populate-dropdownlist-using-ef-core-from-another-model, 2nd suggestion:
            ViewBag.VenuesList = new SelectList(_context.Venues.ToList().OrderBy(x => x.Name), "Id", "Name", venueID); // venueID is the selected value last set, if it's been set
            // DIAG still displays first venue ID in the list initially, tho venueID praram is null. Should have null value ("Select Venue")

            List<Viewsongperformances> songPerfs = await LoadSongPerformances(venueID).ToListAsync();

            SetlistEditable model = new SetlistEditable(songPerfs);
            return View(model);
        }

        IQueryable<Viewsongperformances> LoadSongPerformances(string venueID)
        {
            return _context.Viewsongperformances.FromSql(
                $"SELECT * FROM Songbook.Viewsongperformances WHERE Venue={venueID} ORDER BY PerformanceDate DESC, SongPerfID");
        }

        private bool ViewsongperformancesExists(int id)
        {
            return _context.Viewsongperformances.Any(e => e.PerformanceId == id);
        }
    }
}
