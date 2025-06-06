using GeoGuardian.Data;
using GeoGuardian.Dtos.Alert;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services;

public class AlertService : IAlertService
{
    private readonly GeoGuardianContext _ctx;
    public AlertService(GeoGuardianContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<AlertDto>> GetAllAsync(int userId)
    {
        var list = await _ctx.Alerts
            .Where(a => a.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        return list.Select(ToDto);
    }

    public async Task<AlertDto?> GetByIdAsync(int userId, int id)
    {
        var alert = await _ctx.Alerts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        return alert is null ? null : ToDto(alert);
    }

    public async Task<AlertDto> CreateAsync(int userId, CreateAlertDto dto)
    {
        var address = await _ctx.Addresses
            .Include(a => a.City)
            .FirstOrDefaultAsync(a => a.AddressId == dto.AddressId && a.UserId == userId);

        if (address is null)
            throw new ArgumentException("Endereço inválido ou não pertence ao usuário.");

        var riskArea = await _ctx.RiskAreas
            .FirstOrDefaultAsync(r => r.CityId == address.CityId);
        

        var entity = new Alert
        {
            RiskLevel   = dto.RiskLevel,
            Date        = dto.Date ?? DateTime.UtcNow,
            AlertTypeId = dto.AlertTypeId,
            AddressId   = dto.AddressId,
            UserId      = userId,
            RiskAreaId  = riskArea?.Id
        };


        _ctx.Alerts.Add(entity);
        await _ctx.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int userId, int id, UpdateAlertDto dto)
    {
        var alert = await _ctx.Alerts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        if (alert is null) return false;

        var addressValid = await _ctx.Addresses
            .AnyAsync(a => a.AddressId == dto.AddressId && a.UserId == userId);

        if (!addressValid)
            throw new ArgumentException("Endereço inválido para este usuário.");

        alert.RiskLevel   = dto.RiskLevel;
        alert.AlertTypeId = dto.AlertTypeId;
        alert.AddressId   = dto.AddressId;

        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int userId, int id)
    {
        var alert = await _ctx.Alerts
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (alert is null) return false;

        _ctx.Alerts.Remove(alert);
        await _ctx.SaveChangesAsync();
        return true;
    }

    private static AlertDto ToDto(Alert a) => new()
    {
        Id          = a.Id,
        RiskLevel   = a.RiskLevel,
        Date        = a.Date,
        AlertTypeId = a.AlertTypeId,
        RiskAreaId  = a.RiskAreaId,
        AddressId   = a.AddressId
    };
}
