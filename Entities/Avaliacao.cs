namespace CatalogoFilmesApi.Entities
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        
        // Chave estrangeira
        public int FilmeId { get; set; }
        public Filme Filme { get; set; } = null!;
    }
}