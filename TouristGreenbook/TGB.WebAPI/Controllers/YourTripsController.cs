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

namespace TGB.WebAPI.Views
{
    //[Authorize(Roles="User")]
    public class YourTripsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YourTripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: YourTrips
        public async Task<IActionResult> Index()
        {
            return View(await _context.YourTrips.ToListAsync());
        }

        // GET: YourTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yourTrips = await _context.YourTrips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yourTrips == null)
            {
                return NotFound();
            }

            return View(yourTrips);
        }

        // GET: YourTrips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YourTrips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,StayTimeStart,StayTimeFinish,Budget")] YourTrips yourTrips)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yourTrips);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yourTrips);
        }

        // GET: YourTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yourTrips = await _context.YourTrips.FindAsync(id);
            if (yourTrips == null)
            {
                return NotFound();
            }
            return View(yourTrips);
        }

        // POST: YourTrips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,StayTimeStart,StayTimeFinish,Budget")] YourTrips yourTrips)
        {
            if (id != yourTrips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yourTrips);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YourTripsExists(yourTrips.Id))
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
            return View(yourTrips);
        }

        // GET: YourTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yourTrips = await _context.YourTrips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yourTrips == null)
            {
                return NotFound();
            }

            return View(yourTrips);
        }

        // POST: YourTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yourTrips = await _context.YourTrips.FindAsync(id);
            _context.YourTrips.Remove(yourTrips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YourTripsExists(int id)
        {
            return _context.YourTrips.Any(e => e.Id == id);
        }
    }
}
