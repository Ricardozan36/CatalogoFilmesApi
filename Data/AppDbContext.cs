using Microsoft.EntityFrameworkCore;
using CatalogoFilmesApi.Entities;

namespace CatalogoFilmesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Diretor>().HasData(
                new Diretor { Id = 1, Nome = "Christopher Nolan" },
                new Diretor { Id = 2, Nome = "Quentin Tarantino" }
            );

            modelBuilder.Entity<Filme>().HasData(
                new Filme { Id = 1, Titulo = "Inception", AnoLancamento = 2010, DiretorId = 1 },
                new Filme { Id = 2, Titulo = "Pulp Fiction", AnoLancamento = 1994, DiretorId = 2 }
            );
        }
    }
}