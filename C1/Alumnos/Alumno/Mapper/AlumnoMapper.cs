using System.Globalization;
using Alumnos.Alumno.Database;

namespace Alumnos.Alumno.Mapper;

public static class AlumnoMapper
{
    public static Alumno ToAlumno(this AlumnoEntity entity)
    {
        return new Alumno(
            entity.Nombre,
            entity.Calificacion,
            DateTime.Parse(entity.CreatedAt, null, DateTimeStyles.RoundtripKind),
            DateTime.Parse(entity.UpdatedAt, null, DateTimeStyles.RoundtripKind),
            entity.IsDeleted,
            entity.Id);
    }

    public static AlumnoEntity ToAlumnoEntity(this Alumno alumno)
    {
        return new AlumnoEntity
        {
            Id = alumno.Id,
            Nombre = alumno.Nombre,
            Calificacion = alumno.Calificacion,
            CreatedAt = alumno.CreatedAt.ToString("o"),
            UpdatedAt = alumno.UpdatedAt.ToString("o"),
            IsDeleted = alumno.IsDeleted
        };
    }
}