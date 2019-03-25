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
            var City = new List<string>()
            {
                "Lviv",
                "Chernivci",
                "Kyiv"
            };
            //TO DO Get city from data base
            return View(City);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult FinallStage(string city, DateTime start, DateTime finish, int budget)
        {
            return View();
        }
    }
}
