using AquarelaApi.Contexts;
using AquarelaApi.Models;
using AquarelaApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AquarelaApi.Repositories;

public class AnaliseRepository : IAnaliseRepository
{
    private readonly AppDbContext _context;

    public AnaliseRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Analise>> GetAllAsync()
        => await _context.Analises.ToListAsync();

    public async Task<IEnumerable<Analise>> GetByAnoAsync(int ano)
        => await _context.Analises.Where(a => a.Ano == ano).ToListAsync();
}
