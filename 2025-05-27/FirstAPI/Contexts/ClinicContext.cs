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
        public DbSet<Appointment> appointments{ get; set; }
    }
}