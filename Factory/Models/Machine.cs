// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace Factory.Models
// {
//   public class Patient
//   {
//     public Patient() 
//     {
//       this.JoinEntities = new HashSet<PatientDoctor>();
//     }
//     public int PatientId { get; set; }
//     public string PatientName { get; set; }
//     [DataType(DataType.Date)]
//     public DateTime AppointmentDate { get; set; }
//     public virtual ICollection<PatientDoctor> JoinEntities { get; } 
//   }
// }