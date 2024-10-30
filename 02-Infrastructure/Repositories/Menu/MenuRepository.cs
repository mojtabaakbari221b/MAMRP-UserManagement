using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using Npgsql;
using Dapper;
namespace UserManagement.Infrastructure.Repositories;


public class MenuRepository : IMenuRepository
{
    private readonly UserManagementDbContext _db;

    public MenuRepository(UserManagementDbContext db)
    {
        _db = db;
    }
    public async void Add(Menu menu)
    {
        _db.Menus.Add(menu);
        await _db.SaveChangesAsync();
        // var connectionString = "Host=localhost;Port=5432;Database=mamrp_user_management;Username=mojtaba;Password=mojtaba3204";
        // using (var connection = new NpgsqlConnection(connectionString)) {
        //     // 'INSERT INTO "Menus" ("Name", "Url", "Description", "GroupId", "RecordDatetime", "PersianRecordDatetime", "RegisteringUser", "UpdateDatetime", "PersianUpdateDatetime", "UpdaterUser", "IsActive") VALUES ('name', '/', 'Description', 2, now(), now(), 2, now(), now(), 1, true)'
        //     var sql = "INSERT INTO \"Menus\" (\"Name\", \"Url\", \"Description\", \"GroupId\", \"RecordDatetime\", \"PersianRecordDatetime\", \"UpdateDatetime\", \"PersianUpdateDatetime\") VALUES ";
        //     var sqlParameter = new {
        //         menu.Name,
        //         menu.Url,
        //         menu.Description,
        //         menu.GroupId,
        //         DateTime.UtcNow,
        //         DateTime.Now,
        //         UpdateDatetime = DateTime.UtcNow,
        //         PersianUpdateDatetime = DateTime.Now,
        //     };
        //     connection.Execute(sql, sqlParameter);
        // }
    }
}