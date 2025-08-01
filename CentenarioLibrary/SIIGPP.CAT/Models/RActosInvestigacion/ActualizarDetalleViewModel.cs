using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.RActosInvestigacion
{
    public class ActualizarDetalleViewModel
    {
        public Guid IdActosInDetetalle { get; set; }
        public Guid RActosInvestigacionId { get; set; }
        public string Servicio { get; set; }
        public string ServicioNM { get; set; }
        public string Status { get; set; }
        public string TextoFinal { get; set; }
        public string FechaRecibido { get; set; }
        public string FechaAceptado { get; set; }
        public string FechaFinalizado { get; set; }
        public string FechaEntregado { get; set; }
        public DateTime UltmimoStatus { get; set; }
        public string Respuesta { get; set; }
        public string Conclusion { get; set; }
    }
}
