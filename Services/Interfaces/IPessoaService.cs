using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<List<Pessoa>> ObterTodasAsPessoasAsync(CancellationToken ct);
        Task<Pessoa?> ObterPessoasPorIdAsync(long id, CancellationToken ct);
        Task CriarPessoaAsync(Pessoa pessoa, CancellationToken ct);
        Task<bool> AtualizarPessoaAsync(long id, Pessoa data, CancellationToken ct);
        Task<bool> DeletarPessoaAsync(long id, CancellationToken ct);
    }
}
