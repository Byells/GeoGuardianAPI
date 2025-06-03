namespace GeoGuardian.Entities;

public class Country
{
    public int CountryId { get; set; }
    public string Name   { get; set; } = null!;

    public ICollection<State> States { get; set; } = [];
    public ICollection<Address> Addresses { get; set; } = [];
}