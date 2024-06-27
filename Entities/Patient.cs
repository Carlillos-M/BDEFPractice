using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDProductCatalog.Entities
{
    public class Patient
    {

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime Birth { get; set; }

        public Guid? SpecialistId { get; set; }

        public Specialist? Specialist{ get; set; }

        public Guid? ExpedienteId { get; set;}

        public Expediente? Expediente { get; set;}
    }
}