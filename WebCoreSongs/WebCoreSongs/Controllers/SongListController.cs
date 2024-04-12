using Microsoft.AspNetCore.Mvc;
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
    public class SongListController : Controller
    {
        private readonly SongsContext _context;

        public SongListController(SongsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Viewsongssinglefield.ToListAsync());
        }
    }
}