// 1. LOCATION CONTROLLER
using GeoGuardian.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly GeoGuardianContext _ctx;
    public LocationController(GeoGuardianContext ctx) => _ctx = ctx;

    [Authorize]
    [HttpGet("countries")]
    public async Task<ActionResult<IEnumerable<object>>> GetCountries()
    {
        var list = await _ctx.Countries
            .Select(c => new { c.CountryId, c.Name })
            .ToListAsync();
        return Ok(list);
    }

    [Authorize]
    [HttpGet("states")]
    public async Task<ActionResult<IEnumerable<object>>> GetStates()
    {
        var list = await _ctx.States
            .Select(s => new { s.StateId, s.Name, s.CountryId })
            .ToListAsync();
        return Ok(list);
    }

    [Authorize]
    [HttpGet("cities")]
    public async Task<ActionResult<IEnumerable<object>>> GetCities()
    {
        var list = await _ctx.Cities
            .Select(c => new { c.CityId, c.Name, c.StateId })
            .ToListAsync();
        return Ok(list);
    }
}
