namespace CatalogoFilmesApi.DTOs
{
    public class FilmeResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AnoLancamento { get; set; }
        
        // Retornamos apenas o nome do diretor para facilitar a leitura e evitar loops
        public string NomeDiretor { get; set; } = string.Empty; 
    }
}