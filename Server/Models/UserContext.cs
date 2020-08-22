using Microsoft.EntityFrameworkCore;
using MyBlazor.Shared;
namespace MyBlazor.Server.Models {
    public class UserContext : DbContext {
        public UserContext (DbContextOptions<UserContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
    }
}
