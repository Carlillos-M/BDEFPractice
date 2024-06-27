using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDProductCatalog.Entities;
using CRUDProductCatalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDProductCatalog.Controllers
{
    public class ExpedienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExpedienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ExpedienteList()
        {

            List<ExpedienteModel> list = new List<ExpedienteModel>();
            list = _context.Expedientes.Select(e => new ExpedienteModel()
            {
                Id = e.Id,
                Name = e.Name,
                LastName = e.LastName,
                Description = e.Description,
                Diagnostic = e.Diagnostic,
                DiagnosticDate = e.DiagnosticDate,         
            }).ToList();

            return View(list);

        }

        [HttpGet]
        public IActionResult ExpedienteAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExpedienteAdd(ExpedienteModel model)
        {
            //para insertar
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Expediente e = new Expediente();
            e.Id = model.Id;
            e.Name = model.Name;
            e.LastName = model.LastName;
            e.Description = model.Description;
            e.Diagnostic = model.Diagnostic;
            e.DiagnosticDate = model.DiagnosticDate;

            this._context.Expedientes.Add(e);
            this._context.SaveChanges();

            return RedirectToAction("ExpedienteList", "Expediente");
        }

        [HttpGet]
        public IActionResult ExpedienteEdit(Guid Id)
        {
            Expediente? expediente = this._context.Expedientes.Where(e => e.Id == Id).FirstOrDefault();

            if (expediente == null)
            {
                return RedirectToAction("ExpedienteList", "Expediente");
            }

            ExpedienteModel model = new ExpedienteModel();
            model.Id = Id;
            model.Name = expediente.Name;
            model.LastName = expediente.LastName;
            model.Description = expediente.Description;
            model.Diagnostic = expediente.Diagnostic;
            model.DiagnosticDate = expediente.DiagnosticDate;

            return View(model);
        }

        [HttpPost]
        public IActionResult ExpedienteEdit(ExpedienteModel model)
        {

            Expediente expedienteactualiza = this._context.Expedientes
            .Where(e => e.Id == model.Id).First();

            if (expedienteactualiza == null)
            {
                return RedirectToAction("ExpedienteList", "Expediente");
            }

            expedienteactualiza.Name = model.Name;
            expedienteactualiza.LastName = model.LastName;
            expedienteactualiza.Description = model.Description;
            expedienteactualiza.Diagnostic = model.Diagnostic;
            expedienteactualiza.DiagnosticDate = model.DiagnosticDate;

            this._context.Expedientes.Update(expedienteactualiza);
            this._context.SaveChanges();
            return RedirectToAction("ExpedienteList", "Expediente");
        }

        [HttpGet]
        public IActionResult ExpedienteDelete(Guid Id)
        {
            //borrar registro
            Expediente? expedienteborrado = this._context.Expedientes.Where(p => p.Id == Id).FirstOrDefault();

            if (expedienteborrado == null)
            {
                return RedirectToAction("ExpedienteList", "Expediente");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ExpedienteList", "Expediente");
            }

            ExpedienteModel model = new ExpedienteModel();
            model.Id = Id;
            model.Name = expedienteborrado.Name;
            model.LastName = expedienteborrado.LastName;
            model.Description = expedienteborrado.Description;
            model.Diagnostic = expedienteborrado.Diagnostic;
            model.DiagnosticDate = expedienteborrado.DiagnosticDate;

            return View(model);
        }

        [HttpPost]
        public IActionResult ExpedienteDelete(ExpedienteModel model)
        {
            bool exists = this._context.Expedientes.Any(p => p.Id == model.Id);

            if (!exists)
            {
                return RedirectToAction("ExpedienteList", "Expediente");
            }

            Expediente expedienteborrado = this._context.Expedientes.Where(p => p.Id == model.Id).First();
            expedienteborrado.Name = model.Name;
            expedienteborrado.LastName = model.LastName;
            expedienteborrado.Description = model.Description;
            expedienteborrado.Diagnostic = model.Diagnostic;
            expedienteborrado.DiagnosticDate = model.DiagnosticDate;

            this._context.Expedientes.Remove(expedienteborrado);
            this._context.SaveChanges();

            return RedirectToAction("ExpedienteList", "Expediente");
        }
    }
}