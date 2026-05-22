using AquarelaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Divida> Dividas { get; set; }
    public DbSet<Analise> Analises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Conta>()
            .Property(c => c.NrSaldo)
            .HasColumnType("numeric(10,2)");

        modelBuilder.Entity<Divida>()
            .Property(d => d.NrValor)
            .HasColumnType("numeric(10,2)");

        modelBuilder.Entity<Analise>()
            .ToView("vw_analise")
            .Property(a => a.Jan).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Fev).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Mar).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Abr).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Mai).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Jun).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Jul).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Ago).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Set).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Out).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Nov).HasColumnType("numeric(10,2)");
        modelBuilder.Entity<Analise>().Property(a => a.Dez).HasColumnType("numeric(10,2)");
    }
}
