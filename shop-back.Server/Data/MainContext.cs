using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using shop_back.Server.Entities;

namespace shop_back.Server.Data;

public class MainContext : IdentityDbContext<IdentityUser>
{

#pragma warning disable CS8618 // Required by Entity Framework
    public MainContext(DbContextOptions<MainContext> options) : base(options)
    {
    }
#pragma warning restore CS8618 // Required by Entity Framework
    public DbSet<FeedbackEntity> Feedbacks { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
