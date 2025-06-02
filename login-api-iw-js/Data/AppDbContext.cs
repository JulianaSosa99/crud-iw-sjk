using Microsoft.EntityFrameworkCore;
using login_api_iw_js.Models;
using login_api_iw_js.LoginApi_Models;

namespace login_api_iw_js.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tema> Tema { get; set; }
        public DbSet<UsuarioObjetivo> UsuarioObjetivo { get; set; }
        public DbSet<Objetivo> Objetivo { get; set; }
        public DbSet<Hito> Hito { get; set; }
        public DbSet<Progreso> Progreso { get; set; }
        public DbSet<Subtema> Subtema { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabla intermedia TemaObjetivo
            modelBuilder.Entity<Tema>()
                .HasMany(t => t.Objetivos)
                .WithMany(o => o.Temas)
                .UsingEntity(j => j.ToTable("TemaObjetivo"));

            // Clave compuesta para UsuarioObjetivo
            modelBuilder.Entity<UsuarioObjetivo>()
                .HasKey(uo => new { uo.UsuarioId, uo.ObjetivoId });

            // Relaciones de UsuarioObjetivo
            modelBuilder.Entity<UsuarioObjetivo>()
                .HasOne(uo => uo.Usuario)
                .WithMany(u => u.UsuarioObjetivos)
                .HasForeignKey(uo => uo.UsuarioId);

            modelBuilder.Entity<UsuarioObjetivo>()
                .HasOne(uo => uo.Objetivo)
                .WithMany(o => o.UsuarioObjetivos)
                .HasForeignKey(uo => uo.ObjetivoId);

            // Relación Hito -> Progreso
            modelBuilder.Entity<Progreso>()
                .HasOne(p => p.Hito)
                .WithMany(h => h.Progresos)
                .HasForeignKey(p => p.HitoId);

            // Relación compuesta: Progreso -> UsuarioObjetivo
            modelBuilder.Entity<Progreso>()
                .HasOne(p => p.UsuarioObjetivo)
                .WithMany(uo => uo.Progresos)
                .HasForeignKey(p => new { p.UsuarioId, p.ObjetivoId })
                .HasPrincipalKey(uo => new { uo.UsuarioId, uo.ObjetivoId })
                .OnDelete(DeleteBehavior.Restrict); // para evitar ciclo o múltiples cascadas
        }


































    }
}
