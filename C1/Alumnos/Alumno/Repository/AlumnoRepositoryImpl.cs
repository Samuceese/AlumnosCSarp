using CSharpLocalAndRemote.Logger;

namespace Alumnos.Alumno.repository;

public class AlumnoRepositoryImpl : IAlumnoRepository<long, Alumno>

{
    private readonly Serilog.Core.Logger _logger = LoggerUtils<AlumnoRepositoryImpl>.GetLogger();

    
    public Task<List<Alumno>> GetAllAsync()
    {
        _logger.Debug("Obteniendo todos los alumnos de la bd");
        throw new NotImplementedException();
    }

    public Task<Alumno> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Alumno> SaveAlumno(Alumno alumno)
    {
        throw new NotImplementedException();
    }

    public Task<Alumno> UpdateAlumno(long id, Alumno alumno)
    {
        throw new NotImplementedException();
    }

    public Task<Alumno> DeleteById(long id)
    {
        throw new NotImplementedException();
    }
    
}