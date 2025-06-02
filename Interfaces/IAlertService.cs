using GeoGuardian.Dtos.Alert;

namespace GeoGuardian.Interfaces;

public interface IAlertService
{
    Task<IEnumerable<AlertDto>> GetAllAsync();
    Task<AlertDto?>            GetByIdAsync(int id);
    Task<AlertDto>             CreateAsync(CreateAlertDto dto);
    Task<bool>                 UpdateAsync(int id, UpdateAlertDto dto);
    Task<bool>                 DeleteAsync(int id);
}