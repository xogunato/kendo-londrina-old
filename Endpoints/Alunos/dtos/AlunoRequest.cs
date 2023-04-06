﻿namespace w_escolas.Endpoints.Alunos.dtos;

public class AlunoRequest
{
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Nacionalidade { get; set; }
    public string? UfNascimento { get; set; }
    public string? CidadeNascimento { get; set; }
    public string? Sexo { get; set; }
    public string? Rg { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }
    public string? TelCelular { get; set; }
    public string? Religiao { get; set; }
}
