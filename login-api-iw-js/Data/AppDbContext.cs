using Microsoft.EntityFrameworkCore;
using login_api_iw_js.Models;
namespace login_api_iw_js.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        } 
        public DbSet<Tema> Tema { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }
        public DbSet<Hito> Hito { get; set; }
        public DbSet<Progreso> Progreso { get; set; }
        public DbSet<Recomendacion> Recomendacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tema>().ToTable("Tema");
            modelBuilder.Entity<Objetivo>().ToTable("Objetivo");
            modelBuilder.Entity<Hito>().ToTable("Hito");
            modelBuilder.Entity<Progreso>().ToTable("Progreso");
        }
    }
}
