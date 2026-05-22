namespace AquarelaApi.DTOs;

public record AnaliseResponse(
    string NmDivida,
    int Ano,
    int Dia,
    decimal Jan,
    decimal Fev,
    decimal Mar,
    decimal Abr,
    decimal Mai,
    decimal Jun,
    decimal Jul,
    decimal Ago,
    decimal Set,
    decimal Out,
    decimal Nov,
    decimal Dez
);
