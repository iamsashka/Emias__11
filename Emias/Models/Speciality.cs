using System;
using System.Collections.Generic;

namespace EMIAS.Models;

public partial class Speciality
{
    public int? IdSpeciality { get; set; }

    public string Name { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public virtual ICollection<Direction> Directions { get; set; } = new List<Direction>();

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
