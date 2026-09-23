namespace CatalogoFilmesApi.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AnoLancamento { get; set; }
        
        
        public int DiretorId { get; set; }
        public Diretor Diretor { get; set; } = null!;
        
        
        public List<Avaliacao> Avaliacoes { get; set; } = new();
    }
}