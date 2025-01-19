using Microservices.Service1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Service1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

}
