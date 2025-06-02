namespace GeoGuardian.Dtos.Alert;

public class AlertDto
{
    public int      Id           { get; set; }
    public int      RiskLevel    { get; set; }
    public DateTime Date         { get; set; }
    public int      AlertTypeId  { get; set; }
    public int      RiskAreaId   { get; set; }
}