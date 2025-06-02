using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Sensor
{
    /// <summary>
    /// Dados enviados para atualizar um Sensor existente.
    /// </summary>
    public class UpdateSensorDto
    {
        /// <summary>
        /// Novo status do sensor (ex.: “ACTIVE”, “INACTIVE”).
        /// </summary>
        [Required]
        public string Status { get; set; } = null!;

        /// <summary>
        /// ID de um novo SensorModel (opcional, caso queira trocar o modelo de hardware).
        /// </summary>
        [Required]
        public int SensorModelId { get; set; }
    }
}