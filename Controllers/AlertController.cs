using GeoGuardian.Dtos.Alert;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertController : ControllerBase
{
    private readonly IAlertService _svc;
    public AlertController(IAlertService svc) => _svc = svc;

    private int GetUserId() =>
        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlertDto>>> Get()
        => Ok(await _svc.GetAllAsync(GetUserId()));

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlertDto>> Get(int id)
    {
        var dto = await _svc.GetByIdAsync(GetUserId(), id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AlertDto>> Post([FromBody] CreateAlertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _svc.CreateAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateAlertDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _svc.UpdateAsync(GetUserId(), id, dto);
        return ok ? NoContent() : NotFound();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(GetUserId(), id);
        return ok ? NoContent() : NotFound();
    }
}