using GeoGuardian.Entities;

public class Alert
{
    public int Id            { get; set; }
    public int RiskLevel     { get; set; }
    public DateTime Date     { get; set; }
    public int AlertTypeId   { get; set; }

    public int UserId        { get; set; }
    public User User         { get; set; } = null!;

    public int? RiskAreaId    { get; set; }
    public RiskArea? RiskArea { get; set; }

    public AlertType? AlertType { get; set; }

    public int AddressId     { get; set; }                     
    public Address Address   { get; set; } = null!;            

    public ICollection<UserAlert> UserAlerts { get; set; } = [];
}