using GeoGuardian.Dtos.Sensor;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly ISensorService _service;
    private readonly IUserService _userService;

    public SensorController(ISensorService service, IUserService userService)
    {
        _service = service;
        _userService = userService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorDto>>> Get() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SensorDto>> Get(int id)
    {
        var dto = await _service.GetByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    [Route("admin/{userId:int}")]
    public async Task<ActionResult<SensorDto>> Post(int userId, [FromBody] CreateSensorDto dto)
    {
        if (!await _userService.IsAdminAsync(userId))
            return Unauthorized("User is not authorized.");

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }


    [HttpPut("admin/{userId:int}/{id:int}")]
    public async Task<IActionResult> Put(int userId, int id, [FromBody] UpdateSensorDto dto)
    {
        if (!await _userService.IsAdminAsync(userId))
            return Unauthorized("User is not authorized.");

        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("admin/{userId:int}/{id:int}")]
    public async Task<IActionResult> Delete(int userId, int id)
    {
        if (!await _userService.IsAdminAsync(userId))
            return Unauthorized("User is not authorized.");

        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}