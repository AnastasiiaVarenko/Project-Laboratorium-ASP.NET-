using Data;
using Laboratorium_3.Models.PostModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Laboratorium_3.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostApiController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostApiController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationsByName(string? q)
        {
            try
            {
                var posts = await _postService.FindAllAsync();

                if (!string.IsNullOrEmpty(q))
                {
                    posts = posts
                        .Where(r => r.ContactName.Contains(q, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}