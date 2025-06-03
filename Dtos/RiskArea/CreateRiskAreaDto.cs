using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.RiskArea
{
    public class CreateRiskAreaDto
    {
        [Required] public string Name { get; set; } = null!;
        [Required] public int RiskAreaTypeId { get; set; }
        [Required] public int CityId { get; set; }  
    }
}