using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CatalogoFilmesApi.Repositories;
using CatalogoFilmesApi.Entities;
using CatalogoFilmesApi.DTOs;

namespace CatalogoFilmesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeRepository _repository;

        public FilmesController(IFilmeRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeResponseDTO>>> GetFilmes()
        {
            var filmes = await _repository.GetAllAsync();
            
            var filmesDto = filmes.Select(f => new FilmeResponseDTO
            {
                Id = f.Id,
                Titulo = f.Titulo,
                AnoLancamento = f.AnoLancamento,
                NomeDiretor = f.Diretor?.Nome ?? "Desconhecido"
            });
            
            return Ok(filmesDto);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeResponseDTO>> GetFilme(int id)
        {
            var filme = await _repository.GetByIdAsync(id);
            if (filme == null) return NotFound("Filme não encontrado.");

            var filmeDto = new FilmeResponseDTO
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                AnoLancamento = filme.AnoLancamento,
                NomeDiretor = filme.Diretor?.Nome ?? "Desconhecido"
            };
            
            return Ok(filmeDto);
        }

        
        [HttpPost]
        public async Task<ActionResult<FilmeResponseDTO>> PostFilme([FromBody] FilmeRequestDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var novoFilme = new Filme
            {
                Titulo = request.Titulo,
                AnoLancamento = request.AnoLancamento,
                DiretorId = request.DiretorId
            };

            var filmeCriado = await _repository.AddAsync(novoFilme);

            return CreatedAtAction(nameof(GetFilme), new { id = filmeCriado.Id }, request);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, [FromBody] FilmeRequestDTO request)
        {
            if (id <= 0) return BadRequest("ID inválido.");

            var filmeExistente = await _repository.GetByIdAsync(id);
            if (filmeExistente == null) return NotFound("Filme não encontrado.");

            filmeExistente.Titulo = request.Titulo;
            filmeExistente.AnoLancamento = request.AnoLancamento;
            filmeExistente.DiretorId = request.DiretorId;

            await _repository.UpdateAsync(filmeExistente);
            return NoContent();
        }

        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var filme = await _repository.GetByIdAsync(id);
            if (filme == null) return NotFound("Filme não encontrado.");

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}