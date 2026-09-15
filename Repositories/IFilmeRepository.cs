using CatalogoFilmesApi.Entities;

namespace CatalogoFilmesApi.Repositories
{
    public interface IFilmeRepository
    {
        Task<IEnumerable<Filme>> GetAllAsync();
        Task<Filme?> GetByIdAsync(int id);
        Task<Filme> AddAsync(Filme filme);
        Task UpdateAsync(Filme filme);
        Task DeleteAsync(int id);
    }
}