using ApolloGraphQLFederationExtensions;

namespace DevTalk.Accounts.Api.GraphQL;

public class MySchema : FederatedSchema
{
    public MySchema(IServiceProvider serviceProvider, MyQuery myQuery) : base(serviceProvider)
    {
        Query = myQuery;
    }
}