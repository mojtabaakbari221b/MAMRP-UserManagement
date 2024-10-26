using Bogus;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Dtos;
using UserManagement.Application.Dtos.MenuDtos;


namespace UserManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<MenuDto>> ListAsync()
        {
            var menuId = 1;

            var menuFaker = new Faker<MenuDto>("fa")
                .RuleFor(u => u.Id, _ => menuId++)
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Url, f => f.Internet.UrlRootedPath())
                .RuleFor(u => u.GroupId, f => f.Random.Int())
                .RuleFor(u => u.Description, f => f.Lorem.Paragraph());

            var menus = menuFaker.Generate(20);

            return Ok(menus);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<MenuDto>> GetByIdAsync(int Id)
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
        public async Task<ActionResult<bool>> DeleteByIdAsync(int Id)
        {
            return Ok(true);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<ActionResult<MenuDto>> UpdateByIdAsync(int Id, UpdateMenuDto request)
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
        public async Task<ActionResult<MenuDto>> CreateAsync(int Id, CreateMenuDto request) 
        {
            Faker faker = new Faker();
            MenuDto menuDto = new MenuDto
            {
                Id = faker.Random.Long(2000,20000000),
                Name = request.Name,
                Url = request.Url,
                Description = request.Description,
                GroupId = request.GroupId,
            };
            return Ok(menuDto);
        }
    }
}
