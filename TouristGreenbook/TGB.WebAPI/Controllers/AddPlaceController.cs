using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGB.WebAPI.Data;
using TGB.WebAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TGB.WebAPI.Controllers
{
    public class AddPlaceController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddingNewPlaceFromUser()
        {
            return View();
        }
        [HttpPost]
        public string AddingNewPlaceFromUser(string name, string type, string city, string address, TimeSpan workTimeStart, TimeSpan workTimeFinish, double latitude, double longtitude, string description)
        {
            Point newPoint = new Point(latitude, longtitude);
            Place newPlace = new Place();
            newPlace.Name = name;
            newPlace.Type = type;
            newPlace.City = city;
            newPlace.Address = address;
            newPlace.WorkTimeStart = workTimeStart;
            newPlace.WorkTimeFinish = workTimeFinish;
            newPlace.Coordinates = newPoint;
            newPlace.Description = description;
            _context.Places.Add(newPlace);
            _context.SaveChanges();

            return "Thank you for helping us!";
        }

        public IActionResult ShowPlaces()
        {
            List<Place> PlacesToShow = _context.Places
    .Select(c => new Place { Id = c.Id, Name = c.Name, Address = c.Address, City = c.City,  })
    .ToList();
            return View(PlacesToShow);
        }

        private readonly ApplicationDbContext _context;
        public AddPlaceController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
