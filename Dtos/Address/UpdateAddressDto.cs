using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Address;

public class UpdateAddressDto
{
    [Required] public string Number { get; set; } = null!;
    public string? Complement { get; set; }
    public decimal? Latitude  { get; set; }
    public decimal? Longitude { get; set; }
}