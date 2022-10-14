// using Microsoft.EntityFrameworkCore;

// namespace Factory.Models
// {
//   public class FactoryContext : DbContext
//   {
//     public DbSet<Patient> Patients { get; set; }
//     public DbSet<Doctor> Doctors { get; set; }

//     public DbSet<PatientDoctor> PatientDoctor { get; set; }

//     public FactoryContext(DbContextOptions options) : base(options) { }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//       optionsBuilder.UseLazyLoadingProxies();
//     }
//   }
// }