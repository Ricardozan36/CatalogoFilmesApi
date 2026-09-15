using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmesApi.DTOs
{
    public class FilmeRequestDTO
    {
        // Validações obrigatórias dos dados de entrada
        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Range(1888, 2100, ErrorMessage = "Ano de lançamento inválido.")]
        public int AnoLancamento { get; set; }

        [Required(ErrorMessage = "O ID do diretor é obrigatório.")]
        public int DiretorId { get; set; }
    }
}