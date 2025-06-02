using GeoGuardian.Dtos.Address;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/users/{userId:int}/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _svc;
    public AddressController(IAddressService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> Get(int userId) =>
        Ok(await _svc.GetAllAsync(userId));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AddressDto>> GetById(int userId, int id)
    {
        var dto = await _svc.GetByIdAsync(userId, id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> Post(int userId, [FromBody] CreateAddressDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _svc.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetById), new { userId, id = created.AddressId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int userId, int id, [FromBody] UpdateAddressDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _svc.UpdateAsync(userId, id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int userId, int id)
    {
        var ok = await _svc.DeleteAsync(userId, id);
        return ok ? NoContent() : NotFound();
    }
}