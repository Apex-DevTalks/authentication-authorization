using ApolloGraphQLFederationExtensions;

namespace DevTalk.Accounts.Api.GraphQL;

public class HeroTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Height { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}

public class HeroType : FederatedObjectGraphType<HeroTypeDTO>
{
    public HeroType()
    {
        Name = nameof(HeroTypeDTO);
        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.Height);
        Field(x => x.Weight);
        Field(x => x.DateOfBirth);
    }
}