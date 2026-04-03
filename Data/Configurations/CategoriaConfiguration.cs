using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> entity)
        {
            entity.ToTable("categoria");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
            .UseIdentityByDefaultColumn();

            entity.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasMaxLength(200)
                .IsRequired();

            entity.HasIndex(c => c.Nome)
                .IsUnique();

            entity.Property(c => c.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(400)
                .IsRequired();

            entity.Property(c => c.Finalidade)
                .HasColumnName("finalidade")
                .HasConversion<int>();
        }
    }
}
