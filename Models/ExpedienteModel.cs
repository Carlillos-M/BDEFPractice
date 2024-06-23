using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDProductCatalog.Models
{
    public class ExpedienteModel
    {
        public ExpedienteModel()
        {
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Description { get; set; }
        public string? Diagnostic { get; set; }
        public DateTime DiagnosticDate { get; set; }
    }
}