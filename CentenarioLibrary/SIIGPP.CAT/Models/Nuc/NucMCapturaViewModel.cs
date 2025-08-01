using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Nuc
{
    public class NucMCapturaViewModel
    {
        public Guid distritoId { get; set; }
        public Guid agenciaId { get; set; }
        public string Etapanuc { get; set; }
        public string NUC { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
