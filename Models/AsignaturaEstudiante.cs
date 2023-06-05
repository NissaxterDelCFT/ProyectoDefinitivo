using System;
using System.Collections.Generic;

namespace ProyectoDefinitivo.Models;

public partial class AsignaturaEstudiante
{
    public int Id { get; set; }

    public int Asignaturaid { get; set; }

    public int Estudiantesid { get; set; }

    public DateOnly? Fecharegistro { get; set; }

    public virtual Asignatura Asignatura { get; set; } = null!;

    public virtual Estudiante Estudiantes { get; set; } = null!;
}
