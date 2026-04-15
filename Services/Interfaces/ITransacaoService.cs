using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces
{
    public interface ITransacaoService
    {
        Task<List<Transacao>> ObterTodasAsTransacoesAsync(CancellationToken ct);
        Task<Transacao?> ObterTransacaoPorIdAsync(long id, CancellationToken ct);
        Task CriarTransacaoAsync(Transacao transacao, CancellationToken ct);
        Task<bool> AtualizarTransacaoAsync(long id, Transacao data, CancellationToken ct);
        Task<bool> DeletarTransacaoAsync(long id, CancellationToken ct);
    }
}
