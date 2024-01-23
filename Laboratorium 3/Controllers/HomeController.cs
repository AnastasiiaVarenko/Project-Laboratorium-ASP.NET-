using Laboratorium_3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Laboratorium_3.Controllers
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
            var lastVisitDate = HttpContext.Items[LastVisitCookie.CookieName] as DateTime?;
            if (lastVisitDate == null) 
            {
                ViewBag.LastVisitDate = HttpContext.Items[LastVisitCookie.CookieName];
            }
            else
            {
                ViewBag.LastVisitDate = lastVisitDate?.Date;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}