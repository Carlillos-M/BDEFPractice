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

        public async Task<IActionResult> PatientList()
        {

            List<PatientModel> patients
            = await _context.Patients
            .Include(s => s.Specialist)
            .Include(e => e.Expediente)
            .Select(paciente => new PatientModel()
            {
                Id = paciente.Id,
                Name = paciente.Name,
                LastName = paciente.LastName,
                Email = paciente.Email,
                Birth = paciente.Birth,
                SpecialistName = paciente.Specialist.Name,
                SpecialistLastName = paciente.Specialist.LastName,
                SpecialistMajor = paciente.Specialist.Major,
                ExpedienteDiagnostic = paciente.Expediente.Diagnostic,
            }).ToListAsync();

            return View(patients);

        }

        [HttpGet]
        public async Task<IActionResult> PatientAdd()
        {
            PatientModel patient = new PatientModel();

            patient.ListaSpecialist = 
            await _context.Specialists.Select(s => new SelectListItem()
            {Value = s.Id.ToString(), Text = s.Name + " " + s.LastName + " " + s.Major}
            ).ToListAsync();

            patient.ListaExpediente = 
            await _context.Expedientes.Select(e => new SelectListItem()
            {Value = e.Id.ToString(), Text = e.Diagnostic}
            ).ToListAsync();

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> PatientAdd(PatientModel model)
        {
            //para insertar
            if (!ModelState.IsValid)
            {
                model.ListaSpecialist =
                await _context.Specialists.Select(s => new SelectListItem()
                {Value = s.Id.ToString(), Text = s.Name + " " + s.LastName + " " + s.Major}
                ).ToListAsync();

                model.ListaExpediente =
                await _context.Expedientes.Select(e => new SelectListItem()
                {Value = e.Id.ToString(), Text = e.Diagnostic}
                ).ToListAsync();
            
                return View(model);
            }

            var p = new Patient();
            p.Id = new Guid();
            p.Name = model.Name;
            p.LastName = model.LastName;
            p.Email = model.Email;
            if (model.Birth.HasValue)
            {
                p.Birth = model.Birth.Value;
            }
            p.SpecialistId = model.SpecialistId;
            p.ExpedienteId = model.ExpedienteId;

            this._context.Patients.Add(p);
            await this._context.SaveChangesAsync();

            return RedirectToAction("PatientList", "Patient");
        }

        [HttpGet]
        public async Task<IActionResult> PatientEdit(Guid Id)
        {
            Patient? paciente = await this._context.Patients.Where(p => p.Id == Id).FirstOrDefaultAsync();

            if (paciente == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            PatientModel model = new PatientModel();
            model.Id = paciente.Id;
            model.Name = paciente.Name;
            model.LastName = paciente.LastName;
            model.Email = paciente.Email;
            model.Birth = paciente.Birth;

            if (paciente.SpecialistId.HasValue)
            {
                model.SpecialistId = paciente.SpecialistId.Value;
            }

            if (paciente.ExpedienteId.HasValue)
            {
                model.ExpedienteId = paciente.ExpedienteId.Value;
            }

                model.ListaSpecialist =
                await _context.Specialists.Select(s => new SelectListItem()
                {Value = s.Id.ToString(), Text = s.Name + " " + s.LastName + " " + s.Major}
                ).ToListAsync();

                model.ListaExpediente =
                await _context.Expedientes.Select(e => new SelectListItem()
                {Value = e.Id.ToString(), Text = e.Diagnostic}
                ).ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PatientEdit(PatientModel model)
        {

            bool exists = await _context.Patients.AnyAsync(p => p.Id == model.Id);

            if (!exists)
            {
                return View (model);
            }

            if (!ModelState.IsValid)
            {
                model.ListaSpecialist =
                await _context.Specialists.Select(s => new SelectListItem()
                {Value = s.Id.ToString(), Text = s.Name + " " + s.LastName + " " + s.Major}
                ).ToListAsync();

                model.ListaExpediente =
                await _context.Expedientes.Select(e => new SelectListItem()
                {Value = e.Id.ToString(), Text = e.Diagnostic}
                ).ToListAsync();
            
                return View(model);
            }

            Patient pacienteactualiza = await _context.Patients
            .Where(p => p.Id == model.Id).FirstAsync();
            pacienteactualiza.Name = model.Name;
            pacienteactualiza.LastName = model.LastName;
            pacienteactualiza.Email = model.Email;
            if (model.Birth.HasValue)
            {
                pacienteactualiza.Birth = model.Birth.Value;
            }
            pacienteactualiza.SpecialistId = model.SpecialistId;
            pacienteactualiza.ExpedienteId = model.ExpedienteId;

            this._context.Patients.Update(pacienteactualiza);
            await this._context.SaveChangesAsync();
            return RedirectToAction("PatientList", "Patient");
        }

        [HttpGet]
        public async Task<IActionResult> PatientDelete(Guid Id)
        {
            //borrar registro
            Patient? pacienteborrado = await this._context.Patients
            .Where(p => p.Id == Id).FirstOrDefaultAsync();

            if (pacienteborrado == null)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("PatientList", "Patient");
            }

            PatientModel model = new PatientModel();
            model.Id = pacienteborrado.Id;
            model.Name = pacienteborrado.Name;
            model.LastName = pacienteborrado.LastName;
            model.Email = pacienteborrado.Email;
            model.Birth = pacienteborrado.Birth;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PatientDelete(PatientModel model)
        {
            bool exists = await this._context.Patients.AnyAsync(p => p.Id == model.Id);

            if (!exists)
            {
                return View (model);
            }

            Patient pacienteborrado = await this._context.Patients
            .Where(p => p.Id == model.Id).FirstAsync();

            this._context.Patients.Remove(pacienteborrado);
            await this._context.SaveChangesAsync();

            return RedirectToAction("PatientList", "Patient");
        }
    }
}
