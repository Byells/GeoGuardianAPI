using GeoGuardian.Data;
using GeoGuardian.Dtos.RiskArea;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services;

public class RiskAreaService : IRiskAreaService
{
    private readonly GeoGuardianContext _context;

    public RiskAreaService(GeoGuardianContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RiskAreaDto>> GetAllAsync()
    {
        var list = await _context.RiskAreas.AsNoTracking().ToListAsync();
        return list.Select(ToDto);
    }

    public async Task<RiskAreaDto?> GetByIdAsync(int id)
    {
        var entity = await _context.RiskAreas.FindAsync(id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<RiskAreaDto> CreateAsync(CreateRiskAreaDto dto)
    {
        var entity = new RiskArea
        {
            Name           = dto.Name,
            RiskAreaTypeId = dto.RiskAreaTypeId,
            StreetId       = dto.StreetId
        };

        _context.RiskAreas.Add(entity);
        await _context.SaveChangesAsync();

        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateRiskAreaDto dto)
    {
        var entity = await _context.RiskAreas.FindAsync(id);
        if (entity is null) return false;

        entity.Name           = dto.Name;
        entity.RiskAreaTypeId = dto.RiskAreaTypeId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.RiskAreas.FindAsync(id);
        if (entity is null) return false;

        _context.RiskAreas.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }


    private static RiskAreaDto ToDto(RiskArea r) => new()
    {
        Id            = r.Id,
        Name          = r.Name,
        RiskAreaTypeId = r.RiskAreaTypeId
    };
}