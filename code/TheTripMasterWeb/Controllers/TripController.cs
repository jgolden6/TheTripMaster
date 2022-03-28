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

        /*
         * Creates a Trip using the specified data and retrieves it's waypoints from the datalayer.
         *
         * Returns: A TripDetails view with the trip as a model.
         */
        public IActionResult TripDetails(int tripId, string name, DateTime start, DateTime end)
        {
            Trip trip = new Trip {TripId = tripId, Name = name, StartDate = start, EndDate = end};
            trip.Events = WaypointDataLayer.GetTripWaypoints(trip.TripId);
            List<Event> events = trip.Events.ToList();
            events.AddRange(TransportationDataLayer.GetTripTransportations(tripId));
            trip.Events = events.AsEnumerable();
            trip.Events = trip.Events.OrderBy(p => p.StartDate);
            trip.Lodgings = LodgingDataLayer.GetTripLodgings(trip.TripId);
            SelectedTrip.Trip = trip;
            return View(model: trip);
        }

        /*
         * Checks if the DateTimes are valid and not overlapping existing trips. Updates the trip id if valid.
         *
         * Returns: If valid, Redirects to Homepage action
         *          otherwise, Returns TripDetails page populated with errors.
         */
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

                return View(new Trip { Name = name, StartDate = startDateTime, EndDate = endDateTime, Events = SelectedTrip.Trip.Events });
            }

            TripDataLayer.UpdateTrip(name, startDateTime, endDateTime);
            return RedirectToAction("Homepage", "Home");
        }

        /*
         * Checks if the specified timeframe overlaps any of the ActiveUser's trips.
         *
         * Returns: false if the timeframe overlaps a trip where tripName != name,
         *          true otherwise.
         */
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

        /*
         * Returns a TripDetails view using the selected trip as a model.
         */
        public IActionResult SelectedTripDetails()
        {
            Trip trip = SelectedTrip.Trip;
            return View("TripDetails", model: trip);
        }
    }
}
