using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.Cursos;

namespace w_escolas.Infra.Data.Config;

public class CursoConfig : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.TipoDeCursoId, t.Codigo })
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

        builder.ToTable("Cursos");

        builder.HasOne(c => c.TipoDeCurso)
                .WithMany(c => c.Cursos)
                .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Escola)
                .WithMany(c => c.Cursos)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
