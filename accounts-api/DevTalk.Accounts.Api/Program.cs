using ApolloGraphQLFederationExtensions;
using DevTalk.Accounts.Api;
using DevTalk.Accounts.Api.GraphQL;
using GraphQL.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

builder.Services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication("Bearer", options =>
            {
                options.Authority = "https://localhost:4001";
                options.ApiName = "DevTalk.Authorization.ApiAPI";
            });

builder.Services.AddGraphQL(options => {
    options.EnableMetrics = true;
})
.AddSystemTextJson()
.AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = environment.Equals("Development"))
.AddDataLoader()
.AddGraphTypes(typeof(MySchema))
.AddFederation(typeof(MySchema).Assembly)
.AddUserContextBuilder(httpContext => new MyGraphQLUserContext(httpContext.User))
.AddGraphQLAuthorization(options => {
    options.AddPolicy("AuthenticatedUserPolicy", policy => policy.RequireAuthenticatedUser());
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
});

builder.Services.AddSingleton<MySchema>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground("/graphql/playground");
}
app.UseAuthentication();

app.UseGraphQL<MySchema>();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
