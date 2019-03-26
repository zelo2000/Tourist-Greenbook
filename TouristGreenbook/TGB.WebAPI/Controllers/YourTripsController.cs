using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGB.WebAPI.Models;

namespace TGB.WebAPI.Controllers
{
    public class YourTripsController : Controller
    {
        private static Trip tempTrip;
        private static List<Trip> tripList;
        public IActionResult Index()
        {
            //var tripList = new List<Trip>();
            tripList = new List<Trip>();
            tripList.Add(new Trip{Id=1,Budget=100,City="Lviv"});
            // TODO load Trips from DataBase
            return View(tripList);
        }

        public IActionResult Trip(int? id)
        {
            //ViewData["id"] = id;

            //tempTrip= = id;
            // TODO load Trip by ID from database
            //if (id.HasValue)
            {
                //if (id>0 && id<=tripList.Count)
                {
                    return View(tripList.[(int) id-1]);
                }
            }
            
            //return string "Wrong trip Id";
        }
    }
}