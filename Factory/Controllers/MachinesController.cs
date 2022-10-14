// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Factory.Models;
// using System.Collections.Generic;
// using System.Linq;

// namespace Factory.Controllers
// {
//   public class PatientsController : Controller
//   {
//     private readonly FactoryContext _db;

//     public PatientsController(FactoryContext db)
//     {
//       _db = db;
//     }

//     public ActionResult Index()
//     {
//       return View( _db.Patients.ToList().OrderBy(model => model.PatientName).ToList());
//     }

//     public ActionResult Create()
//     {
//       ViewBag.DoctorId = new SelectList( _db.Doctors, "DoctorId", "DoctorName");
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Create(Patient patient, int DoctorId)
//     {
//       _db.Patients.Add(patient);
//       _db.SaveChanges();
//       if (DoctorId != 0)
//       {
//         _db.PatientDoctor.Add(new PatientDoctor() { DoctorId = DoctorId, PatientId = patient.PatientId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Details(int id)
//     {
//       var thisPatient = _db.Patients
//           .Include(patient => patient.JoinEntities)
//           .ThenInclude(join => join.Doctor)
//           .FirstOrDefault(patient => patient.PatientId == id);
//       return View(thisPatient);
//     }

//     public ActionResult Edit(int id)
//     {
//       var thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
//       ViewBag.DoctorId = new SelectList( _db.Doctors, "DoctorId", "DoctorName");
//       return View(thisPatient);
//     }

//     [HttpPost]
//     public ActionResult Edit(Patient patient, int DoctorId)
//     {
//       if (DoctorId != 0)
//       {
//         _db.PatientDoctor.Add(new PatientDoctor() { DoctorId = DoctorId, PatientId = patient.PatientId });
//       }
//       _db.Entry(patient).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult AddCourse(int id)
//     {
//       var thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == 0);
//       ViewBag.DoctorId = new SelectList( _db.Doctors, "DoctorId", "DoctorName");
//       return View(thisPatient);
//     }

//     [HttpPost]
//     public ActionResult AddCourse(Patient patient, int DoctorId)
//     {
//       if (DoctorId != 0)
//       {
//         _db.PatientDoctor.Add(new PatientDoctor() { DoctorId = DoctorId, PatientId = patient.PatientId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       var thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
//       return View(thisPatient);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
//       _db.Patients.Remove(thisPatient);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     [HttpPost]
//     public ActionResult DeleteDoctor(int joinId)
//     {
//       var joinEntry = _db.PatientDoctor.FirstOrDefault(entry => entry.PatientDoctorId == joinId );
//       _db.PatientDoctor.Remove(joinEntry);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
//   }
// }