namespace Alumnos.Alumno.repository;

public interface IAlumnoRepository<Long , Alumno>
{
    Task<List<Alumno>> GetAllAsync();
    Task<Alumno> GetById(Long id);
    Task<Alumno> SaveAlumno(Alumno alumno);
    Task<Alumno> UpdateAlumno(Long id, Alumno alumno);
    Task<Alumno> DeleteById(Long id);
    Task RemoveAllAsync();
    Task<List<Alumno>> SaveAllAsync(List<Alumno> alumnos);

}