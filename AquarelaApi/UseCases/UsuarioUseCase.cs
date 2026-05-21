using AquarelaApi.DTOs;
using AquarelaApi.Mappings;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;

namespace AquarelaApi.UseCases;

public class UsuarioUseCase
{
    private readonly IUsuarioRepository _repository;
    private readonly UsuarioMapper _mapper;

    public UsuarioUseCase(IUsuarioRepository repository, UsuarioMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UsuarioResponse>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return usuarios.Select(_mapper.ToDto);
    }

    public async Task<UsuarioResponse?> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        return usuario is null ? null : _mapper.ToDto(usuario);
    }

    public async Task<UsuarioResponse> CreateAsync(CreateUsuarioRequest request)
    {
        var usuario = _mapper.ToEntity(request);
        var created = await _repository.CreateAsync(usuario);
        return _mapper.ToDto(created);
    }

    public async Task<UsuarioResponse> UpdateAsync(int id, UpdateUsuarioRequest request)
    {
        // Buscar usuário existente para manter a senha
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            throw new InvalidOperationException($"Usuário com ID {id} não encontrado.");

        // Mapear apenas os campos que podem ser atualizados
        _mapper.UpdateEntity(existing, request);

        var updated = await _repository.UpdateAsync(existing);
        return _mapper.ToDto(updated);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
