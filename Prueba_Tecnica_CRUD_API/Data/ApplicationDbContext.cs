using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica_CRUD_API.Models;

namespace Prueba_Tecnica_CRUD_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación muchos-a-muchos: Curso <-> Alumno
            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Alumnos)
                .WithMany(a => a.Cursos);

            // Relación uno-a-muchos: Profesor -> Cursos
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Profesor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(c => c.ProfesorId);
        }
    }
}
