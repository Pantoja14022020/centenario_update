using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.PRegistro
{
    public class CrearPreHechoViewModel
    {
        public Guid PatencionId { get; set; }
        public Guid distritoId { get; set; }
        public string FechaReporte { get; set; }
        public DateTime FechaHoraSuceso { get; set; }
        public DateTime FechaHoraSuceso2 { get; set; }
        public string rbreve { get; set; }
        public string Texto { get; set; }
        public string Observaciones { get; set; }
        
    }
}