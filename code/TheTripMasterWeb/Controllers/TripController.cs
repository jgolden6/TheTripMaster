using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
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
            bool isTimeframeAvailable = this.IsTimeframeAvailable(name, startDateTime, endDateTime);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                TripDataLayer.AddTrip(new Trip { Name = name.Trim(), StartDate = startDateTime, EndDate = endDateTime });
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

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps an existing trip");
            }

            return View();
        }

        public IActionResult TripDetails(int tripId)
        {
            /*ViewData["name"] = name;
            ViewData["start"] = start;
            ViewData["end"] = end;*/

            Trip trip = TripDataLayer.GetTrip(tripId);
            return View(model: trip);
        }

        [HttpPost]
        public IActionResult TripDetails(string name, DateTime startDateTime, DateTime endDateTime)
        {
            bool areDateTimesValid = TripValidation.ValidateDateTimes(startDateTime, endDateTime);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(name, startDateTime, endDateTime);

            if (!areDateTimesValid || !isTimeframeAvailable)
            {
                IEnumerable<Waypoint> waypoints = TripDataLayer.GetTripWaypoints(name);

                if (!areDateTimesValid)
                {
                    ModelState.AddModelError("", "Invalid time frame.");
                }

                if (!isTimeframeAvailable)
                {
                    ModelState.AddModelError("", "Time-frame overlaps an existing trip.");
                }

                return View(new Trip { Name = name, StartDate = startDateTime, EndDate = endDateTime, Waypoints = waypoints });
            }

            TripDataLayer.UpdateTrip(name, startDateTime, endDateTime);
            return RedirectToAction("Homepage", "Home");
        }

        private bool IsTimeframeAvailable(string name, DateTime startDateTime, DateTime endDateTime)
        {
            int userId = UserDataLayer.GetUserId(ActiveUser.User);
            IEnumerable<Trip> trips = TripDataLayer.GetAllTripsOfUser(userId);

            foreach (Trip trip in trips)
            {

                if (name.Trim() != trip.Name.Trim())
                {
                    if (trip.StartDate < endDateTime && startDateTime < trip.EndDate)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
