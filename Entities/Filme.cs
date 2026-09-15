namespace CatalogoFilmesApi.Entities
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AnoLancamento { get; set; }
        
        // Chave estrangeira
        public int DiretorId { get; set; }
        public Diretor Diretor { get; set; } = null!;
        
        // Propriedade de navegação
        public List<Avaliacao> Avaliacoes { get; set; } = new();
    }
}