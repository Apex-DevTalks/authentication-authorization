using ApolloGraphQLFederationExtensions;

namespace DevTalk.Accounts.Api.GraphQL;

public class MySchema : FederatedSchema
{
    public MySchema(IServiceProvider serviceProvider, AppQueries myQuery) : base(serviceProvider)
    {
        Query = myQuery;
    }
}