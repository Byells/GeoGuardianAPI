using System.ComponentModel.DataAnnotations;

public class UpdateAddressDto
{
    [Required] public string Neighborhood { get; set; } = null!;
    [Required] public string StreetName   { get; set; } = null!;
    [Required] public string Number       { get; set; } = null!;

    public string? Complement             { get; set; }
    public decimal? Latitude              { get; set; }
    public decimal? Longitude             { get; set; }
}