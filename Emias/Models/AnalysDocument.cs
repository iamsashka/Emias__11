using System;
using System.Collections.Generic;

namespace EMIAS.Models;

public partial class AnalysDocument
{
    public int? IdAppointment { get; set; }

    public string Name { get; set; } = null!;

    public string Rtf { get; set; } = null!;

    public virtual Appointment IdAppointmentNavigation { get; set; } = null!;
}
