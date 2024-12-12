using Alumnos.Alumno.Database;
using CSharpLocalAndRemote.Logger;
using Microsoft.EntityFrameworkCore;


public class AlumnosDbContext : DbContext
{
    private readonly Serilog.Core.Logger _logger = LoggerUtils<AlumnosDbContext>.GetLogger();

    public AlumnosDbContext(DbContextOptions<AlumnosDbContext> options) : base(options)
    {
    }

    public virtual DbSet<AlumnoEntity>
        Alumno { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlumnoEntity>(entity =>
        {
            entity.Property(e => e.CreatedAt).IsRequired()
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt).IsRequired()
                .ValueGeneratedOnUpdate();
        });
    }
}

public static class DbContextExtensions
{
    private static readonly Serilog.Core.Logger _logger = LoggerUtils<AlumnosDbContext>.GetLogger();

    public static async Task RemoveAllAsync(this DbContext context)
    {
        _logger.Debug("Borrando todos los tenistas locales en bd");
        context.Set<AlumnoEntity>()
            .RemoveRange(context
                .Set<AlumnoEntity>());
        _logger.Debug("Reseteando el contador de la tabla TenistaEntity");
        await context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name = 'AlumnoEntity'");
        _logger.Debug("Borrados todos los tenistas locales en bd y reseteado el contador de la tabla AlumnoEntity");
    }
}