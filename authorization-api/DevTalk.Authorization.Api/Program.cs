using System.IdentityModel.Tokens.Jwt;
using ApolloGraphQLFederationExtensions;
using DevTalk.Authorization.Api;
using DevTalk.Authorization.Api.Auth;
using DevTalk.Authorization.Api.GraphQL;
using GraphQL.Server;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using static IdentityModel.OidcConstants;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

// WARNING: REMOVE THE OPTION "TrustServerCertificate=True". IT'S FOR TESTING PURPOSES ONLY!!
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=.;Database=DevTalkApolloFederation;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    {
        options.ApiResources.Add(new ApiResource("AccountsApi"));
        options.ApiScopes.Add(new ApiScope("AccountsApi"));
        options.Clients.Add(new Client
        {
            ClientName = "Client Application1",
            ClientId = "t8agr5xKt4$3",
            AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("eb300de4-add9-42f4-a3ac-abd3c60f1919".Sha256()) },
            AllowedScopes = {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                "role",
                "DevTalk.Authorization.ApiAPI"
            }
        });
        options.Clients.Add(new Client
        {
            ClientName = "oidc-pkce",
            ClientId = "t8agr5xKt4$4",
            AllowedGrantTypes = IdentityServer4.Models.GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            AllowedScopes = {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                "role",
                "DevTalk.Authorization.ApiAPI",
                "AccountsApi"
            },
            RedirectUris = {
                "http://localhost:3000",
            },
            PostLogoutRedirectUris = {
                "http://localhost:3000"
            },
            AllowedCorsOrigins = {
                "http://localhost:3000"
            }
        });
        options.IdentityResources.Add(new IdentityResource()
        {
            Name = "role",
            DisplayName = "Role",
            UserClaims = { JwtClaimTypes.Role }
        });
    })
    .AddProfileService<MyProfileService>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication()
    .AddIdentityServerJwt()
    .AddOpenIdConnect(options =>
    {
        options.RequireHttpsMetadata = false;
        options.ClientId = "DevTalk.Authorization.ApiAPI";
        options.Authority = "http://localhost:5000";
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000");
        });
});

builder.Services.AddSingleton<MySchema>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground("/graphql/playground");
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();

app.UseCors();

app.UseIdentityServer();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.UseGraphQL<MySchema>();

app.Run();
