using Laboratorium_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorium_2.Controllers
{
    public class BirthController : Controller
    {
        [HttpPost]
        public IActionResult Result(Birth model)
        {
            if (!model.IsValid())
            {
                return View("Error");
            }
            return View(model);
        }
        public IActionResult Birth()
        {
            return View();
        }

    }
}
