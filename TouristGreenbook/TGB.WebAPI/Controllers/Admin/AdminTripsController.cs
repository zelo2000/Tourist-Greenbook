using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using TGB.WebAPI.Data;
using TGB.WebAPI.Models;

namespace TGB.WebAPI.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminTripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminTripsController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AdminTrips
        public async Task<IActionResult> Index(string filterCity, string filterUser, int page = 1, string sortExpression = "City")
        {
            var qry = _context.Trips;
            var applicationDbContext = qry.Include(t => t.ConcreteUser).AsNoTracking();
            foreach (var item in applicationDbContext)
            {
                if (item.ConcreteUserId != null)
                {
                    item.ConcreteUser.Email = _userManager.FindByIdAsync(item.ConcreteUserId).Result.Email;
                }
            }
            if (!string.IsNullOrWhiteSpace(filterCity))
            {
                applicationDbContext = applicationDbContext.Where(p => p.City.Contains(filterCity));
            }
            if (!string.IsNullOrWhiteSpace(filterUser))
            {
                applicationDbContext = applicationDbContext.Where(p => p.ConcreteUser.Email.Contains(filterUser));
            }
            var model = await PagingList.CreateAsync(applicationDbContext, 3, page, sortExpression, "City");
            model.RouteValue = new RouteValueDictionary {
                { "filterCity", filterCity}, { "filterUser", filterUser},
            };
            return View(model);
        }

        // GET: AdminTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(t => t.ConcreteUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: AdminTrips/Create
        public IActionResult Create()
        {
            ViewData["ConcreteUserId"] = new SelectList(_context.ConcreteUsers, "Id", "Id");
            return View();
        }

        // POST: AdminTrips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,StayTimeStart,StayTimeFinish,Budget,ConcreteUserId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.ConcreteUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcreteUserId"] = new SelectList(_context.ConcreteUsers, "Id", "Id", trip.ConcreteUserId);
            return View(trip);
        }

        // GET: AdminTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            ViewData["ConcreteUserId"] = new SelectList(_context.ConcreteUsers, "Id", "Id", trip.ConcreteUserId);
            return View(trip);
        }

        // POST: AdminTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,StayTimeStart,StayTimeFinish,Budget,ConcreteUserId")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
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
            ViewData["ConcreteUserId"] = new SelectList(_context.ConcreteUsers, "Id", "Id", trip.ConcreteUserId);
            return View(trip);
        }

        // GET: AdminTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(t => t.ConcreteUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: AdminTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
