namespace GeoGuardian.Entities;

public class City
{
    public int CityId  { get; set; }
    public string Name { get; set; } = null!;
    public int StateId { get; set; }

    public State? State { get; set; }
    public ICollection<Neighbourhood> Neighbourhoods { get; set; } = [];
    public ICollection<RiskArea> RiskAreas { get; set; } = [];
}