using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "TypeDesc" : "Type";
            ViewData["CitySortParm"] = sortOrder == "City" ? "CityDesc" : "City";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "AddressDesc" : "Address";
            ViewData["WorkTimeStartSortParm"] = sortOrder == "WorkTimeStart" ? "WorkTimeStartDesc" : "WorkTimeStart";
            ViewData["WorkTimeFinishSortParm"] = sortOrder == "WorkTimeFinish" ? "WorkTimeFinishDesc" : "WorkTimeFinish";
            ViewData["StateSortParm"] = sortOrder == "State" ? "StateDesc" : "State";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "DescriptionDesc" : "Description";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "RatingDesc" : "Rating";
            var places = from p in _context.Places
                        select p;
            switch (sortOrder)
            {
                case "NameDesc":
                    places = places.OrderByDescending(u => u.Name);
                    break;
                case "City":
                    places = places.OrderBy(s => s.City);
                    break;
                case "CityDesc":
                    places = places.OrderByDescending(u => u.City);
                    break;
                case "Rating":
                    places = places.OrderBy(s => s.Rating);
                    break;
                case "RatingDesc":
                    places = places.OrderByDescending(u => u.Rating);
                    break;
                case "Address":
                    places = places.OrderBy(s => s.Address);
                    break;
                case "AddressDesc":
                    places = places.OrderByDescending(u => u.Address);
                    break;
                case "State":
                    places = places.OrderBy(s => s.State);
                    break;
                case "StateDesc":
                    places = places.OrderByDescending(u => u.State);
                    break;
                case "Description":
                    places = places.OrderBy(s => s.Description);
                    break;
                case "DescriptionDesc":
                    places = places.OrderByDescending(u => u.Description);
                    break;
                case "WorkTimeStart":
                    places = places.OrderBy(s => s.WorkTimeStart);
                    break;
                case "WorkTimeStartDesc":
                    places = places.OrderByDescending(u => u.WorkTimeStart);
                    break;
                case "WorkTimeFinish":
                    places = places.OrderBy(s => s.WorkTimeFinish);
                    break;
                case "WorkTimeFinishDesc":
                    places = places.OrderByDescending(u => u.WorkTimeFinish);
                    break;
                case "Type":
                    places = places.OrderBy(s => s.Type);
                    break;
                case "TypeDesc":
                    places = places.OrderByDescending(u => u.Type);
                    break;
                default:
                    places = places.OrderBy(s => s.Name);
                    break;
            }
            var applicationDbContext = places.Include(p => p.Trip);
            return View(await applicationDbContext.ToListAsync());
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
