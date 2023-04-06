using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.Enderecos;

namespace w_escolas.Infra.Data.Config;

public class EnderecoConfig : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Cep)
            .HasColumnType("char")
            .HasMaxLength(9)
            .IsRequired(true);
        builder.Property(c => c.Uf)
            .HasColumnType("char")
            .HasMaxLength(2)
            .IsRequired(true);
        builder.Property(c => c.Cidade)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired(true);
        builder.Property(c => c.Bairro)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired(true);
        builder.Property(c => c.Distrito)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired(false);
        builder.Property(c => c.Complemento)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired(false);
        builder.Property(c => c.Logradouro)
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired(true);

        builder.ToTable("Enderecos");
    }
}
