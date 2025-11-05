using DevTalk.Application.Dtos.User;
using DevTalk.Application.Service.Interfaces;
using DevTalk.Application.UseCases.User.Command.Create;
using DevTalk.Application.UseCases.User.Query.GetById;
using DevTalk.Domain.BackOffice.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevTalk.Api.Extensions;

public static class MapAuth
{
    public static void UseMapAuth(this WebApplication app)
    {
        app.MapPost("/Register", async (RegisterUser user,IMediator mediator) =>
        {
            if (await mediator.Send(new GetByNameUserQuery(user.Name)) is not null)
                Results.NotFound("User Existing");
            var userCreate = new User(user.Name, user.Password, user.Email);
            var result = await mediator.Send(new CreateUserCommand(userCreate));
            return result ? Results.Created() : Results.BadRequest();
        });
        
        app.MapPost("/Login", async (LoginUser userLogin,IMediator mediator,
            ITokenService tokenService,IConfiguration configuration) =>
        {
            var user = await mediator.Send(new GetByNameUserQuery(userLogin.Name));
            if (user is  null || !user.CheckPassword(user.Password))
                Results.NotFound("User not existing or password is invalid!");

            var claimsUser = tokenService.GetClaimsFromUser(user!);
            var token = tokenService.GenerateAccessToken(claimsUser,configuration);
            
            return Results.Ok(token);
        });
        
        
    }
}