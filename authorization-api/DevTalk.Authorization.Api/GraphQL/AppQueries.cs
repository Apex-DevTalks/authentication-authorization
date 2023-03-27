using ApolloGraphQLFederationExtensions;
using DevTalk.Authorization.Api.Auth;

namespace DevTalk.Authorization.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    private IIdentityService _identityService;
    public AppQueries(IIdentityService identityService)
    {
        _identityService = identityService;

        AuthQueries();
        AspNetUserQueries();
    }
}