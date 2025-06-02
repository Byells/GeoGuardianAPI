using GeoGuardian.Dtos.User;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _svc;
    public UserController(IUserService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get() =>
        Ok(await _svc.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var dto = await _svc.GetByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.UserId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _svc.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}