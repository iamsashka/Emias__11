using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EMIAS.Models;

public partial class EmiasContext : DbContext
{
    public EmiasContext()
    {
    }

    public EmiasContext(DbContextOptions<EmiasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AnalysDocument> AnalysDocuments { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentDocument> AppointmentDocuments { get; set; }

    public virtual DbSet<Direction> Directions { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<ResearchDocument> ResearchDocuments { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<StatusType> StatusTypes { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.IdAdmin).HasName("PK__Admin__4C3F97F429E4A142");

            entity.ToTable("Admin");

            entity.Property(e => e.EnterPassword).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<AnalysDocument>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__AnalysDo__ECE24AABCB743924");

            entity.ToTable("AnalysDocument");

            entity.Property(e => e.IdAppointment).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAppointmentNavigation).WithOne(p => p.AnalysDocument)
                .HasForeignKey<AnalysDocument>(d => d.IdAppointment)
                .HasConstraintName("FK__AnalysDoc__IdApp__72C60C4A");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__Appointm__ECE24AABC1B9AA74");

            entity.Property(e => e.Oms).HasColumnName("OMS");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK__Appointme__IdDoc__571DF1D5");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Appointme__IdSta__5812160E");

            entity.HasOne(d => d.OmsNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Oms)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Appointment__OMS__5629CD9C");
        });

        modelBuilder.Entity<AppointmentDocument>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__Appointm__ECE24AAB04CC3B7A");

            entity.ToTable("AppointmentDocument");

            entity.Property(e => e.IdAppointment).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAppointmentNavigation).WithOne(p => p.AppointmentDocument)
                .HasForeignKey<AppointmentDocument>(d => d.IdAppointment)
                .HasConstraintName("FK__Appointme__IdApp__6FE99F9F");
        });

        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.IdDirection).HasName("PK__Directio__7780E2B286732134");

            entity.Property(e => e.Oms).HasColumnName("OMS");

            entity.HasOne(d => d.IdSpecialityNavigation).WithMany(p => p.Directions)
                .HasForeignKey(d => d.IdSpeciality)
                .HasConstraintName("FK__Direction__IdSpe__52593CB8");

            entity.HasOne(d => d.OmsNavigation).WithMany(p => p.Directions)
                .HasForeignKey(d => d.Oms)
                .HasConstraintName("FK__Directions__OMS__534D60F1");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__Doctor__F838DB3E4046CEF2");

            entity.ToTable("Doctor");

            entity.Property(e => e.EnterPassword).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.WorkAddress).HasMaxLength(50);

            entity.HasOne(d => d.IdSpecialityNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdSpeciality)
                .HasConstraintName("FK__Doctor__IdSpecia__4D94879B");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Oms).HasName("PK__Patient__CB396B8BA0EB1454");

            entity.ToTable("Patient");

            entity.Property(e => e.Oms).HasColumnName("OMS");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LivingAddress).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Nickname).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(18);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<ResearchDocument>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__Research__ECE24AAB5797AE42");

            entity.ToTable("ResearchDocument");

            entity.Property(e => e.IdAppointment).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAppointmentNavigation).WithOne(p => p.ResearchDocument)
                .HasForeignKey<ResearchDocument>(d => d.IdAppointment)
                .HasConstraintName("FK__ResearchD__IdApp__75A278F5");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.IdSpeciality).HasName("PK__Speciali__5C8D4E68E447C562");

            entity.Property(e => e.ImagePath).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<StatusType>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__StatusTy__B450643A35EA7235");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
