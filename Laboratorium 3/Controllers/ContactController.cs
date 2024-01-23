using Laboratorium_3.Models.ContactModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Laboratorium_3.Controllers
{
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.FindAllAsync();
            return View(contacts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> PagedIndex(int page = 1, int size = 5)
        {
            if (size < 1)
            {
                return BadRequest();
            }

            var contactsPaged = await _contactService.FindPageAsync(page, size);
            return View(contactsPaged);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var organizations = CreateOrganizationItemList();
            var model = new Contact { OrganizationList = organizations };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddAsync(model);
                return RedirectToAction("Index");
            }

            model.OrganizationList = CreateOrganizationItemList();
            return View(model);
        }

        private List<SelectListItem> CreateOrganizationItemList()
        {
            var group = new SelectListGroup { Name = "Organizacje" };
            return _contactService.FindAllOrganizationsAsync()
                .Result 
                .Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString(), Group = group })
                .Append(new SelectListItem { Text = "Brak organizacji", Value = "", Selected = true, Group = new SelectListGroup { Name = "Brak" } })
                .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _contactService.FindByIdAsync(id);
            model.OrganizationList = CreateOrganizationItemList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Contact model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _contactService.FindByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact model)
        {
            await _contactService.DeleteByIdAsync(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _contactService.FindByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Details(Contact model)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateApi()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateApi(Contact model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
