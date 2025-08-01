using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIIGPP.PI.Models.InformacionJuridico.BusquedaDomicilios
{
    public class ActualizarViewModel
    {
        public Guid IdBusquedaDomicilio { get; set; }
        public Guid ModuloServicioId { get; set; }
        public string Status { get; set; }
        public DateTime FAsignacion { get; set; }
        public DateTime FUltmimoStatus { get; set; }
        public DateTime FFinalizacion { get; set; }
    }
}
