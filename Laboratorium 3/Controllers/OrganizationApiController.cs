using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorium_3.Controllers;

[Route("api/organizations")]
[ApiController]
public class OrganizationApiController : ControllerBase
{
    private readonly AppDbContext _context;
    public OrganizationApiController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult GetOrganizationsByName(string? q)
    {
        return Ok(
            q == null ? _context.Organizations
            .Select(o => new { o.Name, o.Id })
            .ToList() :
            _context.Organizations.Where(o => o.Name.ToUpper().StartsWith(q.ToUpper()))
            .Select(o => new { o.Name, o.Id })
            .ToList()
        );
    }
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        var organization = _context.Organizations.Find(id);
        if (organization == null)
        {
            return NotFound();

        }
        else
        {
            return Ok(organization);
        }
    }
}
