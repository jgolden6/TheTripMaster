using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterWeb.Controllers
{
    public class TransportationController : Controller
    {
        private readonly ILogger<TransportationController> _logger;

        public TransportationController(ILogger<TransportationController> logger)
        {
            _logger = logger;
        }

        /*
         * Makes a transportation object and returns the view.
         *
         * Return: AddTransportation view.
         */
        public IActionResult AddTransportation(string name)
        {
            Transportation transportation = new Transportation { TripName = name };
            return View("AddTransportation", transportation);
        }

        /*
         * Validates and adds the transportation
         *
         * Returns: Redirect to TripDetails action if valid, return AddTransportation view otherwise.
         */
        [HttpPost]
        public IActionResult AddTransportation(Transportation model)
        {
            bool isNameValid = model.TransportationType != null;
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                model.TripName = SelectedTrip.Trip.Name;
                TransportationDataLayer.AddTransportation(model);

                var routeData = new
                {
                    TripId = SelectedTrip.Trip.TripId,
                    name = SelectedTrip.Trip.Name,
                    start = SelectedTrip.Trip.StartDate,
                    end = SelectedTrip.Trip.EndDate
                };
                return RedirectToAction("TripDetails", "Trip", routeData);
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Please select a transport type.");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps an existing event.");
            }

            return View(model);
        }

        /*
         * Creates a transportation object and returns a TransportationDetails view
         *
         * Returns: A TransportationDetails view.
         */
        [HttpGet]
        public IActionResult TransportationDetails(int transportationId, string type, DateTime start, DateTime end)
        {
            Transportation transport = new Transportation
            {
                Id = transportationId,
                TripId = SelectedTrip.Trip.TripId,
                TripName = SelectedTrip.Trip.Name,
                TransportationType = type,
                StartDate = start,
                EndDate = end
            };
            return View(model: transport);
        }

        [HttpPost]
        public IActionResult TransportationDetails(int transportationId)
        {
            return RedirectToAction("RemoveTransportation", new { transportationId = transportationId });
        }

        /*
         * Uses the datalayer to retrieve transportation data and returns a View..
         *
         * Returns: A RemoveTransportation view using the Transportation as a model.
         */
        [HttpGet]
        public IActionResult RemoveTransportation(int transportationId)
        {
            Transportation transportation = TransportationDataLayer.GetTransportation(transportationId);
            return View(transportation);
        }

        /*
         * Uses the datalayer to delete the specified Transportation from the database.
         *
         * Returns: Redirect to TripDetails page using the SelectedTrip as a model.
         */
        [HttpPost]
        public IActionResult RemoveTransportation(Transportation transportation)
        {
            TransportationDataLayer.DeleteTransportation(transportation.Id);

            var routeData = new
            {
                TripId = SelectedTrip.Trip.TripId,
                name = SelectedTrip.Trip.Name,
                start = SelectedTrip.Trip.StartDate,
                end = SelectedTrip.Trip.EndDate
            };
            return RedirectToAction("TripDetails", "Trip", routeData);
        }

        /*
         * Checks if the specified timeframe overlaps any of the trip's existing waypoints.
         *
         * Returns: false, if overlap,
         *          true otherwise.
         */
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
