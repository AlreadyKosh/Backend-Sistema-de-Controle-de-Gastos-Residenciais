using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Data;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.DTOs.Transacoes;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transacao>> ObterTodasAsTransacoesAsync(CancellationToken ct)
        {
            return await _context.Transacoes
                    .AsNoTracking()
                    .Include(t => t.Pessoa)
                    .Include(t => t.Categoria)
                    .ToListAsync(ct);
        }

        public async Task<Transacao?> ObterTransacaoPorIdAsync(long id, CancellationToken ct)
        {
            return await _context.Transacoes
               .AsNoTracking()
               .Include(t => t.Pessoa)
               .Include(t => t.Categoria)
               .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task CriarTransacaoAsync(Transacao transacao, CancellationToken ct)
        {
            var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId);

            if (pessoa == null)
            {
                throw new Exception("Pessoa não encontrada.");
            }
            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada.");
            }

            ValidarTransacao(transacao, pessoa, categoria);

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<bool> AtualizarTransacaoAsync(long id, Transacao data, CancellationToken ct)
        {
            var transacao = await _context.Transacoes
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (transacao == null) {
                return false;
            }

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == data.PessoaId, ct);

            if (pessoa == null)
            {
                throw new Exception("Pessoa não encontrada");
            }
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == data.CategoriaId, ct);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            ValidarTransacao(data, pessoa, categoria);

            transacao.Descricao = data.Descricao;
            transacao.Valor = data.Valor;
            transacao.Tipo = data.Tipo;
            transacao.PessoaId = data.PessoaId;
            transacao.CategoriaId = data.CategoriaId;

            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeletarTransacaoAsync(long id, CancellationToken ct)
        {
            var transacao = await _context.Transacoes.FindAsync(id);

            if (transacao == null) { 
                return false;
            }

            _context.Transacoes.Remove(transacao);

            await _context.SaveChangesAsync(ct);
            return true;
        }

        private void ValidarTransacao(Transacao transacao, Pessoa pessoa, Categoria categoria)
        {
            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
            {
                throw new Exception("Menor de idade só pode ter despesas.");
            }

            if (transacao.Valor <= 0)
            {
                throw new Exception("Valor deve ser positivo.");
            }

            if (transacao.Tipo == TipoTransacao.Despesa && categoria.Finalidade == FinalidadeCategoria.Receita)
            {
                throw new Exception("Categoria inválida para despesa.");
            }

            if (transacao.Tipo == TipoTransacao.Receita && categoria.Finalidade == FinalidadeCategoria.Despesa)
            {
                throw new Exception("Categoria inválida para receita.");
            }
        }
    }
}
