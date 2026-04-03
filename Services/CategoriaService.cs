using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Data;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObterTodasAsCategoriasAsync(CancellationToken ct)
        {
            return await _context.Categorias
                    .AsNoTracking()
                    .ToListAsync(ct);
        }

        public async Task<Categoria?> ObterCategoriaPorIdAsync(long id, CancellationToken ct)
        {
            return await _context.Categorias
               .AsNoTracking()
               .FirstOrDefaultAsync(t => t.Id == id, ct);
        }

        public async Task CriarCategoriaAsync(Categoria categoria, CancellationToken ct)
        {
            try
            {
                categoria.Nome = categoria.Nome.Trim().ToLower();

                var categoriaExistente = await _context.Categorias
                     .AsNoTracking()
                     .AnyAsync(c => c.Nome.ToLower() == categoria.Nome, ct);

                if (categoriaExistente)
                {
                    throw new Exception("Categoria com esse nome já existe.");
                }

                _context.Categorias.Add(categoria);
            
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") ?? false)
            {
                throw new Exception("Categoria com esse nome já existe.");
            }
        }

        public async Task<bool> AtualizarCategoriaAsync(long id, Categoria data, CancellationToken ct)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (categoria == null)
            {
                return false;
            }

            categoria.Nome = data.Nome;
            categoria.Descricao = data.Descricao;
            categoria.Finalidade = data.Finalidade;

            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<bool> DeletarCategoriaAsync(long id, CancellationToken ct)
        {
            var categorias = await _context.Categorias.FindAsync(id);

            if (categorias == null)
            {
                return false;
            }

            _context.Categorias.Remove(categorias);

            await _context.SaveChangesAsync(ct);
            return true;
        }

    }
}
