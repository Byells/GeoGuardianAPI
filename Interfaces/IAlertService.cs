using GeoGuardian.Dtos.Alert;

namespace GeoGuardian.Interfaces
{
    public interface IAlertService
    {
        Task<IEnumerable<AlertDto>> GetAllAsync(int userId);
        Task<AlertDto?>             GetByIdAsync(int userId, int id);
        Task<AlertDto>              CreateAsync(int userId, CreateAlertDto dto);
        Task<bool>                  UpdateAsync(int userId, int id, UpdateAlertDto dto);
        Task<bool>                  DeleteAsync(int userId, int id);
    }
}