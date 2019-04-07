using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGB.WebAPI.Data;
using TGB.WebAPI.Models;

namespace TGB.WebAPI.Controllers
{
    public class YourTripsController : Controller
    {
        public YourTripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var tripList = _context.Trips.ToList();
            return View(tripList);
        }

        // GET: /<controller>/
        public IActionResult Trip(int? id)
        {
            Trip trip;
            if (id != null)
            {
                trip = _context.Trips.Where(x => x.Id == id).First();
            }
            else
            {
                trip = new Trip();
            }
            return View(trip);
        }

        private readonly ApplicationDbContext _context;
    }
}