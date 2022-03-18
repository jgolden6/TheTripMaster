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
    public class LodgingController : Controller
    {
        private readonly ILogger<LodgingController> _logger;

        public LodgingController(ILogger<LodgingController> logger)
        {
            _logger = logger;
        }

        /*
         * Makes a waypoint and returs the AddWaypoint view.
         *
         * Returns: an AddWaypoint view using the new waypoint as a model.
         */
        public IActionResult AddLodging(string name)
        {
            Lodging lodging = new Lodging { TripName = name };
            return View(lodging);
        }

        /*
         * Calls AddEvent to validate and add the waypoint
         *
         * Returns: Redirect to TripDetails action if valid, return AddWaypoint view otherwise.
         */
        [HttpPost]
        public IActionResult AddLodging(Lodging model)
        {
            bool isStreetAddressValid = LodgingValidation.ValidateAddressField(model.StreetAddress);
            bool isCityValid = LodgingValidation.ValidateAddressField(model.City);
            bool isStateValid = LodgingValidation.ValidateAddressField(model.State);
            bool isZipCodeValid = LodgingValidation.ValidateZipCode(model.ZipCode);
            bool isDescriptionValid = LodgingValidation.ValidateDescription(model.Description);
            bool areDateTimesValid = LodgingValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.StartDate, model.EndDate);

            if (isStreetAddressValid && isCityValid && isStateValid && isZipCodeValid && isDescriptionValid &&
                areDateTimesValid && isTimeframeAvailable)
            {
                if (model.Description == null)
                {
                    model.Description = "";
                }
                LodgingDataLayer.AddLodging(model);
                var routeData = new
                {
                    TripId = SelectedTrip.Trip.TripId,
                    name = SelectedTrip.Trip.Name,
                    start = SelectedTrip.Trip.StartDate,
                    end = SelectedTrip.Trip.EndDate
                };
                return RedirectToAction("TripDetails", "Trip", routeData);
            }

            if (!isStreetAddressValid)
            {
                ModelState.AddModelError("", "Invalid Street Address.");
            }

            if (!isCityValid)
            {
                ModelState.AddModelError("", "Invalid City.");
            }

            if (!isStateValid)
            {
                ModelState.AddModelError("", "Invalid State/Province.");
            }

            if (!isZipCodeValid)
            {
                ModelState.AddModelError("", "Invalid Zip Code");
            }

            if (!isDescriptionValid)
            {
                ModelState.AddModelError("", "Description too long. 1-256 characters");
            }

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps an existing event.");
            }

            return View(new Lodging { TripName = SelectedTrip.Trip.Name});
        }

        /*
         * Checks if the specified timeframe overlaps any of the trip's existing lodgings.
         *
         * Returns: false, if overlap,
         *          true otherwise.
         */
        private bool IsTimeframeAvailable(int tripId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Lodging> lodgings = LodgingDataLayer.GetTripLodgings(tripId);

            foreach (Lodging lodging in lodgings)
            {
                if (lodging.StartDate < endDateTime && startDateTime < lodging.EndDate)
                {
                    return false;
                }
            }

            return true;
        }

        /*
         * Creates a lodging and returns a LodgingDetails view
         *
         * Returns: A LodgingDetails view.
         */
        [HttpGet]
        public IActionResult LodgingDetails(int lodgingId, string street, string city, string state, string zip, string description, DateTime start, DateTime end)
        {
            Lodging lodging = new Lodging
            {
                LodgingId = lodgingId,
                TripName = SelectedTrip.Trip.Name,
                StreetAddress = street,
                City = city,
                State = state,
                ZipCode = zip,
                Description = description,
                StartDate = start,
                EndDate = end
            };
            return View(model: lodging);
        }

        [HttpPost]
        public IActionResult LodgingDetails(int lodgingId)
        {
            return RedirectToAction("RemoveLodging", new { lodgingId = lodgingId });
        }

        /*
         * Uses the datalayer to retrieve lodging data and returns a View..
         *
         * Returns: A RemoveLodging view using the lodging as a model.
         */
        [HttpGet]
        public IActionResult RemoveLodging(int lodgingId)
        {
            Lodging lodging = LodgingDataLayer.GetLodging(lodgingId);
            Debug.WriteLine(lodgingId);
            return View(lodging);
        }

        /*
         * Uses the datalayer to delete the specified lodging from the database.
         *
         * Returns: Redirect to TripDetails page using the SelectedTrip as a model.
         */
        [HttpPost]
        public IActionResult RemoveLodging(Lodging lodging)
        {
            LodgingDataLayer.DeleteLodging(lodging.LodgingId);

            var routeData = new
            {
                TripId = SelectedTrip.Trip.TripId,
                name = SelectedTrip.Trip.Name,
                start = SelectedTrip.Trip.StartDate,
                end = SelectedTrip.Trip.EndDate
            };
            return RedirectToAction("TripDetails", "Trip", routeData);
        }
    }
}
