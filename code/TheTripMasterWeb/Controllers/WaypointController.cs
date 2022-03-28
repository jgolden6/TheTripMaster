﻿using Microsoft.AspNetCore.Mvc;
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
        private WaypointDataLayer waypointDataLayer;

        public WaypointController(ILogger<WaypointController> logger)
        {
            _logger = logger;
            this.waypointDataLayer = new WaypointDataLayer();
        }

        /*
         * Makes a waypoint and returs the AddWaypoint view.
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
            bool areDateTimesValid = TripValidation.ValidateDateTimes(model.StartDate, model.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, model.StartDate, model.EndDate);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                model.TripName = SelectedTrip.Trip.Name;
                this.waypointDataLayer.AddWaypoint(model);

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
         * Validates the waypoint data and uses the datalayer to add it to the database if valid.
         *
         * Returns: null if valid, the waypoint otherwise.
         */
        private Waypoint AddEvent(Waypoint waypoint)
        {
            bool isNameValid = TripValidation.ValidateName(waypoint.WaypointName);
            bool areDateTimesValid = TripValidation.ValidateDateTimes(waypoint.StartDate, waypoint.EndDate);
            bool isTimeframeAvailable = this.IsTimeframeAvailable(SelectedTrip.Trip.TripId, waypoint.StartDate, waypoint.EndDate);

            if (isNameValid && areDateTimesValid && isTimeframeAvailable)
            {
                this.waypointDataLayer.AddWaypoint(waypoint);
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

        /*
         * Creates a waypoint and returns a WaypointDetails view
         *
         * Returns: A WaypointDetails.
         */
        [HttpGet]
        public IActionResult WaypointDetails(int waypointId, string name, DateTime start, DateTime end)
        {
            Waypoint waypoint = new Waypoint { Id = waypointId, 
                                               TripId = SelectedTrip.Trip.TripId, 
                                               TripName = SelectedTrip.Trip.Name, 
                                               WaypointName = name, 
                                               StartDate = start, 
                                               EndDate = end };
            return View(model: waypoint);
        }

        [HttpPost]
        public IActionResult WaypointDetails(int waypointId)
        {
            return RedirectToAction("RemoveWaypoint", new {waypointId = waypointId});
        }

        /*
         * Checks if the specified timeframe overlaps any of the trip's existing waypoints.
         *
         * Returns: false, if overlap,
         *          true otherwise.
         */
        private bool IsTimeframeAvailable(int tripId, DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Waypoint> waypoints = this.waypointDataLayer.GetTripWaypoints(tripId);

            foreach (Waypoint waypoint in waypoints)
            {
                if (waypoint.StartDate < endDateTime && startDateTime < waypoint.EndDate)
                {
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
