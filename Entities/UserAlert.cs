namespace GeoGuardian.Entities;

public class UserAlert
{
    public int UserId    { get; set; }
    public int AlertId   { get; set; }

    public string SendMode            { get; set; } = "PUSH";
    public bool   ConfirmedReception  { get; set; } = false;  

    public User?  User  { get; set; }
    public Alert? Alert { get; set; }
}