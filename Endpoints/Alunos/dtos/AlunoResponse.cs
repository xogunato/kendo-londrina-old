namespace w_escolas.Endpoints.Alunos.dtos;

public class AlunoResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Nacionalidade { get; set; }
    public string? UFNascimento { get; set; }
    public string? CidadeNascimento { get; set; }
    public string? Sexo { get; set; }
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? Email { get; set; }
    public string? TelCelular { get; set; }
    public string? Religiao { get; set; }
}
