using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.Temporadas;

namespace w_escolas.Infra.Data.Config;

public class TemporadaConfig : IEntityTypeConfiguration<Temporada>
{
    public void Configure(EntityTypeBuilder<Temporada> builder)
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
        builder.Property(t => t.Ano)
            .HasColumnType("smallint")
            .IsRequired(false);
        builder.Property(t => t.Semestre)
            .HasColumnType("tinyint")
            .IsRequired(false);
        builder.Property(t => t.Quadrimestre)
            .HasColumnType("tinyint")
            .IsRequired(false);
        builder.Property(t => t.Trimestre)
            .HasColumnType("tinyint")
            .IsRequired(false);
        builder.Property(t => t.Bimestre)
            .HasColumnType("tinyint")
            .IsRequired(false);
        builder.Property(t => t.Mes)
            .HasColumnType("tinyint")
            .IsRequired(false);

        builder.ToTable("Temporadas");

        builder.HasOne(c => c.Escola)
                .WithMany(c => c.Temporadas)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
