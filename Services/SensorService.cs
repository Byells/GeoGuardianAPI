using GeoGuardian.Data;
using GeoGuardian.Dtos.Sensor;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services;

public class SensorService : ISensorService
{
    private readonly GeoGuardianContext _ctx;

    public SensorService(GeoGuardianContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<SensorDto>> GetAllAsync()
    {
        var list = await _ctx.Sensors.AsNoTracking().ToListAsync();
        return list.Select(ToDto);
    }

    public async Task<SensorDto?> GetByIdAsync(int id)
    {
        var s = await _ctx.Sensors.FindAsync(id);
        return s is null ? null : ToDto(s);
    }

    public async Task<SensorDto> CreateAsync(CreateSensorDto dto)
    {
        bool areaExists = await _ctx.RiskAreas.AnyAsync(r => r.Id == dto.RiskAreaId);
        bool modelExists = await _ctx.SensorModels.AnyAsync(m => m.SensorModelId == dto.SensorModelId);
        if (!areaExists || !modelExists)
            throw new ArgumentException("RiskAreaId ou SensorModelId inválido");

        var newUuid = Guid.NewGuid().ToString();

        var entity = new Sensor
        {
            Uuid          = newUuid,
            Status        = dto.Status ?? "ACTIVE",
            RiskAreaId    = dto.RiskAreaId,
            SensorModelId = dto.SensorModelId
        };

        _ctx.Sensors.Add(entity);
        await _ctx.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSensorDto dto)
    {
        var entity = await _ctx.Sensors.FindAsync(id);
        if (entity is null) return false;

        entity.Status        = dto.Status;
        entity.SensorModelId = dto.SensorModelId;

        await _ctx.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _ctx.Sensors.FindAsync(id);
        if (entity is null) return false;

        _ctx.Sensors.Remove(entity);
        await _ctx.SaveChangesAsync();
        return true;
    }

    private static SensorDto ToDto(Sensor s) => new()
    {
        Id            = s.Id,
        Uuid          = s.Uuid,
        Status        = s.Status,
        RiskAreaId    = s.RiskAreaId,
        SensorModelId = s.SensorModelId
    };
}