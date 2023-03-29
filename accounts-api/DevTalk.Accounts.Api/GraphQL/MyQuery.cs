using ApolloGraphQLFederationExtensions;
using GraphQL.Types;

namespace DevTalk.Accounts.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    public AppQueries()
    {
        AccountQueries();
        HeroQueries();
    }
}