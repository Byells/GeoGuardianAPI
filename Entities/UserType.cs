namespace GeoGuardian.Entities;

public class UserType
{
    public int UserTypeId { get; set; }
    public string Name    { get; set; } = null!;  
    public ICollection<User> Users { get; set; } = [];
}
