using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2020RJ601.Models
{
    public class blogContext : DbContext
    {
        public blogContext(DbContextOptions<blogContext> options) : base(options) 
        {

        }

        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
    }
}
