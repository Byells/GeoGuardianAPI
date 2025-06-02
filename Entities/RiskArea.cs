namespace GeoGuardian.Entities;

public class RiskArea
{
    public int Id              { get; set; }            
    public string Name         { get; set; } = null!;   
    public int? RiskAreaTypeId      { get; set; }            
    public int StreetId    { get; set; }         
    public Street? Street { get; set; }

    public ICollection<Sensor> Sensors { get; set; } = [];
    public ICollection<Alert>  Alerts  { get; set; } = [];
}