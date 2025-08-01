using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.MedidasCautelares
{
    public class CrearViewModel
    { 
        public Guid PersonaId { get; set; }

        public Guid RHechoId { get; set; }
        public string MedidaCautelar { get; set; }
        public string Tiempo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }

        public DateTime FechaSys { get; set; }
        public string Distrito { get; set; }
        public string DirSubProc { get; set; }
        public string Agencia { get; set; }
        public string Usuario { get; set; }
        public string Puesto { get; set; }
    }
}
