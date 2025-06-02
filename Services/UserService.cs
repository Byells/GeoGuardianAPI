using GeoGuardian.Data;
using GeoGuardian.Dtos.User;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GeoGuardian.Services;

public class UserService : IUserService
{
    private readonly GeoGuardianContext _ctx;
    public UserService(GeoGuardianContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var list = await _ctx.Users.AsNoTracking().ToListAsync();
        return list.Select(ToDto);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        return u is null ? null : ToDto(u);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var entity = new User
        {
            FullName   = dto.FullName,
            Email      = dto.Email,
            Password   = Hash(dto.Password),
            UserTypeId = dto.UserTypeId
        };
        _ctx.Users.Add(entity);
        await _ctx.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
    {
        var entity = await _ctx.Users.FindAsync(id);
        if (entity is null) return false;

        entity.FullName = dto.FullName;
        entity.Email    = dto.Email;
        if (!string.IsNullOrWhiteSpace(dto.Password))
            entity.Password = Hash(dto.Password);
        if (dto.UserTypeId.HasValue) entity.UserTypeId = dto.UserTypeId.Value;

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.Users.FindAsync(id);
        if (entity is null) return false;

        _ctx.Users.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }

    // helpers
    private static string Hash(string pw)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pw));
        return Convert.ToHexString(bytes);
    }

    private static UserDto ToDto(User u) => new()
    {
        UserId     = u.UserId,
        FullName   = u.FullName,
        Email      = u.Email,
        Created    = u.Created,
        UserTypeId = u.UserTypeId
    };
}
