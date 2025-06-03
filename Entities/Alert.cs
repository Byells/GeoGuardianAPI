namespace GeoGuardian.Entities;

public class Alert
{
    public int Id           { get; set; }               
    public int RiskLevel    { get; set; }               
    public DateTime Date    { get; set; }               
    public int AlertTypeId { get; set; }               
    public int RiskAreaId   { get; set; }               
    
    public RiskArea? RiskArea { get; set; }
    public AlertType? AlertType { get; set; } 

    public ICollection<UserAlert> UserAlerts { get; set; } = [];
}