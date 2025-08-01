using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RSeguimientoCumplimiento
{
    public class PUT_StatuPagoViewModel
    {
        public Guid IdSeguimientoCumplimiento { get; set; }
        public DateTime? FechaProrroga { get; set; }
        public String StatusPago { get; set; } 
        public string Titulo { get; set; }
    }
}
