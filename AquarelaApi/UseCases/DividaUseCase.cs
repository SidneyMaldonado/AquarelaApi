using AquarelaApi.DTOs;
using AquarelaApi.Mappings;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class DividaUseCase
{
    private readonly IDividaRepository _repository;
    private readonly DividaMapper _mapper;

    public DividaUseCase(IDividaRepository repository, DividaMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DividaResponse>> GetAllAsync()
    {
        var dividas = await _repository.GetAllAsync();
        return dividas.Select(_mapper.ToDto);
    }

    public async Task<DividaResponse?> GetByIdAsync(int id)
    {
        var divida = await _repository.GetByIdAsync(id);
        return divida is null ? null : _mapper.ToDto(divida);
    }

    public async Task<IEnumerable<DividaResponse>> GetByUsuarioIdAsync(int idUsuario)
    {
        var dividas = await _repository.GetByUsuarioIdAsync(idUsuario);
        return dividas.Select(_mapper.ToDto);
    }

    public async Task<DividaResponse> CreateAsync(CreateDividaRequest request)
    {
        var divida = _mapper.ToEntity(request);
        var created = await _repository.CreateAsync(divida);
        return _mapper.ToDto(created);
    }

    public async Task<DividaResponse> UpdateAsync(int id, UpdateDividaRequest request)
    {
        if (id != request.IdDivida)
            throw new ArgumentException("ID da rota não corresponde ao ID da dívida no corpo da requisição.");

        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new InvalidOperationException($"Dívida com ID {id} não encontrada.");

        _mapper.UpdateEntity(existing, request);
        var updated = await _repository.UpdateAsync(existing);
        return _mapper.ToDto(updated);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
