using Microsoft.EntityFrameworkCore;
using CatalogoFilmesApi.Data;
using CatalogoFilmesApi.Entities;

namespace CatalogoFilmesApi.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly AppDbContext _context;

        public FilmeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Filme>> GetAllAsync()
        {
            
            return await _context.Filmes.Include(f => f.Diretor).ToListAsync();
        }

        public async Task<Filme?> GetByIdAsync(int id)
        {
            return await _context.Filmes.Include(f => f.Diretor).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Filme> AddAsync(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task UpdateAsync(Filme filme)
        {
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();
            }
        }
    }
}