using ApolloGraphQLFederationExtensions;

namespace DevTalk.Accounts.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    public void AccountQueries()
    {
        Field<MyCustomType>(
            "accounts_getDataAnonymously",
            resolve: ctx => {
                return new MyCustomTypeDTO() { Id = 456, DateCreated = DateTime.Now };
            }
        );
    }
}