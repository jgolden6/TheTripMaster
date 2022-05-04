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
        private TripDataLayer tripDataLayer;
        private WaypointDataLayer waypointDataLayer;
        private TransportationDataLayer transportationDataLayer;
        private LodgingDataLayer lodgingDataLayer;

        public TripController(ILogger<TripController> logger)
        {
            _logger = logger;
            this.tripDataLayer = new TripDataLayer();
            this.waypointDataLayer = new WaypointDataLayer();
            this.transportationDataLayer = new TransportationDataLayer();
            this.lodgingDataLayer = new LodgingDataLayer();
        }

        /*
         * Returns AddTrip view.
         */
        public IActionResult AddTrip()
        {
            return View();
        }

        /*
         *Validates the Trip Data and Adds the trip if valid.
         *
         * Returns: If valid, Redirect to homepage action.
         *          otherwise, Returns AddTripView with model errors.
         */
        [HttpPost]
        public IActionResult AddTrip(string name, DateTime startDateTime, DateTime endDateTime)
        {
            bool isNameValid = TripValidation.ValidateName(name);
            bool isNameAvailable = this.isNameAvailable(name);
            bool areDateTimesValid = TripValidation.ValidateStartBeforeEnd(startDateTime, endDateTime);
            bool isStartAfterNow = TripValidation.ValidateDateTimesAfterNow(startDateTime);
            bool isTimeframeAvailable = this.isTimeframeAvailable(name, startDateTime, endDateTime);

            if (isNameValid && isNameAvailable && areDateTimesValid && isStartAfterNow && isTimeframeAvailable)
            {
                this.tripDataLayer.AddTrip(new Trip { Name = name.Trim(), StartDate = startDateTime, EndDate = endDateTime });
                return RedirectToAction("Homepage", "Home");
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Invalid trip name.");
            }

            if (!isNameAvailable)
            {
                ModelState.AddModelError("", "A Trip by that name already exists.");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "The end date cannot be before the start date.");
            }

            if (!isStartAfterNow)
            {
                ModelState.AddModelError("", "Waypoint cannot start before the current time.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps event: " + ViewData["Overlap"]);
            }

            return View();
        }

        /*
         * Creates a Trip using the specified data and retrieves it's waypoints from the datalayer.
         *
         * Returns: A TripDetails view with the trip as a model.
         */
        public IActionResult TripDetails(int tripId, string name, DateTime start, DateTime end)
        {
            Trip trip = new Trip {TripId = tripId, Name = name, StartDate = start, EndDate = end};
            trip.Events = this.waypointDataLayer.GetTripWaypoints(trip.TripId);
            List<Event> events = trip.Events.ToList();
            events.AddRange(this.transportationDataLayer.GetTripTransportations(tripId));
            trip.Events = events.AsEnumerable();
            trip.Events = trip.Events.OrderBy(p => p.StartDate);
            trip.Lodgings = this.lodgingDataLayer.GetTripLodgings(trip.TripId);
            SelectedTrip.Trip = trip;
            return View(model: trip);
        }

        /*
         * Checks if the specified timeframe overlaps any of the ActiveUser's trips.
         *
         * Returns: false if the timeframe overlaps a trip where tripName != name,
         *          true otherwise.
         */
        private bool isTimeframeAvailable(string name, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Trip> trips = this.tripDataLayer.GetAllTripsOfUser(ActiveUser.User.UserId);

            foreach (Trip trip in trips)
            {

                if (name == null || name.Trim() != trip.Name.Trim())
                {
                    if (trip.StartDate < endDateTime && startDateTime < trip.EndDate)
                    {
                        ViewData["Overlap"] = trip.Name + ": [" + trip.StartDate.ToString() + " - " +
                                              trip.EndDate.ToString() + "]";
                        return false;
                    }
                }
            }
            return true;
        }

        /*
         * Checks if the specified name is already used by any trips.
         *
         * Returns: false if the name exists,
         *          true otherwise.
         */
        private bool isNameAvailable(string name)
        {
            IEnumerable<Trip> trips = this.tripDataLayer.GetAllTripsOfUser(ActiveUser.User.UserId);

            foreach (Trip trip in trips)
            {

                if (name != null && name.Trim() == trip.Name.Trim())
                {
                    return false;
                    
                }
            }
            return true;
        }

        /*
         * Returns a TripDetails view using the selected trip as a model.
         */
        public IActionResult SelectedTripDetails()
        {
            var routeData = new
            {
                TripId = SelectedTrip.Trip.TripId,
                name = SelectedTrip.Trip.Name,
                start = SelectedTrip.Trip.StartDate,
                end = SelectedTrip.Trip.EndDate
            };
            return RedirectToAction("TripDetails", routeData);
        }
    }
}
