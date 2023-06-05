using System;
using System.Collections.Generic;

namespace ProyectoDefinitivo.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Rut { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Edad { get; set; }

    public DateOnly? Fechanacimiento { get; set; }

    public virtual ICollection<AsignaturaEstudiante> AsignaturaEstudiantes { get; set; } = new List<AsignaturaEstudiante>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
