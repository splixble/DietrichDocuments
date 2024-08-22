using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebCoreSongs.Models;
using WebCoreSongs.DLib;

namespace WebCoreSongs.Controllers
{
    public class SongsLastPerformanceController : Controller
    {
        private readonly SongsContext _context;

        public SongsLastPerformanceController(SongsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.OrdinalConv = new OrdinalConverter();
            return View(await _context.Viewsongperformancetotals.FromSql(
                $"SELECT * FROM Songbook.Viewsongperformancetotals").ToListAsync());
        }
    }
}
