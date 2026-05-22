using AquarelaApi.Models;

namespace AquarelaApi.Repositories.Interfaces;

public interface IAnaliseRepository
{
    Task<IEnumerable<Analise>> GetAllAsync();
    Task<IEnumerable<Analise>> GetByAnoAsync(int ano);
}
