using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ResearchDocument
{
    public int IdAppointment { get; set; }

    public string Name { get; set; } = null!;

    public string Rtf { get; set; } = null!;

    public byte[]? Attachment { get; set; }

    public virtual Appointment IdAppointmentNavigation { get; set; } = null!;
}
