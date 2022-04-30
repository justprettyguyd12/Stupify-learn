using Microsoft.EntityFrameworkCore;

namespace Stupify.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Song> Songs { get; set; }
    
    public DbSet<Artist> Artists { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
}