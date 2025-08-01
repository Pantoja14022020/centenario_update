using System;
using SIIGPP.Entidades.M_JR.RAcuerdoReparatorio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class ConsultaIdSeguimientoDSesionViewModel
    {
        public Guid IdSeguimientoCumplimiento { get; set; }
        public Guid ConjuntoDerivacionesId { get; set; }
        public Guid IdAC { get; set; }
        public Guid IdAcuerdoReparatorio { get; set; }
        public DateTime Fecha { get; set; }
    }
    
}
