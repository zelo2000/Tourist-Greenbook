using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TGB.WebAPI.Data;
using TGB.WebAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TGB.WebAPI.Controllers
{
    public class AddPlaceController : Controller
    {
        public AddPlaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult AddingNewPlaceFromUser()
        {
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult AddingPlaceToDataBase(string name, string type, string city, string address, TimeSpan workTimeStart, TimeSpan workTimeFinish, double latitude, double longtitude, string description)
        {

            Point newPoint = new Point(latitude, longtitude);
            Place newPlace = new Place
            {
                Name = name,
                Type = type,
                City = city,
                Address = address,
                WorkTimeStart = workTimeStart,
                WorkTimeFinish = workTimeFinish,
                Coordinates = newPoint,
                Description = description,
                Latitude = latitude,
                Longtitude = longtitude
            };
            _context.Places.Add(newPlace);
            _context.Points.Add(newPoint);
            _context.SaveChanges();

            return View();
        }

        private readonly ApplicationDbContext _context;
    }
}
