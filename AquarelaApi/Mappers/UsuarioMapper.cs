using AquarelaApi.DTOs;
using AquarelaApi.Models;

namespace AquarelaApi.Mappings;

public class UsuarioMapper
{
    public UsuarioResponse ToDto(Usuario usuario)
    {
        return new UsuarioResponse(
            usuario.IdUsuario,
            usuario.NmUsuario,
            usuario.DsEmail,
            usuario.DmAtivo
        );
    }

    public Usuario ToEntity(CreateUsuarioRequest request)
    {
        return new Usuario
        {
            NmUsuario = request.NmUsuario,
            DsEmail = request.DsEmail,
            DsSenha = request.DsSenha,
            DmAtivo = request.DmAtivo
        };
    }

    public void UpdateEntity(Usuario usuario, UpdateUsuarioRequest request)
    {
        usuario.NmUsuario = request.NmUsuario;
        usuario.DsEmail = request.DsEmail;
        usuario.DmAtivo = request.DmAtivo;
    }
}
