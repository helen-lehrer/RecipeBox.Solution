using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View( _db.Machines.ToList().OrderBy(model => model.MachineName).ToList());
    }

//     public ActionResult Create()
//     {
//       ViewBag.DoctorId = new SelectList( _db.Doctors, "DoctorId", "DoctorName");
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Create(Machine Machine, int DoctorId)
//     {
//       _db.Machines.Add(Machine);
//       _db.SaveChanges();
//       if (DoctorId != 0)
//       {
//         _db.MachineDoctor.Add(new MachineDoctor() { DoctorId = DoctorId, MachineId = Machine.MachineId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Details(int id)
//     {
//       var thisMachine = _db.Machines
//           .Include(Machine => Machine.JoinEntities)
//           .ThenInclude(join => join.Doctor)
//           .FirstOrDefault(Machine => Machine.MachineId == id);
//       return View(thisMachine);
//     }

//     public ActionResult Edit(int id)
//     {
//       var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
//       ViewBag.DoctorId = new SelectList( _db.Doctors, "DoctorId", "DoctorName");
//       return View(thisMachine);
//     }

//     [HttpPost]
//     public ActionResult Edit(Machine Machine, int DoctorId)
//     {
//       if (DoctorId != 0)
//       {
//         _db.MachineDoctor.Add(new MachineDoctor() { DoctorId = DoctorId, MachineId = Machine.MachineId });
//       }
//       _db.Entry(Machine).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult AddCourse(int id)
//     {
//       var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == 0);
//       ViewBag.DoctorId = new SelectList( _db.Doctors, "DoctorId", "DoctorName");
//       return View(thisMachine);
//     }

//     [HttpPost]
//     public ActionResult AddCourse(Machine Machine, int DoctorId)
//     {
//       if (DoctorId != 0)
//       {
//         _db.MachineDoctor.Add(new MachineDoctor() { DoctorId = DoctorId, MachineId = Machine.MachineId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
//       return View(thisMachine);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
//       _db.Machines.Remove(thisMachine);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     [HttpPost]
//     public ActionResult DeleteDoctor(int joinId)
//     {
//       var joinEntry = _db.MachineDoctor.FirstOrDefault(entry => entry.MachineDoctorId == joinId );
//       _db.MachineDoctor.Remove(joinEntry);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
  }
}