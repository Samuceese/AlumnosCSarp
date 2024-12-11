namespace Alumnos.Alumno;

public class Alumno
{
    private long id;
    private string nombre { get; set; } 
    private double calificacion { get; set; }
    private DateTime? createdAt = DateTime.Now;
    private DateTime? updatedAt = DateTime.Now;
    private Boolean isDeleted = false;

    private static long contador = 0;

    public Alumno(string nombre, double calificacion, DateTime createdAt, DateTime updatedAt, Boolean isDeleted)
    {
        contador++;
        id = contador;
        this.nombre = nombre;
        this.calificacion = calificacion;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
        this.isDeleted = isDeleted;
    }

    public override string ToString()
    {
        return $"Alumno ID: {id}, Nombre: {nombre}, Calificación: {calificacion}, Creado: {createdAt}, Actualizado: {updatedAt}, Eliminado: {isDeleted}";
    }
    
}