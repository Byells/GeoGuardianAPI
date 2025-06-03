using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.RiskArea
{
    public class UpdateRiskAreaDto
    {
        [Required] public string Name { get; set; } = null!;
        public int? RiskAreaTypeId { get; set; }
        public int? CityId { get; set; } // ← também pode mudar a cidade se quiser
    }
}