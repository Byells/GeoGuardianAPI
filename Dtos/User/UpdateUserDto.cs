using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.User;

public class UpdateUserDto
{
    [Required] public string FullName { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    [Range(1, int.MaxValue)]
    public int? UserTypeId { get; set; }
}