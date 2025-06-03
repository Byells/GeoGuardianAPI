using System.ComponentModel.DataAnnotations;

public class CreateAddressDto
{
    [Required] public int CountryId { get; set; }
    [Required] public int StateId { get; set; }
    [Required] public int CityId { get; set; }

    [Required] public string Neighborhood { get; set; } = null!;
    [Required] public string StreetName   { get; set; } = null!;
    [Required] public string Number       { get; set; } = null!;

    public string? Complement             { get; set; }
    public decimal? Latitude              { get; set; }
    public decimal? Longitude             { get; set; }
}