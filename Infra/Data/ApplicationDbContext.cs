using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using w_escolas.Domain.Alunos;
using w_escolas.Domain.Cursos;
using w_escolas.Domain.Enderecos;
using w_escolas.Domain.Escolas;
using w_escolas.Domain.Matriculas;
using w_escolas.Domain.Temporadas;
using w_escolas.Domain.TiposDeCursos;
using w_escolas.Domain.Turmas;
using w_escolas.Infra.Data.Config;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Escola>? Escolas { get; set; }
    public DbSet<TipoDeCurso>? TiposDeCursos { get; set; }
    public DbSet<Curso>? Cursos { get; set; }
    public DbSet<Turma>? Turmas { get; set; }
    public DbSet<Aluno>? Alunos { get; set; }
    public DbSet<Endereco>? Enderecos { get; set; }
    public DbSet<Temporada>? Temporadas { get; set; }
    public DbSet<Matricula>? Matriculas { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new EscolaConfig());
        builder.ApplyConfiguration(new TipoDeCursoConfig());
        builder.ApplyConfiguration(new CursoConfig());
        builder.ApplyConfiguration(new TurmaConfig());
        builder.ApplyConfiguration(new AlunoConfig());
        builder.ApplyConfiguration(new EnderecoConfig());
        builder.ApplyConfiguration(new TemporadaConfig());
        builder.ApplyConfiguration(new MatriculaConfig());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100)
            .HaveColumnType("varchar");
    }
}
