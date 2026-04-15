using AutoMapper;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Transacoes;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Tags("Transação")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _servicoTransacao;
        private readonly IMapper _mapper;

        public TransacaoController(IMapper mapper, ITransacaoService serviceTransacao)
        {
            _servicoTransacao = serviceTransacao;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos as transações cadastradas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransacaoResponseDto>), 200)]
        public async Task<ActionResult> ObterTodasAsTransacoesAsync(CancellationToken ct)
        {
            try
            {
                var result = await _servicoTransacao.ObterTodasAsTransacoesAsync(ct);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna uma transacao pelo id
        /// </summary>
        [HttpGet("{id}", Name = "ObterTransacaoPorIdAsync")]
        [ProducesResponseType(typeof(TransacaoResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> ObterTransacaoPorIdAsync(long id, CancellationToken ct)
        {
            try
            {
                var transacao = await _servicoTransacao.ObterTransacaoPorIdAsync(id, ct);

                return transacao == null ? NotFound("Transação não encontrada") : Ok(_mapper.Map<TransacaoResponseDto>(transacao));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria uma nova transação
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CriarTransacao([FromBody] TransacaoCriarDto dto, CancellationToken ct)
        {
            try
            {
                var transacao = _mapper.Map<Transacao>(dto);

                await _servicoTransacao.CriarTransacaoAsync(transacao, ct);

                var response = _mapper.Map<TransacaoResponseDto>(transacao);

                return CreatedAtAction(
                    nameof(ObterTransacaoPorIdAsync),
                    new { id = transacao.Id },
                    response
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma transação existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AtualizarTransacaoAsync(long id, TransacaoCriarDto dto, CancellationToken ct)
        {
            try
            {
                var entidade = _mapper.Map<Transacao>(dto);

                var atualizado = await _servicoTransacao.AtualizarTransacaoAsync(id, entidade, ct);

                return atualizado ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma transação pelo id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletarTransacao(long id, CancellationToken ct)
        {
            try
            {
                var atualizado = await _servicoTransacao.DeletarTransacaoAsync(id, ct);
                return atualizado ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
