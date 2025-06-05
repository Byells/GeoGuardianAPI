using BCrypt.Net;
using GeoGuardian.Data;
using GeoGuardian.Dtos.User;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services;

public class AuthService
{
    private readonly GeoGuardianContext _context;
    private readonly JwtService _jwt;

    public AuthService(GeoGuardianContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
    {
        var user = await _context.Users.Include(u => u.UserType).FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;

        return new LoginResponseDto
        {
            Token = _jwt.GenerateToken(user),
            User  = new UserDto
            {
                UserId   = user.UserId,
                FullName = user.FullName,
                Email    = user.Email,
                UserType = user.UserType?.Name ?? "Desconhecido"
            }
        };
    }
}