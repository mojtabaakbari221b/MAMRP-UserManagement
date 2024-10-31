using System.Data.SqlClient;
using System.Data;
using Dapper;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Repositories;


public class SectionRepository : ISectionRepository
{
    public async Task<Section> Add(Section menu)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Name = menu.Name, Url = menu.Url, Description = menu.Description, GroupId = menu.GroupId, RecordDatetime = DateTime.UtcNow, PersianRecordDatetime = DateTime.Now };
            var result = await connection.QueryFirstOrDefaultAsync<Section>("CreateSection", values, commandType: CommandType.StoredProcedure);
            return result;
        }
    }

    public async void Delete(Section menu)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Id = menu.Id };
            var result = connection.Execute("DeleteSection", values, commandType: CommandType.StoredProcedure);
        }
    }

    public Section GetById(long Id)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Id = Id };
            var menu = connection.QueryFirstOrDefault<Section>("GetByIdSection", values, commandType: CommandType.StoredProcedure);
            return menu;
        }
    }

    public IList<Section> List()
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var menus = connection.Query<Section>("GetAllSection", commandType: CommandType.StoredProcedure).ToList();
            return menus;
        }
    }

    public void Update(Section menu)
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
            var result = connection.Execute("UpdateSection", values, commandType: CommandType.StoredProcedure);
        }
    }
}