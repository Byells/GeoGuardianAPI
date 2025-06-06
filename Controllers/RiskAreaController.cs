using GeoGuardian.Dtos.RiskArea;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RiskAreaController : ControllerBase
{
    private readonly IRiskAreaService _service;
    private readonly IUserService _userService;

    public RiskAreaController(IRiskAreaService service, IUserService userService)
    {
        _service = service;
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RiskAreaDto>>> Get() =>
        Ok(await _service.GetAllAsync());

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RiskAreaDto>> Get(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [Authorize(Roles = "1")]
    [HttpPost("admin")]
    public async Task<ActionResult<RiskAreaDto>> Post([FromBody] CreateRiskAreaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [Authorize(Roles = "1")]
    [HttpPut("admin/{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateRiskAreaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [Authorize(Roles = "1")]
    [HttpDelete("admin/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}