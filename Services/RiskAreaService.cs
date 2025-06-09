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
                                     .Include(ra => ra.City) // <-- ADICIONADO: Carrega a cidade
                                     .AsNoTracking()
                                     .ToListAsync();
            return list.Select(ToDto);
        }

        public async Task<RiskAreaDto?> GetByIdAsync(int id)
        {
            // ALTERADO: Usar FirstOrDefaultAsync com Include em vez de FindAsync
            // FindAsync não carrega relações (.Include)
            var entity = await _context.RiskAreas
                                     .Include(ra => ra.City) // <-- ADICIONADO: Carrega a cidade
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

            // <-- ADICIONADO: Recarregar a entidade com a cidade para o DTO de retorno
            var createdEntityWithCity = await _context.RiskAreas
                                     .Include(ra => ra.City)
                                     .FirstOrDefaultAsync(ra => ra.Id == entity.Id);

            return ToDto(createdEntityWithCity); // <-- Usar a entidade recarregada
        }

        public async Task<bool> UpdateAsync(int id, UpdateRiskAreaDto dto)
        {
            var entity = await _context.RiskAreas.FindAsync(id);
            if (entity is null)
                return false;

            // Supondo que você queira que estas sejam atualizáveis.
            // Se o DTO.RiskAreaTypeId/CityId for nulo, mantem o valor atual.
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

            // Este método não precisa ser alterado para o nome da cidade no DTO
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

        // ALTERADO: Incluindo CityId e CityName no DTO
        private static RiskAreaDto ToDto(RiskArea r) => new()
        {
            Id             = r.Id,
            Name           = r.Name,
            RiskAreaTypeId = r.RiskAreaTypeId,
            CityId         = r.CityId, // <-- ADICIONADO: Mapeando o CityId
            CityName       = r.City?.Name // <-- ADICIONADO: Mapeando o nome da cidade (usa ?. para segurança)
        };
    }
}