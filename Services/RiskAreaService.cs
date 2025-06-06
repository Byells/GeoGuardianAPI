using GeoGuardian.Data;
using GeoGuardian.Dtos.RiskArea;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services
{
    public class RiskAreaService : IRiskAreaService
    {
        private readonly GeoGuardianContext _context;

        public RiskAreaService(GeoGuardianContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RiskAreaDto>> GetAllAsync()
        {
            var list = await _context.RiskAreas
                                     .AsNoTracking()
                                     .ToListAsync();
            return list.Select(ToDto);
        }

        public async Task<RiskAreaDto?> GetByIdAsync(int id)
        {
            var entity = await _context.RiskAreas.FindAsync(id);
            return entity is null ? null : ToDto(entity);
        }

        public async Task<RiskAreaDto> CreateAsync(CreateRiskAreaDto dto)
        {
            var typeExists = await _context.RiskAreaTypes
                .Where(rt => rt.RiskAreaTypeId == dto.RiskAreaTypeId)
                .Select(rt => 1)
                .FirstOrDefaultAsync() == 1;

            var cityExists = await _context.Cities
                .Where(c => c.CityId == dto.CityId)
                .Select(c => 1)
                .FirstOrDefaultAsync() == 1;

            if (!typeExists || !cityExists)
                throw new ArgumentException("RiskAreaTypeId ou CityId inválido");

            var entity = new RiskArea
            {
                Name           = dto.Name,
                RiskAreaTypeId = dto.RiskAreaTypeId,
                CityId         = dto.CityId
            };

            _context.RiskAreas.Add(entity);
            await _context.SaveChangesAsync();

            await AtualizarAlertasParaCidade(dto.CityId);

            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateRiskAreaDto dto)
        {
            var entity = await _context.RiskAreas.FindAsync(id);
            if (entity is null)
                return false;

            entity.Name           = dto.Name;
            entity.RiskAreaTypeId = dto.RiskAreaTypeId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.RiskAreas.FindAsync(id);
            if (entity is null)
                return false;

            int cityId = entity.CityId;

            await AtualizarAlertasParaCidade(cityId, entity.Id);

            _context.RiskAreas.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task AtualizarAlertasParaCidade(int cityId, int? riskAreaId = null)
        {
            var alerts = await _context.Alerts
                .Include(a => a.Address)
                .Where(a => a.Address.CityId == cityId)
                .ToListAsync();

            if (riskAreaId != null)
            {
                foreach (var alert in alerts)
                {
                    if (alert.RiskAreaId == riskAreaId)
                        alert.RiskAreaId = null;
                }
            }
            else
            {
                var novaArea = await _context.RiskAreas.FirstOrDefaultAsync(r => r.CityId == cityId);

                foreach (var alert in alerts)
                    alert.RiskAreaId = novaArea?.Id;
            }

            await _context.SaveChangesAsync();
        }

        private static RiskAreaDto ToDto(RiskArea r) => new()
        {
            Id             = r.Id,
            Name           = r.Name,
            RiskAreaTypeId = r.RiskAreaTypeId
        };
    }
}
