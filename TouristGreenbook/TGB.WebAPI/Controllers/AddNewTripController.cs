using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TGB.WebAPI.Controllers
{
    public class AddNewTripController : Controller
    {
        // GET: /<controller>/
        public IActionResult InitialStage()
        {
            List<string> cities = new List<string>()
            {
                "Lviv",
                "Kyiv",
                "Rivne"
            };
            ViewBag.Cities = cities;

            List<string> tags = new List<string>()
            {
                "Accomodation",
                "Entertaiment",
                "Food"
            };
            ViewBag.Tags = tags;

            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult FinallStage(string city, DateTime start, DateTime finish, double budget, string[] chosenTags)
        {
            ViewBag.City = city;
            ViewBag.Start = start.Date.ToString();
            ViewBag.Finish = finish.Date.ToString();
            ViewBag.Budget = budget;
            ViewBag.ChosenTags = chosenTags;
            return View();
        }
    }
}
