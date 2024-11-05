using DafTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DafTask.Data
{
    public class AppDbContext : IdentityDbContext<UserProfile>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public virtual DbSet<Post> Posts { get; set; }
    }
}
