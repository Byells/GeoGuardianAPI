using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.User;

public class CreateUserDto
{
    [Required] public string FullName { get; set; } = null!;

    [Required, EmailAddress] public string Email { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;

    [Range(1, int.MaxValue)] public int UserTypeId { get; set; } = 2;
}