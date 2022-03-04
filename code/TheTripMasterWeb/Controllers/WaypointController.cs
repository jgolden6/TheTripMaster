using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterWeb.Controllers
{
    public class WaypointController : Controller
    {
        private readonly ILogger<WaypointController> _logger;

        public WaypointController(ILogger<WaypointController> logger)
        {
            _logger = logger;
        }

        public IActionResult AddWaypoint(string name)
        {
            Waypoint waypoint = new Waypoint { TripName = name };
            return View(waypoint);
        }

        [HttpPost]
        public IActionResult AddWaypoint(Waypoint model)
        {
            Debug.WriteLine("HERE");
            bool isNameValid = TripValidation.ValidateName(model.WaypointName);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.trip.TripId, model.StartDate, model.EndDate);
            
            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                TripDataLayer.AddWaypoint(model);
                var routeData = new
                {
                    TripId = SelectedTrip.trip.TripId,
                    name = SelectedTrip.trip.Name,
                    start = SelectedTrip.trip.StartDate,
                    end = SelectedTrip.trip.EndDate
                };
                return RedirectToAction("TripDetails", "Trip", routeData);
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Invalid name.");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps an existing event.");
            }

            model.TripName = SelectedTrip.trip.Name;
            return View(model);
        }

        public IActionResult AddTransportation(string name)
        {
            Waypoint transportation = new Waypoint { TripName = name };
            return View("AddTransportation", transportation);
        }

        private bool IsTimeframeAvailable(int tripId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Waypoint> waypoints = TripDataLayer.GetTripWaypoints(tripId);

            foreach (Waypoint waypoint in waypoints)
            {
                
                if (waypoint.StartDate < endDateTime && startDateTime < waypoint.EndDate)
                {
                    return false;
                }
                
            }

            return true;
        }
    }
}
