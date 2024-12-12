namespace Alumnos.Alumno;
public class Alumno
{
    public const long NewId = 0;
    private static long contador = NewId;

    // Constructor
    public Alumno(
        string nombre,
        double calificacion,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        bool isDeleted = false,
        long id = NewId
    )
    {
        Id = id == NewId ? ++contador : id; 
        Nombre = nombre;
        Calificacion = calificacion;
        CreatedAt = createdAt ?? DateTime.Now; 
        UpdatedAt = updatedAt ?? DateTime.Now;
        IsDeleted = isDeleted;
    }

    // Propiedades
    public long Id { get; set; }
    public string Nombre { get; set; }
    public double Calificacion { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public override string ToString()
    {
        return $"Alumno(Id: {Id}, Nombre: {Nombre}, Calificación: {Calificacion}, " +
               $"Created At: {CreatedAt:yyyy-MM-dd}, Updated At: {UpdatedAt:yyyy-MM-dd}, Is Deleted: {IsDeleted})";
    }
}