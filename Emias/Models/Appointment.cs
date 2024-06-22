using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Appointment
{
    public int IdAppointment { get; set; }

    public long? Oms { get; set; }

    public int IdDoctor { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly AppointmentTime { get; set; }

    public int? IdStatus { get; set; }

    public virtual AnalysDocument? AnalysDocument { get; set; }

    public virtual AppointmentDocument? AppointmentDocument { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual StatusType? IdStatusNavigation { get; set; }

    public virtual Patient? OmsNavigation { get; set; }

    public virtual ResearchDocument? ResearchDocument { get; set; }
}
