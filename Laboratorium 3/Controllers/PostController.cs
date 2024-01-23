using Laboratorium_3.Models.PostModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Diagnostics;

namespace Laboratorium_3.Controllers
{
    [Authorize(Roles = "admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.FindAllAsync();
            return View(posts);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PagedIndex(int page = 1, int size = 5)
        {
            if (size < 1)
            {
                return BadRequest();
            }
            return View(await _postService.FindPageAsync(page, size));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var contacts = CreateContactItemList();
            var model = new Post { ContactList = contacts };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post model)
        {
            if (ModelState.IsValid)
            {
                await _postService.AddAsync(model);
                return RedirectToAction("Index");
            }
            model.ContactList = CreateContactItemList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _postService.FindByIdAsync(id);
            model.ContactList = CreateContactItemList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Post model)
        {
            if (ModelState.IsValid)
            {
                await _postService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            model.ContactList = CreateContactItemList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _postService.FindByIdAsync(id);
            model.ContactList = CreateContactItemList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Post model)
        {
            await _postService.DeleteByIdAsync(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _postService.FindByIdAsync(id));
        }

        private List<SelectListItem> CreateContactItemList()
        {
            var group = new SelectListGroup { Name = "Kontakty" };
            return _postService.FindAllContactsAsync()
                .Result
                .Select(e => new SelectListItem { Text = e.Name, Value = e.ContactId.ToString(), Group = group })
                .ToList();
        }

    }
}