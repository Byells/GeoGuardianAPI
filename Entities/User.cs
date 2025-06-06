namespace GeoGuardian.Entities;

public class User
{
    public int    UserId     { get; set; }
    public string FullName   { get; set; } = null!;
    public string Email      { get; set; } = null!;
    public string Password   { get; set; } = null!; 
    public DateTime Created  { get; set; } = DateTime.UtcNow;
    public int    UserTypeId { get; set; }

    public UserType? UserType { get; set; }

    public ICollection<Address>   Addresses   { get; set; } = [];
    public ICollection<UserAlert> UserAlerts  { get; set; } = [];
    
    public ICollection<Alert> Alerts { get; set; } = [];

}