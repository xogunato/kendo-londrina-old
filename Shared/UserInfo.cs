using System.Security.Claims;

namespace w_escolas.Shared;

public class UserInfo
{
    public IHttpContextAccessor Context { get; private set; }
    public UserInfo(IHttpContextAccessor httpContext)
    {
        Context = httpContext;
    }

    public Guid GetEscolaId()
    {
        var context = Context.HttpContext;
        if (context == null)
            throw new Exception("http context unavailable");

        if (context.User.Identity is not ClaimsIdentity identity)
            throw new Exception("user identity unavailable");

        var claims = identity.Claims.ToList();
        var escolaIdDoUsuarioCorrente = claims.First(item => item.Type == "EscolaId").Value;
        return Guid.Parse(escolaIdDoUsuarioCorrente);
    }
}
