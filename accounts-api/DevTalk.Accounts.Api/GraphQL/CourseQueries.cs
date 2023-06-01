using ApolloGraphQLFederationExtensions;
using GraphQL;
using GraphQL.Types;

namespace DevTalk.Accounts.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    private List<CourseTypeDTO> _courses = new List<CourseTypeDTO>
    {
        new CourseTypeDTO
        {
            Id = 1,
            CourseName = "C# Advanced Topics: Prepare for technical interviews",
            ProfessorName = "Mosh Hamedani",
            Description = @"Chances are you're familiar with the basics of C# and are hungry to learn more. 
            Or you've been out of touch with C# for a while and are looking for a quick course as a refresher 
            to get you up to speed with advanced C# constructs. If so, then this course is for you.",
            ImageURL = "https://img-b.udemycdn.com/course/240x135/356030_0209_4.jpg",
            LastEditionDate = DateTime.Now.AddYears(-2).AddMonths(-2).AddDays(-10)
        },
        new CourseTypeDTO
        {
            Id = 2,
            CourseName = "Boost Your C# With Structural And Creational Design Patterns",
            ProfessorName = "Mark Farragher",
            Description = @"In this course I will teach you the first 12 design patterns. These are all 5 
            creational- and all 7 structural design patterns. You use these patterns to create new objects 
            efficiently and to create structure in your application architecture.",
            ImageURL = "https://img-b.udemycdn.com/course/750x422/587146_9ce1_5.jpg",
            LastEditionDate = DateTime.Now.AddYears(-1).AddMonths(-4).AddDays(-15)
        },
        new CourseTypeDTO
        {
            Id = 3,
            CourseName = "Secure .Net Microservices with IdentityServer4 OAuth2, OpenID",
            ProfessorName = "Mehmet Ozkaya",
            Description = @"You will learn how to secure microservices with using standalone Identity Server 4 and 
            backing with Ocelot API Gateway. We’re going to protect our ASP.NET Web MVC and API applications with using 
            OAuth 2 and OpenID Connect in IdentityServer4. Securing your web application and API with tokens, working 
            with claims, authentication and authorization middlewares and applying policies, and so on.",
            ImageURL = "https://img-c.udemycdn.com/course/480x270/3626722_85b7_4.jpg",
            LastEditionDate = DateTime.Now.AddYears(-2).AddMonths(-9).AddDays(-5)
        }
    };

    public void CourseQueries()
    {
        Field<CourseType>(
            "accounts_getCourseById",
            arguments: new QueryArguments(new QueryArgument<IntGraphType>(){ Name= "Id" }),
            resolve: ctx => {
                var id = ctx.GetArgument<int>("Id");
                return _courses.First(x => x.Id == id);
            }
        ).AuthorizeWith("AuthenticatedUserPolicy");
        
        Field<ListGraphType<CourseType>>(
            "accounts_getAllCourses",
            resolve: ctx => {
                return _courses;
            }
        ).AuthorizeWith("AuthenticatedUserPolicy");
    }
}