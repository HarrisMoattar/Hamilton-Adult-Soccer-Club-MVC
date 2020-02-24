using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assignment2.Models;

/// <summary>
/// Author: Harris Moattar
/// Controller for the recent scores page.
/// </summary>
namespace assignment2.Controllers
{
    public class RecentScoresController : Controller
    {
        private readonly hascContext _context;

        public RecentScoresController(hascContext context)
        {
            _context = context;
        }

        // GET: RecentScores
        public async Task<IActionResult> Index()
        {
            //Get the game info with home and away name, sorted by date and filered by a home score and only return the top ten.
            var recentScores = (from games in _context.Games.Include(a => a.HomeTeam).Include(a => a.AwayTeam).Where(a => a.HomeTeamScore != null).OrderByDescending(a => a.GameDate).Take(10)
                                select games);

            return View(await recentScores.AsNoTracking().ToListAsync());

        }


        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
