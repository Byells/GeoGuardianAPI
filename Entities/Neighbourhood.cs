namespace GeoGuardian.Entities;

public class Neighbourhood
{
    public int NeighbourhoodId { get; set; }
    public string Name         { get; set; } = null!;
    public int CityId          { get; set; }

    public City? City { get; set; }
    public ICollection<Street> Streets { get; set; } = [];
}