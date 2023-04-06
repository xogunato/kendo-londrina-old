using Dapper;
using Microsoft.Data.SqlClient;

namespace w_escolas.Infra.Data.DapperQueries;

public record UsuarioResponse(string Email, string NomeDoUsuario);

public class QueryAllUsersWithClaimNomeDoUsuario
{
    private readonly IConfiguration configuration;
    public QueryAllUsersWithClaimNomeDoUsuario(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IEnumerable<UsuarioResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["Database:ConnectionString"]);
        var query =
            @"
            select Email, ClaimValue as NomeDoUsuario
            from AspNetUsers as U
            left outer join AspNetUserClaims as C
                on U.Id = C.UserId and claimType = 'NomeDoUsuario'
            order by NomeDoUsuario
            OFFSET (@page - 1) * @rows
            ROWS FETCH NEXT @rows ROWS ONLY
            ";
        return db.Query<UsuarioResponse>(
            query, new { page, rows }
        );
    }
}
