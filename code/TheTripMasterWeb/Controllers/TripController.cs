using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheTripMasterWeb.DataLayer;
using TheTripMasterWeb.Models;

namespace TheTripMasterWeb.Controllers
{
    public class TripController : Controller
    {
        private readonly ILogger<TripController> _logger;

        public TripController(ILogger<TripController> logger)
        {
            _logger = logger;
        }

        public IActionResult AddTrip()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTrip(string name, DateTime startDateTime, DateTime endDateTime)
        {
            bool isNameValid = TripValidation.ValidateName(name);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(startDateTime, endDateTime);

            if (isNameValid && areDateTimesValid)
            {
                TripDataLayer.AddTrip(new Trip { Name = name, StartDate = startDateTime, EndDate = endDateTime });
                return RedirectToAction("Homepage", "Home");
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Invalid trip name.");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            return View();
        }

        public IActionResult TripDetails(string name, string start, string end)
        {
            ViewData["name"] = name;
            ViewData["start"] = start;
            ViewData["end"] = end;

            IEnumerable<Waypoint> waypoints = TripDataLayer.GetTripWaypoints(name);
            Trip trip = new Trip { 
                Name = name, 
                StartDate = Convert.ToDateTime(start), 
                EndDate = Convert.ToDateTime(end), 
                Waypoints = waypoints};

            return View(model: trip);
        }

        [HttpPost]
        public IActionResult TripDetails(string name, DateTime startDateTime, DateTime endDateTime)
        {
            bool areDateTimesValid = TripValidation.ValidateDateTimes(startDateTime, endDateTime);

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
                return View(new Trip { Name = name, StartDate = startDateTime, EndDate = endDateTime });
            }

            TripDataLayer.UpdateTrip(name, startDateTime, endDateTime);
            return RedirectToAction("Homepage", "Home");
        }

        public IActionResult AddWaypoint(string name)
        {
            Waypoint waypoint = new Waypoint {TripName = name};
            return View(waypoint);
        }

        [HttpPost]
        public IActionResult AddWaypoint(Waypoint model, string name)
        {
            Debug.WriteLine(name);
            bool isNameValid = TripValidation.ValidateName(model.WaypointName);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid)
            {
                Debug.WriteLine(name);
                TripDataLayer.AddWaypoint(model, name);
                return RedirectToAction("Homepage", "Home");
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Invalid name.");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            return View(model);
        }
    }
}
