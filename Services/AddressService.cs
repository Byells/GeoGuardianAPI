using GeoGuardian.Data;
using GeoGuardian.Dtos.Address;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services;

public class AddressService : IAddressService
{
    private readonly GeoGuardianContext _ctx;
    public AddressService(GeoGuardianContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<AddressDto>> GetAllAsync(int userId)
    {
        var list = await _ctx.Addresses
                             .Where(a => a.UserId == userId)
                             .AsNoTracking()
                             .ToListAsync();
        return list.Select(ToDto);
    }

    public async Task<AddressDto?> GetByIdAsync(int userId, int addressId)
    {
        var addr = await _ctx.Addresses
                             .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
        return addr is null ? null : ToDto(addr);
    }

    public async Task<AddressDto> CreateAsync(int userId, CreateAddressDto dto)
    {
        var entity = new Address
        {
            UserId     = userId,
            StreetId   = dto.StreetId,
            Number     = dto.Number,
            Complement = dto.Complement,
            Latitude   = dto.Latitude,
            Longitude  = dto.Longitude
        };
        _ctx.Addresses.Add(entity);
        await _ctx.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int userId, int addressId, UpdateAddressDto dto)
    {
        var entity = await _ctx.Addresses
                               .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
        if (entity is null) return false;

        entity.Number     = dto.Number;
        entity.Complement = dto.Complement;
        entity.Latitude   = dto.Latitude;
        entity.Longitude  = dto.Longitude;
        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int userId, int addressId)
    {
        var entity = await _ctx.Addresses
                               .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
        if (entity is null) return false;

        _ctx.Addresses.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }

    private static AddressDto ToDto(Address a) => new()
    {
        AddressId  = a.AddressId,
        StreetId   = a.StreetId,
        UserId     = a.UserId,
        Number     = a.Number,
        Complement = a.Complement,
        Latitude   = a.Latitude,
        Longitude  = a.Longitude
    };
}
