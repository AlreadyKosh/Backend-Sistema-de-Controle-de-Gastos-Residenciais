using Backend_Sistema_de_Controle_de_Gastos_Residenciais.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend_Sistema_de_Controle_de_Gastos_Residenciais.Data.Configurations
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> entity)
        {
            entity.ToTable("pessoa");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
            .UseIdentityByDefaultColumn();

            entity.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasMaxLength(200)
                .IsRequired();

            entity.HasIndex(c => c.Nome)
                .IsUnique();

            entity.Property(p => p.Idade)
                .HasColumnName("idade")
                .IsRequired();
        }
    }
}
