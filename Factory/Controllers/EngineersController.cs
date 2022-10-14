using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      return View(_db.Engineers.ToList());
    }

//         public ActionResult Create()
//     {
//       ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Create(Engineer doctor)
//     {
//       _db.Engineers.Add(doctor);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Details(int id)
//     {
//       var thisEngineer = _db.Engineers
//           .Include(doctor => doctor.JoinEntities)
//           .ThenInclude(join => join.Patient)
//           .FirstOrDefault(doctor => doctor.EngineerId == id);
//       return View(thisEngineer);
//     }

//     public ActionResult Edit(int id)
//     {
//       var thisEngineer = _db.Engineers.FirstOrDefault(doctor => doctor.EngineerId == id);
//       return View(thisEngineer);
//     }

//     [HttpPost]
//     public ActionResult Edit(Engineer doctor)
//     {
//       _db.Entry(doctor).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult AddPatient(int id)
//     {
//       var thisEngineer = _db.Engineers.FirstOrDefault(doctor => doctor.EngineerId == id);
//       ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "PatientName");
//       return View(thisEngineer);
//     }

//     [HttpPost]
//     public ActionResult AddPatient(Engineer doctor, int PatientId)
//     {
//       if (PatientId != 0)
//       {
//         _db.PatientEngineer.Add(new PatientEngineer() { PatientId = PatientId, EngineerId = doctor.EngineerId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       var thisEngineer = _db.Engineers.FirstOrDefault(doctor => doctor.EngineerId == id);
//       return View(thisEngineer);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisEngineer = _db.Engineers.FirstOrDefault(doctor => doctor.EngineerId == id);
//       _db.Engineers.Remove(thisEngineer);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
  }
}