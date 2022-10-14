using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {
    public Machine() 
    {
      this.JoinEntities = new HashSet<EngineerMachine>();
    }
    public int MachineId { get; set; }
    public string MachineName { get; set; }
    public string MachineStatus { get; set; }
    [DataType(DataType.Date)]
    public DateTime InstallationDate { get; set; }
    public virtual ICollection<MachineDoctor> JoinEntities { get; } 
  }
}