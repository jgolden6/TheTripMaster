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
            Waypoint waypoint = AddEvent(model);

            if (waypoint == null)
            {
                var routeData = new
                {
                    TripId = SelectedTrip.Trip.TripId,
                    name = SelectedTrip.Trip.Name,
                    start = SelectedTrip.Trip.StartDate,
                    end = SelectedTrip.Trip.EndDate
                };
                return RedirectToAction("TripDetails", "Trip", routeData);
            }

            return View(waypoint);
        }

        public IActionResult AddTransportation(string name)
        {
            Waypoint transportation = new Waypoint { TripName = name };
            return View("AddTransportation", transportation);
        }

        [HttpPost]
        public IActionResult AddTransportation(Waypoint model)
        {
            Waypoint waypoint = AddEvent(model);

            if (waypoint == null)
            {
                var routeData = new
                {
                    TripId = SelectedTrip.Trip.TripId,
                    name = SelectedTrip.Trip.Name,
                    start = SelectedTrip.Trip.StartDate,
                    end = SelectedTrip.Trip.EndDate
                };
                return RedirectToAction("TripDetails", "Trip", routeData);
            }

            return View(waypoint);
        }

        private Waypoint AddEvent(Waypoint waypoint)
        {
            bool isNameValid = TripValidation.ValidateName(waypoint.WaypointName);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(waypoint.StartDate, waypoint.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, waypoint.StartDate, waypoint.EndDate);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                WaypointDataLayer.AddWaypoint(waypoint);
                return null;
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

            waypoint.TripName = SelectedTrip.Trip.Name;
            return waypoint;
        }

        private bool IsTimeframeAvailable(int tripId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Waypoint> waypoints = WaypointDataLayer.GetTripWaypoints(tripId);

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
