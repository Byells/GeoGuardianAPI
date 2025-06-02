using GeoGuardian.Dtos.Sensor;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly ISensorService _service;
    public SensorController(ISensorService service) => _service = service;

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
    public async Task<ActionResult<SensorDto>> Post([FromBody] CreateSensorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateSensorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}