using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TGB.WebAPI.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TGB.WebAPI.Controllers
{
    public class ShowPlacesController : Controller
    {
        public ShowPlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult ShowAllPlaces()
        {
            var PlacesToShow = _context.Places.ToList();
            return View(PlacesToShow);
        }

        private readonly ApplicationDbContext _context;
    }
}
