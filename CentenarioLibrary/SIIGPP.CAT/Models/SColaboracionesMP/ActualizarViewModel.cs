using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.SColaboracionesMP
{
    public class ActualizarViewModel
    {
        public Guid IdSColaboracionMP { get; set; }
        public string Respuesta { get; set; }
        public string Status { get; set; }
        public DateTime FechaRespuesta { get; set; }
        public DateTime FechaRechazo { get; set; }

    }
}
