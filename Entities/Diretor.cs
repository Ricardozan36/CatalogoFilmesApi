namespace CatalogoFilmesApi.Entities
{
    public class Diretor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        
        
        public List<Filme> Filmes { get; set; } = new();
    }
}