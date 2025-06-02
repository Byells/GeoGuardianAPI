namespace GeoGuardian.Dtos.Sensor
{
    /// <summary>
    /// Dados que serão retornados nas operações de Sensor.
    /// </summary>
    public class SensorDto
    {
        /// <summary>
        /// ID interno (chave primária) do Sensor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// UUID único gerado no backend.
        /// </summary>
        public string Uuid { get; set; } = null!;

        /// <summary>
        /// Status atual do sensor (ex.: “ACTIVE”, “INACTIVE”).
        /// </summary>
        public string Status { get; set; } = null!;

        /// <summary>
        /// ID da RiskArea a que este sensor pertence.
        /// </summary>
        public int RiskAreaId { get; set; }

        /// <summary>
        /// ID do SensorModel (modelo de hardware) deste sensor.
        /// </summary>
        public int SensorModelId { get; set; }
    }
}