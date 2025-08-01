using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.CAT.Models.Captura
{
    public class CrearCapturaViewModel
    {
        public Guid RHechoId { get; set; }
        public Guid RegistroTableroId { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid UModuloServicioId { get; set; }
        public Guid RemitioModuloServicioId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}