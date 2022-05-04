using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using GoogleMaps.LocationServices;
using Microsoft.Extensions.Logging;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;
using Xamarin.Forms.Maps;

namespace TheTripMasterWeb.Controllers
{
    public class LodgingController : Controller
    {
        private readonly ILogger<LodgingController> _logger;
        private LodgingDataLayer lodgingDataLayer;

        public LodgingController(ILogger<LodgingController> logger)
        {
            _logger = logger;
            this.lodgingDataLayer = new LodgingDataLayer();
        }

        /*
         * Makes a waypoint and returs the AddWaypoint view.
         *
         * Returns: an AddWaypoint view using the new waypoint as a model.
         */
        public IActionResult AddLodging(string name)
        {
            Lodging lodging = new Lodging
            {
                TripName = name,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
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
            bool isStreetAddressValid = AddressValidation.ValidateAddressField(model.StreetAddress);
            bool isCityValid = AddressValidation.ValidateAddressField(model.City);
            bool isStateValid = AddressValidation.ValidateAddressField(model.State);
            bool isZipCodeValid = AddressValidation.ValidateZipCode(model.ZipCode);
            bool isDescriptionValid = LodgingValidation.ValidateDescription(model.Description);
            bool areDateTimesValid = TripValidation.ValidateStartBeforeEnd(model.StartDate, model.EndDate);
            bool isStartAfterNow = TripValidation.ValidateDateTimesAfterNow(model.StartDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.LodgingId, model.StartDate, model.EndDate);
            bool isAddressValid = AddressValidation.ValidateAddress(model.StreetAddress, model.City, model.State, model.ZipCode);

            if (isStreetAddressValid && isCityValid && isStateValid && isZipCodeValid && isDescriptionValid &&
                areDateTimesValid && isStartAfterNow && isTimeframeAvailable && isAddressValid)
            {
                if (model.Description == null)
                {
                    model.Description = "";
                }
                var routeData = new
                {
                    lodgingId = model.LodgingId,
                    street = model.StreetAddress,
                    city = model.City,
                    state = model.State,
                    zip = model.ZipCode,
                    start = model.StartDate,
                    end = model.EndDate
                };
                model.LodgingId = this.lodgingDataLayer.AddLodging(model);
                return RedirectToAction("LodgingDetails", routeData);
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

            if (!isAddressValid)
            {
                ModelState.AddModelError("", "This address does not exist.");
            }

            model.TripName = SelectedTrip.Trip.Name;
            return View(model);
        }

        /*
         * Calls AddEvent to validate and edit the waypoint
         *
         * Returns: Redirect to TripDetails action if valid, return to LodgingDetails view otherwise.
         */
        [HttpPost]
        public IActionResult EditLodging(Lodging model)
        {
            bool isStreetAddressValid = AddressValidation.ValidateAddressField(model.StreetAddress);
            bool isCityValid = AddressValidation.ValidateAddressField(model.City);
            bool isStateValid = AddressValidation.ValidateAddressField(model.State);
            bool isZipCodeValid = AddressValidation.ValidateZipCode(model.ZipCode);
            bool isDescriptionValid = LodgingValidation.ValidateDescription(model.Description);
            bool areDateTimesValid = TripValidation.ValidateStartBeforeEnd(model.StartDate, model.EndDate);
            bool isStartAfterNow = TripValidation.ValidateDateTimesAfterNow(model.StartDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.LodgingId, model.StartDate, model.EndDate);
            bool isAddressValid = AddressValidation.ValidateAddress(model.StreetAddress, model.City, model.State, model.ZipCode);

            if (isStreetAddressValid && isCityValid && isStateValid && isZipCodeValid && isDescriptionValid &&
                areDateTimesValid && isStartAfterNow && isTimeframeAvailable && isAddressValid)
            {
                if (model.Description == null)
                {
                    model.Description = "";
                }
                this.lodgingDataLayer.EditLodging(model);
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

            if (!isAddressValid)
            {
                ModelState.AddModelError("", "This address does not exist.");
            }

            var route = new
            {
                lodgingId = model.LodgingId,
                street = model.StreetAddress,
                city = model.City,
                state = model.State,
                zip = model.ZipCode,
                start = model.StartDate,
                end = model.EndDate
            };
            return RedirectToAction("LodgingDetails", route); 
        }

        /*
         * Checks if the specified timeframe overlaps any of the trip's existing lodgings.
         *
         * Returns: false, if overlap,
         *          true otherwise.
         */
        private bool IsTimeframeAvailable(int tripId, int lodgingId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Lodging> lodgings = this.lodgingDataLayer.GetTripLodgings(tripId);

            foreach (Lodging lodging in lodgings)
            {
                if ((lodging.StartDate < endDateTime && startDateTime < lodging.EndDate) && (lodgingId != lodging.LodgingId))
                {
                    ViewData["Overlap"] =  "Lodging: [" + lodging.StartDate.ToString() + " - " +
                                          lodging.EndDate.ToString() + "]";
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
            
            Coordinates coords = AddressValidation.GetLatitudeLongitude(lodging.StreetAddress, lodging.City, lodging.State, lodging.ZipCode);

            lodging.Latitude = coords.Latitude;
            lodging.Longitude = coords.Longitude;

            return View(model: lodging);
        }

        /*
         * Uses the datalayer to retrieve lodging data and returns a View..
         *
         * Returns: A RemoveLodging view using the lodging as a model.
         */
        [HttpGet]
        public IActionResult RemoveLodging(int lodgingId)
        {
            Lodging lodging = this.lodgingDataLayer.GetLodging(lodgingId);
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
            this.lodgingDataLayer.DeleteLodging(lodging.LodgingId);

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
