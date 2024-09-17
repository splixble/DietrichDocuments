using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCoreSongs.Models;
using WebCoreSongs.DLib;

namespace WebCoreSongs.Controllers
{
    public class SongListController : Controller
    {
        private readonly SongsContext _context;

        public SongListController(SongsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortField)
        {
			// Orig: return View(await _context.Viewsongssinglefield.ToListAsync());

            // Get recs from DB:
			List<Viewsongssinglefield> songs = await _context.Viewsongssinglefield.ToListAsync();

            // Sort recs (in sep. function? DIAG make orderedSongs into a Sql stmt?)
            IOrderedEnumerable<Viewsongssinglefield> orderedSongs;
            if (sortField == "Title")
                orderedSongs = songs.OrderBy("Title", "ASC").ThenBy("TitlePrefix", "ASC").ThenBy("ID", "ASC");
            else if (sortField == "Artist")
                orderedSongs = songs.OrderBy("ArtistLastName", "ASC").ThenBy("ArtistFirstName", "ASC").ThenBy("Title", "ASC").ThenBy("TitlePrefix", "ASC").ThenBy("ID", "ASC");
            else
                return null;
            // DIAG By Artist, it should list covers (songs under EACH artist)
            // TODO make descending sort an option?

			//orderedSongs
			return View(orderedSongs); 
        }
    }
}