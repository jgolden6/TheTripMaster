using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheTripMasterWeb.Models;

namespace TheTripMasterWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            username ??= "";
            password ??= "";

            User authenticatedUser = UserDataLayer.Authenticate(username, password);

            if (authenticatedUser == null)
            {
                ModelState.AddModelError("", "Incorrect username or password.");
                return View("Index");
            }

            return View("Privacy");
        }

        public IActionResult Register()
        {
            return View();
        }

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
                    UserDataLayer.AddUser(new User { FirstName = firstName, LastName = lastName, Email = email, Username = username, Password = password});
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
    }
}
