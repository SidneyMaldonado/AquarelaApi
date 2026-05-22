using AquarelaApi.DTOs;
using AquarelaApi.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AnalisesController : ControllerBase
{
    private readonly AnaliseUseCase _useCase;

    public AnalisesController(AnaliseUseCase useCase) => _useCase = useCase;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _useCase.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("ano/{ano:int}")]
    public async Task<IActionResult> GetByAno(int ano)
    {
        var response = await _useCase.GetByAnoAsync(ano);
        return Ok(response);
    }
}
