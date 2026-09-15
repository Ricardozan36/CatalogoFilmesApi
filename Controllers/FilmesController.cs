using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // <-- ADICIONADO PARA A AUTENTICAÇÃO
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

        // GET: api/filmes 
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
            
            return Ok(filmesDto); // Retorna HTTP 200
        }

        // GET: api/filmes/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeResponseDTO>> GetFilme(int id)
        {
            var filme = await _repository.GetByIdAsync(id);
            if (filme == null) return NotFound("Filme não encontrado."); // Retorna HTTP 404

            var filmeDto = new FilmeResponseDTO
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                AnoLancamento = filme.AnoLancamento,
                NomeDiretor = filme.Diretor?.Nome ?? "Desconhecido"
            };
            
            return Ok(filmeDto); // Retorna HTTP 200
        }

        // POST: api/filmes 
        [HttpPost]
        public async Task<ActionResult<FilmeResponseDTO>> PostFilme([FromBody] FilmeRequestDTO request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Retorna HTTP 400

            var novoFilme = new Filme
            {
                Titulo = request.Titulo,
                AnoLancamento = request.AnoLancamento,
                DiretorId = request.DiretorId
            };

            var filmeCriado = await _repository.AddAsync(novoFilme);

            return CreatedAtAction(nameof(GetFilme), new { id = filmeCriado.Id }, request); // Retorna HTTP 201
        }

        // PUT: api/filmes/5 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, [FromBody] FilmeRequestDTO request)
        {
            if (id <= 0) return BadRequest("ID inválido."); // Retorna HTTP 400

            var filmeExistente = await _repository.GetByIdAsync(id);
            if (filmeExistente == null) return NotFound("Filme não encontrado."); // Retorna HTTP 404

            filmeExistente.Titulo = request.Titulo;
            filmeExistente.AnoLancamento = request.AnoLancamento;
            filmeExistente.DiretorId = request.DiretorId;

            await _repository.UpdateAsync(filmeExistente);
            return NoContent(); // Retorna HTTP 204
        }

        // DELETE: api/filmes/5 
        [Authorize] // <-- ADICIONADO AQUI PARA BLOQUEAR A EXCLUSÃO SEM TOKEN
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var filme = await _repository.GetByIdAsync(id);
            if (filme == null) return NotFound("Filme não encontrado."); // Retorna HTTP 404

            await _repository.DeleteAsync(id);
            return NoContent(); // Retorna HTTP 204
        }
    }
}