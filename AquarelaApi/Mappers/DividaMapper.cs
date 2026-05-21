using AquarelaApi.DTOs;
using AquarelaApi.Models;

namespace AquarelaApi.Mappings;

public class DividaMapper
{
    public DividaResponse ToDto(Divida divida)
    {
        return new DividaResponse(
            divida.IdDivida,
            divida.IdUsuario,
            divida.NmDivida,
            divida.DiaVencimento,
            divida.DtPrimeiroVencimento,
            divida.NrParcelas,
            divida.NrValor,
            divida.Usuario?.NmUsuario
        );
    }

    public Divida ToEntity(CreateDividaRequest request)
    {
        return new Divida
        {
            IdUsuario = request.IdUsuario,
            NmDivida = request.NmDivida,
            DiaVencimento = request.DiaVencimento,
            DtPrimeiroVencimento = request.DtPrimeiroVencimento,
            NrParcelas = request.NrParcelas,
            NrValor = request.NrValor
        };
    }

    public void UpdateEntity(Divida divida, UpdateDividaRequest request)
    {
        divida.IdUsuario = request.IdUsuario;
        divida.NmDivida = request.NmDivida;
        divida.DiaVencimento = request.DiaVencimento;
        divida.DtPrimeiroVencimento = request.DtPrimeiroVencimento;
        divida.NrParcelas = request.NrParcelas;
        divida.NrValor = request.NrValor;
    }
}
