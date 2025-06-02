namespace GeoGuardian.Entities;

public class Street
{
    public int StreetId         { get; set; }
    public string Name          { get; set; } = null!;
    public int NeighbourhoodId  { get; set; }

    public Neighbourhood? Neighbourhood { get; set; }
    public ICollection<Address> Addresses { get; set; } = [];
    public ICollection<RiskArea> RiskAreas { get; set; } = [];
}