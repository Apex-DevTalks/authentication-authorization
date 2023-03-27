using ApolloGraphQLFederationExtensions;

namespace DevTalk.Authorization.Api.GraphQL;

public class MySchema : FederatedSchema
{
    public MySchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<AppQueries>();
    }
}