using System.Security.Claims;
using GeoGuardian.Dtos.Address;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/users/me/addresses")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressController(IAddressService service)
    {
        _service = service;
    }

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll()
        => Ok(await _service.GetAllAsync(GetUserId()));

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AddressDto>> Post(CreateAddressDto dto)
    {
        var created = await _service.CreateAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetAll), new { id = created.AddressId }, created);
    }

    [Authorize]
    [HttpPut("{addressId}")]
    public async Task<IActionResult> Put(int addressId, UpdateAddressDto dto)
        => await _service.UpdateAsync(GetUserId(), addressId, dto) ? NoContent() : NotFound();

    [Authorize]
    [HttpDelete("{addressId}")]
    public async Task<IActionResult> Delete(int addressId)
        => await _service.DeleteAsync(GetUserId(), addressId) ? NoContent() : NotFound();
}