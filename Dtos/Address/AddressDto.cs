namespace GeoGuardian.Dtos.Address;

public class AddressDto
{
    public int    AddressId  { get; set; }
    public string? Number     { get; set; }
    public string? Complement { get; set; }
    public decimal? Latitude  { get; set; }
    public decimal? Longitude { get; set; }

    public int StreetId { get; set; }
    public int UserId   { get; set; }
}