using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.SP.ModelsSP.PeritoAsignadoForaneas
{
    public class ActualizarViewModel
    {
        public Guid IdPeritosAsignadoForaneas { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Respuesta { get; set; }
        public int NumeroInterno { get; set; }
        public string Conclusion { get; set; }
        public string FechaRecibido { get; set; }
        public string FechaAceptado { get; set; }
        public string FechaFinalizado { get; set; }
        public string FechaEntregado { get; set; }
        public DateTime Fechasysfinalizado { get; set; }

        public DateTime UltmimoStatus { get; set; }
    }
}
