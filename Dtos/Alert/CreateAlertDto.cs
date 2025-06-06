using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Alert;

public class CreateAlertDto
{
    [Required] public int RiskLevel   { get; set; }
    [Required] public int AlertTypeId { get; set; }
    
    
    [Required] public int AddressId    { get; set; } 
    public DateTime? Date { get; set; }   
}