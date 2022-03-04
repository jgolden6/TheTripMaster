using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

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

        public IActionResult TripDetails(int tripId, string name, DateTime start, DateTime end)
        {
            Trip trip = new Trip {TripId = tripId, Name = name, StartDate = start, EndDate = end};
            trip.Waypoints = WaypointDataLayer.GetTripWaypoints(trip.TripId);
            SelectedTrip.Trip = trip;
            return View(model: trip);
        }

        [HttpPost]
        public IActionResult TripDetails(string name, DateTime startDateTime, DateTime endDateTime)
        {
            bool areDateTimesValid = TripValidation.ValidateDateTimes(startDateTime, endDateTime);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(name, startDateTime, endDateTime);

            if (!areDateTimesValid || !isTimeframeAvailable)
            {

                if (!areDateTimesValid)
                {
                    ModelState.AddModelError("", "Invalid time frame.");
                }

                if (!isTimeframeAvailable)
                {
                    ModelState.AddModelError("", "Time-frame overlaps an existing trip.");
                }

                return View(new Trip { Name = name, StartDate = startDateTime, EndDate = endDateTime, Waypoints = SelectedTrip.Trip.Waypoints });
            }

            TripDataLayer.UpdateTrip(name, startDateTime, endDateTime);
            return RedirectToAction("Homepage", "Home");
        }

        private bool IsTimeframeAvailable(string name, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Trip> trips = TripDataLayer.GetAllTripsOfUser(ActiveUser.User.UserId);

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
