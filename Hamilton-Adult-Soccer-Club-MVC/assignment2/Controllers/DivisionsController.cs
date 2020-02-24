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
/// Controller for the divisions page.
/// </summary>
namespace assignment2.Controllers
{
    public class DivisionsController : Controller
    {
        private readonly hascContext _context;

        public DivisionsController(hascContext context)
        {
            _context = context;
        }

        // GET: Divisions
        public async Task<IActionResult> Index()
        {
            //Get all the info from the divisions page.
            var divisions = from d in _context.Divisions
                              select d;

            //Set the order by name.
            divisions = divisions.OrderBy(d => d.DivisionName);

            return View(await divisions.AsNoTracking().ToListAsync());
        }

        private bool DivisionsExists(int id)
        {
            return _context.Divisions.Any(e => e.DivisionId == id);
        }
    }
}
