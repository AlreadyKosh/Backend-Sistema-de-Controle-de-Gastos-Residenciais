using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Data;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly AppDbContext _context;

        public PessoaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pessoa>> ObterTodasAsPessoasAsync(CancellationToken ct)
        {
            return await _context.Pessoas
                    .AsNoTracking()
                    .ToListAsync(ct);
        }

        public async Task<Pessoa?> ObterPessoaPorIdAsync(long id)
        {
            return await _context.Pessoas
               .AsNoTracking()
               .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CriarPessoaAsync(Pessoa pessoa, CancellationToken ct)
        {
            try
            {
                pessoa.Nome = pessoa.Nome.Trim().ToLower();

                var pessoaExistente = await _context.Pessoas
                     .AsNoTracking()
                     .AnyAsync(c => c.Nome.ToLower() == pessoa.Nome, ct);

                if (pessoaExistente)
                {
                    throw new Exception("Já existe um membro da familia com esse nome.");
                }

                _context.Pessoas.Add(pessoa);

                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                throw new Exception("Já existe um membro da familia com esse nome.");
            }
        }

        public async Task<bool> AtualizarPessoaAsync(long id, Pessoa data, CancellationToken ct)
        {
            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (pessoa == null)
            {
                return false;
            }

            pessoa.Nome = data.Nome;
            pessoa.Idade = data.Idade;

            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeletarPessoaAsync(long id, CancellationToken ct)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                return false;
            }

            _context.Pessoas.Remove(pessoa);

            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
