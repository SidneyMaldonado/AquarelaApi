using AquarelaApi.DTOs;
using AquarelaApi.Mappings;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class ContaUseCase
{
    private readonly IContaRepository _repository;
    private readonly ContaMapper _mapper;

    public ContaUseCase(IContaRepository repository, ContaMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContaResponse>> GetAllAsync()
    {
        var contas = await _repository.GetAllAsync();
        return contas.Select(_mapper.ToDto);
    }

    public async Task<ContaResponse?> GetByIdAsync(int id)
    {
        var conta = await _repository.GetByIdAsync(id);
        return conta is null ? null : _mapper.ToDto(conta);
    }

    public async Task<IEnumerable<ContaResponse>> GetByUsuarioIdAsync(int idUsuario)
    {
        var contas = await _repository.GetByUsuarioIdAsync(idUsuario);
        return contas.Select(_mapper.ToDto);
    }

    public async Task<ContaResponse> CreateAsync(CreateContaRequest request)
    {
        var conta = _mapper.ToEntity(request);
        var created = await _repository.CreateAsync(conta);
        return _mapper.ToDto(created);
    }

    public async Task<ContaResponse> UpdateAsync(int id, UpdateContaRequest request)
    {
        if (id != request.IdConta)
            throw new ArgumentException("ID da rota não corresponde ao ID da conta no corpo da requisição.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new InvalidOperationException($"Conta com ID {id} não encontrada.");

        _mapper.UpdateEntity(existing, request);
        var updated = await _repository.UpdateAsync(existing);
        return _mapper.ToDto(updated);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
