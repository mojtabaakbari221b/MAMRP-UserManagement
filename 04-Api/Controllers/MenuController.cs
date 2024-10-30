using MediatR;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Dtos.MenuDtos;
using UserManagement.Application.Queries.Handlers;
using FluentValidation;
using Azure.Core;

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
    [Route("")]
    public ActionResult<IList<MenuDto>> GetAll([FromQuery] GetAllMenuQuery command)
    {
        var menus = _mediator.Send(command);
        var menuDtos = menus.Result.Adapt<IList<MenuDto>>();

        return Ok(menuDtos);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<MenuDto>> GetById([FromRoute] GetMenuByIdQuery command)
    {
        var menu = _mediator.Send(command);
        var menuDto = menu.Result.Adapt<MenuDto>();

        return Ok(menuDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] DeleteMenuCommand command)
    {
        var menu = _mediator.Send(command);
        return Ok(true);
    }

    [HttpPut]
    [Route("{id:long}")]
    public async Task<ActionResult<bool>> Update(long Id, [FromBody] UpdateMenuCommand command)
    {
        command.Id = Id;
        await _mediator.Send(command);
        return Ok(true);
    }

    [HttpPost]
    public async Task<ActionResult<MenuDto>> Create([FromBody] AddMenuCommand command) 
    {
        await _mediator.Send(command);
        return Ok();
    }
}

