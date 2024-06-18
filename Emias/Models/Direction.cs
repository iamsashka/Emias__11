using System;
using System.Collections.Generic;

namespace EMIAS.Models;

public partial class Direction
{
    public int? IdDirection { get; set; }

    public int? IdSpeciality { get; set; }

    public long Oms { get; set; }

    public virtual Speciality IdSpecialityNavigation { get; set; } = null!;

    public virtual Patient OmsNavigation { get; set; } = null!;
}
