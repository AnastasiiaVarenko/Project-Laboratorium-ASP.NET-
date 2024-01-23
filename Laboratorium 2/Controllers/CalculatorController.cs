using Laboratorium_2.Models;
using Microsoft.AspNetCore.Mvc;
namespace Laboratorium_2.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpPost]
        public IActionResult Result(Calculator model)
        {
            if (!model.IsValid())
            {
                return View("Error");
            }
            return View(model);          
        }
        public IActionResult Form()
        {
            return View();
        }        
    }
}
