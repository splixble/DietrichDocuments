using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebCoreSongs.Models;

namespace WebCoreSongs.Controllers
{
    public class DBPageController : Controller
    {
        private readonly SongsContext _context;

        public DBPageController(SongsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SongAndPerfInfo(string songID)
        {
            Viewsongssinglefield songRow = await _context.Viewsongssinglefield.FirstOrDefaultAsync(m => m.Id == int.Parse(songID));

            List<Viewsongperformances> songPerfs = await _context.Viewsongperformances.FromSql(
                $"SELECT * FROM Songbook.Viewsongperformances WHERE Song={songID} ORDER BY PerformanceDate DESC, SongPerfID").ToListAsync();

            SongAndPerformancesInfo model = new SongAndPerformancesInfo(songRow, songPerfs);
            return View(model);
        }
    }
}
