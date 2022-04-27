using Microsoft.EntityFrameworkCore;

namespace Stupify;

public class ApplicationContext : DbContext
{
    public DbSet<Song> Songs { get; set; }

    public ApplicationContext()
    {
        
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        
    }
}