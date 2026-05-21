using AquarelaApi.DTOs;
using AquarelaApi.Models;

namespace AquarelaApi.Mappings;

public class ContaMapper 
{
    public ContaResponse ToDto(Conta conta) 
    {
        return new ContaResponse(
            conta.IdConta,
            conta.IdUsuario,
            conta.NmConta,
            conta.NrSaldo,
            conta.Usuario?.NmUsuario
        );
    }

    public Conta ToEntity(CreateContaRequest request)
    {
        return new Conta
        {
            IdUsuario = request.IdUsuario,
            NmConta = request.NmConta,
            NrSaldo = request.NrSaldo
        };
    }

    public void UpdateEntity(Conta conta, UpdateContaRequest request)
    {
        conta.IdUsuario = request.IdUsuario;
        conta.NmConta = request.NmConta;
        conta.NrSaldo = request.NrSaldo;
    }
}
