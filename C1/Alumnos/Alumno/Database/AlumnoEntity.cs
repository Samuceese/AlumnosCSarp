using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alumnos.Alumno.Database;

[Table("AlumnoEntity")]
public class AlumnoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] 
    [MaxLength(100)] public string Nombre { get; set; } = string.Empty;
    
    [Required] 
    public double Calificacion { get; set; }

    [Required] public string CreatedAt { get; set; } = string.Empty;
    [Required] public string UpdatedAt { get; set; } = string.Empty;

    [DefaultValue(false)] public bool IsDeleted { get; set; }
    
    
    public override string ToString()
    {
        return $"Alumno ID: {Id}, Nombre: {Nombre}, Calificación: {Calificacion}, Creado: {CreatedAt}, Actualizado: {UpdatedAt}, Eliminado: {IsDeleted}";
    }

}