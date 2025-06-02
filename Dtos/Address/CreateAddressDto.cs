using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Address;

public class CreateAddressDto
{
    [Required] public int StreetId { get; set; }
    [Required] public string Number { get; set; } = null!;
    public string? Complement { get; set; }
    public decimal? Latitude  { get; set; }
    public decimal? Longitude { get; set; }
}