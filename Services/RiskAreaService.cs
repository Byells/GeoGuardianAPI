using GeoGuardian.Data;
using GeoGuardian.Dtos.RiskArea;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Garante que o .Select esteja disponível para LINQ

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
                                     .Include(ra => ra.City)
                                         .ThenInclude(c => c.State) 
                                             .ThenInclude(s => s.Country) 
                                     .AsNoTracking()
                                     .ToListAsync();
            return list.Select(ToDto);
        }

        public async Task<RiskAreaDto?> GetByIdAsync(int id)
        {
            var entity = await _context.RiskAreas
                                     .Include(ra => ra.City)
                                         .ThenInclude(c => c.State) 
                                             .ThenInclude(s => s.Country) 
                                     .FirstOrDefaultAsync(ra => ra.Id == id);
            
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

            var createdEntityWithRelations = await _context.RiskAreas
                                     .Include(ra => ra.City)
                                         .ThenInclude(c => c.State) 
                                             .ThenInclude(s => s.Country) 
                                     .FirstOrDefaultAsync(ra => ra.Id == entity.Id);

            return ToDto(createdEntityWithRelations); 
        }

        public async Task<bool> UpdateAsync(int id, UpdateRiskAreaDto dto)
        {
            var entity = await _context.RiskAreas.FindAsync(id);
            if (entity is null)
                return false;

            entity.Name           = dto.Name;
            entity.RiskAreaTypeId = dto.RiskAreaTypeId ?? entity.RiskAreaTypeId;
            entity.CityId         = dto.CityId ?? entity.CityId; 

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

        // Método ToDto atualizado com todos os IDs e Nomes
        private static RiskAreaDto ToDto(RiskArea r) => new()
        {
            Id             = r.Id,
            Name           = r.Name,
            RiskAreaTypeId = r.RiskAreaTypeId,
            CityId         = r.CityId, 
            CityName       = r.City?.Name, 
            StateId        = r.City?.StateId, // Mapeando o ID do Estado
            StateName      = r.City?.State?.Name,    
            CountryId      = r.City?.State?.CountryId, // Mapeando o ID do País
            CountryName    = r.City?.State?.Country?.Name 
        };
    }
}