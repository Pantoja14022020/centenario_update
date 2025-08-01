using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.PeritoAsignadoPI
{
    public class CrearViewModel
    {
        public Guid ModuloServicioId { get; set; }
        public Guid RActoInvestigacionId { get; set; }
        public string Respuesta { get; set; }
        public int NumeroInterno { get; set; }
        public string Conclusion { get; set; }
        public string FechaRecibido { get; set; }
        public string FechaAceptado { get; set; }
        public string FechaFinalizado { get; set; }
        public string FechaEntregado { get; set; }
        public string uDistrito { get; set; }
        public string uSubproc { get; set; }
        public string uAgencia { get; set; }
        public string uUsuario { get; set; }
        public string uPuesto { get; set; }
        public string uModulo { get; set; }
        public DateTime Fechasysregistro { get; set; }
        public DateTime Fechasysfinalizado { get; set; }
        public DateTime UltmimoStatus { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaCambio { get; set; }

    }
}
