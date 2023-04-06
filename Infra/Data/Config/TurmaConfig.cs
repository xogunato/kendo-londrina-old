using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using w_escolas.Domain.Turmas;

namespace w_escolas.Infra.Data.Config;

public class TurmaConfig : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => new { t.CursoId, t.Codigo })
            .IsUnique(true);
        builder.HasIndex(t => new { t.CursoId, t.Nome })
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
        builder.Property(t => t.MaxAlunos)
            .HasColumnType("int")
            .IsRequired(false);
        builder.Property(t => t.DataInicial)
            .HasColumnType("date")
            .IsRequired(false);
        builder.Property(t => t.DataFinal)
            .HasColumnType("date")
            .IsRequired(false);

        builder.ToTable("Turmas");

        builder.HasOne(c => c.Curso)
                .WithMany(c => c.Turmas)
                .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Escola)
                .WithMany(c => c.Turmas)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
