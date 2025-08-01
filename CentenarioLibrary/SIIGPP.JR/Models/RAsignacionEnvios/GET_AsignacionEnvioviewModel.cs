using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.JR.Models.RAsignacionEnvios
{
    public class GET_AsignacionEnvioviewModel
    {
        public Guid envioId { get; set; }
        public Guid modulo1 { get; set; }
        public Guid modulo2 { get; set; }
    }
    public class GET_EnvioxFacilitador
    {
        public Guid idAsignacionEnvio { get; set; }
        public Guid envioId { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Tipo { get; set; }

    }
}
