using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class AdminPlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPlaces
        public async Task<IActionResult> Index(string filterName, string filterType, string filterAddress, string filterCity, int page = 1, string sortExpression = "Name")
        {
            var qry = _context.Places;
            var applicationDbContext = qry.Include(t => t.Trip).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(filterName))
            {
                applicationDbContext = applicationDbContext.Where(p => p.Name.Contains(filterName));
            }
            if (!string.IsNullOrWhiteSpace(filterType))
            {
                applicationDbContext = applicationDbContext.Where(p => p.Type.Contains(filterType));
            }
            if (!string.IsNullOrWhiteSpace(filterCity))
            {
                applicationDbContext = applicationDbContext.Where(p => p.City.Contains(filterCity));
            }
            if (!string.IsNullOrWhiteSpace(filterAddress))
            {
                applicationDbContext = applicationDbContext.Where(p => p.Address.Contains(filterAddress));
            }
            var model = await PagingList.CreateAsync(applicationDbContext, 3, page, sortExpression, "Name");
            model.RouteValue = new RouteValueDictionary {
                { "filterName", filterName}, { "filterType", filterType}, { "filterCity", filterCity}, { "filterAddress", filterAddress}
            };
            return View(model);
        }

        // GET: AdminPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .Include(p => p.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: AdminPlaces/Create
        public IActionResult Create()
        {
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id");
            return View();
        }

        // POST: AdminPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,City,Address,WorkTimeStart,WorkTimeFinish,State,Description,Rating,TripId")] Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", place.TripId);
            return View(place);
        }

        // GET: AdminPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", place.TripId);
            return View(place);
        }

        // POST: AdminPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,City,Address,WorkTimeStart,WorkTimeFinish,State,Description,Rating,TripId")] Place place)
        {
            if (id != place.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.Id))
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
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Id", place.TripId);
            return View(place);
        }

        // GET: AdminPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .Include(p => p.Trip)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: AdminPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _context.Places.FindAsync(id);
            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.Id == id);
        }
    }
}
