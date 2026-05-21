using AquarelaApi.DTOs;
using AquarelaApi.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContasController : ControllerBase
{
    private readonly ContaUseCase _useCase;

    public ContasController(ContaUseCase useCase) => _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _useCase.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _useCase.GetByIdAsync(id);
        if (response is null) return NotFound();
        return Ok(response);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<IActionResult> GetByUsuario(int idUsuario)
    {
        var response = await _useCase.GetByUsuarioIdAsync(idUsuario);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContaRequest request)
    {
        var response = await _useCase.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.IdConta }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContaRequest request)
    {
        if (id != request.IdConta) return BadRequest("ID da rota não corresponde ao ID da conta no corpo da requisição.");

        try
        {
            var response = await _useCase.UpdateAsync(id, request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _useCase.DeleteAsync(id);
        return NoContent();
    }
}