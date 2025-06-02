using GeoGuardian.Dtos.RiskArea;
using GeoGuardian.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuardian.Controllers;

/// <summary>
/// Controller para gerenciamento de Áreas de Risco.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RiskAreaController : ControllerBase
{
    private readonly IRiskAreaService _service;

    public RiskAreaController(IRiskAreaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retorna todas as Áreas de Risco existentes.
    /// </summary>
    /// <returns>Lista de <see cref="RiskAreaDto"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RiskAreaDto>>> Get()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    /// <summary>
    /// Retorna uma única Área de Risco pelo seu ID.
    /// </summary>
    /// <param name="id">ID da Área de Risco a ser buscada.</param>
    /// <returns>
    /// Objeto <see cref="RiskAreaDto"/> com os dados da área de risco,
    /// ou 404 Not Found caso não exista.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RiskAreaDto>> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Cria uma nova Área de Risco.
    /// </summary>
    /// <param name="dto">Dados para criação de uma <see cref="CreateRiskAreaDto"/>.</param>
    /// <returns>
    /// O objeto criado (<see cref="RiskAreaDto"/>) com ID atribuído,
    /// ou 400 BadRequest se os dados forem inválidos.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<RiskAreaDto>> Post([FromBody] CreateRiskAreaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    /// <summary>
    /// Atualiza uma Área de Risco existente.
    /// </summary>
    /// <param name="id">ID da Área de Risco a ser atualizada.</param>
    /// <param name="dto">Dados para atualização (<see cref="UpdateRiskAreaDto"/>).</param>
    /// <returns>
    /// 204 No Content se atualizado com sucesso,
    /// 400 BadRequest se ModelState inválido,
    /// 404 Not Found se a área não existir.
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateRiskAreaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Remove uma Área de Risco pelo seu ID.
    /// </summary>
    /// <param name="id">ID da Área de Risco a ser removida.</param>
    /// <returns>
    /// 204 No Content se removida com sucesso,
    /// ou 404 Not Found se não existir.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
