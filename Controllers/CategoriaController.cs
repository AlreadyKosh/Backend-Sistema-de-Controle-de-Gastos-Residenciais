using AutoMapper;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Categorias;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Tags("Categorias")]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _servicoCategoria;
        private readonly IMapper _mapper;

        public CategoriaController(IMapper mapper, ICategoriaService servicoCategoria)
        {
            _servicoCategoria = servicoCategoria;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todas as categorias cadastradas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoriaResponseDto>), 200)]
        public async Task<ActionResult> ObterTodasAsTransacoesAsync(CancellationToken ct)
        {
            try
            {
                var result = await _servicoCategoria.ObterTodasAsCategoriasAsync(ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma categoria pelo id
        /// </summary>
        [HttpGet("{id}", Name = "ObterCategoriaPorIdAsync")]
        [ProducesResponseType(typeof(CategoriaResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ObterCategoriaPorIdAsync(long id, CancellationToken ct)
        {
            try
            {
                var categoria = await _servicoCategoria.ObterCategoriaPorIdAsync(id, ct);

                return categoria == null ? NotFound("Categoria não encontrada") : Ok(_mapper.Map<CategoriaResponseDto>(categoria));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria uma nova categoria
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CriarCategoriaAsync([FromBody] CategoriaResponseDto dto, CancellationToken ct)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(dto);

                await _servicoCategoria.CriarCategoriaAsync(categoria, ct);

                var response = _mapper.Map<CategoriaResponseDto>(categoria);

                return CreatedAtAction(
                    nameof(ObterCategoriaPorIdAsync),
                    new { id = categoria.Id },
                    response
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma categoria existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AtualizarCategoriaAsync(long id, CategoriaResponseDto dto, CancellationToken ct)
        {
            try
            {
                var entidade = _mapper.Map<Categoria>(dto);

                var atualizado = await _servicoCategoria.AtualizarCategoriaAsync(id, entidade, ct);

                return atualizado ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma categoria pelo id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletarCategoria(long id, CancellationToken ct)
        {
            try
            {
                var atualizado = await _servicoCategoria.DeletarCategoriaAsync(id, ct);
                return atualizado ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
