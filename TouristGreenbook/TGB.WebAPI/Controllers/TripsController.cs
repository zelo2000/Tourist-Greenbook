using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Newtonsoft.Json;
using TGB.WebAPI.Data;
using TGB.WebAPI.Models;

namespace TGB.WebAPI.Controllers
{
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Trip _newTrip;

        public TripsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index(string sortOrder)
        {
            //var tpl = new SortedDictionary<Trip, List<Place>>();
            
            TripWithPlaces trips = new TripWithPlaces()
            {
                Trips = await _context.Trips.ToListAsync(),
                Places = await _context.Places.ToListAsync(),                
            };
            
            ViewData["CitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "CityDesc" : "";
            ViewData["StayTimeStartSortParm"] = sortOrder == "StayTimeStart" ? "StayTimeStartDesc" : "StayTimeStart";
            ViewData["StayTimeFinishSortParm"] = sortOrder == "StayTimeFinish" ? "StayTimeFinishDesc" : "StayTimeFinish";
            ViewData["BudgetSortParm"] = sortOrder == "Budget" ? "BudgetDesc" : "Budget";
            //var trips = from t in _context.Trips
            //            select t;
            
	    switch (sortOrder)
            {
                case "CityDesc":
                    trips.Trips = trips.Trips.OrderByDescending(u => u.City).ToList();
                    break;
                case "StayTimeStart":
                    trips.Trips = trips.Trips.OrderBy(s => s.StayTimeStart).ToList();
                    break;
                case "StayTimeStartDesc":
                    trips.Trips = trips.Trips.OrderByDescending(s => s.StayTimeStart).ToList();
                    break;
                case "StayTimeFinish":
                    trips.Trips = trips.Trips.OrderBy(s => s.StayTimeFinish).ToList();
                    break;
                case "StayTimeFinishDesc":
                    trips.Trips = trips.Trips.OrderByDescending(s => s.StayTimeFinish).ToList();
                    break;
                case "Budget":
                    trips.Trips = trips.Trips.OrderBy(s => s.Budget).ToList();
                    break;
                case "BudgetDesc":
                    trips.Trips = trips.Trips.OrderByDescending(s => s.Budget).ToList();
                    break;
                default:
                    trips.Trips = trips.Trips.OrderBy(s => s.City).ToList();
                    break;
            }
            return View(trips); //await trips.AsNoTracking().ToListAsync()
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trips == null)
            {
                return NotFound();
            }

            return View(trips);
        }

        // GET: Trips/Create ///<controller>/
        public IActionResult Create()//InitialStage()
        {
            var cities = _context.Places
                .Select(x => x.City)
                .Distinct()
                .ToList();
            ViewBag.Cities = cities;

            var tags = _context.Places
                .Select(x => x.Type)
                .Distinct()
                .ToList();
            ViewBag.Tags = tags;

            var places = _context.Places.ToList();

            var ids = _context.Places
                .Select(x => x.Id)
                .ToList();
            ViewBag.Ids = ids;

            return View(places);
        }

        // POST: Trips/CreateFinal
        [HttpPost]
        public async Task<IActionResult> SelectPlaces(string city, DateTime startDate, TimeSpan startTime, 
            DateTime finishDate, TimeSpan finishTime, double budget, string[] chosenTags)//, string sortOrder)
        {
            startDate = startDate.AddHours(startTime.Hours);
            startDate = startDate.AddMinutes(startTime.Minutes);
            finishDate = finishDate.AddHours(finishTime.Hours);
            finishDate = finishDate.AddMinutes(finishTime.Minutes);
            
            //ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            //ViewData["WorkTimeStartSortParam"] = sortOrder == "WorkTimeStart" ? "WorkTimeStartDesc" : "WorkTimeStart";
            //ViewData["WorkTimeFinishSortParam"] = sortOrder == "WorkTimeFinish" ? "WorkTimeFinishDesc" : "WorkTimeFinish";
            //ViewData["RatingSortParam"] = sortOrder == "Rating" ? "RatingDesc" : "Rating";
            //ViewData["TypeSortParam"] = sortOrder == "Type" ? "TypeDesc" : "Type";
            //ViewData["AddressSortParam"] = sortOrder == "Address" ? "AddressDesc" : "Address";

            var tagedPlace = new Dictionary<string, List<Place>>();
            var tagedPlaces = new List<Place>();
            foreach (var tag in chosenTags)
            {
                tagedPlace.Add(tag, (_context.Places.Where(x => x.City == city && x.Type == tag)).ToList());
                tagedPlaces.AddRange(_context.Places.Where(x => x.City == city && x.Type == tag 
                                                                               && x.State == PlaceState.Сonfirmed));
            }

            ViewBag.TagedPlace = tagedPlace;
            
            return View(tagedPlaces);
            //var tmpTagedPlaces1 = from pl in tagedPlaces
            //                        select pl;
            //var tmpTagedPlaces = tmpTagedPlaces1.AsQueryable();
            //switch (sortOrder)
            //{
            //    case "NameDesc":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderByDescending(u => u.Name);//.ToList();
            //        break;
            //    case "Type":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderBy(s => s.Type);//.ToList();
            //        break;
            //    case "TypeDesc":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderByDescending(s => s.Type);//.ToList();
            //        break;
            //    case "Address":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderBy(s => s.Address);//.ToList();
            //        break;
            //    case "AddressDesc":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderByDescending(s => s.Address);//.ToList();
            //        break;
            //    case "WorkTimeStart":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderBy(s => s.WorkTimeStart);//.ToList();
            //        break;
            //    case "WorkTimeStartDesc":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderByDescending(s => s.WorkTimeStart);//.ToList();
            //        break;
            //    case "WorkTimeFinish":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderBy(s => s.WorkTimeFinish);//.ToList();
            //        break;
            //    case "WorkTimeFinishDesc":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderByDescending(s => s.WorkTimeFinish);//.ToList();
            //        break;
            //    case "Rating":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderBy(s => s.Rating);//.ToList();
            //        break;
            //    case "RatingDesc":
            //        tmpTagedPlaces = tmpTagedPlaces.OrderByDescending(s => s.Rating);//.ToList();
            //        break;
            //    default:
            //        tmpTagedPlaces = tmpTagedPlaces.OrderBy(s => s.Name);//.ToList();
            //        break;
            //}

            
            //return View(await tmpTagedPlaces.AsNoTracking().ToListAsync());
        }
    

        
        [HttpPost]
        public IActionResult CreateTrip(string ids, string trip)
        {
            string [] places =ids.Split('*');
            int[] idsInt = new int[places.GetLength(0)];
            for (var i=0; i < places.GetLength(0); i++)
            {
                idsInt[i] = int.Parse(places[i]);
            }

            string[] tripProps = trip.Split('|');
            string tmpDate1 = tripProps[1] + " " + tripProps[2];
            string tmpDate2 = tripProps[3] + " " + tripProps[4];
            DateTime DT1 = Convert.ToDateTime(tmpDate1);
            DateTime DT2 = Convert.ToDateTime(tmpDate2);
            //for (var i = 0; i < tripProps.GetLength(0); i++)
            //{
            //    idsInt[i] = int.Parse(places[i]);
            //}


            _newTrip = new Trip()
            {
                City = tripProps[0],
                StayTimeStart =DT1,
                StayTimeFinish = DT2,
                Budget = Convert.ToDouble(tripProps[5]),
                Places = new List<Place>(),
                ConcreteUserId =  User.FindFirst(ClaimTypes.NameIdentifier).Value
            };

            //_newTrip = (Trip) TempData["_newTrip"];
            foreach (var id in idsInt)
            {
                //_newTrip.Places.Add(_context.Places.FirstOrDefault(pl=>pl.Id==id));
                _newTrip.Places.Add(_context.Places.SingleOrDefault(pl=>pl.Id==id));
            }

            _context.Trips.Add(_newTrip);
            _context.SaveChanges();
            return View();
        }


        // Here we have some problems in commented code. Please look at this.

        //// POST: Trips/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,City,StayTimeStart,StayTimeFinish,Budget")] Trip trip)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(trip);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(trip);
        //}

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips.FindAsync(id);
            if (trips == null)
            {
                return NotFound();
            }
            var trp = new TripWithPlaces()
            {
                Trips = new List<Trip>(){trips},
                Places = await _context.Places.Where(pl => pl.Trip != null && pl.Trip.Id == trips.Id).ToListAsync(),
            };

            return View(trp); //trips
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,StayTimeStart,StayTimeFinish,Budget")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }
            var trp = new TripWithPlaces()
            {
                Trips = new List<Trip>() { trip },
                Places = await _context.Places.Where(pl => pl.Trip != null && pl.Trip.Id == trip.Id).ToListAsync(),
            }; ;
            if (ModelState.IsValid)
            {
                
                try
                {
                    //trp.Places = ; //Bind Places 
                    
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripsExists(trip.Id))
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
            return View(trp); //trip
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trips = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trips == null)
            {
                return NotFound();
            }

            return View(trips);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trips = await _context.Trips.FindAsync(id);
            _context.Trips.Remove(trips);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripsExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
