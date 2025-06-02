
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

    public async Task<IEnumerable<AlertDto>> GetAllAsync()
        => (await _ctx.Alerts.AsNoTracking().ToListAsync()).Select(ToDto);

    public async Task<AlertDto?> GetByIdAsync(int id)
        => ToDto(await _ctx.Alerts.FindAsync(id));

    public async Task<AlertDto> CreateAsync(CreateAlertDto dto)
    {
        bool areaOk = await _ctx.RiskAreas.AnyAsync(r => r.Id == dto.RiskAreaId);
        bool typeOk = await _ctx.Set<AlertType>()
                                .AnyAsync(t => t.AlertTypeId == dto.AlertTypeId);
        if (!areaOk || !typeOk) throw new ArgumentException("Invalid IDs");

        var entity = new Alert
        {
            RiskLevel   = dto.RiskLevel,
            Date        = dto.Date ?? DateTime.UtcNow,
            AlertTypeId = dto.AlertTypeId,   
            RiskAreaId  = dto.RiskAreaId
        };
        _ctx.Alerts.Add(entity);
        await _ctx.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateAlertDto dto)
    {
        var entity = await _ctx.Alerts.FindAsync(id);
        if (entity is null) return false;

        entity.RiskLevel   = dto.RiskLevel;
        entity.AlertTypeId = dto.AlertTypeId;   
        await _ctx.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.Alerts.FindAsync(id);
        if (entity is null) return false;

        _ctx.Alerts.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }

    private static AlertDto ToDto(Alert? a) => a is null ? null! : new AlertDto
    {
        Id          = a.Id,
        RiskLevel   = a.RiskLevel,
        Date        = a.Date,
        AlertTypeId = a.AlertTypeId,   
        RiskAreaId  = a.RiskAreaId
    };
}
