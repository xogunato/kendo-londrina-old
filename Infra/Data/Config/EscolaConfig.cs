using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.Escolas;

namespace w_escolas.Infra.Data.Config;

public class EscolaConfig : IEntityTypeConfiguration<Escola>
{
    public void Configure(EntityTypeBuilder<Escola> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.NomeFantasia })
           .IsUnique(true);
        builder.Property(t => t.NomeFantasia)
            .IsRequired();
        builder.Property(t => t.Cnpj)
            .HasColumnType("char")
            .HasMaxLength(14)
            .IsRequired(false);
        builder.Property(t => t.RazaoSocial)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired(false);
        builder.Property(t => t.Cep)
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired(false);
        builder.Property(t => t.Uf)
            .HasColumnType("char")
            .HasMaxLength(2)
            .IsRequired();
        builder.Property(t => t.Cidade)
            .IsRequired();
        builder.Property(t => t.Bairro)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired(false);
        builder.Property(t => t.Endereco)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired(false);
        builder.Property(t => t.Telefone)
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired(false);
        builder.Property(t => t.Email)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired(false);
        builder.Property(t => t.Website)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired(false);

        builder.ToTable("Escolas");
    }
}
