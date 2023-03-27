using ApolloGraphQLFederationExtensions;
using GraphQL;
using GraphQL.Types;

namespace DevTalk.Authorization.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    public void AuthQueries()
    {
        
        FieldAsync<StringGraphType>(
            "authorization_login",
            arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "Email" },
                    new QueryArgument<NonNullGraphType<StringGraphType>>() { Name = "Password" }
                ),
            resolve: async context =>
            {
                var email = context.GetArgument<string>("Email");
                var password = context.GetArgument<string>("Password");

                var result = await _identityService.GetToken(email, password);
                switch (result.Status)
                {
                    case System.Net.HttpStatusCode.OK:
                        return result.Message;
                    case System.Net.HttpStatusCode.BadRequest:
                        context.Errors.Add(new ExecutionError("Either the username or password is incorrect"));
                        break;
                    default:
                        context.Errors.Add(new ExecutionError(result.Message ?? "Internal Server Error"));
                        break;
                }
                return string.Empty;
            }
        );

        Field<MyCustomType>(
            "authorization_getMyCustomType",
            resolve: ctx => 
            {
                return new MyCustomTypeDTO() { Id = 123, DateCreated = DateTime.Now };
            }
        ).AuthorizeWith("MyPolicy");
        
        Field<StringGraphType>(
            "authorization_protectedByAdminRole",
            resolve: ctx => {
                return "Success!";
            }
        ).AuthorizeWith("AdminPolicy");
    }
}