using Microsoft.EntityFrameworkCore;
using shop_back.Server.Data;

namespace shop_back.Server.Services;
// For Entity Framework
public static class InitDB
{
    public static void Init(WebApplicationBuilder builder)
    {
        switch (builder.Configuration.GetValue<string>("ConnectionStrings:Provider"))
        {
            case "mssql":
            case "sqlserver":
                //builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                break;
            case "sqlite":
                builder.Services.AddDbContext<MainContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
                break;
            case "postgres":
            case "postgresql":
                //builder.Services.AddDbContext<MainContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
                break;
        }
    }
}