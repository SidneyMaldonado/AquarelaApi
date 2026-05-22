using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Models;

[Keyless]
[Table("vw_analise")]
public class Analise
{
    [Column("nm_divida")]
    public string NmDivida { get; set; } = string.Empty;

    [Column("ano")]
    public int Ano { get; set; }

    [Column("dia")]
    public int Dia { get; set; }

    [Column("Jan")]
    public decimal Jan { get; set; }

    [Column("Fev")]
    public decimal Fev { get; set; }

    [Column("Mar")]
    public decimal Mar { get; set; }

    [Column("Abr")]
    public decimal Abr { get; set; }

    [Column("Mai")]
    public decimal Mai { get; set; }

    [Column("Jun")]
    public decimal Jun { get; set; }

    [Column("Jul")]
    public decimal Jul { get; set; }

    [Column("Ago")]
    public decimal Ago { get; set; }

    [Column("Set")]
    public decimal Set { get; set; }

    [Column("Out")]
    public decimal Out { get; set; }

    [Column("Nov")]
    public decimal Nov { get; set; }

    [Column("Dez")]
    public decimal Dez { get; set; }
}
