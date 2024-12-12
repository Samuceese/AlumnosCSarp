using Alumnos.Alumno.Database;
using Alumnos.Alumno.Exceptions;
using Alumnos.Alumno.Mapper;
using CSharpLocalAndRemote.Logger;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.Alumno.repository;

public class AlumnoRepositoryImpl : IAlumnoRepository<long, Alumno>

{
    private readonly Serilog.Core.Logger _logger = LoggerUtils<AlumnoRepositoryImpl>.GetLogger();
    private readonly DbContext _db;

    public AlumnoRepositoryImpl(DbContext dbContext)
    {
        _db = dbContext;
        Init();
    }

    private async void Init()
    {
        _logger.Debug("Inicializando el repositorio local");
        await _db.Database.EnsureCreatedAsync();
        await _db.SaveChangesAsync();
        await _db.RemoveAllAsync();
        await _db.SaveChangesAsync();
    }
    
    public async Task<List<Alumno>> GetAllAsync()
    {
        _logger.Debug("Obteniendo todos los alumnos de la bd");
        try
        {
            var alumnos = await _db.Set<AlumnoEntity>()
                .Select(entity => entity.ToAlumno())
                .ToListAsync();

            return alumnos;
        }
        catch (Exception e)
        {
            _logger.Error(e, "Ocurrió un error al obtener todos los alumnos.");
            throw new AlumnoException.DatabaseError("Ocurrió un error al acceder a la base de datos. Detalle: " + e.Message);
        }
    }


    public async Task<Alumno> GetById(long id)
    {
        _logger.Debug("Buscando alumno por la id " + id);
        try
        {
            var entityAlumno = await _db.Set<AlumnoEntity>().FindAsync(id);

            if (entityAlumno == null)
            {
                _logger.Error($"No se encontró un alumno con la id {id}.");
                throw new AlumnoException.AlumnoNotFound($"No se ha encontrado al alumno por id {id}.");
            }

            return AlumnoMapper.ToAlumno(entityAlumno);
        }
        catch (Exception e)
        {
            _logger.Error(e, $"Ocurrió un error al obtener el alumno con id {id}.");
            throw new AlumnoException.DatabaseError($"Ocurrió un error al acceder a la base de datos. Detalle: {e.Message}");
        }
    }


    public async Task<Alumno> SaveAlumno(Alumno alumno)
    {
        _logger.Debug("Guardando alumno en la base de datos");

        try
        {
            var timeStamp = DateTime.Now.ToString("o");
            var alumnoSave = alumno.ToAlumnoEntity();

            alumnoSave.CreatedAt = timeStamp;
            alumnoSave.UpdatedAt = timeStamp;
            alumnoSave.Id = Alumno.NewId;

            await _db.Set<AlumnoEntity>().AddAsync(alumnoSave);
            await _db.SaveChangesAsync();

            _logger.Debug("Alumno guardado en la base de datos: " + alumnoSave);
            return AlumnoMapper.ToAlumno(alumnoSave);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error al guardar el alumno en la base de datos");
            throw new AlumnoException.DatabaseError($"Ocurrió un error al guardar el alumno en la base de datos. Detalle: {e.Message}");
        }
    }


    public async Task<Alumno> UpdateAlumno(long id, Alumno alumno)
    {
        _logger.Debug("Actualizando el alumno "+ alumno + " con id " + id);
        try
        {
            var alumnoEntity = await _db.Set<AlumnoEntity>().FindAsync(id);

            if (alumno==null)
            {
                _logger.Error($"No se encontró un alumno con la id {id}.");
                throw new AlumnoException.AlumnoNotFound($"No se ha encontrado al alumno por id {id}.");
            }

            var timeStamp = DateTime.Now.ToString("o");
            alumnoEntity.UpdateFrom(alumno.ToAlumnoEntity());
            alumnoEntity.UpdatedAt = timeStamp;

            await _db.SaveChangesAsync();
            return AlumnoMapper.ToAlumno(alumnoEntity);

        }
        catch (Exception e)
        {
            _logger.Error(e, "Error al guardar el alumno en la base de datos");
            throw new AlumnoException.DatabaseError($"Ocurrió un error al guardar el alumno en la base de datos. Detalle: {e.Message}");
        }
    }

    public async Task<Alumno> DeleteById(long id)
    {
        _logger.Debug("Eliminando a un alumno de la bd con id " + id);
        try
        {
            var alumnoDeleted = await _db.Set<AlumnoEntity>().FindAsync(id);

            if (alumnoDeleted == null)
            {
                _logger.Error($"No se encontró un alumno con la id {id}.");
                throw new AlumnoException.AlumnoNotFound($"No se ha encontrado el alumno con id {id}.");
            }

            _db.Set<AlumnoEntity>().Remove(alumnoDeleted);
            await _db.SaveChangesAsync();

            return AlumnoMapper.ToAlumno(alumnoDeleted);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error al guardar el alumno en la base de datos");
            throw new AlumnoException.DatabaseError($"Ocurrió un error al guardar el alumno en la base de datos. Detalle: {e.Message}");
        }
    }

    public Task RemoveAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Alumno>> SaveAllAsync(List<Alumno> alumnos)
    {
        _logger.Debug("Guardando todos los alumnos en la bd");
        try
        {
            var entityList = alumnos.Select(alumno => alumno.ToAlumnoEntity()).ToList();
            var timeStamp = DateTime.Now.ToString("o");

            foreach (var alumno in entityList)
            {
                alumno.CreatedAt = timeStamp;
                alumno.UpdatedAt = timeStamp;
                alumno.Id = Alumno.NewId;
            }

            await _db.Set<AlumnoEntity>().AddRangeAsync(entityList);
            await _db.SaveChangesAsync();
            
            return entityList
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
}

public static class AlumnoEntityExtensions
{
    public static void UpdateFrom(this AlumnoEntity entity, AlumnoEntity model)
    {
        entity.Nombre = model.Nombre;
        entity.Calificacion = model.Calificacion;
        entity.CreatedAt = model.CreatedAt;
        entity.UpdatedAt = model.UpdatedAt;
        entity.IsDeleted = model.IsDeleted;
    }
}