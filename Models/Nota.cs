using System;
using System.Collections.Generic;

namespace ProyectoDefinitivo.Models;

public partial class Nota
{
    public int Id { get; set; }

    public float Ponderacion { get; set; }

    public float Calificacion { get; set; }

    public DateOnly? Fecharegistro { get; set; }

    public int Estudiantesid { get; set; }

    public int Asignaturaid { get; set; }

    public virtual Asignatura Asignatura { get; set; } = null!;

    public virtual Estudiante Estudiantes { get; set; } = null!;
}
