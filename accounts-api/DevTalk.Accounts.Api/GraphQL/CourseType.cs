using ApolloGraphQLFederationExtensions;

namespace DevTalk.Accounts.Api.GraphQL;

public class CourseTypeDTO
{
    public int Id { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string ProfessorName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageURL { get; set; } = string.Empty;
    public DateTime LastEditionDate { get; set; }
}

public class CourseType : FederatedObjectGraphType<CourseTypeDTO>
{
    public CourseType()
    {
        Name = nameof(CourseTypeDTO);
        Field(x => x.Id);
        Field(x => x.CourseName);
        Field(x => x.ProfessorName);
        Field(x => x.Description);
        Field(x => x.ImageURL);
        Field(x => x.LastEditionDate);
    }
}