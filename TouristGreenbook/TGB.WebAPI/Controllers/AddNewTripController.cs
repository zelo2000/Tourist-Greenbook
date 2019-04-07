using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TGB.WebAPI.Data;
using TGB.WebAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TGB.WebAPI.Controllers
{
    public class AddNewTripController : Controller
    {
        public AddNewTripController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult InitialStage()
        {
            var cities = _context.Trips
                .Select(x => x.City)
                .Distinct()
                .ToList();
            ViewBag.Cities = cities;

            var tags = _context.Places
                .Select(x => x.Type)
                .Distinct()
                .ToList();
            ViewBag.Tags = tags;

            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult FinallStage(string city, DateTime startDate, TimeSpan startTime, DateTime finishDate, TimeSpan finishTime, double budget, string[] chosenTags)
        {
            startDate = startDate.AddHours(startTime.Hours);
            startDate = startDate.AddMinutes(startTime.Minutes);
            finishDate = finishDate.AddHours(finishTime.Hours);
            finishDate = finishDate.AddMinutes(finishTime.Minutes);
            _newTrip = new Trip()
            {
                City = city,
                StayTimeStart = startDate,
                StayTimeFinish = finishDate,
                Budget = budget
            };

            //ViewBag.City = city;
            //ViewBag.Start = startDate.ToString();
            //ViewBag.Finish = finishDate.ToString();
            //ViewBag.Budget = budget;
            //ViewBag.ChosenTags = chosenTags;

            //var tags = _context.Places
            //    .Select(x => x.Type)
            //    .Distinct()
            //    .ToList();

            var tagedPlace = new Dictionary<string, List<Place>>();
            foreach (var tag in chosenTags)
            {
                tagedPlace.Add(tag, _context.Places.Where(x => x.City == city && x.Type == tag).ToList());
            }

            ViewBag.TagedPlace = tagedPlace;

            return View();
        }

        private readonly ApplicationDbContext _context;
        private Trip _newTrip;
    }
}
