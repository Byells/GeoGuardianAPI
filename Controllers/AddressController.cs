using GeoGuardian.Dtos.Address;
using GeoGuardian.Interfaces;
using GeoGuardian.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/users/{userId}/addresses")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressController(IAddressService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll(int userId)
    {
        return Ok(await _service.GetAllAsync(userId));
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> Post(int userId, CreateAddressDto dto)
    {
        var created = await _service.CreateAsync(userId, dto);
        return CreatedAtAction(nameof(GetAll), new { userId }, created);
    }

    [HttpPut("{addressId}")]
    public async Task<IActionResult> Put(int userId, int addressId, UpdateAddressDto dto)
    {
        await _service.UpdateAsync(userId, addressId, dto);
        return NoContent();
    }

    [HttpDelete("{addressId}")]
    public async Task<IActionResult> Delete(int userId, int addressId)
    {
        await _service.DeleteAsync(userId, addressId);
        return NoContent();
    }
}