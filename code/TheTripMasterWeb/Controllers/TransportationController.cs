using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterWeb.Controllers
{
    public class TransportationController : Controller
    {
        private readonly ILogger<TransportationController> _logger;
        private TransportationDataLayer transportationDataLayer;

        public TransportationController(ILogger<TransportationController> logger)
        {
            _logger = logger;
            this.transportationDataLayer = new TransportationDataLayer();
        }

        /*
         * Makes a transportation object and returns the view.
         *
         * Return: AddTransportation view.
         */
        public IActionResult AddTransportation(string name)
        {
            Transportation transportation = new Transportation { 
                TripName = name, 
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
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
            bool areDateTimesValid = TripValidation.ValidateStartBeforeEnd(model.StartDate, model.EndDate);
            bool isStartAfterNow = TripValidation.ValidateDateTimesAfterNow(model.StartDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.Id, model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid && isStartAfterNow && isTimeframeAvailable)
            {
                model.TripName = SelectedTrip.Trip.Name;

                var routeData = new
                {
                    transportationId = this.transportationDataLayer.AddTransportation(model),
                    type = model.TransportationType,
                    start = model.StartDate,
                    end = model.EndDate
                };
                return RedirectToAction("transportationDetails", routeData);
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Please select a transport type.");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "The end date cannot be before the start date.");
            }

            if (!isStartAfterNow)
            {
                ModelState.AddModelError("", "Transportation cannot start before the current time.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps event: " + ViewData["Overlap"]);
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
            var transportTypes = (from TransportationType i in Enum.GetValues(typeof(TransportationType))
                select new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            ViewBag.TransportTypes = transportTypes;

            return View(model: transport);
        }

        /*
         * Validates and updates the transportation
         *
         * Returns: return to details page if invalid, redirect to TripDetails view otherwise.
         */
        [HttpPost]
        public IActionResult EditTransportation(Transportation model)
        {
            bool isNameValid = model.TransportationType != null;
            bool areDateTimesValid = TripValidation.ValidateStartBeforeEnd(model.StartDate, model.EndDate);
            bool isStartAfterNow = TripValidation.ValidateDateTimesAfterNow(model.StartDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.Id, model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid && isStartAfterNow && isTimeframeAvailable)
            {
                model.TripName = SelectedTrip.Trip.Name;
                this.transportationDataLayer.EditTransportation(model);

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

            return View("TransportationDetails", model);
        }

        /*
         * Uses the datalayer to retrieve transportation data and returns a View..
         *
         * Returns: A RemoveTransportation view using the Transportation as a model.
         */
        [HttpGet]
        public IActionResult RemoveTransportation(int transportationId)
        {
            Transportation transportation = this.transportationDataLayer.GetTransportation(transportationId);
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
            this.transportationDataLayer.DeleteTransportation(transportation.Id);

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
        private bool IsTimeframeAvailable(int tripId, int transportationId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Transportation> transports = this.transportationDataLayer.GetTripTransportations(tripId);

            foreach (Transportation transport in transports)
            {
                if ((transport.StartDate < endDateTime && startDateTime < transport.EndDate) && (transportationId != transport.Id))
                {
                    ViewData["Overlap"] = transport.ToString() + ": [" + transport.StartDate.ToString() + " - " +
                                          transport.EndDate.ToString() + "]";
                    return false;
                }
            }

            return true;
        }
    }
}
