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
/// Controller for the New Scores page.
/// </summary>
namespace assignment2.Controllers
{
    public class NewScoreController : Controller
    {
        private readonly hascContext _context;

        public NewScoreController(hascContext context)
        {
            _context = context;
        }

        // GET: NewScore
        public async Task<IActionResult> Index(string RefereeId)
        {
            //Get the first and last name of the referee to be output in a dropdown menu later.
            var person = from n in _context.Persons
                               orderby n.LastName
                               where n.RefereeExperience != null
                               select new { Name = n.FirstName + " " + n.LastName, n.PersonId };

            ViewData["PersonId"] = new SelectList(person, "PersonId", "Name");

            // Get the game info with home and away name, sorted by date and filered by a home score.
            var newScores = (from games in _context.Games.Include(a => a.HomeTeam).Include(a => a.AwayTeam).Where(a => a.HomeTeamScore != null).OrderByDescending(a => a.GameDate)
                                select games);

            // If the referee id does not exist then do not filter by referee.
            if (!String.IsNullOrEmpty(RefereeId))
            {
                newScores = newScores.Where(a => a.RefereeId == Int32.Parse(RefereeId));
            }

            return View(await newScores.AsNoTracking().ToListAsync());
        }

        // GET: NewScore/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FindAsync(id);
            if (games == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", games.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", games.HomeTeamId);
            ViewData["RefereeId"] = new SelectList(_context.Persons, "PersonId", "PersonId", games.RefereeId);
            return View(games);
        }

        // POST: NewScore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,GameDate,Field,HomeTeamId,HomeTeamScore,AwayTeamId,AwayTeamScore,RefereeId,GameNotes")] Games games)
        {
            if (id != games.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(games);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamesExists(games.GameId))
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
            ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", games.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", games.HomeTeamId);
            ViewData["RefereeId"] = new SelectList(_context.Persons, "PersonId", "PersonId", games.RefereeId);
            return View(games);
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
