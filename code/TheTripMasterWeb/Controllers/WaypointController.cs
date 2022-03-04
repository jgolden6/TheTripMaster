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
        public IActionResult AddWaypoint(Waypoint model, string name)
        {
            bool isNameValid = TripValidation.ValidateName(model.WaypointName);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(name, model.StartDate, model.EndDate);
            
            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                int tripId = TripDataLayer.AddWaypoint(model, name);
                return RedirectToAction("TripDetails", "Trip", new {tripId = tripId});
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

            model.TripName = name;
            return View(model);
        }

        private bool IsTimeframeAvailable(string tripName, DateTime startDateTime, DateTime endDateTime)
        {
            int userId = UserDataLayer.GetUserId(ActiveUser.User);
            IEnumerable<Waypoint> waypoints = TripDataLayer.GetTripWaypoints(tripName);

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
