using System.Security.Claims;
using ApolloGraphQLFederationExtensions;
using GraphQL;
using GraphQL.Types;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using GraphQL.MicrosoftDI;

namespace DevTalk.Authorization.Api.GraphQL;

public partial class AppQueries : FederatedQuery
{
    public void AspNetUserQueries()
    {
        Field<ListGraphType<StringGraphType>>()
        .Name("authorization_getRoles")
        .Resolve()
        .WithScope()
        .WithService<RoleManager<IdentityRole>>()
        .Resolve((context, _roleManager) =>
        {
            return _roleManager.Roles.Select(x => x.Name).ToList();
        });

        Field<BooleanGraphType>()
        .Name("authorization_addRole")
        .Argument<StringGraphType>("Name")
        .Resolve()
        .WithScope()
        .WithService<RoleManager<IdentityRole>>()
        .ResolveAsync(async (context, _roleManager) =>
        {
            var name = context.GetArgument<string>("Name");
            var result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = name
            });
            return result.Succeeded;
        });

        Field<BooleanGraphType>()
        .Name("authorization_addRoleToUser")
        .Argument<StringGraphType>("Id")
        .Argument<StringGraphType>("Role")
        .Resolve()
        .WithScope()
        .WithServices<RoleManager<IdentityRole>, UserManager<ApplicationUser>>()
        .ResolveAsync(async (context, _roleManager, _userManager) =>
        {
            var id = context.GetArgument<string>("Id");
            var role = context.GetArgument<string>("Role");
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        });
        
        Field<BooleanGraphType>()
        .Name("authorization_createUser")
        .Argument<StringGraphType>("UserName")
        .Argument<StringGraphType>("Email")
        .Argument<StringGraphType>("Password")
        .Argument<StringGraphType>("Role")
        .Resolve()
        .WithScope()
        .WithServices<RoleManager<IdentityRole>, UserManager<ApplicationUser>>()
        .ResolveAsync(async (context, _roleManager, _userManager) =>
        {
            var userName = context.GetArgument<string>("UserName");
            var email = context.GetArgument<string>("Email");
            var password = context.GetArgument<string>("Password");
            var role = context.GetArgument<string>("Role");
            
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    context.Errors.Add(new ExecutionError($"{error.Code}: {error.Description}"));

            result = await _userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(JwtClaimTypes.PreferredUserName, userName),
                        new Claim(JwtClaimTypes.Email, email),
                    });
            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    context.Errors.Add(new ExecutionError($"{error.Code}: {error.Description}"));

            result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    context.Errors.Add(new ExecutionError($"{error.Code}: {error.Description}"));

            return result.Succeeded;            
        });
    }
}