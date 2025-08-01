using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RDHechos
{
    public class ComprobarViewModel
    {
        public Guid IdRAP { get; set; }
        public Guid IdRHecho { get; set; }
        public DateTime? FechaHoraSuceso { get; set; }
        public Guid? IdDDelito { get; set; }
        public string ClasificacionPersona { get; set; }
        public string StatusNUC { get; set; }
        public string Etapanuc { get; set; }
        public Guid? IdPersona { get; set; }

    }
}
