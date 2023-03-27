using System.Security.Claims;

namespace DevTalk.Authorization.Api;

public class MyGraphQLUserContext : Dictionary<string, object?>
{
    public ClaimsPrincipal User { get; set; }

    public MyGraphQLUserContext(ClaimsPrincipal user)
    {
        User = user;
    }
}