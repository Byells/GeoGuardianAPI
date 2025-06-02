namespace GeoGuardian.Interfaces;

using GeoGuardian.Dtos.RiskArea;

public interface IRiskAreaService
{
    Task<IEnumerable<RiskAreaDto>> GetAllAsync();
    Task<RiskAreaDto?> GetByIdAsync(int id);
    Task<RiskAreaDto> CreateAsync(CreateRiskAreaDto dto);
    Task<bool> UpdateAsync(int id, UpdateRiskAreaDto dto);
    Task<bool> DeleteAsync(int id);
}
