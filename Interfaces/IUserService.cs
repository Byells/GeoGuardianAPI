using GeoGuardian.Dtos.User;

namespace GeoGuardian.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?>            GetByIdAsync(int id);
    Task<UserDto>             CreateAsync(CreateUserDto dto);
    Task<bool>                UpdateAsync(int id, UpdateUserDto dto);
    Task<bool>                DeleteAsync(int id);
}