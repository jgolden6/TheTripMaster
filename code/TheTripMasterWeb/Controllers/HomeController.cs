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

            User authenticatedUser = Authentication.Authenticate(username, password);

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
            return null;
        }
    }
}
