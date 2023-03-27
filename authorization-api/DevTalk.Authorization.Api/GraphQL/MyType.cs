using ApolloGraphQLFederationExtensions;

namespace DevTalk.Authorization.Api.GraphQL;

public class MyCustomTypeDTO
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
}

public class MyCustomType : FederatedObjectGraphType<MyCustomTypeDTO>
{
    public MyCustomType()
    {
        Name = nameof(MyCustomTypeDTO);
        Field(x => x.Id);
        Field(x => x.DateCreated);
    }
}