using Microsoft.EntityFrameworkCore;
using proyectojcortes.Models;

namespace proyectojcortes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Criatura> Criaturas { get; set; }
        public DbSet<Registro> Registros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Criatura>().ToTable("Criaturas");
            modelBuilder.Entity<Registro>().ToTable("Registros");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Criatura>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Registro>().HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
