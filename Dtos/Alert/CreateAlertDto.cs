using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Alert;

public class CreateAlertDto
{
    [Required] public int RiskLevel   { get; set; }
    [Required] public int AlertTypeId { get; set; }
    [Required] public int RiskAreaId  { get; set; }
    public DateTime? Date { get; set; }   
}