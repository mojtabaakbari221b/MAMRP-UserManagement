using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentResults;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Application.Queries.Sections;

namespace UserManagement.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<Result<IList<MenuDto>>> GetAll([FromQuery] GetAllMenuQuery command)
    {
        var SectionDtos = await _mediator.Send(command);
        return Result.Ok(SectionDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<Result<MenuDto>> GetById([FromRoute] GetMenuByIdQuery command)
    {
        var menuDto = await _mediator.Send(command);
        return Result.Ok(menuDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<Result<bool>> Delete([FromRoute] DeleteMenuCommand command)
    {
        var menu = _mediator.Send(command);
        return Result.Ok(true);
    }

    [HttpPut("{id:long}")]
    public async Task<Result<bool>> Update(long Id, [FromBody] UpdateMenuCommand command)
    {
        command.Id = Id;
        await _mediator.Send(command);
        return Result.Ok(true);
    }

    [HttpPost]
    public async Task<Result<MenuDto>> Create([FromBody] AddMenuCommand command) 
    {
        var menuDto = await _mediator.Send(command);
        return Result.Ok(menuDto);
    }
}

