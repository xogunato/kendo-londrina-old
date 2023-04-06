namespace w_escolas.Endpoints.Temporadas.dtos;

public class TemporadaResponse
{
    public Guid Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nome { get; set; }
    public int? Ano { get; set; }
    public int? Semestre { get; set; }
    public int? Quadrimestre { get; set; }
    public int? Trimestre { get; set; }
    public int? Bimestre { get; set; }
    public int? Mes { get; set; }
}
