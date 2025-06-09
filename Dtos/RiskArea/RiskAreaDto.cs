namespace GeoGuardian.Dtos.RiskArea;

/// <summary>
/// Objeto retornado ao consultar uma Área de Risco.
/// </summary>
public class RiskAreaDto
{
    /// <summary>
    /// ID da Área de Risco.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome da Área de Risco.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// ID do tipo de Área de Risco (pode ser nulo se não informado).
    /// </summary>
    public int? RiskAreaTypeId { get; set; }

    /// <summary>
    /// ID da Cidade à qual a Área de Risco pertence.
    /// </summary>
    public int CityId { get; set; } // Adicionada ou confirmada no DTO de saída

    /// <summary>
    /// Nome da Cidade à qual a Área de Risco pertence.
    /// </summary>
    public string? CityName { get; set; } // NOVA PROPRIEDADE PARA O NOME DA CIDADE
}