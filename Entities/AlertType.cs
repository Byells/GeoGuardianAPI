namespace GeoGuardian.Entities;

public class AlertType
{
    public int AlertTypeId { get; set; }
    public string Name     { get; set; } = null!;      // e.g. WARNING, CRITICAL
    public ICollection<Alert> Alerts { get; set; } = [];
}