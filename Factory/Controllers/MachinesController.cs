using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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

    public ActionResult Create()
    {
      
     List<SelectListItem> MachineStatus = new List<SelectListItem>();

     MachineStatus.Add(new SelectListItem { Text = "Operational", Value = "Operational"});

     MachineStatus.Add(new SelectListItem { Text = "Malfunctioning", Value = "Malfunctioning" });

     MachineStatus.Add(new SelectListItem { Text = "In Repair Process", Value = "In Repair Process"});

     ViewBag.MachineStatus = MachineStatus;
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine Machine, int EngineerId)
    {
      _db.Machines.Add(Machine);
      _db.SaveChanges();
      if (EngineerId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = Machine.MachineId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisMachine = _db.Machines
          .Include(Machine => Machine.JoinEntities)
          .ThenInclude(join => join.Engineer)
          .FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    public ActionResult Edit(int id)
    {
      List<SelectListItem> MachineStatus = new List<SelectListItem>();
     MachineStatus.Add(new SelectListItem { Text = "Operational", Value = "Operational"});
      MachineStatus.Add(new SelectListItem { Text = "Malfunctioning", Value = "Malfunctioning" });
     MachineStatus.Add(new SelectListItem { Text = "In Repair Process", Value = "In Repair Process"});
     ViewBag.MachineStatus = MachineStatus;

      var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      ViewBag.EngineerId = new SelectList( _db.Engineers, "EngineerId", "EngineerName");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine Machine, int EngineerId)
    {
      var joinEntry = _db.EngineerMachine.ToList().First(model => model.EngineerId.ToString().Contains(EngineerId.ToString()));

      Console.WriteLine(_db.EngineerMachine.ToList().First(model => model.EngineerId.ToString().Contains(EngineerId.ToString())).EngineerId);
      
      if (EngineerId != 0)
      {
        if (joinEntry.EngineerId.ToString() == EngineerId.ToString())
        {
        }
        else
        {
        _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = Machine.MachineId });
        }
      }
        _db.Entry(Machine).State = EntityState.Modified;
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddEngineer(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      ViewBag.EngineerId = new SelectList( _db.Engineers, "EngineerId", "EngineerName");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int EngineerId)
    {
      if (EngineerId != 0)
      {
        _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machine.MachineId, EngineerId = EngineerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      return View(thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMachine = _db.Machines.FirstOrDefault(Machine => Machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
