using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.TiposDeCursos;

namespace w_escolas.Infra.Data.Config;

public class TipoDeCursoConfig : IEntityTypeConfiguration<TipoDeCurso>
{
    public void Configure(EntityTypeBuilder<TipoDeCurso> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.EscolaId, t.Codigo })
            .IsUnique(true);
        builder.Property(t => t.Codigo)
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(t => t.Nome)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(t => t.Ordem)
            .HasColumnType("int")
            .IsRequired();

        builder.ToTable("TiposDeCursos");

        builder.HasOne(c => c.Escola)
                .WithMany(c => c.TiposDeCursos)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
