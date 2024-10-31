using System.Data.SqlClient;
using System.Data;
using Dapper;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Repositories;


public class MenuRepository : IMenuRepository
{
    public async void Add(Menu menu)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Name = menu.Name, Url = menu.Url, Description = menu.Description, GroupId = menu.GroupId, RecordDatetime = DateTime.UtcNow, PersianRecordDatetime = DateTime.Now };
            var result = connection.Execute("CreateMenu", values, commandType: CommandType.StoredProcedure);
        }
    }

    public async void Delete(Menu menu)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Id = menu.Id };
            var result = connection.Execute("DeleteMenu", values, commandType: CommandType.StoredProcedure);
        }
    }

    public Menu GetById(long Id)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Id = Id };
            var menu = connection.QueryFirstOrDefault<Menu>("GetByIdMenu", values, commandType: CommandType.StoredProcedure);
            return menu;
        }
    }

    public IList<Menu> List()
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var menus = connection.Query<Menu>("GetAllMenu", commandType: CommandType.StoredProcedure).ToList();
            return menus;
        }
    }

    public void Update(Menu menu)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new {
                Id = menu.Id,
                Name = menu.Name,
                Url = menu.Url,
                Description = menu.Description,
                GroupId = menu.GroupId,
                UpdateDatetime = DateTime.UtcNow,
                PersianUpdateDatetime = DateTime.Now,
            };
            var result = connection.Execute("UpdateMenu", values, commandType: CommandType.StoredProcedure);
        }
    }
}