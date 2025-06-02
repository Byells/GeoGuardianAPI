namespace GeoGuardian.Entities;

public class Address
{
    public int     AddressId   { get; set; }
    public int     UserId      { get; set; }
    public int     StreetId    { get; set; }
    public string? Number      { get; set; }
    public string? Complement  { get; set; }
    public decimal? Latitude   { get; set; }   
    public decimal? Longitude  { get; set; }

    public User?   User   { get; set; }
    public Street? Street { get; set; }
}