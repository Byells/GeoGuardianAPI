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
    public int CityId { get; set; } 

    /// <summary>
    /// Nome da Cidade à qual a Área de Risco pertence.
    /// </summary>
    public string? CityName { get; set; }

    /// <summary>
    /// Nome do Estado ao qual a Área de Risco pertence.
    /// </summary>
    public string? StateName { get; set; } 

    /// <summary>
    /// Nome do País ao qual a Área de Risco pertence.
    /// </summary>
    public string? CountryName { get; set; } 
}