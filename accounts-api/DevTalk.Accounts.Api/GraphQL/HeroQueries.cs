using ApolloGraphQLFederationExtensions;
using GraphQL;
using GraphQL.Types;

namespace DevTalk.Accounts.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    private List<HeroTypeDTO> _heroes = new List<HeroTypeDTO>
    {
        new HeroTypeDTO
        {
            Id = 1,
            Name = "Luke Skywalker",
            Height = "1.80 m",
            Weight = "77",
            DateOfBirth = DateTime.Now.AddYears(-20)
        },
        new HeroTypeDTO
        {
            Id = 2,
            Name = "Anakin Skywalker",
            Height = "1.75 m",
            Weight = "75",
            DateOfBirth = DateTime.Now.AddYears(-28)
        },
        new HeroTypeDTO
        {
            Id = 3,
            Name = "Obi-Wan Kenobi",
            Height = "1.90 m",
            Weight = "80",
            DateOfBirth = DateTime.Now.AddYears(-53)
        }
    };

    public void HeroQueries()
    {
        Field<HeroType>(
            "accounts_getHeroById",
            arguments: new QueryArguments(new QueryArgument<IntGraphType>(){ Name= "Id" }),
            resolve: ctx => {
                var id = ctx.GetArgument<int>("Id");
                return _heroes.First(x => x.Id == id);
            }
        );
        
        Field<ListGraphType<HeroType>>(
            "accounts_getAllHeroes",
            resolve: ctx => {
                return _heroes;
            }
        );
    }
}