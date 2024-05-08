using System;
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
                $"SELECT * FROM Songbook.Viewsongperformancetotals ORDER BY LastPerformed DESC").ToListAsync());
        }
    }

    public class OrdinalConverter
    {
        // DIAG put in its own module, and/or in some lib, right?
        public string OrdinalTerm(long? numNullable)
        {
            if (numNullable == null)
                return "";

            int num = (int) numNullable;
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }
    }
}
