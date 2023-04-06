using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.Matriculas;

namespace w_escolas.Infra.Data.Config;

public class MatriculaConfig : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.CursoId, t.AlunoId, t.TemporadaId })
            .IsUnique(true);
        builder.Property(t => t.DataMatricula)
            .HasColumnType("date")
            .IsRequired();
        builder.Property(t => t.Cancelada)
            .HasColumnType("bit")
            .IsRequired();

        builder.ToTable("Matriculas");

        builder.HasOne(c => c.Escola)
                .WithMany(c => c.Matriculas)
                .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Curso)
                .WithMany(c => c.Matriculas)
                .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Aluno)
                .WithMany(c => c.Matriculas)
                .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Temporada)
                .WithMany(c => c.Matriculas)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
