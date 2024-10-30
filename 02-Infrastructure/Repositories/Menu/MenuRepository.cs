using UserManagement.Domain.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Data;

namespace UserManagement.Infrastructure.Repositories;


public class MenuRepository : IMenuRepository
{
    public async void Add(Menu menu)
    {
        var connectionString = "Server=localhost,1433;Database=mamrp_user_management;User Id=sa; Password=123456;Encrypt=True;TrustServerCertificate=True;";
        using (var connection = new SqlConnection(connectionString))
        {
            var values = new { Name = menu.Name, Url = menu.Url, Description = menu.Description, GroupId = menu.GroupId };
            var result = connection.Execute("CreateMenu", values, commandType: CommandType.StoredProcedure);
        }
    }
}