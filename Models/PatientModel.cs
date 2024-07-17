using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDProductCatalog.Models
{
    public class PatientModel
    {
        public PatientModel()
        {

        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? Birth { get; set; }



        public Guid? SpecialistId { get; set; }
        public string? SpecialistName { get; set; }
        public string? SpecialistLastName { get; set; }
        public string? SpecialistMajor { get; set; }
        public SpecialistModel? Specialist { get; set; }
        public List<SelectListItem>? ListaSpecialist { get; set; }

        public Guid? ExpedienteId { get; set; }
        public string? ExpedienteDiagnostic { get; set; }
        public ExpedienteModel? Expediente { get; set; }
        public List<SelectListItem>? ListaExpediente { get; set; }


    }
}