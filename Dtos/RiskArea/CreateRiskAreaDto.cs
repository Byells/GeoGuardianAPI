using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.RiskArea;

/// <summary>
/// Dados enviados para criar uma nova Área de Risco.
/// </summary>
public class CreateRiskAreaDto
{
    /// <summary>
    /// Nome da Área de Risco (ex.: “Morro Azul”).
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Tipo de Área de Risco (1 = FLOOD, 2 = LANDSLIDE, 3 = DAM_BREAK).
    /// </summary>
    [Required]
    public int RiskAreaTypeId { get; set; }

    /// <summary>
    /// ID da rua (Street) associada a esta Área de Risco.
    /// </summary>
    [Required]
    public int StreetId { get; set; }
}