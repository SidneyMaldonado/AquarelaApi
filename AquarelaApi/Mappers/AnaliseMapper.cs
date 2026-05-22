using AquarelaApi.DTOs;
using AquarelaApi.Models;

namespace AquarelaApi.Mappings;

public class AnaliseMapper
{
    public AnaliseResponse ToDto(Analise analise)
    {
        return new AnaliseResponse(
            analise.NmDivida,
            analise.Ano,
            analise.Dia,
            analise.Jan,
            analise.Fev,
            analise.Mar,
            analise.Abr,
            analise.Mai,
            analise.Jun,
            analise.Jul,
            analise.Ago,
            analise.Set,
            analise.Out,
            analise.Nov,
            analise.Dez
        );
    }
}
