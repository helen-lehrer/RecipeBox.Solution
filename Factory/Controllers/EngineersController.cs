// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Factory.Models;
// using System.Collections.Generic;
// using System.Linq;

// namespace Factory.Controllers
// {
//   public class DoctorsController : Controller
//   {
//     private readonly FactoryContext _db;

//     public DoctorsController(FactoryContext db)
//     {
//       _db = db;
//     }
    
//     public ActionResult Index()
//     {
//       List<Doctor> model = _db.Doctors.ToList();
//       return View(model);
//     }

//         public ActionResult Create()
//     {
//       ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "DoctorName");
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Create(Doctor doctor)
//     {
//       _db.Doctors.Add(doctor);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Details(int id)
//     {
//       var thisDoctor = _db.Doctors
//           .Include(doctor => doctor.JoinEntities)
//           .ThenInclude(join => join.Patient)
//           .FirstOrDefault(doctor => doctor.DoctorId == id);
//       return View(thisDoctor);
//     }

//     public ActionResult Edit(int id)
//     {
//       var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
//       return View(thisDoctor);
//     }

//     [HttpPost]
//     public ActionResult Edit(Doctor doctor)
//     {
//       _db.Entry(doctor).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult AddPatient(int id)
//     {
//       var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
//       ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "PatientName");
//       return View(thisDoctor);
//     }

//     [HttpPost]
//     public ActionResult AddPatient(Doctor doctor, int PatientId)
//     {
//       if (PatientId != 0)
//       {
//         _db.PatientDoctor.Add(new PatientDoctor() { PatientId = PatientId, DoctorId = doctor.DoctorId });
//         _db.SaveChanges();
//       }
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
//       return View(thisDoctor);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
//       _db.Doctors.Remove(thisDoctor);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
//   }
// }