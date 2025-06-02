using GeoGuardian.Dtos.Sensor;

namespace GeoGuardian.Interfaces;

public interface ISensorService
{
    Task<IEnumerable<SensorDto>> GetAllAsync();
    Task<SensorDto?>            GetByIdAsync(int id);
    Task<SensorDto>             CreateAsync(CreateSensorDto dto);
    Task<bool>                  UpdateAsync(int id, UpdateSensorDto dto);
    Task<bool>                  DeleteAsync(int id);
}