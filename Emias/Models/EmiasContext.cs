using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6435JQ5\\SQLEXPRESS;Initial Catalog=EMIAS;Integrated Security=True;Trust Server Certificate=True");

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
            entity.HasKey(e => e.IdAppointment).HasName("PK__AnalysDo__ECE24AABE08A34FE");

            entity.ToTable("AnalysDocument");

            entity.Property(e => e.IdAppointment).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAppointmentNavigation).WithOne(p => p.AnalysDocument)
                .HasForeignKey<AnalysDocument>(d => d.IdAppointment)
                .HasConstraintName("FK__AnalysDoc__IdApp__236943A5");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__Appointm__ECE24AABB5A7BEFC");

            entity.Property(e => e.Oms).HasColumnName("OMS");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdDoctor)
                .HasConstraintName("FK__Appointme__IdDoc__1CBC4616");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Appointme__IdSta__1DB06A4F");

            entity.HasOne(d => d.OmsNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Oms)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Appointment__OMS__1BC821DD");
        });

        modelBuilder.Entity<AppointmentDocument>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__Appointm__ECE24AABD03CADD0");

            entity.ToTable("AppointmentDocument");

            entity.Property(e => e.IdAppointment).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAppointmentNavigation).WithOne(p => p.AppointmentDocument)
                .HasForeignKey<AppointmentDocument>(d => d.IdAppointment)
                .HasConstraintName("FK__Appointme__IdApp__208CD6FA");
        });

        modelBuilder.Entity<Direction>(entity =>
        {
            entity.HasKey(e => e.IdDirection).HasName("PK__Directio__7780E2B27A98F88E");

            entity.Property(e => e.Oms).HasColumnName("OMS");

            entity.HasOne(d => d.IdSpecialityNavigation).WithMany(p => p.Directions)
                .HasForeignKey(d => d.IdSpeciality)
                .HasConstraintName("FK__Direction__IdSpe__17F790F9");

            entity.HasOne(d => d.OmsNavigation).WithMany(p => p.Directions)
                .HasForeignKey(d => d.Oms)
                .HasConstraintName("FK__Directions__OMS__18EBB532");
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
            entity.HasKey(e => e.Oms).HasName("PK__Patient__CB396B8B40039D42");

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
            entity.HasKey(e => e.IdAppointment).HasName("PK__Research__ECE24AABB34E59DA");

            entity.ToTable("ResearchDocument");

            entity.Property(e => e.IdAppointment).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdAppointmentNavigation).WithOne(p => p.ResearchDocument)
                .HasForeignKey<ResearchDocument>(d => d.IdAppointment)
                .HasConstraintName("FK__ResearchD__IdApp__2645B050");
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
