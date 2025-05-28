using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FirstAPI.Contexts
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }

        public DbSet<Speciality> specialities { get; set; }
        public DbSet<DoctorSpeciality> doctorSpecialities { get; set; }
        public DbSet<Appointment> appointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentNumber).HasName("PK_AppointmentNumber");

            modelBuilder.Entity<Appointment>()
                        .HasOne(a => a.Patient)
                        .WithMany(p => p.Appointments)
                        .HasForeignKey(a => a.PatientId)
                        .HasConstraintName("FK_Appointment_Patient")
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                        .HasOne(a => a.Doctor)
                        .WithMany(d => d.Appointments)
                        .HasForeignKey(a => a.DoctorId)
                        .HasConstraintName("FK_Appointment_Doctor")
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorSpeciality>().HasKey(ds => ds.SerialNumber).HasName("PK_DoctorSpeciality");
            modelBuilder.Entity<DoctorSpeciality>()
                        .HasOne(ds => ds.Doctor)
                        .WithMany(d => d.DoctorSpecialities)
                        .HasForeignKey(ds => ds.DoctorId)
                        .HasConstraintName("FK_DoctorSpeciality_Doctor")
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DoctorSpeciality>()
                        .HasOne(ds => ds.Speciality)
                        .WithMany(s => s.DoctorSpecialities)
                        .HasConstraintName("FK_DoctorSpeciality_Speciality")
                        .OnDelete(DeleteBehavior.Restrict);

                        modelBuilder.Entity<User>().HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                        .HasOne(u => u.UserFollower)
                        .WithMany(us => us.Followers)
                        .HasForeignKey(u => u.FollwerId)
                        .HasConstraintName("FK_Followers")
                        .OnDelete(DeleteBehavior.Restrict);

        }
        
    }
}