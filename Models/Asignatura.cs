using System;
using System.Collections.Generic;

namespace ProyectoDefinitivo.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Codigo { get; set; }

    public DateOnly? Fechaactualizacion { get; set; }

    public virtual ICollection<AsignaturaEstudiante> AsignaturaEstudiantes { get; set; } = new List<AsignaturaEstudiante>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
