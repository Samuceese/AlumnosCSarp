namespace Alumnos.Alumno.Exceptions;

public abstract class AlumnoException : Exception
{
    protected AlumnoException(string message) : base(message) { }

    public class AlumnoNotFound : AlumnoException
    {
        public AlumnoNotFound(string message) : base(message) { }
    }

    public class DatabaseError : AlumnoException
    {
        public DatabaseError(string message) : base(message) { }
    }
}