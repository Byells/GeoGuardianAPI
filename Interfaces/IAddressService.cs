using GeoGuardian.Dtos.Address;

namespace GeoGuardian.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<AddressDto>> GetAllAsync(int userId);
    Task<AddressDto?>             GetByIdAsync(int userId, int addressId);
    Task<AddressDto>              CreateAsync(int userId, CreateAddressDto dto);
    Task<bool>                    UpdateAsync(int userId, int addressId, UpdateAddressDto dto);
    Task<bool>                    DeleteAsync(int userId, int addressId);
}