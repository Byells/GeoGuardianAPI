namespace GeoGuardian.Dtos.User;

public class UserDto
{
    public int    UserId   { get; set; }
    public string FullName { get; set; } = null!;
    public string Email    { get; set; } = null!;
    public DateTime Created { get; set; }
    public int    UserTypeId { get; set; }
    
    public string UserType { get; set; } = string.Empty;

}