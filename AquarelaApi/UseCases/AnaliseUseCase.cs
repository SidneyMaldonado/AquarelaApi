using AquarelaApi.DTOs;
using AquarelaApi.Mappings;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class AnaliseUseCase
{
    private readonly IAnaliseRepository _repository;
    private readonly AnaliseMapper _mapper;

    public AnaliseUseCase(IAnaliseRepository repository, AnaliseMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnaliseResponse>> GetAllAsync()
    {
        var analises = await _repository.GetAllAsync();
        return analises.Select(_mapper.ToDto);
    }

    public async Task<IEnumerable<AnaliseResponse>> GetByAnoAsync(int ano)
    {
        var analises = await _repository.GetByAnoAsync(ano);
        return analises.Select(_mapper.ToDto);
    }
}
