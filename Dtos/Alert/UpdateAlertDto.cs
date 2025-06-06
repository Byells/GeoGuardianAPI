using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Alert;

public class UpdateAlertDto
{
    [Required, Range(1,3)] public int RiskLevel   { get; set; }
    [Required] public int AlertTypeId { get; set; }
    
    [Required] public int AddressId { get; set; } 
    
    
}