using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.Sensor
{
    /// <summary>
    /// Dados enviados para criar um novo Sensor.
    /// O UUID será gerado automaticamente no backend.
    /// </summary>
    public class CreateSensorDto
    {
        /// <summary>
        /// ID da RiskArea à qual este sensor pertence.
        /// </summary>
        [Required]
        public int RiskAreaId { get; set; }

        /// <summary>
        /// ID do SensorModel (modelo de hardware) associado.
        /// </summary>
        [Required]
        public int SensorModelId { get; set; }

        /// <summary>
        /// Status inicial do sensor (ex.: “ACTIVE”, “INACTIVE”). Opcional:
        /// se não for enviado, será considerado “ACTIVE” por padrão.
        /// </summary>
        public string? Status { get; set; } = "ACTIVE";
    }
}