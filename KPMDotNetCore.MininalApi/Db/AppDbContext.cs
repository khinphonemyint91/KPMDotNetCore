using KPMDotNetCore.MininalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KPMDotNetCore.MininalApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
