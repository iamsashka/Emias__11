using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string EnterPassword { get; set; } = null!;

    public string WorkAddress { get; set; } = null!;

    public int IdSpeciality { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Speciality IdSpecialityNavigation { get; set; } = null!;
}
