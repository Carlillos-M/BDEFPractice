using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDProductCatalog.Entities;
using CRUDProductCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUDProductCatalog.Controllers
{
    public class PatientController : Controller
    {

        private readonly ApplicationDbContext _context;
        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult PatientList()
        {

            List<PatientModel> list = new List<PatientModel>();
            list = _context.Patients.Select(paciente => new PatientModel()
            {
                Id = paciente.Id,
                Name = paciente.Name,
                LastName = paciente.LastName,
                Email = paciente.Email,
                Birth = paciente.Birth,         
            }).ToList();

            return View(list);

        }

        [HttpGet]
        public IActionResult PatientAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PatientAdd(PatientModel model)
        {
            //para insertar
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Patient p = new Patient();
            p.Id = model.Id;
            p.Name = model.Name;
            p.LastName = model.LastName;
            p.Email = model.Email;
            p.Birth = model.Birth;

            this._context.Patients.Add(p);
            this._context.SaveChanges();

            return RedirectToAction("PatientList", "Patient");
        }

        [HttpGet]
        public IActionResult PatientEdit(Guid Id)
        {
            Patient? paciente = this._context.Patients.Where(p => p.Id == Id).FirstOrDefault();

            if (paciente == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            PatientModel model = new PatientModel();
            model.Id = Id;
            model.Name = paciente.Name;
            model.LastName = paciente.LastName;
            model.Email = paciente.Email;
            model.Birth = paciente.Birth;

            return View(model);
        }

        [HttpPost]
        public IActionResult PatientEdit(PatientModel model)
        {

            Patient pacienteactualiza = this._context.Patients
            .Where(p => p.Id == model.Id).First();

            if (pacienteactualiza == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            pacienteactualiza.Name = model.Name;
            pacienteactualiza.LastName = model.LastName;
            pacienteactualiza.Email = model.Email;
            pacienteactualiza.Birth = model.Birth;

            this._context.Patients.Update(pacienteactualiza);
            this._context.SaveChanges();
            return RedirectToAction("PatientList", "Patient");
        }

        [HttpGet]
        public IActionResult PatientDelete(Guid Id)
        {
            //borrar registro
            Patient? pacienteborrado = this._context.Patients.Where(p => p.Id == Id).FirstOrDefault();

            if (pacienteborrado == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            PatientModel model = new PatientModel();
            model.Id = Id;
            model.Name = pacienteborrado.Name;
            model.LastName = pacienteborrado.LastName;
            model.Email = pacienteborrado.Email;
            model.Birth = pacienteborrado.Birth;

            return View(model);
        }

        [HttpPost]
        public IActionResult PatientDelete(PatientModel model)
        {
            bool exists = this._context.Patients.Any(p => p.Id == model.Id);

            if (!exists)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            Patient pacienteborrado = this._context.Patients.Where(p => p.Id == model.Id).First();
            pacienteborrado.Id = model.Id;
            pacienteborrado.Name = model.Name;
            pacienteborrado.LastName = model.LastName;
            pacienteborrado.Email = model.Email;
            pacienteborrado.Birth = model.Birth;

            this._context.Patients.Remove(pacienteborrado);
            this._context.SaveChanges();

            return RedirectToAction("PatientList", "Patient");
        }
    }
}
