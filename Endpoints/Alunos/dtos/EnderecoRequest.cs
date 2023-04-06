namespace w_escolas.Endpoints.Alunos.dtos;

public class EnderecoRequest
{
    public string? CEP { get; set; }
    public string? UF { get; set; }
    public string? Cidade { get; set; }
    public string? Bairro { get; set; }
    public string? Distrito { get; set; }
    public string? Complemento { get; set; }
    public string? Logradouro { get; set; }
    public string? Tipo { get; set; }
}
