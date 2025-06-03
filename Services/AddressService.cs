using GeoGuardian.Data;
using GeoGuardian.Dtos.Address;
using GeoGuardian.Entities;
using GeoGuardian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuardian.Services
{
    public class AddressService : IAddressService
    {
        private readonly GeoGuardianContext _ctx;
        public AddressService(GeoGuardianContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<AddressDto>> GetAllAsync(int userId)
        {
            var list = await _ctx.Addresses
                                 .Where(a => a.UserId == userId)
                                 .AsNoTracking()
                                 .ToListAsync();

            return list.Select(ToDto);
        }

        public async Task<AddressDto?> GetByIdAsync(int userId, int addressId)
        {
            var addr = await _ctx.Addresses
                                 .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
            return addr is null ? null : ToDto(addr);
        }

        public async Task<AddressDto> CreateAsync(int userId, CreateAddressDto dto)
        {
            // (Opcional) Validar se existe o country, state e city
            bool countryExists = await _ctx.Countries
                .Where(c => c.CountryId == dto.CountryId)
                .Select(c => 1)
                .FirstOrDefaultAsync() != 0;

            bool stateExists = await _ctx.States
                .Where(s => s.StateId == dto.StateId && s.CountryId == dto.CountryId)
                .Select(s => 1)
                .FirstOrDefaultAsync() != 0;

            bool cityExists = await _ctx.Cities
                .Where(c => c.CityId == dto.CityId && c.StateId == dto.StateId)
                .Select(c => 1)
                .FirstOrDefaultAsync() != 0;


            if (!countryExists || !stateExists || !cityExists)
                throw new ArgumentException("CountryId, StateId ou CityId inválido.");

            var entity = new Address
            {
                UserId       = userId,
                CountryId    = dto.CountryId,
                StateId      = dto.StateId,
                CityId       = dto.CityId,
                Neighborhood = dto.Neighborhood,
                StreetName   = dto.StreetName,
                Complement   = dto.Complement,
                Latitude     = dto.Latitude,
                Longitude    = dto.Longitude,
                Number       = dto.Number
            };

            _ctx.Addresses.Add(entity);
            await _ctx.SaveChangesAsync();
            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(int userId, int addressId, UpdateAddressDto dto)
        {
            var entity = await _ctx.Addresses
                                   .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
            if (entity is null) 
                return false;

            entity.Neighborhood = dto.Neighborhood;
            entity.StreetName   = dto.StreetName;
            entity.Complement   = dto.Complement;
            entity.Latitude     = dto.Latitude;
            entity.Longitude    = dto.Longitude;

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int userId, int addressId)
        {
            var entity = await _ctx.Addresses
                                   .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressId == addressId);
            if (entity is null) 
                return false;

            _ctx.Addresses.Remove(entity);
            await _ctx.SaveChangesAsync();
            return true;
        }

        private static AddressDto ToDto(Address a) => new()
        {
            AddressId    = a.AddressId,
            UserId       = a.UserId,
            CountryId    = a.CountryId,
            StateId      = a.StateId,
            CityId       = a.CityId,
            Neighborhood = a.Neighborhood,
            StreetName   = a.StreetName,
            Complement   = a.Complement,
            Latitude     = a.Latitude,
            Longitude    = a.Longitude,
            Number       = a.Number
        };
    }
}
