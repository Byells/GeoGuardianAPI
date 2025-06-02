namespace GeoGuardian.Entities;

public class Sensor
{
    public int Id           { get; set; }               
    public string Uuid      { get; set; } = null!;      
    public string Status    { get; set; } = "ACTIVE";   
    public int RiskAreaId   { get; set; }               
    public int SensorModelId     { get; set; }               

    public RiskArea? RiskArea { get; set; }
    public ICollection<SensorReading> Readings { get; set; } = [];  
}