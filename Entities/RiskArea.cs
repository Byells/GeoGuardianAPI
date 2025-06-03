namespace GeoGuardian.Entities;

public class RiskArea
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? RiskAreaTypeId { get; set; }

    public int CityId { get; set; } 

    public RiskAreaType? RiskAreaType { get; set; }
    public City? City { get; set; } 

    public ICollection<Sensor> Sensors { get; set; } = [];
    public ICollection<Alert> Alerts  { get; set; } = [];
}