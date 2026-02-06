using AstronautCms.Modules.Users.Core.Commands.CreateUser;
using AstronautCms.Modules.Users.Core.Commands.LogInUser;
using AstronautCms.Modules.Users.Core.Dtos;
using AstronautCms.Modules.Users.Core.Errors;
using AstronautCms.Shared.Abstract.Result.CustomErrors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AstronautCms.Modules.Users.Api.Routes;

public static class UserEndpoints
{
    internal static RouteGroupBuilder MapUserEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", () => "User Module is working!");
        
        group.MapPost("/register", CreateUser)
            .WithName("CreateUser")
            .WithSummary("Register a new user")
            .WithDescription("Creates a new user with the provided email and password.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        group.MapPost("/login", LoginUser)
            .WithName("LoginUser")
            .WithSummary("Log in a user")
            .WithDescription("Logs in a user with the provided email and password.")
            .Produces<TokenDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        return group.WithTags("users");
    }
    
    private static async Task<IResult> LoginUser([FromBody] LogInUserDto request, [FromServices] LogInUserUseCase useCase, CancellationToken ct)
    {
        var res = await useCase.Execute(request, ct);
        if (res.IsSuccess)
        {
            return Results.Ok(res.Value);
        }
        return res.Error switch
        {
            UserExistsError e => Results.Conflict(new{e.Type, e.Message}),
            ValidationError e => Results.BadRequest(new {e.Type, e.Message, e.Errors }),
            InvalidCredentialsError e => Results.BadRequest(new {e.Type, e.Message}),
            BaseError e  => Results.BadRequest(new {e.Type, e.Message}),
            _ => Results.InternalServerError("An unexpected error occurred.")
        };
    }
    
    private static async Task<IResult> CreateUser([FromBody] CreateUserDto request, [FromServices] CreateUserUseCase useCase, CancellationToken ct)
    {
        var res = await useCase.Execute(request, ct);

        if (res.IsSuccess)
        {
            return Results.NoContent();
        }
        return res.Error switch
        {
            UserExistsError e => Results.Conflict(new{e.Type, e.Message}),
            ValidationError e => Results.BadRequest(new {e.Type, e.Message, e.Errors }),
            BaseError e  => Results.BadRequest(new {e.Type, e.Message}),
            _ => Results.InternalServerError("An unexpected error occurred.")
        };
    }
}