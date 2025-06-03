using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Sensor
{
    /// <summary>
    /// Dados enviados para atualizar um Sensor existente.
    /// </summary>
    public class UpdateSensorDto
    {
        [Required]
        public string Status { get; set; } = null!;

        public int? SensorModelId { get; set; }

    }
}