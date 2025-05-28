using Microsoft.EntityFrameworkCore;
using login_api_iw_js.Models;
using login_api_iw_js.LoginApi_Models;
namespace login_api_iw_js.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tema> Tema { get; set; }
        public DbSet<UsuarioObjetivo> UsuarioObjetivo { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }
        public DbSet<Hito> Hito { get; set; }
        public DbSet<Progreso> Progreso { get; set; }
        public DbSet<Subtema> Subtema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tema>()
                .HasMany(t => t.Objetivos)
                .WithMany(o => o.Temas)
                .UsingEntity(j => j.ToTable("TemaObjetivo"));
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioObjetivo>()
                .HasKey(uo => new { uo.UsuarioId, uo.ObjetivoId });

            // Corregido: Relación UsuarioObjetivo <-> Usuario
            modelBuilder.Entity<UsuarioObjetivo>()
                .HasOne<Usuario>()
                .WithMany(u => u.UsuarioObjetivos)
                .HasForeignKey(uo => uo.UsuarioId);

            // Corregido: Relación UsuarioObjetivo <-> Objetivo
            modelBuilder.Entity<UsuarioObjetivo>()
                .HasOne<Objetivo>()
                .WithMany(o => o.UsuarioObjetivos)
                .HasForeignKey(uo => uo.ObjetivoId);

            modelBuilder.Entity<Progreso>()
                .HasOne(p => p.Hito)
                .WithMany(uo => uo.Progresos)
                .HasForeignKey(p => p.HitoId);

            modelBuilder.Entity<Progreso>()
                .HasOne(p => p.UsuarioObjetivo)
                .WithMany()
                .HasForeignKey(p => new { p.UsuarioId, p.ObjetivoId })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
