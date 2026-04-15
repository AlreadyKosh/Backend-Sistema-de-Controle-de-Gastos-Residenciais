using AutoMapper;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Pessoas;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Tags("Pessoa")]
    public class PessoaController : Controller
    {
        private readonly IPessoaService _servicoPessoa;
        private readonly IMapper _mapper;

        public PessoaController(IMapper mapper, IPessoaService servicoPessoa)
        {
            _servicoPessoa = servicoPessoa;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todas as pessoas cadastradas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PessoaResponseDto>), 200)]
        public async Task<ActionResult> ObterTodasAsPessoasAsync(CancellationToken ct)
        {
            try
            {
                var result = await _servicoPessoa.ObterTodasAsPessoasAsync(ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma pessoa pelo id
        /// </summary>
        [HttpGet("{id}", Name = "ObterPessoaPorIdAsync")]
        [ProducesResponseType(typeof(PessoaResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ObterPessoaPorIdAsync(long id)
        {
            try
            {
                var pessoa = await _servicoPessoa.ObterPessoaPorIdAsync(id);

                return pessoa == null ? NotFound("Pessoa não encontrada") : Ok(_mapper.Map<PessoaResponseDto>(pessoa));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria uma nova pessoa
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CriarPessoaAsync([FromBody] PessoaResponseDto dto, CancellationToken ct)
        {
            try
            {
                var pessoa = _mapper.Map<Pessoa>(dto);

                await _servicoPessoa.CriarPessoaAsync(pessoa, ct);

                var response = _mapper.Map<PessoaResponseDto>(pessoa);

                return CreatedAtAction(
                    "ObterPessoaPorIdAsync",
                    new { id = pessoa.Id },
                    response
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma pessoa existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AtualizarPessoaAsync(long id, PessoaResponseDto dto, CancellationToken ct)
        {
            try
            {
                var entidade = _mapper.Map<Pessoa>(dto);

                var atualizado = await _servicoPessoa.AtualizarPessoaAsync(id, entidade, ct);

                return atualizado ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma pessoa pelo id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletarPessoa(long id, CancellationToken ct)
        {
            try
            {
                var atualizado = await _servicoPessoa.DeletarPessoaAsync(id, ct);
                return atualizado ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
