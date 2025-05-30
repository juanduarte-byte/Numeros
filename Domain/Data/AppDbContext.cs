using Microsoft.EntityFrameworkCore;
using ParImparAPI.Domain.Entities;

namespace ParImparAPI.Domain.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Numero> Numeros { get; set; }
    }
}
