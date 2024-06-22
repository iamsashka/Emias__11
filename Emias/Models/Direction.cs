using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Direction
{
    public int IdDirection { get; set; }

    public int IdSpeciality { get; set; }

    public long Oms { get; set; }

    public virtual Speciality IdSpecialityNavigation { get; set; } = null!;

    public virtual Patient OmsNavigation { get; set; } = null!;
}
