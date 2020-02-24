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
/// Controller for the register page.
/// </summary>
namespace assignment2.Controllers
{
    public class RegisterController : Controller
    {
        private readonly hascContext _context;

        public RegisterController(hascContext context)
        {
            _context = context;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            var hascContext = _context.Persons.Include(p => p.Division).Include(p => p.Province).Include(p => p.SkillLevelNavigation).Include(p => p.Team);
            return View(await hascContext.ToListAsync());
        }

        // GET: Register/Create
        public IActionResult Create()
        {
            // Get the division names, order by name where a division exists and is distinct.
            var divisionId = (from n in _context.Persons
                         orderby n.Division.DivisionName
                         where n.Division.DivisionName != null
                         select new { Name = n.Division.DivisionName, n.DivisionId }).Distinct();

            // Prepare the divisions data for the view.
            ViewData["DivisionId"] = new SelectList(divisionId, "DivisionId", "Name");
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "ProvinceId", "ProvinceId");
            ViewData["SkillLevel"] = new SelectList(_context.Skills, "SkillLevel", "SkillLevel");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,DivisionId,Email,Gender,BirthDate,AddressLine1,AddressLine2,City,ProvinceId,PostalCode,Phone,Player,SkillLevel,TeamId,JerseyNumber,Coach,CoachingExperience,Referee,RefereeExperience,Administrator,UserPassword")] Persons persons)
        {
            if (ModelState.IsValid)
            {
                var currentBirthDate = persons.BirthDate;
                if ((DateTime.Today - (DateTime)currentBirthDate).TotalDays >= 365 * 18)
                {
                    var maxId = _context.Persons.Max(m => m.PersonId);
                    persons.PersonId = maxId + 1;
                    _context.Add(persons);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.birthDateUnderAge = "You must be atleast 18 years of age to join the club.";
                }
            }
            var divisionId = (from n in _context.Persons
                              orderby n.Division.DivisionName
                              where n.Division.DivisionName != null
                              select new { Name = n.Division.DivisionName, n.DivisionId }).Distinct();

            ViewData["DivisionId"] = new SelectList(divisionId, "DivisionId", "Name");
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "ProvinceId", "ProvinceId", persons.ProvinceId);
            ViewData["SkillLevel"] = new SelectList(_context.Skills, "SkillLevel", "SkillLevel", persons.SkillLevel);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", persons.TeamId);
            return View(persons);
        }

        private bool PersonsExists(int id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}
