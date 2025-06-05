using System.Security.Claims;
using GeoGuardian.Dtos.User;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _svc;
    public UserController(IUserService svc) => _svc = svc;

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get() =>
        Ok(await _svc.GetAllAsync());

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var dto = await _svc.GetByIdAsync(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMe()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _svc.GetByIdAsync(userId);
        return user is null ? NotFound() : Ok(user);
    }
    
    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> PutMe([FromBody] UpdateUserDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ok = await _svc.UpdateAsync(userId, dto);
        return ok ? NoContent() : NotFound();
    }

    
    [Authorize]
    [HttpDelete("me")]
    public async Task<IActionResult> DeleteMe()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var ok = await _svc.DeleteAsync(userId);
        return ok ? NoContent() : NotFound();
    }


    [HttpPost]
    public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _svc.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.UserId }, created);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ok = await _svc.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _svc.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}