﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class StatusType
{
    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
