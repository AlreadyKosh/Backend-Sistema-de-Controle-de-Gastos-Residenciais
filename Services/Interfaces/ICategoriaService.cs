using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObterTodasAsCategoriasAsync(CancellationToken ct);
        Task<Categoria?> ObterCategoriaPorIdAsync(long id, CancellationToken ct);
        Task CriarCategoriaAsync(Categoria categoria, CancellationToken ct);
        Task<bool> AtualizarCategoriaAsync(long id, Categoria data, CancellationToken ct);
        Task<bool> DeletarCategoriaAsync(long id, CancellationToken ct);
    }
}
