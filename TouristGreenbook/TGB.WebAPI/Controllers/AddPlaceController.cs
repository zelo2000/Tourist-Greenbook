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
        public string AddingNewPlaceFromUser(Place place)
        {
            _context.Places.Add(place);
            _context.SaveChanges();
            return "Thank you for helping us!";
        }

        private readonly ApplicationDbContext _context;
        public AddPlaceController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
