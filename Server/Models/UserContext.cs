using Microsoft.EntityFrameworkCore;
using MyBlazor.Shared;
namespace MyBlazor.Server.Models {
    public class AppDbContext : DbContext {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
    }
}
