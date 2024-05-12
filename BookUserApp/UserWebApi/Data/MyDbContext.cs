using Microsoft.EntityFrameworkCore;
using UserWebApi.Models;

namespace UserWebApi.Data;

public class MyDbContext(DbContextOptions<MyDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }
}
