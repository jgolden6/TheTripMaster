using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterWeb.Controllers
{
    public class WaypointController : Controller
    {
        private readonly ILogger<WaypointController> _logger;
        private WaypointDataLayer waypointDataLayer;

        public WaypointController(ILogger<WaypointController> logger)
        {
            _logger = logger;
            this.waypointDataLayer = new WaypointDataLayer();
        }

        /*
         * Makes a waypoint and returns the AddWaypoint view.
         *
         * Returns: an AddWaypoint view using the new waypoint as a model.
         */
        public IActionResult AddWaypoint(string name)
        {
            Waypoint waypoint = new Waypoint { TripName = name };
            return View(waypoint);
        }

        /*
         * Validates and adds the waypoint
         *
         * Returns: Redirect to TripDetails action if valid, return AddWaypoint view otherwise.
         */
        [HttpPost]
        public IActionResult AddWaypoint(Waypoint model)
        {
            bool isNameValid = TripValidation.ValidateName(model.WaypointName);
            bool isStreetAddressValid = AddressValidation.ValidateAddressField(model.StreetAddress);
            bool isCityValid = AddressValidation.ValidateAddressField(model.City);
            bool isStateValid = AddressValidation.ValidateAddressField(model.State);
            bool isZipCodeValid = AddressValidation.ValidateZipCode(model.ZipCode);
            bool isAddressValid = AddressValidation.ValidateAddress(model.StreetAddress, model.City, model.State, model.ZipCode);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.Id, model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable && isStreetAddressValid && isCityValid && isStateValid && isZipCodeValid && isAddressValid)
            {
                model.TripName = SelectedTrip.Trip.Name;
                model.Id = this.waypointDataLayer.AddWaypoint(model);
                return View("WaypointDetails", model);
            }

            if (!isNameValid)
            {
                ModelState.AddModelError("", "Invalid name.");
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

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps an existing event.");
            }

            if (!isAddressValid)
            {
                ModelState.AddModelError("", "This address does not exist.");
            }

            return View(model);
        }

        /*
         * Creates a waypoint and returns a WaypointDetails view
         *
         * Returns: A WaypointDetails.
         */
        [HttpGet]
        public IActionResult WaypointDetails(int Id, string waypointName, string streetAddress, string city, string state, string zipCode, DateTime start, DateTime end)
        {
            Waypoint waypoint = new Waypoint { Id = Id, 
                                               TripId = SelectedTrip.Trip.TripId, 
                                               TripName = SelectedTrip.Trip.Name, 
                                               WaypointName = waypointName,
                                               StreetAddress = streetAddress,
                                               City = city,
                                               State = state,
                                               ZipCode = zipCode,
                                               StartDate = start, 
                                               EndDate = end };

            Coordinates coords = AddressValidation.GetLatitudeLongitude(waypoint.StreetAddress, waypoint.City, waypoint.State, waypoint.ZipCode);

            waypoint.Latitude = coords.Latitude;
            waypoint.Longitude = coords.Longitude;

            return View(model: waypoint);
        }

        [HttpPost]
        public IActionResult EditWaypoint(Waypoint model)
        {
            bool isNameValid = TripValidation.ValidateName(model.WaypointName);
            bool isStreetAddressValid = AddressValidation.ValidateAddressField(model.StreetAddress);
            bool isCityValid = AddressValidation.ValidateAddressField(model.City);
            bool isStateValid = AddressValidation.ValidateAddressField(model.State);
            bool isZipCodeValid = AddressValidation.ValidateZipCode(model.ZipCode);
            bool isAddressValid = AddressValidation.ValidateAddress(model.StreetAddress, model.City, model.State, model.ZipCode);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.Id, model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable && isStreetAddressValid && isCityValid && isStateValid && isZipCodeValid && isAddressValid)
            {
                model.TripName = SelectedTrip.Trip.Name;
                this.waypointDataLayer.EditWaypoint(model);

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
                ModelState.AddModelError("", "Invalid name.");
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

            if (!areDateTimesValid)
            {
                ModelState.AddModelError("", "Invalid time frame.");
            }

            if (!isTimeframeAvailable)
            {
                ModelState.AddModelError("", "Time-frame overlaps an existing event.");
            }

            if (!isAddressValid)
            {
                ModelState.AddModelError("", "This address does not exist.");
            }

            return View("WaypointDetails", model);
        }

        /*
         * Checks if the specified timeframe overlaps any of the trip's existing waypoints.
         *
         * Returns: false, if overlap,
         *          true otherwise.
         */
        private bool IsTimeframeAvailable(int tripId, int waypointId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Waypoint> waypoints = this.waypointDataLayer.GetTripWaypoints(tripId);

            Debug.WriteLine(waypointId);
            foreach (Waypoint waypoint in waypoints)
            {
                if ((waypoint.StartDate < endDateTime && startDateTime < waypoint.EndDate) && (waypointId != waypoint.Id))
                {
                    Debug.WriteLine(waypoint.Id);
                    return false;
                }
            }

            return true;
        }

        /*
         * Uses the datalayer to retrieve waypoint data and returns a View..
         *
         * Returns: A RemoveWaypoint view using the waypoint as a model.
         */
        [HttpGet]
        public IActionResult RemoveWaypoint(int waypointId)
        {
            Waypoint waypoint = this.waypointDataLayer.GetWaypoint(waypointId);
            return View(waypoint);
        }

        /*
         * Uses the datalayer to delete the specified waypoint from the database.
         *
         * Returns: Redirect to TripDetails page using the SelectedTrip as a model.
         */
        [HttpPost]
        public IActionResult RemoveWaypoint(Waypoint waypoint)
        {
            this.waypointDataLayer.DeleteWaypoint(waypoint.Id);

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
