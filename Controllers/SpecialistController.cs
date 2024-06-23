using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDProductCatalog.Entities;
using CRUDProductCatalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDProductCatalog.Controllers
{
    public class SpecialistController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SpecialistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SpecialistList()
        {

            List<SpecialistModel> list = new List<SpecialistModel>();
            list = _context.Specialists.Select(s => new SpecialistModel()
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName,
                Major = s.Major,
            }).ToList();

            return View(list);

        }


        [HttpGet]
        public IActionResult SpecialistAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SpecialistAdd(SpecialistModel model)
        {
            //para insertar
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Specialist s = new Specialist();
            s.Id = model.Id;
            s.Name = model.Name;
            s.LastName = model.LastName;
            s.Major = model.Major;

            this._context.Specialists.Add(s);
            this._context.SaveChanges();

            return RedirectToAction("SpecialistList", "Specialist");
        }

        [HttpGet]
        public IActionResult SpecialistEdit(Guid Id)
        {
            Specialist? especialista = this._context.Specialists.Where(s => s.Id == Id).FirstOrDefault();

            if (especialista == null)
            {
                return RedirectToAction("SpecialistList", "Specialist");
            }

            SpecialistModel model = new SpecialistModel();
            model.Id = Id;
            model.Name = especialista.Name;
            model.LastName = especialista.LastName;
            model.Major = especialista.Major;

            return View(model);
        }

        [HttpPost]
        public IActionResult SpecialistEdit(SpecialistModel model)
        {

            Specialist especialistaactualiza = this._context.Specialists
            .Where(s => s.Id == model.Id).First();

            if (especialistaactualiza == null)
            {
                return RedirectToAction("SpecialistList", "Specialist");
            }

            especialistaactualiza.Name = model.Name;
            especialistaactualiza.LastName = model.LastName;
            especialistaactualiza.Major = model.Major;


            this._context.Specialists.Update(especialistaactualiza);
            this._context.SaveChanges();
            return RedirectToAction("SpecialistList", "Specialist");
        }

        [HttpGet]
        public IActionResult SpecialistDelete(Guid Id)
        {
            //borrar registro
            Specialist? especialistaborrado = this._context.Specialists.Where(s => s.Id == Id).FirstOrDefault();

            if (especialistaborrado == null)
            {
                return RedirectToAction("SpecialistList", "Specialist");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("SpecialistList", "Specialist");
            }

            SpecialistModel model = new SpecialistModel();
            model.Id = Id;
            model.Name = especialistaborrado.Name;
            model.LastName = especialistaborrado.LastName;
            model.Major = especialistaborrado.Major;

            return View(model);
        }

        [HttpPost]
        public IActionResult SpecialistDelete(SpecialistModel model)
        {
            bool exists = this._context.Specialists.Any(s => s.Id == model.Id);

            if (!exists)
            {
                return RedirectToAction("SpecialistList", "Specialist");
            }

            Specialist especialistaborrado = this._context.Specialists.Where(s => s.Id == model.Id).First();

            this._context.Specialists.Remove(especialistaborrado);
            this._context.SaveChanges();

            return RedirectToAction("SpecialistList", "Specialist");
        }
    }
}