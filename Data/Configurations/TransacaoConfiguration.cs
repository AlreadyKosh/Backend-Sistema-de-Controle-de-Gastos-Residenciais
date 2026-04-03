using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Data.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> entity)
        {
            entity.ToTable("transacao");

            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
            .UseIdentityByDefaultColumn();

            entity.Property(t => t.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(400)
                .IsRequired();

            entity.Property(t => t.Valor)
                .HasColumnName("valor")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.HasOne(t => t.Pessoa)
                .WithMany(t => t.Transacoes)
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(t => t.Categoria)
                .WithMany()
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(t => t.Tipo)
                .HasColumnName("tipo")
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
