using ApolloGraphQLFederationExtensions;
using GraphQL.Types;

namespace DevTalk.Accounts.Api.GraphQL;

public class MyQuery : FederatedQuery
{
    public MyQuery()
    {
        Field<MyCustomType>(
            "accounts_GetMyCustomType",
            resolve: ctx => {
                return new MyCustomTypeDTO() { Id = 456, DateCreated = DateTime.Now };
            }
        );
    }
}