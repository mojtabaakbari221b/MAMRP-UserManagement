using Bogus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.Handlers;
using UserManagement.Application.Dtos;
using UserManagement.Application.Dtos.MenuDtos;

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
    public async Task<ActionResult<MenuDto>> GetAll()
    {
        // var menus = _mediator.Send(ListMenuCommand)
        var menuId = 1;

        var menuFaker = new Faker<MenuDto>("fa")
            .RuleFor(u => u.Id, _ => menuId++)
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Url, f => f.Internet.UrlRootedPath())
            .RuleFor(u => u.GroupId, f => f.Random.UInt())
            .RuleFor(u => u.Description, f => f.Lorem.Paragraph());

        var menus = menuFaker.Generate(20);

        return Ok(menus);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<MenuDto>> GetById(int Id)
    {
        var menuFaker = new Faker<MenuDto>("fa")
            .RuleFor(u => u.Id, _ => Id)
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Url, f => f.Internet.UrlRootedPath())
            .RuleFor(u => u.GroupId, f => f.Random.Int())
            .RuleFor(u => u.Description, f => f.Lorem.Paragraph());

        var menus = menuFaker.Generate();

        return Ok(menus);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<bool>> Delete(int Id)
    {
        return Ok(true);
    }

    [HttpPut]
    [Route("{id:long}")]
    public async Task<ActionResult<MenuDto>> Update(int Id, UpdateMenuDto request)
    {
        MenuDto menuDto = new MenuDto
        {
            Id = Id,
            Name = request.Name,
            Url = request.Url,
            Description = request.Description,
            GroupId = request.GroupId,
        };
        return Ok(menuDto);
    }

    [HttpPost]
    public async Task<ActionResult<MenuDto>> Create(AddMenuCommand command) 
    {
        await _mediator.Send(command);
        return Ok();
    }
}

