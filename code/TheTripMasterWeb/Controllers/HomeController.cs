using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using TheTripMasterLibrary.DataLayer;
using TheTripMasterLibrary.Model;

namespace TheTripMasterWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserDataLayer userDataLayer;
        private TripDataLayer tripDataLayer;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this.userDataLayer = new UserDataLayer();
            this.tripDataLayer = new TripDataLayer();
        }

        /*
         * Returns the Index View
         */
        public IActionResult Index()
        {
            return View();
        }


        /*
         * Uses the datalayer to authenticate the credentials. If the user returns null, returns the Index
         * view, with an error. If successfull, the User is set as the Active and Redirected to the homepage.
         *
         * Return: Index View if bad credentials, Redirect to Index action otherwise.
         */
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            username ??= "";
            password ??= "";

            User authenticatedUser = this.userDataLayer.Authenticate(username, password);

            if (authenticatedUser == null)
            {
                ModelState.AddModelError("", "Incorrect username or password.");
                return View("Index");
            }

            ActiveUser.User = authenticatedUser;
            return RedirectToAction("Homepage");
        }

        public IActionResult Register()
        {
            return View();
        }

        /*
         * Validates the User's inputs for validity. If Valid, Adds the new user to the database and returns
         * the Index View. If Invalid, does not add the user and returns the Register page with some errors.
         *
         * Returns: Index View if valid, Register otherwise.
         */
        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string email, string username, string password, string passwordCheck)
        {
            bool isFirstNameValid = UserValidation.ValidateName(firstName);
            bool isLastNameValid = UserValidation.ValidateName(lastName);
            bool isEmailValid = UserValidation.ValidateEmail(email);
            bool isUsernameValid = UserValidation.ValidateUsername(username);
            bool isPasswordValid = UserValidation.ValidatePassword(password);

            if (isFirstNameValid && isLastNameValid && isEmailValid && isUsernameValid && isPasswordValid)
            {
                if (!password.Equals(passwordCheck))
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                }
                else
                {
                    this.userDataLayer.AddUser(new User { FirstName = firstName, LastName = lastName, Email = email, Username = username, Password = password});
                    return View("Index");
                }
            }

            if (!isFirstNameValid) 
            {
                ModelState.AddModelError("", "Invalid first name.");
            }

            if (!isLastNameValid)
            {
                ModelState.AddModelError("", "Invalid last name.");
            }

            if (!isEmailValid)
            {
                ModelState.AddModelError("", "Invalid email.");
            }

            if (!isUsernameValid)
            {
                ModelState.AddModelError("", "Invalid username.");
            }

            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid password.");
            }

            return View();
        }

        /*
         * Gets a list of all of the ActiveUser's trips from the datalayer.
         *
         * Returns: Homepage view using the list as a model.
         */
        public IActionResult Homepage()
        {
            List<Trip> usersTrips = this.tripDataLayer.GetAllTripsOfUser(ActiveUser.User.UserId);
            return View(model:usersTrips);
        }

        /*
         * Sets the ActiveUser to null and returns the Index View.
         *
         * Returns: Index View (Login)
         */
        public IActionResult Logout()
        {
            ActiveUser.User = null;
            return View("Index");
        }
    }
}
