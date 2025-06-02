using System.ComponentModel.DataAnnotations;

namespace GeoGuardian.Dtos.RiskArea;

/// <summary>
/// Dados enviados para atualizar uma Área de Risco existente.
/// </summary>
public class UpdateRiskAreaDto
{
    /// <summary>
    /// Novo nome da Área de Risco (ex.: “Serra Alta”).
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// ID de um possível novo Tipo de Área de Risco (opcional).
    /// </summary>
    public int? RiskAreaTypeId { get; set; }
}